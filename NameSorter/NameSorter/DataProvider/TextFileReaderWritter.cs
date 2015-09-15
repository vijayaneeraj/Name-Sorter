using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using NameSorter.Model;

namespace NameSorter.DataProvider
{
    public class TextFileReaderWritter : ITextFileReaderWritter
    {        
         //Read the newline sepearted pair first and last names 
       
        public IList<NameModel> ReadAll(string filePath)
        {
            IList<NameModel> users = null;
            if (File.Exists(filePath))
            {
                using (var reader = File.OpenRead(filePath))
                using (var textFileParser = new TextFieldParser(reader))
                {
                    textFileParser.TrimWhiteSpace = true;
                    textFileParser.Delimiters = new[] {","};
                    while (!textFileParser.EndOfData)
                    {
                        //Read comma sepearted line
                        var line = textFileParser.ReadFields();
                        //Create mapped model for each row of data

                        var name = GetNameModel(line);
                        if (name != null)
                        {
                            if (users == null) users = new List<NameModel>();
                            users.Add(name);
                        }
                    }
                }
            }
            return users;
        }

        //Write the list of names into output file. 
        public void WriteAll(IList<NameModel> names, string filePath)
        {
            if (names != null && names.Count > 0)
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var name in names)
                    {
                        var lineText = GetTextLine(name);
                        //write non blank lines
                        if (!string.IsNullOrEmpty(lineText))
                        {
                            writer.WriteLine(lineText);
                        }
                    }
                }
            }
        }

        
        protected string GetTextLine(NameModel name)
        {

            if (name == null || (string.IsNullOrEmpty(name.FirstName)
                                 && string.IsNullOrEmpty(name.LastName)))
            {
                return string.Empty;
            }

            return name.ToString();

        }
        //Create name model from fields
        protected NameModel GetNameModel(string[] fields)
        {
            if (fields!=null && fields.Length > 0)
            {
                return new NameModel
                {
                    LastName = fields[0],
                    FirstName = fields.Length >= 2 ? fields[1]:string.Empty
                };
            }
            return null;
        }
    }
}