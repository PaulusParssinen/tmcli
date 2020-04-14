using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace TMCli.Utils
{
    public static class ExerciseUtilities
    {
        public static bool IsExerciseRootDirectory(this DirectoryInfo directory)
        {
            //TODO: Other languages..

            // Java exercise directory must have following structure.
            // {directory}/src/main/java/
            //                /test/java/
            // {directory}/pom.xml

            bool hasCommonDirs = false;

            foreach (var subDirectory in directory.GetDirectories())
            {
                if (subDirectory.Name != "src") continue;

                if (Directory.Exists(Path.Combine(subDirectory.FullName, "main/java/"))
                    && Directory.Exists(Path.Combine(subDirectory.FullName, "test/java/")))
                {
                    hasCommonDirs = true;
                }
            }

            return hasCommonDirs && File.Exists(Path.Combine(directory.FullName, "pom.xml"));
        }
        public static bool TryFindExerciseRootDirectory(this DirectoryInfo directory,
            [NotNullWhen(returnValue: true)] out DirectoryInfo exerciseDirectory)
        {
            if (IsExerciseRootDirectory(exerciseDirectory = directory))
                return true;

            //Check four parent levels of directories for a Java exercise structure
            const int RECURSION_DEPTH = 4;
            for (int i = 0; i < RECURSION_DEPTH; i++)
            {
                if (IsExerciseRootDirectory(exerciseDirectory = exerciseDirectory.Parent))
                    return true;
            }

            exerciseDirectory = default!;
            return false;
        }
    }
}
