using System.Collections.Generic;
using NameSorter.Controller;
using NameSorter.Model;

namespace NameSorterTest.Controller
{
    /*
     * This wrapper exist to test protected members of the  FileNameSorter
     */

    public class FileNameSorterWrapper : FileNameSorter
    {
        public FileNameSorterWrapper(string inputFile, string outputfile) : base(inputFile, outputfile)
        {
        }

        public void PerformSortWrapper()
        {
            PerformSort();
        }

        public void DisplaySortedResultWrapper()
        {
            DisplaySortedResult();
        }

        public void WriteSortedNamesToConsoleWrapper(IList<NameModel> names)
        {
            WriteSortedNamesToConsole(names);
        }

        public bool ValidateInputParameterWrapper()
        {
            return ValidateInputParameter();
        }
    }
}