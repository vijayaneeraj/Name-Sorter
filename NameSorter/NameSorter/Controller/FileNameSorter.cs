using System.Collections.Generic;
using System.IO;
using System.Linq;
using NameSorter.Common;
using NameSorter.DataProvider;
using NameSorter.Model;

namespace NameSorter.Controller
{
    public class FileNameSorter : FileSorter
    {
        private INameSorter _nameSorter;
        private ITextFileReaderWritter _textFileNameProvider;

        public FileNameSorter(string inputFile, string outputfile)
            : base(inputFile, outputfile)
        {
        }

        protected virtual INameSorter NameSorterWrapper
        {
            get { return _nameSorter ?? (_nameSorter = new Common.NameSorter()); }
        }

        protected virtual ITextFileReaderWritter TextFileReaderWritter
        {
            get { return _textFileNameProvider ?? (_textFileNameProvider = new TextFileReaderWritter()); }
        }

        protected virtual void WriteSortedNamesToConsole(IList<NameModel> sortedNames)
        {
            if (sortedNames != null && sortedNames.Count > 0)
            {
                sortedNames.ToList().ForEach(n => { Log(n.ToString()); });
            }
        }

        #region FileSorter Abstract Implimentation

        protected override void PerformSort()
        {
            /*
             * 1. Read Names from File
             * 2. Sort names
             * 3. Write sorted name into output file
             */
            var unsortedNames = TextFileReaderWritter.ReadAll(InputFile);
            var sortedNames = NameSorterWrapper.SortNameByLastAndFirstName(unsortedNames);
            TextFileReaderWritter.WriteAll(sortedNames, OutputFile);
        }

        protected override void DisplaySortedResult()
        {
            if (IsFileExist(OutputFile))
            {
                var sortedNames = TextFileReaderWritter.ReadAll(OutputFile);
                WriteSortedNamesToConsole(sortedNames);
                Log("Finished: Created " + GetFileName(OutputFile));
            }
            else
            {
                Log(FileSorterMessage.OutputFileDoesNotExist);
            }
        }

        protected override bool ValidateInputParameter()
        {
            if (IsFileExist(InputFile))
            {
                return true;
            }
            Log(FileSorterMessage.InputFileDoesNotExist + InputFile);
            return false;
        }

        #endregion

        #region  Wrapper for Suytem dependent code

        protected virtual bool IsFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        protected virtual string GetFileName(string filePath)
        {
            return Path.GetFileName(OutputFile);
        }

        #endregion
    }
}