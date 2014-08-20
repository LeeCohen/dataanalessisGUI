using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dataanalessisGUI
{
    class fileNameComp : IComparable
    {
        private string m_filePath;
        private int m_date;
        private const char c_markOfDateInFileName = '_';
        private const int c_lenOfDate = 8;


        public string filePath
        {
            get { return m_filePath; }
        }

        public fileNameComp(string i_filePath)
        {
            m_filePath = i_filePath;
            string fileName = Path.GetFileNameWithoutExtension(i_filePath);
            //int indexOfMark = fileName.IndexOf(c_markOfDateInFileName);//need to this 2 time for _log_ 
            //indexOfMark = fileName.IndexOf(c_markOfDateInFileName, indexOfMark + 1);
            string YearAndMonth = fileName.Substring(fileName.Length - 6, 6);
            m_date = int.Parse(YearAndMonth);

        }

        public int DateOfFile
        {
            get
            {
                return m_date * 100;// mul by 100 to add the days

            }
        }


        public int CompareTo(object obj)
        {
            fileNameComp t = obj as fileNameComp;
            if (t == null)
            {
                return 0;
            }

            if (t.m_date > m_date)
            {
                return -1;
            }

            return 1;
        }
    }
}
