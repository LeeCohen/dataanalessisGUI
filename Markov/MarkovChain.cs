
using ReadWriteCsv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using markovThatRecieveData;

namespace makrovchain1
{
    public class MarkovChain
    {
        private List<dataLineForMarkov> m_DataLines = new List<dataLineForMarkov>();



        string m_Path;
        CsvFileReader m_DataCsv;
        public int m_NumOfDaysForMarkov;
        public int m_NumOfDaysForStd;
        private int m_LastDayIndexLearningCalculated;
        private string m_FilePath;

        //fixGrade = Diffrece of curr day and last day/ Std of NUM_OF_DAYS_FOR_STD before curr day
        public const int NO_CHANGE = 0;//-0.5<fixGrade<1.5
        public const int INCREASE = 1;//0.5<fixGrade<1.5
        public const int HIGH_INCREASE = 2;//fixGrade>1.5
        public const int DECREASE = 3; //-1.5<fixGrade<0.5
        public const int HIGH_DECREASE = 4;//fixGrade<-1.5
        CultureInfo m_Provider;
        Dictionary<string, int> m_PapersIndexDic = new Dictionary<string, int>();
        List<double> m_AbsNumericDiffList = new List<double>();
        //List<int> m_GoodHitsList = new List<int>();
        //List<int> m_ExcellentHitsList = new List<int>();
        double[] m_SumsOfSummary = new double[4] { 0.0, 0.0, 0.0, 0.0 };
        string m_ChosenPaper;
        List<string> m_AllPapers = new List<string>();
        string m_OutputFileName;
        string m_InputFileName;
        bool m_InitialMatrixWasSet = false;// not used
       // public int m_NumOfRecordsInsideTheMarkov;


        public MarkovChain()
        {
            
        }

        public void Clear()
        {
            m_DataLines.Clear();
            m_AbsNumericDiffList.Clear();
            m_LastDayIndexLearningCalculated = 0;
        }

        public void SetParamertsFromFile(int i_NumOfDaysForMarkov, int i_NumOfDaysForStd, string i_InputFilePath, string i_OutputFilePath)
        {
            //m_NumOfRecordsInsideTheMarkov = 0;
            m_NumOfDaysForMarkov = i_NumOfDaysForMarkov;
            m_NumOfDaysForStd = i_NumOfDaysForStd;
            m_InputFileName = i_InputFilePath;
            m_OutputFileName = i_OutputFilePath;
            initDataSource();
        }


        public void SetParametersAndRun(String i_Paper, Dictionary<DateTime, double> i_DicOfDateValue, int i_NumOfDaysForMarkov, int i_NumOfDaysForStd)
        {
            //m_NumOfRecordsInsideTheMarkov = 0;
            m_NumOfDaysForMarkov = i_NumOfDaysForMarkov;
            m_NumOfDaysForStd = i_NumOfDaysForStd;
            initDataFromDictionary(i_Paper, i_DicOfDateValue);
            this.runThisPaper(i_Paper);
        }

        private void initDataFromDictionary(string i_Paper, Dictionary<DateTime, double> i_DicOfDateValue)
        {
            m_LastDayIndexLearningCalculated = 0;
            m_DataLines.Clear();
            m_AbsNumericDiffList.Clear();
            m_PapersIndexDic.Add(i_Paper, 0);
            m_AllPapers.Add(i_Paper);
            m_ChosenPaper = i_Paper;

            foreach(KeyValuePair<DateTime, double> dateValue in i_DicOfDateValue)
            {
                //if (m_DataLines.Count() != 0 && dateValue.Key != AddOneDaysAndSkipOnSaturdaysAndSundays(m_DataLines[m_DataLines.Count() - 1].Date))
                //{
                //    throw new ArgumentOutOfRangeException("invalid date" + dateValue.ToString());
                //}

                m_DataLines.Add(new dataLineForMarkov(dateValue.Key, dateValue.Value));
            }
        }

