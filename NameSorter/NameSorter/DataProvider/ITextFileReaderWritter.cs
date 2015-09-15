using System.Collections.Generic;
using NameSorter.Model;

namespace NameSorter.DataProvider
{
    public interface ITextFileReaderWritter
    {
        IList<NameModel> ReadAll(string fileName);
        void WriteAll(IList<NameModel> names, string outputFileName);
    }
}