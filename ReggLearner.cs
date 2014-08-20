using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dataanalessisGUI
{
    class ReggLearner :  absReggLearner
    {
        private const string c_mark = ",";
        private double m_zeroBuy;
        private double m_STD;
        private readonly List<dateAndValue> m_dataFromFile = new List<dateAndValue>();// may be a  miss lead so change to another sturc
        private DateTime m_startDate;
        private int m_daysToLearn;
        private string m_filePath;
        private int m_daysForword;
        private double m_slope;
        private double m_intercept;
        private readonly List<dateAndValue> values = new List<dateAndValue>();
        private double m_sumOfValues;
        private int m_countOfValues;
        private double m_PowOfValues;
        private double m_diffrence;

        public override double Slope
        {
            get { return m_slope; }
        }

        public double Intercept
        {
            get { return m_intercept; }
        }

        public override List<dateAndValue> DateAndValuesFromCalc
        {
            get { return values; }
        }

        public ReggLearner(DateTime i_startLreanigDate, int i_daysToLearn)
        {
            m_startDate = i_startLreanigDate;
            m_daysToLearn = i_daysToLearn;
        }

        public override bool Start()
        {
            learmFromFile();
            linearReggretion();
            m_zeroBuy = m_slope * (m_daysToLearn + m_daysForword) + m_intercept;
            clackSTD();
           return true;
        }

        public void ClackNewZeroBuy()
        {
            m_zeroBuy = m_slope * (m_daysToLearn + m_daysForword) + m_intercept; 
        }

        private void linearReggretion()
        {

            double xAvg = 0;
            double yAvg = 0;
            int Xindex = 0;//may be no the besst name

            foreach (dateAndValue x in values)
            {
                xAvg += Xindex;
                yAvg += x.Value;
                Xindex++;
            }

            xAvg = xAvg / Xindex;
            yAvg = yAvg / Xindex;

            //double v1 = 0;
            //double v2 = 0;

            //for (int y = 0; y < values.Count; y++)
            //{
            //    v1 += (y - xAvg) * (values[y].Value - yAvg);
            //    v2 += Math.Pow(y - xAvg, 2);
            //}

            //m_slope = v1 / v2;
            //m_intercept = yAvg - m_slope * xAvg;

            double sumOfMultis = 0;
            double sumOfSqerX = 0;
            for (int i = 0; i < values.Count; i++)
            {
                sumOfMultis += values[i].Value * i;
                sumOfSqerX += Math.Pow(i, 2);
            }

            m_slope = (sumOfMultis - (values.Count * yAvg * xAvg)) / (sumOfSqerX - values.Count * (xAvg * xAvg));
            m_intercept = yAvg - m_slope * xAvg;


        }

        private void clackSTD()
        {
            double v = (m_PowOfValues - ((m_sumOfValues * m_sumOfValues) / m_countOfValues)) / m_countOfValues;
            m_STD = Math.Sqrt(v);
        }

        public void BuildData(List<string> i_dates, List<string> i_values)
        {
            int min = i_dates.Count > i_values.Count ? i_values.Count : i_dates.Count;

            for (int i = 0; i < min; i++ )
            {
                if(i_values[i] != "NaN")
                {
                    m_dataFromFile.Add(new dateAndValue(setDateFormat(i_dates[i]), i_values[i]));
                }
            }
        }

        private string setDateFormat(string text)//change name and make i  for format 00/00/0000 eu 
        {
            if (text == null) return null;
            string outcome;
            //take days
            string days = text.Substring(0, 2);
            string months = text.Substring(2, 2);
            string years = text.Substring(4, 4);


            outcome = years + months + days;

            return outcome;
        }

        private void learmFromFile()
        {
           // readFromFile();
           // DateTime startLreanDate =  m_startDate.AddDays(-m_daysToLearn);
           // m_startDate = m_startDate.AddDays(-1);
            int counter = 0;
            foreach (dateAndValue x in m_dataFromFile)//find the place in the array
            {
                if (m_startDate <= x.Date)
                {
                    break;
                }
                counter++;
            }

            //if (m_startDate < m_dataFromFile[counter].Date)
            //{
            //    counter += -1;//
            //}

               while(counter - m_daysToLearn < 0)//meaning there is no number of days backword
               {
                   m_daysToLearn = m_daysToLearn - counter;
               }

               for (int i = counter - m_daysToLearn ; i < counter ; i++)
               {
                   values.Add(m_dataFromFile[i]);
               }
            
            foreach(dateAndValue x in values)
            {
                m_sumOfValues += x.Value;
                m_PowOfValues += x.Value * x.Value;
            }
            m_countOfValues += values.Count;
        }

        //not in use
        private void readFromFile()
        {
            string filePath = m_filePath;

            using (StreamReader csvLearning = new StreamReader(filePath))
            {
                while (!csvLearning.EndOfStream)
                {
                    string itemread = csvLearning.ReadLine();
                    string date;
                    string value;
                    setData(itemread, out date, out value);
                    try
                    {
                        m_dataFromFile.Add(new dateAndValue(date, value));
                    }
                    catch (FormatException e)
                    {
                        throw e;
                    }

                }
            } // End Using


        }

        private void setData(string i_inputItem, out string i_date, out string i_value)
        {
            int indexOfMark = i_inputItem.IndexOf(c_mark, 0);
            i_date = i_inputItem.Substring(0, indexOfMark);
            i_value = i_inputItem.Substring(indexOfMark + 1, i_inputItem.Length - indexOfMark - 1);
        }


        public override string FilePath
        {
            set
            {
                m_filePath = value;
            }
        }

        public override int Daysforword
        {
            set 
            {
                m_daysForword = value;
            }
            get
            {
                return m_daysForword;
            }
        }

        public override double Diffrence
        {
            get { return m_diffrence; }
        }

        public override int  CurrentDate
        {
            get {
                return 0;
                }
        }

        public override double ZeroBuy
        {
            get { return m_zeroBuy; }
        }

        public override double Std
        {
            get
            {
                return m_STD;
            }
            set
            {
                
            }
        }

        public override int IndexEnded
        {
            get { return 0; }
        }



        public override double lastPoint
        {
            get { return values[values.Count - 1].Value; }
        }
    }
}
