using System;
using System.Web;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using TMCli.Json;
using TMCli.Json.Naming;
using TMCli.Json.Submission;

using OAuth2ClientHandler;
using OAuth2ClientHandler.Authorizer;

namespace TMCli
{
    //TODO: Error handling.. don't fire-forget, stuff always breaks
    //TODO: Not sure whether I'll keep this god class or decouple
    public static class TMCAPI
    {
        private const int API_VERSION = 8;

        private const string CLIENT_NAME = "TMCli";
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

        public static Task<OAuthCredentials> GetOAuthCredentialsAsync(TMCContext context) => GetAsync<OAuthCredentials>(context, $"/application/{CLIENT_NAME}/credentials");

        public static Task<IEnumerable<Organization>> GetCoursesAsync(TMCContext context, Organization org) => GetAsync<IEnumerable<Organization>>(context, $"/core/org/{org.Slug}/courses");

        public static Task<CourseDetails> GetCourseInformationAsync(TMCContext context, CourseDetails course) => GetAsync<CourseDetails>(context, "/core/courses/" + course.Id);
        public static Task<IEnumerable<Review>> GetCourseReviewsAsync(TMCContext context, CourseDetails course) => GetAsync<IEnumerable<Review>>(context, $"/core/courses/{course.Id}/reviews");

        //TODO: Figure out those ten different variations of exercise models
        public static Task<CoreExercise> GetExerciseInformationAsync(TMCContext context, ExerciseDetails exercise) => GetAsync<CoreExercise>(context, $"/core/exercises/{exercise.Id}");
        //public static Task<Memory<byte>> DownloadExerciseAsync(TMCContext context, ExerciseDetails exercise) => GetAsync<Memory<byte>>(context, $"/core/exercises/{exercise.Id}/download");
        //public static Task<Memory<byte>> DownloadExerciseSolutionAsync(TMCContext context, ExerciseDetails exercise) => GetAsync<Memory<byte>>(context, $"/core/exercises/{exercise.Id}/solution/download");

        //public static Task<Submission> CreateSubmissionAsync(TMCContext context, ExerciseDetails exercise) => throw new NotImplementedException("//TODO: IO"); //PostFileAsync<Submission>(context, $"/core/exercises/{exercise.Id}")
        //public static Task<Memory<byte>> DownloadSubmissionAsync(TMCContext context, ExerciseDetails exercise) => GetAsync<Memory<byte>>(context, $"/core/exercises/{exercise.Id}/download");

        public static Task UnlockAsync(TMCContext context, CourseDetails course) => PostAsync(context, $"/core/courses/{course.Id}/unlock");
        
        public static Task<IEnumerable<Organization>> GetOrganizationsAsync(TMCContext context) => GetAsync<IEnumerable<Organization>>(context, "/org");
        
        public static Task<UserInfo> GetCurrentUserAsync(TMCContext context) => GetAsync<UserInfo>(context, "/users/current");
        public static Task<UserInfo> GetUserByIdAsync(TMCContext context, int userId) => GetAsync<UserInfo>(context, "/users/" + userId);

        public static Task<IEnumerable<ExerciseDetails>> GetExercisesAsync(TMCContext context, CourseDetails course) => GetExercisesAsync(context, course.Id);
        public static Task<IEnumerable<ExerciseDetails>> GetExercisesAsync(TMCContext context, int courseId) => GetAsync<IEnumerable<ExerciseDetails>>(context, $"/courses/{courseId}/exercises");

        public static Task<string> FetchAuthorizationTokenAsync(TMCContext context, OAuthCredentials credentials)
        {
            var oauthHttpHandler = new OAuthHttpHandler(new OAuthHttpHandlerOptions
            {
                AuthorizerOptions = new AuthorizerOptions
                {
                    TokenEndpointUrl = new Uri(context.BaseUri, "/oauth/token"),
                    GrantType = GrantType.ResourceOwnerPasswordCredentials,
                    ClientId = credentials.ApplicationId,
                    ClientSecret = credentials.Secret,
                    
                    //Username = string.Empty,
                    //Password = string.Empty
                }

                //Redir: urn:ietf:wg:oauth:2.0:oob
            });

            using HttpClient oauthClient = new HttpClient(oauthHttpHandler);
            throw new NotImplementedException();
        }

        private static void AddSessionParameters(TMCContext context, IDictionary<string, object> queryParameters)
        {
            queryParameters.Add("client", CLIENT_NAME);
            queryParameters.Add("client_version", CLIENT_VERSION);
            //queryParameters.Add("access_token", string.Empty);
        }
        private static string GetQueryString(IDictionary<string, object> parameters)
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
            AddSessionParameters(context, queryParameters);

            string apiVersionSegment = "/api/v" + API_VERSION;

            var request = new HttpRequestMessage(method, 
                context.BaseUri.GetLeftPart(UriPartial.Authority) + apiVersionSegment + path + GetQueryString(queryParameters));

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

        public static async Task PostAsync(TMCContext context, string path,
            IDictionary<string, object> queryParameters = default,
            IDictionary<string, string> parameters = default)
        {
            using var request = CreateRequest(context, HttpMethod.Post, path, queryParameters, parameters);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            //TODO:
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
                    //TODO: Yes

                    _ => default,
                };

            }

            if (responseContentConverter != null)
                return await responseContentConverter(response.Content).ConfigureAwait(false);

            if (typeof(T) == typeof(string))
                return (T)(object)await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (typeof(T) == typeof(byte[]))
                return (T)(object)await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            if (typeof(T) == typeof(Memory<T>))
                throw new NotImplementedException(); //TODO:

            if (response.Content.Headers.ContentType.MediaType == "application/json")
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync().ConfigureAwait(false), _serializerOptions).ConfigureAwait(false);

            return default;
        }
    }
}