        private void setActuallValuesNumericDiffAndHits()
        {
            for (int i = m_NumOfDaysForMarkov + m_NumOfDaysForStd; i<m_DataLines.Count-1; i++ )
            {
                m_DataLines[i].ActuallNextDayState = m_DataLines[i + 1].state;
                m_DataLines[i].ActuallNextDayValue = m_DataLines[i + 1].Value;
                m_AbsNumericDiffList.Add(((Math.Abs((m_DataLines[i].ActuallNextDayValue / m_DataLines[i].Value) - (m_DataLines[i].EstimatedZeroBuy / m_DataLines[i].Value)))));
                m_DataLines[i].numericDiff = ((m_DataLines[i].ActuallNextDayValue / m_DataLines[i].Value) - (m_DataLines[i].EstimatedZeroBuy / m_DataLines[i].Value));
                m_DataLines[i].updateDiffLowerThan2();
            }

            m_DataLines[m_DataLines.Count - 1].ActuallNextDayState = -10;
        }

        private void setActuallValuesNumericDiffAndHitsForTheDayBeforeTheLastOne()
        {
            int i = m_DataLines.Count() - 2;
            m_DataLines[i].ActuallNextDayState = m_DataLines[i + 1].state;
            m_DataLines[i].ActuallNextDayValue = m_DataLines[i + 1].Value;
            m_AbsNumericDiffList.Add(((Math.Abs((m_DataLines[i].ActuallNextDayValue / m_DataLines[i].Value) - (m_DataLines[i].EstimatedZeroBuy / m_DataLines[i].Value)))));
            m_DataLines[i].numericDiff = ((m_DataLines[i].ActuallNextDayValue / m_DataLines[i].Value) - (m_DataLines[i].EstimatedZeroBuy / m_DataLines[i].Value));
            m_DataLines[i].updateDiffLowerThan2();
            m_DataLines[m_DataLines.Count - 1].ActuallNextDayState = -10;
        }

        private void calculateForecasts()
        {
            int i;
            for (i= m_NumOfDaysForMarkov + m_NumOfDaysForStd; i < m_DataLines.Count; i++)
            {
                m_DataLines[i].NextDayStateForecast = m_DataLines[i].GetExpectedNextState(m_DataLines[i].state);
                setZeroBuy(m_DataLines[i]);
            }
        }

        private void calculateForecastsForLastDay()
        {
            int i = m_DataLines.Count() - 1;
            m_DataLines[i].NextDayStateForecast = m_DataLines[i].GetExpectedNextState(m_DataLines[i].state);
            setZeroBuy(m_DataLines[i]);
        }

        private void printResults()
        {
            initOuptPath(m_ChosenPaper);
            printAttrToFile();           
            printValues();
            printSummary();
                
            }

        private void printSummary()
        {
            initOuptPath("Summary");
            printSummaryValues();
        }

        public void printAvgValuesOfSummary()
        {
             using (CsvFileWriter writer = new CsvFileWriter(m_FilePath, true))
            {                     
                 CsvRow row = new CsvRow();
                 row.Add("Avg");
                 row.Add("--");
                 row.Add("--");
                 row.Add(((m_SumsOfSummary[0]/m_AllPapers.Count)*100).ToString());
                 row.Add(((m_SumsOfSummary[1] / m_AllPapers.Count) * 100).ToString());
                 row.Add(((m_SumsOfSummary[2] / m_AllPapers.Count) * 100).ToString());
                 row.Add(((m_SumsOfSummary[3] / m_AllPapers.Count) * 100).ToString());
                 writer.WriteRow(row);
            }
        }


        private void printSummaryAttrToFile()
        {
            using (CsvFileWriter writer = new CsvFileWriter(m_FilePath, true))
            {
                //subject bid=>ask
                CsvRow subRow = new CsvRow();
                subRow.Add("Paper");
                subRow.Add("Num of days for STD");
                subRow.Add("Num of days for markov learning");
                subRow.Add("Avg of Numeric diff between forecast precent and tomorrow precent");
                subRow.Add("Std of Numeric diff between forecast precent and tomorrow precent");
                subRow.Add("precent of exact forecast");
                subRow.Add("precent of good forecast");    

                writer.WriteRow(subRow);
            }
        }

