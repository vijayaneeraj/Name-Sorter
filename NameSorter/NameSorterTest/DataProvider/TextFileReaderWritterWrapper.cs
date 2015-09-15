using NameSorter.DataProvider;
using NameSorter.Model;

namespace NameSorterTest.DataProvider
{
    public class TextFileReaderWritterWrapper : TextFileReaderWritter
    {
        new public string GetTextLine(NameModel name)
        {
           return base.GetTextLine(name);
        }

        new public NameModel GetNameModel(string[] fields)
        {
           return base.GetNameModel(fields);
        }
    }
}
