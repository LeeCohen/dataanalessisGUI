using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    internal class ReadFile
    {
        private  int m_lenOfLine = 27;//27 len of data + 4  (",bid/ask")
        private string[] m_files;
        private int m_currentFile;
        private string m_text;
        private List<fileNameComp> m_sortedFiles = new List<fileNameComp>();

        public int CurrentFileNumber
        {
            get { return m_currentFile; }
        }


        public int LenOfLine
        {
            get { return m_lenOfLine; }
            set { m_lenOfLine = value; }
        }
        public ReadFile(string i_sourcFile) 
        {
            m_text = System.IO.File.ReadAllText(i_sourcFile);

        }

        public   ReadFile(string[] i_sourcesFile) 
        {
            
              //  m_text = System.IO.File.ReadAllText(i_sourcesFile[0]);
                m_currentFile = 0;
                m_files = i_sourcesFile;

                for (int i = 0; i < m_files.Length; i++)
                {
                    m_sortedFiles.Add(new fileNameComp(m_files[i]));
                }
                m_sortedFiles.Sort();

              

        }

        public void setFilesDate(int i_date)
        {
            int i;
            for (i = 0; i < m_sortedFiles.Count; i++ )
            {
                if (m_sortedFiles[i].DateOfFile > i_date)
                {
                    break;
                }
            }
            m_currentFile = i - 1;
            m_text = System.IO.File.ReadAllText(m_sortedFiles[m_currentFile].filePath);
            
        }
        

      

        public bool NextFile()
        {
            bool isEndOfFiles = false;
            if(m_files != null){
                if (m_currentFile + 1 < m_sortedFiles.Count)
                {
                    m_text = System.IO.File.ReadAllText(m_sortedFiles[m_currentFile + 1].filePath);
                    m_currentFile++;
                }
                else
                {
                    isEndOfFiles = true;
                }
            }

            return isEndOfFiles;
        }

        public int LenOfText
        {
            get { return m_text.Length; }
        }

        public string Text 
        {
            get { return m_text; }
        }

        public bool setLine(int i_lineNumber, out string i_date, out string i_value, out string i_time)
        {
            int startOfval;
            i_date = null;
            i_value = null;
            i_time = null;
     //       checkLineLenth();
            try
            {
                i_date = m_text.Substring(0 + (i_lineNumber - 1) * m_lenOfLine, 8);
                i_time = m_text.Substring(9 + (i_lineNumber - 1) * m_lenOfLine, 6);
                startOfval = m_text.IndexOf(';', (i_lineNumber - 1) * m_lenOfLine);
                i_value = m_text.Substring(startOfval + 1, 8);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
               
                return false;
            }

            return true;

        }

        private void checkLineLenth()
        {
            int counter = 0;

            foreach(char x in m_text)
            {
                counter++;
                if(x == '\n')
                {
                    break;
                }
            }

            m_lenOfLine = counter;


        }

        public int getDate(int i_lineNumber)
        {
           // int outCome = 0;
            string date = null;
            //int lenOfLine = 28;
     //       checkLineLenth();
         /*   try
            {
                date = m_text.Substring(0 + (i_lineNumber - 1) * (lenOfLine + 1), 8);
               

                if (int.TryParse(date, out outCome) == true)
                {
                    m_lenOfLine = lenOfLine + 1;
                    return outCome;
                }
                else
                {
                    date = m_text.Substring(0 + (i_lineNumber - 1) * (lenOfLine), 8);
                    if (int.TryParse(date, out outCome) == true)
                    {
                        m_lenOfLine = lenOfLine;
                        return outCome;
                    }
                    else
                    {
                        date = m_text.Substring(0 + (i_lineNumber - 1) * (lenOfLine - 1), 8);
                        if (int.TryParse(date, out outCome) == true)
                        {
                            m_lenOfLine = lenOfLine - 1;
                            return outCome;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return 0;
            }

            if(outCome == 0)//sanety check
            {
                //throw new Exception("data corapted");
                return 0;
            }*/
            date = m_text.Substring(0 + (i_lineNumber - 1) * (m_lenOfLine), 8);
                return int.Parse(date);
           
            
        }

    }
}