        private void printSummaryValues()
        {
            
            statistics statsNumericDiff =new statistics(m_AbsNumericDiffList.ToArray());
            //statistics statsGoodHit = new statistics(m_GoodHitsList.ToArray());
            double avgOfNumericDiff = statsNumericDiff.mean();            
            
            double stdForNumericDiff = statsNumericDiff.STD();
            double precentOfExcellentHit = getPrecentOfExcellentHit();
            double precentOfGoodHit = getPrecentOfGoodHIt();

            m_SumsOfSummary[0] += avgOfNumericDiff;
            m_SumsOfSummary[1] += stdForNumericDiff;
            m_SumsOfSummary[2] += precentOfExcellentHit;
            m_SumsOfSummary[3] += precentOfGoodHit;

            using (CsvFileWriter writer = new CsvFileWriter(m_FilePath, true))
            {
                CsvRow row = new CsvRow();
                row.Add(m_ChosenPaper);//Paper name
                row.Add(m_NumOfDaysForStd.ToString());//Num of days for STD
                row.Add(m_NumOfDaysForMarkov.ToString());//"Num of days for markov learning
                row.Add((avgOfNumericDiff * 100).ToString());//Avg of Numeric diff between forecast precent and tomorrow precent
                row.Add((stdForNumericDiff*100).ToString());//Std of Numeric diff between forecast precent and tomorrow precent
                row.Add((precentOfExcellentHit*100).ToString());//precent of exact forecast
                row.Add((precentOfGoodHit*100).ToString());//precent of good forecast
                writer.WriteRow(row);
            }
        }

        private double getPrecentOfGoodHIt()
        {
            double sum = 0;
            for (int i = m_NumOfDaysForMarkov + m_NumOfDaysForStd; i < m_DataLines.Count; i++)
            {
                sum += m_DataLines[i].goodEnoughHit;
            }

            return sum / (m_DataLines.Count - m_NumOfDaysForStd - m_NumOfDaysForMarkov);
        }

        private double getPrecentOfExcellentHit()
        {
            double sum = 0;
            for (int i = m_NumOfDaysForMarkov + m_NumOfDaysForStd; i < m_DataLines.Count - 1; i++)
            {
                sum += m_DataLines[i].excellentHit;
            }

            return sum / (m_DataLines.Count - m_NumOfDaysForStd - m_NumOfDaysForMarkov);
        }
        
        void initOuptPath(string i_Type)
        {
            m_Provider = CultureInfo.InvariantCulture;
            m_FilePath = String.Format(@"{0}\{1}-{2}-{3}.csv", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                                        i_Type, m_OutputFileName, DateTime.Now.ToString("dd.MM.yyyy", m_Provider), m_NumOfDaysForStd, m_NumOfDaysForMarkov);
            
        }

        private void printAttrToFile()
        {
            using (CsvFileWriter writer = new CsvFileWriter(m_FilePath))
            {
                //subject bid=>ask
                CsvRow subRow = new CsvRow();
                subRow.Add("Date");
                subRow.Add("Value");
                subRow.Add("Value Diff From Last Day");
                
                subRow.Add("Std");
                subRow.Add("diffDIVstd");
                subRow.Add("state");

                subRow.Add("Next Day State Forecast");
                subRow.Add("Actuall Next Day State");
                subRow.Add("hit is good enough");
                subRow.Add("excellent hit!");
                subRow.Add("f_Zero Buy");

                subRow.Add("forecast precent for today");//for
                subRow.Add("tomorrow precent for today");
                subRow.Add("numeric diff between the last to 2 cols");
                subRow.Add("abs(numeric diff)");
                subRow.Add("numeric diff divded by std");

                subRow.Add("");
                subRow.Add("0=>0");
                subRow.Add("0=>1");
                subRow.Add("0=>2");
                subRow.Add("0=>-1");
                subRow.Add("0=>-2");

                subRow.Add("1=>0");
                subRow.Add("1=>1");
                subRow.Add("1=>2");
                subRow.Add("1=>-1");
                subRow.Add("1=>-2");


                subRow.Add("2=>0");
                subRow.Add("2=>1");
                subRow.Add("2=>2");
                subRow.Add("2=>-1");
                subRow.Add("2=>-2");

                subRow.Add("-1=>0");
                subRow.Add("-1=>1");
                subRow.Add("-1=>2");
                subRow.Add("-1=>-1");
                subRow.Add("-1=>-2");

                subRow.Add("-2=>0");
                subRow.Add("-2=>1");
                subRow.Add("-2=>2");
                subRow.Add("-2=>-1");
                subRow.Add("-2=>-2");
                writer.WriteRow(subRow);
            }
        }

