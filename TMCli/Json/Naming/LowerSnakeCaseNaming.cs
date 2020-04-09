using System;
using System.Text.Json;

namespace TMCli.Json.Naming
{
    // PascalCase => snake_case
    public class LowerSnakeCaseNaming : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            int upperCaseLength = 0;

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] >= 'A' && name[i] <= 'Z' && i != 0)
                {
                    upperCaseLength++;
                }
            }

            int length = name.Length + upperCaseLength;

            //Might just be most disgusting piece of code I have ever written
            //TODO: .NET 5 should have snake_case so then I hopefully get rid of this abomination
            static void SplitTo(int index, ref int lastSplit, ref int nextInsert, Span<char> outputSpan, Span<char> inputSpan)
            {
                //If not first, prefix the chunk with underscore
                if (nextInsert != 0)
                    outputSpan[nextInsert - 1] = '_';

                //Insert the chunk
                inputSpan[lastSplit..index].CopyTo(outputSpan.Slice(nextInsert));

                //Bump the offsets for next insert
                int length = index - lastSplit;
                lastSplit += length;
                nextInsert += length + 1;
            }
            
            return string.Create(length, name, 
                (chars, state) => {
                    int lastSplit = 0;
                    int nextInsert = 0;

                    //To lower case, hacky eh
                    Span<char> lowerCaseSpan = stackalloc char[name.Length];
                    name.AsSpan().ToLowerInvariant(lowerCaseSpan);

                    for (int i = 0; i < name.Length; i++)
                    {
                        if (name[i] >= 'A' && name[i] <= 'Z' && i != 0)
                            SplitTo(i, ref lastSplit, ref nextInsert, chars, lowerCaseSpan);
                    }
                    SplitTo(name.Length, ref lastSplit, ref nextInsert,chars, lowerCaseSpan);
                });
        }
    }
}
