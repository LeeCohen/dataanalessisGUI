using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dataanalessisGUI
{
  public  class csvReader
    {
          private string m_fileName;
        private Dictionary<string,List<string>> m_columnsOfData = new Dictionary<string,List<string>>();


        public int numberOfColumes
        {
            get 
            {
                return m_columnsOfData.Count;
            }
        }

        public List<string> GetColume(string i_key)
        {
            return m_columnsOfData[i_key];
        }

        public csvReader(string i_filePath)
        {
            m_fileName = i_filePath;
            setDictionary();
        }

        private void setDictionary()
        {

            //  int counter = 0;
            List<string> dataInStrings = getData();
            List<string> attriubts = GetAttribuits();
            //  List<string> ToAdd = new List<string>();

            foreach (string x in attriubts)//set the dicsonery
            {
                m_columnsOfData.Add(x, new List<string>());
            }
            foreach (string x in dataInStrings)//feel the columes
            {
                string[] splitedValues = x.Split(',');
                for (int i = 0; i < splitedValues.Length; i++)
                {

                    m_columnsOfData[attriubts[i]].Add(splitedValues[i]);
                }
            }
        }
                    
 


        private List<string> getData()
        {
            List<string> data = new List<string>();
            string itemread;
            try
            {
                using (StreamReader comboboxsr = new StreamReader(m_fileName))
                {
                    comboboxsr.ReadLine();//remove first line
                    while (!comboboxsr.EndOfStream)
                    {
                         itemread = comboboxsr.ReadLine();
                         data.Add(itemread);
                    }
                } // End Using
            }
            catch (DirectoryNotFoundException dnf)
            {
                // Exception Processing
            }
            catch (FileNotFoundException fnf)
            {
                // Exception Processing
            }
            catch (Exception e)
            {
                // Exception Processing
            }


            return data;
        }


    


        public List<string> GetAttribuits()
        { 
            List<string> Attribuits = new List<string>();
            string line;
            line = readFirstLineline();
            string[] attr = line.Split(',');
            
            foreach(string x in attr)
            {
                Attribuits.Add(x);
            }

            return Attribuits;
        }



        private string readFirstLineline()
        {
            string itemread = null;
            
            try
            {
                using (StreamReader comboboxsr = new StreamReader(m_fileName))
                {
                    
                        
                        itemread = comboboxsr.ReadLine();
                 
                     
                } // End Using
            }
            catch (DirectoryNotFoundException dnf)
            {
                // Exception Processing
            }
            catch (FileNotFoundException fnf)
            {
                // Exception Processing
            }
            catch (Exception e)
            {
                // Exception Processing
        
            }
            return itemread;

        }
    }

}