         private void printValues()
        {
            
             
             //write result to debug\resultBidAsk.csv
            using (CsvFileWriter writer = new CsvFileWriter(m_FilePath, true))
            {
                //bid=>ask
                foreach (dataLineForMarkov line in m_DataLines)
                {
                    CsvRow row = new CsvRow();
                    row.Add(line.Date.ToString("ddMMMyyyy", m_Provider));//Date
                    row.Add(line.Value.ToString());//Value
                    row.Add(line.Difference.ToString());//Diff From Last Day                    
                    row.Add(line.Std.ToString());//Std
                    row.Add(line.diffDivStd.ToString());//diff/std
                    row.Add((stateToScale(line.state)).ToString());//state

                    row.Add((stateToScale(line.NextDayStateForecast)).ToString());//expected
                    row.Add((stateToScale(line.ActuallNextDayState)).ToString());// 1 if the diff between the expected state to the actuall is lower or equal than 1, 0 otherwise
                    row.Add(line.goodEnoughHit.ToString());// hit is good enough
                    row.Add(line.excellentHit.ToString());// excellent hit
                    row.Add(line.EstimatedZeroBuy.ToString());//zero buy

                    row.Add(((line.EstimatedZeroBuy / line.Value) * 100).ToString());//forecast precent for today
                    row.Add(((line.ActuallNextDayValue / line.Value) * 100).ToString());//tomorrowDIVtoday tomorrow precent for today
                    row.Add((line.numericDiff).ToString());//numeric diff between the last 2 cols
                    row.Add((Math.Abs(line.numericDiff)).ToString());//abs(numeric diff)                   

                    double stdOfNumericDiff = GetStdOfNumericDiff();
                    
                    row.Add(((line.numericDiff)/stdOfNumericDiff).ToString());//numeric diff divded to std");
                    row.Add("");

                    row.Add(line.givenStateProp[NO_CHANGE].NoChangeProp.ToString());
                    row.Add(line.givenStateProp[NO_CHANGE].IncreaseProp.ToString());
                    row.Add(line.givenStateProp[NO_CHANGE].HighIncreaseProp.ToString());
                    row.Add(line.givenStateProp[NO_CHANGE].DecreaseProp.ToString());
                    row.Add(line.givenStateProp[NO_CHANGE].highDecreaseProp.ToString());

                    row.Add(line.givenStateProp[INCREASE].NoChangeProp.ToString());
                    row.Add(line.givenStateProp[INCREASE].IncreaseProp.ToString());
                    row.Add(line.givenStateProp[INCREASE].HighIncreaseProp.ToString());
                    row.Add(line.givenStateProp[INCREASE].DecreaseProp.ToString());
                    row.Add(line.givenStateProp[INCREASE].highDecreaseProp.ToString());

                    row.Add(line.givenStateProp[HIGH_INCREASE].NoChangeProp.ToString());
                    row.Add(line.givenStateProp[HIGH_INCREASE].IncreaseProp.ToString());
                    row.Add(line.givenStateProp[HIGH_INCREASE].HighIncreaseProp.ToString());
                    row.Add(line.givenStateProp[HIGH_INCREASE].DecreaseProp.ToString());
                    row.Add(line.givenStateProp[HIGH_INCREASE].highDecreaseProp.ToString());

                    row.Add(line.givenStateProp[DECREASE].NoChangeProp.ToString());
                    row.Add(line.givenStateProp[DECREASE].IncreaseProp.ToString());
                    row.Add(line.givenStateProp[DECREASE].HighIncreaseProp.ToString());
                    row.Add(line.givenStateProp[DECREASE].DecreaseProp.ToString());
                    row.Add(line.givenStateProp[DECREASE].highDecreaseProp.ToString());

                    row.Add(line.givenStateProp[HIGH_DECREASE].NoChangeProp.ToString());
                    row.Add(line.givenStateProp[HIGH_DECREASE].IncreaseProp.ToString());
                    row.Add(line.givenStateProp[HIGH_DECREASE].HighIncreaseProp.ToString());
                    row.Add(line.givenStateProp[HIGH_DECREASE].DecreaseProp.ToString());
                    row.Add(line.givenStateProp[HIGH_DECREASE].highDecreaseProp.ToString());
                    writer.WriteRow(row);
                }
            }
         }

