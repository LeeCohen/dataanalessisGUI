using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dataanalessisGUI
{
    public class Datalearer : ImotherDataLearner
    {
        private const string c_mark = ",";
        private double m_zeroBuy;
        private double m_STDOfAveragePerDay;
        private int m_endToLearn;
        private int m_indexEnded;
        private int m_counter = 0;
        private int m_currentDate;
        private double m_sumOfValues;
        private int m_countOfValues;
        private double m_PowOfValues;
        private List<DealLine> m_dealsAskBid = new List<DealLine>();
        private double m_slope;
        private double m_intercept;
        private int m_endOfSim;
        List<double> m_averagePerDay = new List<double>();
        List<string> m_DatePerDay = new List<string>();
        private double m_lastValue;
        private double m_diffrence;
        List<double> values = new List<double>();
        private readonly Queue<dateAndValue> m_dataFromFile = new Queue<dateAndValue>();// may be a  miss lead so change to another sturc
        private DateTime m_startDate;
        private int m_daysToLearn;
        private string m_filePath;
        private int m_daysForword;



        public List<dateAndValue> DateAndValuesFromCalc
        {
            get { return null; }
        }

        public int Daysforword
        {
            set { m_daysForword = value; }
        }
        public string FilePath
        {
            set 
            {
                m_filePath = value;
            }
        }


        public double Diffrence
        {
            get { return m_diffrence; }
        }

        public int CurrentDate
        {
            get { return m_currentDate; }
        }

        public double ZeroBuy 
        {
            get
            {
                return m_zeroBuy;
            }
        }
        public double Std
        {
            get
            {
                return m_STDOfAveragePerDay;
            }
            set
            {
                m_STDOfAveragePerDay = value;
            }
        }

        public int IndexEnded
        {
            get { return m_indexEnded; }
        }

     
        public Datalearer(List<DealLine> i_dealsAskBid ,int i_endDayToLearn,int i_DateEndOfSim)
        {
            m_dealsAskBid = i_dealsAskBid; // pass buy pointer
            m_endToLearn = i_endDayToLearn;
            m_endOfSim = i_DateEndOfSim;

            
        }

        public void SetDataForNewStd(double i_value)
        {
            m_sumOfValues += i_value;
            m_PowOfValues += i_value * i_value;
            m_countOfValues++;
        }

        public void ClackNewSTD()
        {
            clackSTD();
            m_sumOfValues = 0;
            m_PowOfValues = 0;
            m_countOfValues = 0;
        }



        public bool Start()//rename pls
        {
            bool islearningDone = false;
        
          

                islearningDone = clackZeroBuy();

                if (islearningDone == true)
                {
                    //linearReggretion();
                    m_zeroBuy = m_lastValue;//m_averagePerDay[m_averagePerDay.Count - 1];//m_slope * (ClackDayToTheEndOfPreiod() + m_averagePerDay.Count)+ m_intercept;
                    clackSTD();
                }
                m_sumOfValues = 0;
                m_PowOfValues = 0;
                m_countOfValues = 0;
         
         
          return islearningDone;
        }


 

     

        private double ClackDayToTheEndOfPreiod()
        {
            double outCome = 0;
            string startDate = m_endToLearn.ToString();
            string endDate = m_endOfSim.ToString();

            int dayStart = int.Parse(startDate.Substring(6, 2));
            int monthStart = int.Parse(startDate.Substring(4, 2));
            int yearStart = int.Parse(startDate.Substring(0, 4));

            int dayend = int.Parse(endDate.Substring(6, 2));
            int monthend = int.Parse(endDate.Substring(4, 2));
            int yearend = int.Parse(endDate.Substring(0, 4));


            DateTime d1 = new DateTime(yearStart,monthStart,dayStart);
            DateTime d2 = new DateTime(yearend,monthend,dayend);
            outCome =  (d2 - d1).TotalDays; 
            return outCome;
             
        }

        private bool clackZeroBuy()
        {
            int i = 1 ;
            bool isDateReached = false;
        
            int currentDay = int.Parse( m_dealsAskBid[0].Date);
            m_currentDate = int.Parse( m_dealsAskBid[0].Date);
            double sum = 0;
            int counter = 0;
            //values for clack the diffrence
            while(m_counter < m_endToLearn && i < m_dealsAskBid.Count)
            {

                
                sum += m_dealsAskBid[i].Value;
                counter++;
                int nextDay = int.Parse( m_dealsAskBid[i].Date);
                //values.Add(m_dealsAskBid[i].Value);
                
                if(currentDay  < nextDay)// if day have been pass
                {
                    values.Add((sum / (double)counter));
                     // m_DatePerDay.Add(currentDay.ToString());//delete after test
                     currentDay = nextDay;
                }

                if(nextDay >= m_endToLearn)
                {
                    m_lastValue = m_dealsAskBid[i].Value;
                    isDateReached = true;
                    break;
                }
                i++;
            }
            m_indexEnded = i;



            m_sumOfValues += values.Sum();
            m_PowOfValues += values.Select(val => val * val).Sum();
            m_countOfValues += values.Count;

            return isDateReached;
        }

        private void moveZeroBuy()
        {
 
        }

     
        private void linearReggretion()
        {

            double xAvg = 0;
            double yAvg = 0;
            int timeCounter = 0;//may be no the besst name

            foreach (double x in values)

            {
                xAvg += timeCounter;
                yAvg += x;
                timeCounter++;
            }

            xAvg = xAvg / timeCounter;
            yAvg = yAvg / timeCounter;

            double v1 = 0;
            double v2 = 0;

            for (int x = 0 ; x < values.Count ; x++)
            {
                v1 += (x - xAvg) * (values[x] - yAvg);
                v2 += Math.Pow(x - xAvg, 2);
            }

            m_slope = v1 / v2;
            m_intercept = yAvg - m_slope * xAvg;

          
        }

        private void clackSTD()
        {
            double v = (m_PowOfValues - ((m_sumOfValues * m_sumOfValues ) / m_countOfValues) ) / m_countOfValues;   
            m_STDOfAveragePerDay = Math.Sqrt(v);
        

        }
        
        public double Slope
        {
            get { return m_slope; }
        }

        public double Intercept
        {
            get { return m_intercept; }
        }

        public double lastPoint
        {
            get { return m_lastValue; }
        }
    }
}
