using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using makrovchain1;

namespace dataanalessisGUI
{
    public class MarkovLearner : ImotherDataLearner
    {
        private MarkovChain m_Chain;
        private string m_TypeOfPaper;
        private int m_Daysforword;
        private List<dateAndValue> m_ListOfDateaAndValues;
        private List<dataLineForMarkov> m_ListOfDataLines;

        

        public MarkovLearner (MarkovChain i_Chain, string i_TypeOfPaper)
        {
            m_Chain = i_Chain;
            m_TypeOfPaper = i_TypeOfPaper;
        }

        public bool Start()
        {
            return true;
        }


        public int Daysforword
        {
            set { m_Daysforword = value;}
        }



        public double ZeroBuy
        {
            get { return m_Chain.GetZeroBuy(); }
        }

        public double lastPoint
        {
            get { return m_Chain.getLastSeenValue(); }

        }

        public double Std
        {
            get
            {
                return m_Chain.GetStd();
            }
            set
            {
                ;
            }
        }

        public double GetStdOfNumericDiff()
        {
            return m_Chain.GetStdOfNumericDiff();
        }


        public List<dateAndValue> DateAndValuesFromCalc
        {
            get
            {
                List<dateAndValue>listToReturn = new List<dateAndValue>();
                Dictionary<DateTime, double> dicOfDatesAndValues = m_Chain.GetDicOfDatesAndValues();
                foreach(KeyValuePair<DateTime, double> pair in dicOfDatesAndValues)
                {
                    listToReturn.Add(new dateAndValue(pair.Key, pair.Value));
                }
                return listToReturn;

            }
        }


        public List<dataLineForMarkov> ListOfDataLines
        {
            get { return m_Chain.DataLines; }
        } 
    }
}