         public double GetStdOfNumericDiff()
         {
             statistics stats = new statistics(m_AbsNumericDiffList.ToArray());
             return stats.STD();
         }

         

         private void setZeroBuy(dataLineForMarkov line)
         {
             line.Std = calculateStdStartFrom(m_DataLines.IndexOf(line) - m_NumOfDaysForStd);
             double sum = 0;
             sum += line.givenStateProp[line.state].IncreaseProp * 1;
             sum += line.givenStateProp[line.state].HighIncreaseProp * 2;
             sum += line.givenStateProp[line.state].highDecreaseProp * (-2);
             sum += line.givenStateProp[line.state].DecreaseProp * (-1);
             //there is no need to add no change because we'll multiply it by zero anyway..
             line.EstimatedZeroBuy = sum * line.Std + line.Value;
             
         }

         public static int stateToScale(int i_State)
         {
             if (i_State == 3)
                 return (-1);
             else if (i_State == 4)
                 return (-2);
             else    
                return i_State;
         }

        private void calaculateRestOfLearning()
        {
            while (makeMarksOnDataForNextDay(null, null)) ;
        }


        private void initDataSource()
        {
            int j = 1;//papers Index
            m_AllPapers.Clear();
            m_PapersIndexDic.Clear();

            using (m_DataCsv = new CsvFileReader(m_InputFileName))
            {
                CsvRow row = new CsvRow();
                m_DataCsv.ReadRow(row);

                //get papers names
                while(j < row.Count)
                {
                    m_PapersIndexDic.Add(row[j], j-1);
                    m_AllPapers.Add(row[j]);
                    j++;
                }                   
            }
            m_Path = m_InputFileName;
            m_ChosenPaper = m_AllPapers[0].ToString();
        }

        private void initInputPath()
        {
            m_Path = String.Format(@"{0}fullTableDays.csv", AppDomain.CurrentDomain.BaseDirectory);
            m_OutputFileName = "results";
            m_InputFileName = m_Path;
        }

        private void getDataForChosenPaper()
        {

            m_LastDayIndexLearningCalculated = 0;
            m_DataLines.Clear();
            m_AbsNumericDiffList.Clear();
            int i = 0;

            using (m_DataCsv = new CsvFileReader(m_Path))
            {
                CsvRow row = new CsvRow();
                m_DataCsv.ReadRow(row);

                //get papers names

                while (m_DataCsv.ReadRow(row))
                {
                    m_DataLines.Add(new dataLineForMarkov());
                    m_DataLines[i].Date = new DateTime(Int32.Parse(row[0].Substring(0, 4)), Int32.Parse(row[0].Substring(5, 2)), Int32.Parse(row[0].Substring(8, 2)));
                    m_DataLines[i].Value = double.Parse(row[m_PapersIndexDic[m_ChosenPaper]+1]);//chosen paper-> index
                    i++;
                }
            }
        }

        public bool makeMarksOnDataForNextDay(double? i_NewValue, DateTime? i_Date)
        {
            int i;
            bool found = true;
            if (i_NewValue == null)
            {
                found = m_LastDayIndexLearningCalculated < m_DataLines.Count - 1;
                i_NewValue = m_DataLines[m_LastDayIndexLearningCalculated].Value;
            }

            if (found)
            {
                //calculateLearningProps(m_LastDayIndexLearningCalculated + 1 - m_NumOfDaysForMarkov);//--//

                addNewValueToMarkov((double)i_NewValue, i_Date);
                m_LastDayIndexLearningCalculated++;
                // m_NumOfRecordsInsideTheMarkov++;
                //}
                i = m_LastDayIndexLearningCalculated;
                /////-----/////
                calculateCurrDayState(i, i - 1);
                calculatePropsForLearning(i);
                /////-----/////
                m_DataLines[i].NextDayStateForecast = m_DataLines[i].GetExpectedNextState(m_DataLines[i].state);
                setZeroBuy(m_DataLines[i]);
                CalcsAfterMarkovLearningAndResultsForLastDay();
            }
            return found;
        }

