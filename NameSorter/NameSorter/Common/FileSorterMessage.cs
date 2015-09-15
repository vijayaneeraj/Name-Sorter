namespace NameSorter.Common
{
    public struct FileSorterMessage
    {
        public const string SortetFileNameSuffix = "-sorted";
        public const string UsageError = "Usage: name-sorter.exe <input-file-text>.txt";

        public const string UnhandledException =
            "Something went wrong while sorting input file. Sorry log is not available.";

        public const string OutputFileDoesNotExist =
            "Finished. Sorted output file has not created. Could be input file is empty";

        public const string InputFileDoesNotExist = "InputFile does not exist. Verify inputfile path. You entered:";
    }
}