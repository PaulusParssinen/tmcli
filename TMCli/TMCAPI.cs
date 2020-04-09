using System;
using System.Web;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using TMCli.Json;
using TMCli.Json.Naming;

namespace TMCli
{
    public static class TMCAPI
    {
        private const int API_VERSION = 8;

        private const string CLIENT_NAME = "TMCCli";
        private const string CLIENT_VERSION = "1.0";
        private const string USER_AGENT = CLIENT_NAME + "/" + CLIENT_VERSION + " (https://github.com/PaulusParssinen/TMCli)";

        private readonly static HttpClient _client;
        private readonly static JsonSerializerOptions _serializerOptions;

        static TMCAPI()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new LowerSnakeCaseNaming()
            };
        }

        public static Task<IEnumerable<Organization>> GetOrganizationsAsync(TMCContext context) => GetAsync<IEnumerable<Organization>>(context, "/api/v" + API_VERSION + "/org");
        public static Task<IEnumerable<Organization>> GetCoursesAsync(TMCContext context, Organization org) => GetAsync<IEnumerable<Organization>>(context, "/api/v" + API_VERSION + $"/core/org/{org}/courses");

        public static Task<IEnumerable<Exercise>> GetExercisesAsync(TMCContext context, Course course) => GetExercisesAsync(context, course.Id);
        public static Task<IEnumerable<Exercise>> GetExercisesAsync(TMCContext context, int courseId) => GetAsync<IEnumerable<Exercise>>(context, "/api/v" + API_VERSION + $"/courses/{courseId}/exercises");

        //public static Task UnlockAsync(TMCContext context, Course course) => GetAsync<IEnumerable<Exercise>>(context, course.UnlockUrl); //TODO: Rely on response urls or on documentation? - note: doc based easier

        private static void AddSessionParameters(IDictionary<string, object> queryParameters)
        {
            queryParameters.Add("client", CLIENT_NAME);
            queryParameters.Add("client_version", CLIENT_VERSION);
            //queryParameters.Add("access_token", "null");
        }

        public static string GetQueryString(IDictionary<string, object> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var (key, value) in parameters)
            {
                query.Add(key, value.ToString());
            }
            return "?" + query.ToString();
        }

        public static HttpRequestMessage CreateRequest(TMCContext context, HttpMethod method, string path,
            IDictionary<string, object> queryParameters = default,
            IEnumerable<KeyValuePair<string, string>> parameters = default)
        {
            queryParameters ??= new Dictionary<string, object>();
            AddSessionParameters(queryParameters);

            var request = new HttpRequestMessage(method, 
                context.BaseUri.GetLeftPart(UriPartial.Authority) + path + GetQueryString(queryParameters));

            if (parameters != default)
                request.Content = new FormUrlEncodedContent(parameters);
            
            return request;
        }

        public static async Task<T> GetAsync<T>(string requestUrl,
            Func<HttpContent, Task<T>> contentDeserializer = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }
        public static async Task<T> GetAsync<T>(TMCContext context, string path,
            IDictionary<string, object> queryParameters = default,
            Func<HttpContent, Task<T>> contentDeserializer = default)
        {
            using var request = CreateRequest(context, HttpMethod.Get, path, queryParameters);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }

        public static async Task<T> PostAsync<T>(TMCContext context, string path,
            IDictionary<string, object> queryParameters = default,
            IDictionary<string, string> parameters = default,
            Func<HttpContent, Task<T>> contentDeserializer = default)
        {
            using var request = CreateRequest(context, HttpMethod.Post, path, queryParameters, parameters);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }

        public static async Task<T> DeserializeContentAsync<T>(this HttpResponseMessage response,
            Func<HttpContent, Task<T>> responseContentConverter = default)
        {
            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode switch
                {
                    //TODO:

                    _ => default,
                };
            }

            if (responseContentConverter != null)
                return await responseContentConverter(response.Content).ConfigureAwait(false);

            if (typeof(T) == typeof(string))
                return (T)(object)await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (typeof(T) == typeof(byte[]))
                return (T)(object)await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            if (response.Content.Headers.ContentType.MediaType == "application/json")
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync().ConfigureAwait(false), _serializerOptions).ConfigureAwait(false);

            return default;
        }
    }
}