        public double GetStd()
        {
            if (m_DataLines.Count() == 0)
            {
                throw new uninitialedMarkovChainException("Set parameters first");
            }

            //statistics stats = new statistics(m_AbsNumericDiffList.ToArray());
            //double stdOfNumericDiff = stats.STD();
            //return stdOfNumericDiff;
            return m_DataLines[m_DataLines.Count() - 1].Std;// changed after combined
        }

        public double GetZeroBuy()
        {
            if (m_DataLines.Count() == 0)
            {
                throw new uninitialedMarkovChainException("Set parameters first");
            }
            return m_DataLines[m_DataLines.Count() - 1].EstimatedZeroBuy;
        }

        public double getLastSeenValue()
        {
            return m_DataLines[m_DataLines.Count() - 1].Value;
        }

        private void addNewValueToMarkov(double i_NewValue, DateTime? i_Date)//prehaps it would be better to change it to nullable too
        {
            int i;
            //if (i_Date != null && i_Date != AddOneDaysAndSkipOnSaturdaysAndSundays(m_DataLines[m_DataLines.Count() - 1].Date))
            //{
            //    throw new ArgumentOutOfRangeException("worng date");
            //}

            if (i_Date != null && m_LastDayIndexLearningCalculated == m_DataLines.Count-1)// we need to add the new value
            {
                m_DataLines.Add(new dataLineForMarkov());
                m_DataLines[m_DataLines.Count() - 1].Value = i_NewValue;
                m_DataLines[m_DataLines.Count() - 1].Date = (DateTime) i_Date;// will make bugs in case there wont be saturdays& sundays dates inside the data
            }
            i = m_LastDayIndexLearningCalculated + 1;// day of new value
            m_DataLines[i].givenStateCounter = m_DataLines[i - 1].givenStateCounter;
            /////-----////
            //calculateCurrDayState(i, i - 1);
            //calculatePropsForLearning(i);
            /////-----////
        }

        //private DateTime AddOneDaysAndSkipOnSaturdaysAndSundays(DateTime i_Date)
        //{
        //    DateTime dateToReturn = i_Date.AddDays(1);

        //    if(dateToReturn.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        dateToReturn = dateToReturn.AddDays(2);
        //    }
        //    else if (dateToReturn.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        dateToReturn = dateToReturn.AddDays(1);
        //    }

        //    return dateToReturn;
        //}

        private void initLearningMarksOnData()
        {
            calculateLearningProps(m_NumOfDaysForStd);
            m_LastDayIndexLearningCalculated = m_NumOfDaysForStd + m_NumOfDaysForMarkov;
            //m_NumOfRecordsInsideTheMarkov = m_NumOfDaysForMarkov;
        }

        private void calculateLearningProps(int i_StartDayIndex)
        {
            //fix - THROW EXECPETION if start day is lower than 1
            int dayToCalcIndex = i_StartDayIndex + m_NumOfDaysForMarkov;

            for (int i = i_StartDayIndex; i < dayToCalcIndex; i++)
            {
                calculateCurrDayState(dayToCalcIndex, i);
            }

            calculatePropsForLearning(dayToCalcIndex);

        }

