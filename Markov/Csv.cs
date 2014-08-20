using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReadWriteCsv
{
    /// <summary>
    /// Class to store one CSV row
    /// </summary>
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    /// <summary>
    /// Class to write data to a CSV file
    /// </summary>
    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename)
            : base(filename)
        {
        }

        public CsvFileWriter(string filename, bool append)
            : base(filename, append)
        {
        }
        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                // Add separator if this isn't the first value
                if (!firstColumn)
                    builder.Append(',');
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                else
                    builder.Append(value);
                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }

    /// <summary>
    /// Class to read data from a CSV file
    /// </summary>
    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream)
            : base(stream)
        {
        }

        public CsvFileReader(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="i_Row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow i_Row)
        {
            i_Row.LineText = ReadLine();
            if (String.IsNullOrEmpty(i_Row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < i_Row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (i_Row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < i_Row.LineText.Length)
                    {
                        // Test for quote character
                        if (i_Row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= i_Row.LineText.Length || i_Row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = i_Row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < i_Row.LineText.Length && i_Row.LineText[pos] != ',')
                        pos++;
                    value = i_Row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < i_Row.Count)
                    i_Row[rows] = value;
                else
                    i_Row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < i_Row.LineText.Length && i_Row.LineText[pos] != ',')
                    pos++;
                if (pos < i_Row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (i_Row.Count > rows)
                i_Row.RemoveAt(rows);

            // Return true if any columns read
            return (i_Row.Count > 0);
        }
    }
}
