using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using TMCli.Json;
using TMCli.Utils;

namespace TMCli
{
    public class CliContext
    {
        //TODO: Course/Exercise caches for lookups

        private const string CACHE_FILE_NAME = ".tmcli-cache.json";

        public DirectoryInfo CurrentDir { get; set; } = new DirectoryInfo(Directory.GetCurrentDirectory());
        
        public TMCContext APIContext { get; set; } = new TMCContext();

        public async ValueTask<ExerciseDetails?> GetExerciseAsync(int courseId, string exerciseName)
        {
            if (!APIContext.IsAuthenticated)
                return default;

            //TODO: First cache lookup

            var exercises = await TMCAPI.GetExercisesAsync(APIContext, courseId).ConfigureAwait(false);

            foreach (var exercise in exercises)
            {
                if (exercise.Name == exerciseName)
                    return exercise;
            }

            return default;
        }

        public ValueTask<CourseDetails?> GetCourseAsync(DirectoryInfo directory) //TODO: dir arg
        {
            if (!APIContext.IsAuthenticated)
                return default;

            if (!directory.TryFindExerciseRootDirectory(out var currentExerciseDir))
                return default;

            string courseName = currentExerciseDir.Parent.Name;

            //TODO: Cache lookup => API lookup

            throw new NotImplementedException();
        }

        public async Task WriteCacheAsync(string cacheFilePath = CACHE_FILE_NAME)
        {
            byte[] data = JsonSerializer.SerializeToUtf8Bytes(APIContext);
            await File.WriteAllBytesAsync(cacheFilePath, data).ConfigureAwait(false);
        }
        public static async ValueTask<CliContext> FromCacheAsync(string cacheFilePath = CACHE_FILE_NAME)
        {
            if (!File.Exists(cacheFilePath))
                return new CliContext();

            using var fs = File.OpenRead(cacheFilePath);
            var sessionContext = await JsonSerializer.DeserializeAsync<TMCContext>(fs).ConfigureAwait(false);
            
            return new CliContext
            {
                APIContext = sessionContext
            };
        }
    }
}