        private void calculateCurrDayState(int i_DayOfMarkovCalc, int i_CurrDayIndex)
        {
            m_DataLines[i_CurrDayIndex].Std = calculateStdStartFrom(i_CurrDayIndex - m_NumOfDaysForStd);//calaculate std for the m_NumOfDaysForStd days before 
            int stateOfThedayBefore = m_DataLines[i_CurrDayIndex - 1].state;
            m_DataLines[i_CurrDayIndex].Difference = m_DataLines[i_CurrDayIndex].Value - m_DataLines[i_CurrDayIndex - 1].Value;// m_DataLines[i].Value = i_NewValue in case of new value
            m_DataLines[i_CurrDayIndex].diffDivStd = m_DataLines[i_CurrDayIndex].Difference / m_DataLines[i_CurrDayIndex].Std;

            if (m_DataLines[i_CurrDayIndex].diffDivStd >= -0.5 && m_DataLines[i_CurrDayIndex].diffDivStd <= 0.5)// no change
            {
                if (i_CurrDayIndex > m_NumOfDaysForStd)//otherwise there no stateOfTheDayBefore...
                    m_DataLines[i_DayOfMarkovCalc].givenStateCounter[stateOfThedayBefore].NoChangeCount++;
                m_DataLines[i_CurrDayIndex].state = NO_CHANGE;
            }

            else if (m_DataLines[i_CurrDayIndex].diffDivStd >= -1.5 && m_DataLines[i_CurrDayIndex].diffDivStd < -0.5)// decrease
            {
                if (i_CurrDayIndex > m_NumOfDaysForStd)//otherwise there no stateOfTheDayBefore...
                    m_DataLines[i_DayOfMarkovCalc].givenStateCounter[stateOfThedayBefore].DecreaseCount++;
                m_DataLines[i_CurrDayIndex].state = DECREASE;
            }

            else if (m_DataLines[i_CurrDayIndex].diffDivStd <= -1.5)//  high decrease
            {
                if (i_CurrDayIndex > m_NumOfDaysForStd)//otherwise there no stateOfTheDayBefore...
                    m_DataLines[i_DayOfMarkovCalc].givenStateCounter[stateOfThedayBefore].highDecreaseCount++;
                m_DataLines[i_CurrDayIndex].state = HIGH_DECREASE;
            }

            else if (m_DataLines[i_CurrDayIndex].diffDivStd <= 1.5 && m_DataLines[i_CurrDayIndex].diffDivStd > 0.5)//increase
            {
                if (i_CurrDayIndex > m_NumOfDaysForStd)//otherwise there no stateOfTheDayBefore...
                    m_DataLines[i_DayOfMarkovCalc].givenStateCounter[stateOfThedayBefore].IncreaseCount++;
                m_DataLines[i_CurrDayIndex].state = INCREASE;
            }

            else if (m_DataLines[i_CurrDayIndex].diffDivStd > 1.5)//  high increase
            {
                if (i_CurrDayIndex > m_NumOfDaysForStd)//otherwise there no stateOfTheDayBefore...
                    m_DataLines[i_DayOfMarkovCalc].givenStateCounter[stateOfThedayBefore].HighIncreaseCount++;
                m_DataLines[i_CurrDayIndex].state = HIGH_INCREASE;
            }
        }

        private void calculatePropsForLearning(int i_DayToCalcIndex)
        {
            calcutlatePropsGiven(INCREASE, i_DayToCalcIndex);
            calcutlatePropsGiven(DECREASE, i_DayToCalcIndex);
            calcutlatePropsGiven(HIGH_INCREASE, i_DayToCalcIndex);
            calcutlatePropsGiven(HIGH_DECREASE, i_DayToCalcIndex);
            calcutlatePropsGiven(NO_CHANGE, i_DayToCalcIndex);
        }

