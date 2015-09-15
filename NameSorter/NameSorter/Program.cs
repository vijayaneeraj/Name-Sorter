using System;
using System.IO;
using NameSorter.Common;
using NameSorter.Controller;

namespace NameSorter
{
    public class Program
    {
        private static void Main(string[] args)
        {       
            if (args.Length != 1)
            {
                Console.WriteLine();
                Console.WriteLine(FileSorterMessage.UsageError);
                Console.ReadKey();
                return;
            }
            try
            {
                string inputFileName = args[0];
                string outputFileName = GenerateOutputFileName(inputFileName);
                /*
                 * Sort the newline seperated names given in inputFileName
                 * and write output into outputFileName
                 */
                ISorter nameSorter = new FileNameSorter(inputFileName, outputFileName);
                nameSorter.Sort();
            }
            catch (Exception)
            {
                //TODO: log the exception.
                Console.WriteLine(FileSorterMessage.UnhandledException);
                Console.ReadKey();
            }
        }
        /*
        *  Creates an auto generated output file to store the result
        */ 
        static string GenerateOutputFileName(string inputFile)
        {
            if (File.Exists(inputFile))
            {
                var sortedFileName = inputFile;
                var lastIndex = sortedFileName.LastIndexOf(".", StringComparison.Ordinal);
                if (lastIndex != -1)
                {
                    sortedFileName = sortedFileName.Insert(lastIndex, FileSorterMessage.SortetFileNameSuffix);
                    if (File.Exists(sortedFileName))
                    {
                        File.Delete(sortedFileName);
                    }
                    return sortedFileName;
                }
            }
            return String.Empty;
        }
    }
}