        private void calcutlatePropsGiven(int i_GivenState, int i_DayToCalcIndex)
        {
            double sum = m_DataLines[i_DayToCalcIndex].givenStateCounter[i_GivenState].GetSum();
            if (sum == 0)// we can't divide by zero, and this result can't be count when we calculate
            {
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].DecreaseProp = 0.00001;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].highDecreaseProp = 0.00001;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].HighIncreaseProp = 0.00001;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].IncreaseProp = 0.00001;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].NoChangeProp = 0.00001;
            }
            else
            {
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].DecreaseProp = m_DataLines[i_DayToCalcIndex].givenStateCounter[i_GivenState].DecreaseCount / sum;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].highDecreaseProp = m_DataLines[i_DayToCalcIndex].givenStateCounter[i_GivenState].highDecreaseCount / sum;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].HighIncreaseProp = m_DataLines[i_DayToCalcIndex].givenStateCounter[i_GivenState].HighIncreaseCount / sum;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].IncreaseProp = m_DataLines[i_DayToCalcIndex].givenStateCounter[i_GivenState].IncreaseCount / sum;
                m_DataLines[i_DayToCalcIndex].givenStateProp[i_GivenState].NoChangeProp = m_DataLines[i_DayToCalcIndex].givenStateCounter[i_GivenState].NoChangeCount / sum;
            }
        }

        private double calculateStdStartFrom(int i_StartDayIndex)
        {
            List<double> Diffrence = new List<double>();

            for (int i = i_StartDayIndex + 1; i < i_StartDayIndex + m_NumOfDaysForStd; i++)//fix- add sainity check, works only on the first num of days
            {
                m_DataLines[i].Difference = m_DataLines[i].Value - m_DataLines[i - 1].Value;
                Diffrence.Add(m_DataLines[i].Difference);
            }

            statistics stats = new statistics(Diffrence.ToArray());
            double std = stats.STD();
            return std;
        }



        private void onRunAllClick(object sender, EventArgs e)
        {
            runOnAllPapers();
        }

        public void runOnAllPapers()//--//
        {
            m_SumsOfSummary = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            initOuptPath("Summary");
            printSummaryAttrToFile();
            //switch paper and run each time
            foreach (string paper in m_AllPapers)
            {
                m_ChosenPaper = paper;
                runOnCurrPaper(paper);
            }
            initOuptPath("Summary");
            printAvgValuesOfSummary();
            
        }

        private void runOnCurrPaper(string i_ChosenPaper)//input from files only
        {            
            //m_NumOfDaysForStd = i_NumOfDaysForStd;
            //m_NumOfDaysForMarkov = i_NumOfDaysForMarkov;
            getDataForChosenPaper();
            initLearningMarksOnData();
            calaculateRestOfLearning();
            CalcsAfterMarkovLearningAndResults();
            printResults();
        }

        private void runThisPaper(string i_ChosenPaper)//--//input from dictionary only
        {
            //m_NumOfDaysForStd = i_NumOfDaysForStd;
            //m_NumOfDaysForMarkov = i_NumOfDaysForMarkov;
            initLearningMarksOnData();
            calaculateRestOfLearning();
            CalcsAfterMarkovLearningAndResults();
        }

        public void CalcsAfterMarkovLearningAndResults()
        {
            calculateForecasts();
            setActuallValuesNumericDiffAndHits();            
        }

        public void CalcsAfterMarkovLearningAndResultsForLastDay()
        {
            calculateForecastsForLastDay();
            setActuallValuesNumericDiffAndHitsForTheDayBeforeTheLastOne();
        }

        public void onRunButtonClick(string i_ChosenPaper)
        {
            m_SumsOfSummary = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            initOuptPath("Summary");
            printSummaryAttrToFile();
            runOnCurrPaper(i_ChosenPaper);
        }

        public void onRunAllParamsButtonClick(int i_MinStd, int i_MaxStd, int i_StdJump, int i_MinLearn, int i_MaxLrearn, int i_LearnJump)
        {
            int std;
            for (std = 20; std <= 100; std += 10)
            {
                for (int learDays = 20; learDays <= 100; learDays += 10)
                {
                    //numOfSdaysStdTextbox.Text = std.ToString();
                    //numOfDaysChainTextBox.Text = learDays.ToString();
                    runOnAllPapers();
                }
            }
        }


        private void onLoadInputFilesButtonClick(object sender, EventArgs e)
        {
            initDataSource();
        }

        public Dictionary<DateTime, double> GetDicOfDatesAndValues()//added after combined
        {
            Dictionary<DateTime, double> dicToReturn = new Dictionary<DateTime, double>();
            foreach (dataLineForMarkov line in m_DataLines)
            {
                dicToReturn.Add(line.Date, line.Value);
            }
            return dicToReturn;
        }

        public List<dataLineForMarkov> DataLines
        {
            get { return m_DataLines; }
        } 
        
    }
}
