using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using makrovchain1;
using System.Reflection;

namespace dataanalessisGUI
{
    class AskBidByDate
    { 

        private double m_MaxprofitPerunit;
        private double m_MinprofitPerunit;
        private double m_minProfit;
        private double m_minlenFromCurrMaxPnl;
        private double m_maxProfit;
        private const double c_unit = 1000000;
        private const double c_MaxValue = 200;
        private readonly List<dealsTransLog> m_trasactioLog = new List<dealsTransLog>();
        private readonly List<DealLine> m_askBidList = new List<DealLine>();
        private double m_mybuy;
        private double m_mysell;
        private int m_pos;
        private double m_dealFactor;
        private double m_dealOrder;
        private double m_zeroBuy;
        private ReadFile m_readAsk;
        private ReadFile m_readBid;
        private int m_order;
        private double m_totalChashflow;
        private string m_logoutputfile;
        private string m_summaryfile;//make up a uniqe name for it
        private  long  m_turnover;
        private List<string> m_nameOfFiles = new List<string>();
        private int m_currentPairOfFiles;
        private double sumOfProfits;
        private int m_numberOfDeals;
        private string m_startDate;
        private string m_EndDate;
        private int m_DateToStart;
        private int m_DateToEnd;
        private int m_numberOfFiles;
        private bool m_isEndDateOrStopLoss;
        private ImotherDataLearner m_learner;
        private int m_daysTolearn;
        private double m_distance;
        private double m_MaxPos;
        private double m_DiffranceAskBid;
        private int m_DateToStartTarde;
        private double m_averageShift;
        private readonly List<double> m_zeroBuyList = new List<double>();//delete after testing
        private readonly List<int> m_posList = new List<int>();//delete after testing
        private readonly List<double> m_diffrecneAskBId = new List<double>();//delete after testing
        private int m_daysCounter;
        private double m_NumOfStd;
        private List<string> m_summarylines = new List<string>();
        private double m_weight;
        private bool m_isPassMaxPos;
        private double m_maxLost;
        private string m_pathOffileToLearnFrom;
        private string m_stringStartDate;
        private bool m_IsRegression;//m_isLearnFromFile
        private double m_maxStd;
        private double m_minStd;
        private bool m_isZeroBuyChecked;
        private double m_Std;
        private double m_lenthZeroBuyStd;
        private double m_lenthZeroBuyFormFirstValue;
        private List<string> m_listOfTradeDays = new List<string>();
        private dealsTransLog m_lastDeal = new dealsTransLog();
        private int m_endPos;
        private List<string> m_openingPrices;
        private readonly Dictionary<string, string> m_historyData = new Dictionary<string,string>();
        private string m_TypeOfPaper;
        private bool m_CalcDisByPrecentian;
        private double m_precentian;
        public string TypeOfPaper
        {
            get { return m_TypeOfPaper; }
            set { m_TypeOfPaper = value; }
        }

        public List<string> openingPrices
        {
            set
            {
                m_openingPrices = value;
            }
        }

        public List<string> ListOfTradeDays
        {
            set
            {
                m_listOfTradeDays = value;
            }
        }

        public double LenthZeroBuyStd
        {
            set 
            {
                m_lenthZeroBuyStd = value;
            }
        }

        public double MaxStd
        {
            set
            {
                m_maxStd = value;
            }
        }
        public double MinStd
        {
            set
            {
                m_minStd = value;
            }
        }

        public int DaysToPredictZeroBuy
        {
            set
            {
              //m_learner.Daysforword = value;
            }
        }

        public string PathOffileToLearnFrom
        {
            set
            {
                m_pathOffileToLearnFrom = value;
            }
        }

        public List<int> PosList
        {
            get
            {
                return m_posList;
            }
        }

        public double STD
        {
            get { return m_learner.Std; }
        }

        public double MySell
        {
            get { return m_mysell; }
        }

        public double MyBuy
        {
            get { return m_mybuy; }
        }

        public double ZeroBuy
        {
            get { return m_zeroBuy;}
        }

        public List<double> ZeroBuyList
        {
            get
            {
                return m_zeroBuyList;
            }
        }

        public List<dealsTransLog> TrasactioLog
        {
            get { return m_trasactioLog; }
        }

        public List<string> Summarylines
        {
            get { return m_summarylines; }
        }
        
         public double MaxProfitPerunit 
        {
            set { m_MaxprofitPerunit = value; }
        }

         public double MinProfitPerunit
         {
             set { m_MinprofitPerunit = value; }
         }

        public int Order
        {
            set { m_order = value; }
        }



        public AskBidByDate(InputClass i_input, string[] i_bidFileSrouces, string[] i_AskFileSrouces,
            string i_outPutFile, string i_summaryFile,bool i_IsReggression)
        {
            m_dealOrder = i_input.Order_Value;
            m_daysTolearn = i_input.DaysBackward;
            m_minStd = i_input.MinStd;
            m_maxStd = i_input.MaxStd;
            m_NumOfStd = i_input.Num_Of_Std;
            m_lenthZeroBuyStd = i_input.LenFormZeroBuy;
            m_maxLost = i_input.Max_Pos;
            m_MinprofitPerunit = i_input.MinPPm;
            m_MaxprofitPerunit = i_input.MaxPPm;
            m_readAsk = new ReadFile(i_AskFileSrouces);
            m_readBid = new ReadFile(i_bidFileSrouces);
            m_minProfit = 10000;//we want to find the min flow of p&l
            m_stringStartDate = i_input.StartDate;
            m_DateToEnd = int.Parse(setDateFormat(i_input.EndDate));
            m_numberOfFiles = i_AskFileSrouces.Length;
            m_order = i_input.Order_Value;
           
            if (i_IsReggression == false)
            {
                m_DateToStart = int.Parse(setDaysBack(i_input.StartDate));
            }
            else
            {
                m_DateToStart = int.Parse(setDateFormat(i_input.StartDate));
            }
            m_IsRegression = i_IsReggression;
            if (m_IsRegression == false)
            {
              //  m_learner = new Datalearer(m_askBidList, m_DateToStartTarde, m_DateToEnd);
            }
            else
            {
                int day = int.Parse(m_stringStartDate.Substring(0, 2));
                int month = int.Parse(m_stringStartDate.Substring(2, 2));
                int year = int.Parse(m_stringStartDate.Substring(4, 4));

                DateTime startDate = new DateTime(year, month, day);
                m_learner = new ReggLearner(startDate, m_daysTolearn);

            }
           // m_learner.Daysforword = i_input.DaysToPre;
            m_logoutputfile = i_outPutFile;
            m_summaryfile = i_summaryFile;


        }

        //use only this c'tor
        public AskBidByDate(string[] i_bidFileSrouces, string[] i_AskFileSrouces,
            string i_outPutFile, string i_summaryFile, string i_startDate, string i_endDate, 
            int i_daysTolearn, double i_averageShift, double i_weight, bool i_isReggression,
            string i_TypeOfPaper, bool i_CalcDistanceByPrecentianOfDiff, double i_precentian)
        {//for mutiplay files work ,no unifile its just too big to work with
            m_precentian = i_precentian;
            m_CalcDisByPrecentian = i_CalcDistanceByPrecentianOfDiff;
            m_TypeOfPaper = i_TypeOfPaper;
            m_IsRegression = i_isReggression;
            m_averageShift = i_averageShift;
            m_daysTolearn = i_daysTolearn;
            m_pos = 0;
            m_weight = i_weight;
            m_logoutputfile = i_outPutFile;
            m_summaryfile = i_summaryFile;
            m_minProfit = 10000;//we want to find the min flow of p&l
            m_turnover = 0;//no need but to make things clear its a sum of all pos in absolute value we add one for evey pos and mult it by m_order
            m_minlenFromCurrMaxPnl = 0;//we asuming that any flow p&l - maxP&l < 0
            m_DateToStartTarde = int.Parse(setDateFormat(i_startDate));
            
         

            m_DateToEnd = int.Parse(setDateFormat(i_endDate));
            m_stringStartDate = i_startDate;
            m_numberOfFiles = i_AskFileSrouces.Length;
            m_readAsk = new ReadFile(i_AskFileSrouces);
            m_readBid = new ReadFile(i_bidFileSrouces);


            m_DateToStart = int.Parse(setDateFormat(i_startDate));
            if (m_IsRegression == true)
            {
                
                int day = int.Parse(m_stringStartDate.Substring(0, 2));
                int month = int.Parse(m_stringStartDate.Substring(2, 2));
                int year = int.Parse(m_stringStartDate.Substring(4, 4));

                DateTime startDate = new DateTime(year, month, day);
                m_learner = new ReggLearner(startDate, m_daysTolearn);               
            }
        }

        public void SetLearner(ImotherDataLearner i_learner)
        {
            m_learner = i_learner;
        }

        private void SetFilesToTheDate()
        {

            m_readAsk.setFilesDate(m_DateToStart);
            m_readBid.setFilesDate(m_DateToStart);
        }

        private void sethistoricData()
        {
            int min = m_listOfTradeDays.Count > m_openingPrices.Count ?m_openingPrices.Count : m_listOfTradeDays.Count;

            for (int i = 0; i < min ; i++)
            {
                m_historyData.Add( setDateFormat( m_listOfTradeDays[i]),m_openingPrices[i]);
            }
        }

       public void RunByAskAndBid(double i_maxLost,double i_NumOfStd)
        {
          
            m_pos = 0;
            bool isFirstDate = false;
            bool isDayexist = true;
          //  readFromListOfDaysFile();
            SetFilesToTheDate();
            //find the file to start with ,include the backward days To learn not if we learn From file
            int start = 0; 
            for(int i = 0; i < m_numberOfFiles && isFirstDate == false ; i++)
            {
                for(int j = 1 ; j < m_readAsk.LenOfText / m_readAsk.LenOfLine && isFirstDate == false ; j++)
                {
                    if(m_readAsk.getDate(j) >= m_DateToStart)
                    {
                        if (m_readAsk.getDate(j) > m_DateToStart)
                        {
                            isDayexist = false;
                        }
                        isFirstDate = true;
                        start = j;

                    }
                }
                if(isFirstDate == false)
                {
                    nextFile();
                }
                
            }
            DateTime startDate;
            DateTime endDate;
     

            setDateAndTime("000000", setDateFromNumTOBackwordString(m_DateToStart.ToString()), out startDate);
            setDateAndTime("000000", setDateFromNumTOBackwordString(m_DateToEnd.ToString()), out endDate);
           if(isDayexist == false)
           {
               if ((startDate - endDate).TotalDays == -1)//if trade time is only one day
               {
                   throw new dayNotExistException();
               }
           }

          
          sethistoricData();
          
           if(!(m_historyData.ContainsKey(setDateFormat(m_stringStartDate))) == true)
           {
               if (((startDate - endDate).TotalDays) == -1)
               {
                       throw new dayNotExistException();

               }
           }

           if ((m_historyData.ContainsKey(setDateFormat(m_stringStartDate))) == true)
           {
               if (m_historyData[setDateFormat(m_stringStartDate)] == " NaN")
               {
                   if ((startDate - endDate).TotalDays == -1)
                   {
                       throw new dayNotExistException();

                   }
               }

           }


             if(start == 0)//line number must be more than 0
             {
                 start = 1;
             }

            UniedBidAskFile(start);

           if(m_learner as ReggLearner != null)
           {
               ReggLearner learner = m_learner as ReggLearner;
               learner.BuildData(m_listOfTradeDays, m_openingPrices);
           }


           m_learner.Start();
           //while(m_learner.Start() == false )
           //{
              
           //    m_askBidList.Clear();
           //    nextFile();
           //    UniedBidAskFile(1);
           //    //if (m_askBidList.Count == 0)
           //    //{

           //    //}

           //}
         
               cleanLearnedData();// reset the list to start from last place who we learned from

       
           // learning is over start from the last place
            m_NumOfStd = i_NumOfStd;
            
            
            m_Std = stdBorderCheck();
            m_zeroBuy = m_learner.ZeroBuy;
            m_zeroBuy = zeroBuyBorderCheck(m_learner.lastPoint);
            if (m_CalcDisByPrecentian == true)
            {
                m_distance = m_precentian;
            }
            else
            {
                m_distance = m_NumOfStd * m_Std;
            }
            m_maxLost = i_maxLost;
            m_MaxPos = (((i_maxLost * 2) / m_distance) * m_zeroBuy);
            m_dealFactor = m_distance / m_MaxPos;
            m_dealOrder = m_dealFactor * m_order;
            
            if(m_askBidList.Count < 2)
            {  
                m_askBidList.Clear();
                UniedBidAskFile(1);
                nextFile();
                
            }

           //if(int.Parse(m_askBidList[0].Date) != m_DateToStartTarde )
           //{
           //    if ((m_DateToEnd - m_DateToStartTarde) == 1)
           //    {
           //        throw new dayNotExistException();

           //    }
           //}

            // do ---> while model with "for loop"
            try
            {
                m_DiffranceAskBid = (m_MinprofitPerunit / c_unit) * m_learner.ZeroBuy; //Math.Abs(m_askBidList[0].Value - m_askBidList[1].Value);
                
                SetMyBuyAndMySell(m_askBidList[0].Value, m_askBidList[1].Value);
                tradByAskAndBid();
                m_startDate = m_askBidList[0].Date;
                sumOfProfits += m_askBidList[m_askBidList.Count - 1].ChashFlow + m_askBidList[m_askBidList.Count - 1].NetPos;
                m_numberOfDeals += m_trasactioLog.Count - 1;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new emptyQueueExcption("empty list in date " + m_DateToStartTarde);
            }
            
           bool isListNotEmpty = true;        
           for (int i = 0; i < m_numberOfFiles - 1  && m_isEndDateOrStopLoss == false; i++ )
            {


                
                m_askBidList.Clear();
                nextFile();
                UniedBidAskFile(1);
                if (m_askBidList.Count > 0)
                {
                    tradByAskAndBid();
                    if (m_trasactioLog.Count  > 0)//if any deals have been made
                    {
                    sumOfProfits += m_trasactioLog[m_trasactioLog.Count - 1].ChashFlow + m_trasactioLog[m_trasactioLog.Count - 1].NetPos;
                    m_numberOfDeals += m_trasactioLog.Count - 1;
                    }
                        // m_currentPairOfFiles++; 
                    m_lastDeal.DealLine = m_askBidList[m_askBidList.Count - 1];
                    m_lastDeal.IndexInfile = m_askBidList.Count - 1;
                    m_EndDate = m_askBidList[m_askBidList.Count - 1].Date;

                }
                else
                {
                    isListNotEmpty = false;
                }

               
            
            }
         

           
          if(isListNotEmpty == true)
          {
                m_lastDeal.DealLine = m_askBidList[m_askBidList.Count - 1];
                m_lastDeal.IndexInfile = m_askBidList.Count - 1;
                m_EndDate = m_askBidList[m_askBidList.Count - 1].Date;
          }

          if (m_trasactioLog.Contains(m_lastDeal) == false)
           {
              m_trasactioLog.Add(m_lastDeal);
           }
            
           
         //  m_trasactioLog[m_trasactioLog.Count - 2].PnlUntilThisPoint = m_trasactioLog[m_trasactioLog.Count - 1].PnlUntilThisPoint;  
       

           //List<string> lines = new List<string>();
           //lines.Add("diffrace ask bid");
           //foreach(double x in m_diffrecneAskBId )
           //{
           //    lines.Add(x.ToString());
           //}
           //System.IO.File.WriteAllLines(@"C:\diffracelist.csv", lines);
            
        }

       public void printOutPutFiles()
       {
           LogfileOfDeals();
           summaryLogFile();
           learningLogFile();
          // printFileAskBid();
       }

       private double zeroBuyBorderCheck(double i_firstValue)
       {
           double outCome = m_zeroBuy;
           m_lenthZeroBuyFormFirstValue = ((i_firstValue - m_zeroBuy) / m_Std);
           double absborder;
           if (m_lenthZeroBuyStd == 0)
           {
               // return outCome;
               absborder = double.PositiveInfinity;
           }
           else
           {
               absborder = m_NumOfStd / m_lenthZeroBuyStd;
           }

           if (m_isZeroBuyChecked == false)
           {
               

               if (!((-absborder < m_lenthZeroBuyFormFirstValue) && (absborder > m_lenthZeroBuyFormFirstValue)))
               {
                   if (absborder < m_lenthZeroBuyFormFirstValue)
                   {
                       outCome = i_firstValue - absborder * m_Std;

                   }
                   else
                   {
                       outCome = i_firstValue + absborder * m_Std;
                   }


               }
               else
               {
                   return outCome;
               }
               if (outCome < 0)//senety Check
               {
                   throw new Exception("zero Buy is in negative value");
               }


              
           }
           m_isZeroBuyChecked = true;

           return outCome;
       }
        
       private double stdBorderCheck()
       {
          

           double outCome = m_learner.Std;
           if (!(m_minStd < (m_learner.Std / m_learner.lastPoint) && (m_learner.Std / m_learner.lastPoint) < m_maxStd))
           {

               if ((m_learner.Std / m_learner.lastPoint) < m_minStd)
               {
                   outCome = m_minStd * m_learner.lastPoint;
               }
               else
               {
                   outCome = m_maxStd * m_learner.lastPoint;
               }
           }
           return outCome;
       }

        private void cleanLearnedData()
        {//we dont change the list address so we copy it
            List<DealLine> temp = new List<DealLine>();
            int index = 0;
            if(m_learner as Datalearer != null)
            {
               // index = m_learner.IndexEnded;
            }
            for (int i = index  ; i < m_askBidList.Count ; i++)
            {
                temp.Add(m_askBidList[i]);

            }
            m_askBidList.Clear();
            for (int i = 0 ; i < temp.Count ; i++ )
            {
                m_askBidList.Add(temp[i]);
            }
        }


        private string setDateFromNumTOBackwordString(string text) 
        {
            if (text == null) return null;
            string outcome;
            //take days
            string years = text.Substring(0, 4);
            string months = text.Substring(4, 2);
            string days = text.Substring(6, 2);


            outcome = years + months + days;

            return outcome;
        }

       private string setDateFormat(string text)//change name and make i  for format 0000/00/00 eu 
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

       private string setDateToRegularFormat(string text)//change name and make i  for format 00/00/0000 eu 
       {
           if (text == null) return null;
           string outcome;
           //take days
           string days = text.Substring(6, 2);
           string months = text.Substring(4, 2);
           string years = text.Substring(0, 4);


           outcome = days + "/" + months + "/" + years;

           return outcome;
       }

       private string setDaysBack(string text)//change name and make i  for format 00/00/0000 eu
       {
           if (text == null) return null;

           // this was posted by petebob as well 
           string outcome;
           //take days
           string days = text.Substring(0,2);
           string months = text.Substring(2,2);
           string years = text.Substring(4,4);
           //clack the days backword
           int day, month, year;
           day = int.Parse(days);
           month = int.Parse(months);
           year = int.Parse(years);

           int howManydays = m_daysTolearn % 30;
           int howManyMonths = m_daysTolearn / 30;
           int howManyYears = howManyMonths / 12;

           day = (day - howManydays) % 30;
           month = (month - howManyMonths) % 12;
           year = year - howManyYears;//i dont think it ever get negetive :P

           if(day <= 0)
           {
               day = day + 30;
               month = month - 1;
           }

           if (month <= 0)
           {
               month = month + 12;
               year = year - 1;
           }

           if (month < 10)
           {
               months = "0" + month;
           }
           else
           {
               months = month.ToString();
           }

           if (day < 10)
           {
               days = "0" + day;
           }
           else
           {
               days = day.ToString();
           }

           years = year.ToString();

           outcome = years + months + days;

           return outcome;
       }

        private void nextFile()
        {
            if (m_readAsk.NextFile() || m_readBid.NextFile())
            {
                m_isEndDateOrStopLoss = true;
            }
          
        }

private void LogfileOfDeals()
        {
            List<string> lines = new List<string>();
            // header of the log file

            lines.Add(@"index in file, date, time, Bid/ask, order, value, pos,net pos,my_buy, my_sell,cash flow ,flow P&L," + "total chashflow = " + m_totalChashflow);

            if (m_trasactioLog.Count == 0)
           {
               return; //throw pr send message
           }

          foreach (dealsTransLog  x in m_trasactioLog)
            {
                lines.Add(string.Format(@"{0},{1},{2},{3},{4},{5},{6},{7},{8}, {9},{10},{11}",
               x.IndexInfile , x.Date, x.Time, x.DealOffer, x.OrderSize,
               x.Value, x.Pos, x.NetPos, x.Mybuy, x.Mysell, x.ChashFlow, x.PnlUntilThisPoint));
               
            }

            string newName;
            string name = Path.GetFileNameWithoutExtension(m_logoutputfile);
            string directory = Path.GetDirectoryName(m_logoutputfile);
           
            

            newName = directory + @"\" + name  + "_" + m_startDate+".csv";
            System.IO.File.WriteAllLines(newName, lines.ToArray());
            //now we write xml files witch every line is in xml format
            //public string date;
            //public string time;
            //public string BidAsk;
            //public int order;
            //public int pos;
            //public int netPos;
            //public int indexInFile;
            //public double my_buy;
            //public double my_sell;
            //public double cashFlow;
            //public double flowPNL; 
            //public double value;  
            //in this order
            lines.Clear();
            foreach (dealsTransLog x in m_trasactioLog)
            {
                lines.Add(string.Format(
@"<dataline>
<date>{0}</date>
<time>{1}</time>
<BidAsk>{2}</BidAsk>
<order>{3}</order>
<pos>{4}</pos>
<netPos>{5}</netPos>
<indexInFile>{6}</indexInFile>
<my_buy>{7}</my_buy>
<my_sell>{8}</my_sell>
<cashFlow>{9}</cashFlow>
<flowPNL>{10}</flowPNL>
<value>{11}</value>
</dataline>",
                 x.Date, x.Time,x.DealOffer.ToString(),
                (int)x.DealOffer, (int)x.Pos,(float)x.NetPos, 
                x.IndexInfile, (float)x.Mybuy,(float)x.Mysell, 
                (float)x.ChashFlow, (float)(x.PnlUntilThisPoint),(float)x.Value));

            }
            newName = directory + @"\" + name + "_log" + "_" + m_startDate +".txt";
            System.IO.File.WriteAllLines(newName, lines.ToArray());
            
        }

private void clackZeroBuy()
{   
   // m_zeroBuyList.Add(m_zeroBuy);
    //m_posList.Add(m_pos);
    m_zeroBuy = m_zeroBuy - (m_dealFactor * m_pos * m_averageShift);
}

private void clackNewSTD()
{
    double oldSTD = m_learner.Std;
   // m_learner.ClackNewSTD();//#TODO get the next lin inside the learner
    double newSTD = ((m_weight * m_learner.Std * 1 )+ oldSTD *  m_daysTolearn) / (m_daysTolearn + 1 * m_weight );//1 is the number of days that the std was taken just keep that in mind

    m_learner.Std = newSTD;
   
    double distance = m_NumOfStd * m_learner.Std;
    m_dealFactor = distance / m_MaxPos;
    m_dealOrder = m_dealFactor * m_order;
    m_distance = distance;
}

private void clackDiffrence(double i_sum,int i_size)
{
    m_DiffranceAskBid = i_sum / i_size;
//    m_diffrecneAskBId.Add(m_DiffranceAskBid);
    if (m_DiffranceAskBid * c_unit > m_MaxprofitPerunit)
    {
        m_DiffranceAskBid = (m_MaxprofitPerunit / c_unit) * m_zeroBuy;
    }

    if (m_DiffranceAskBid * c_unit < m_MinprofitPerunit)
    {
        m_DiffranceAskBid = (m_MinprofitPerunit / c_unit) * m_zeroBuy;
    }
}

private void setDateAndTime(string i_time,string i_date,out DateTime i_timeAndDate)
{
    int hour =int.Parse(i_time.Substring(0, 2));
    int min = int.Parse(i_time.Substring(2, 2));
    int sec = int.Parse(i_time.Substring(4, 2));

    int  year =  int.Parse(i_date.Substring(0, 4));
    int  month = int.Parse(i_date.Substring(4, 2));
    int  day =  int.Parse(i_date.Substring(6, 2));
    i_timeAndDate = new DateTime(year, month, day, hour, min, sec);

}

private void tradByAskAndBid()
        {
            double netPos = 0;
            int linecounter = 0;
            bool isTrasactioHappen = false;
            int currentDay = int.Parse(m_askBidList[0].Date);
            List<double> TenMinOfValueds = new List<double>();
            bool isStopLoss = false;
            string startTime = m_askBidList[0].Time;
            string startDate = m_askBidList[0].Date;
            double maxValue = 0;
            double minValue = c_MaxValue;
            double sumOfTenMin = 0;
            int countOfSum = 0;
            DateTime passHour;
            DateTime passTenMin;
            setDateAndTime(startTime, startDate, out passHour);
            setDateAndTime(startTime,startDate,  out passTenMin);
                 
    foreach (DealLine x in m_askBidList)
            {
                x.Pos = m_pos;// save last pos
                x.Mybuy = m_mybuy;
                x.Mysell = m_mysell;
               // m_learner.SetDataForNewStd(x.Value);
                //update zero buy for each day
                if(currentDay < int.Parse(x.Date))
                {
                 currentDay = int.Parse(x.Date);
                //    m_daysCounter++;
                //clackNewSTD();//reset the learnig
                 clackZeroBuy();
                    
                }

                if (maxValue < x.Value)
                {
                    maxValue = x.Value;
                }

                if (minValue > x.Value)
                {
                    minValue = x.Value;
                }

                DateTime currentTime;
                setDateAndTime(x.Time, x.Date, out currentTime);
                if (passTenMin.AddMinutes(10) < currentTime)// mean that ten min have been pass
                {
                    sumOfTenMin += maxValue - minValue;//may be do that in learner  
                    minValue = c_MaxValue;// we assume that 200 is max value 
                    maxValue = 0;
                    countOfSum ++;
                    passTenMin = currentTime;
                }

                if (passHour.AddHours(1)  < currentTime)// mean that one hour have been pass
                {
                   clackDiffrence(sumOfTenMin,countOfSum);
                   SetMyBuyAndMySell(x.Value, x.Value);
                    sumOfTenMin = 0;
                    countOfSum = 0;
                    passHour = currentTime;
                    passTenMin = currentTime;
                }
                
                //if(m_daysCounter == m_daysTolearn)
                //{
                //    m_daysCounter = 0;

                //}
                
                if (isStopLoss == false)
                {
                    if (x.DealOffer == MarketDeal.BID)
                    {
                        
                        if (m_mysell < x.Value && m_pos > -m_MaxPos)
                        {
                            isTrasactioHappen = true;

                            m_pos = m_pos - m_order;

                            x.OrderSize = -m_order;
                            SetMyBuyAndMySell(x.Value, x.Value);

                            x.ChashFlow = m_order * x.Value;
                        }
                    }

                    if (x.DealOffer == MarketDeal.ASK)
                    {
                        if (m_mybuy > x.Value && m_pos < m_MaxPos)
                        {
                            isTrasactioHappen = true;
                            // x.Pos = m_pos;//save the current pos
                            m_pos = m_pos + m_order;

                            x.OrderSize = m_order;
                            SetMyBuyAndMySell(x.Value, x.Value);
                            x.ChashFlow = -m_order * x.Value;

                        }

                    }
                    m_totalChashflow = m_totalChashflow + x.ChashFlow;
                    x.ChashFlow = m_totalChashflow;//the cash flow until this point
                    
                }
                else
                {
                    x.ChashFlow = m_totalChashflow;
                    x.PnlUntilThisPoint = m_totalChashflow;
                }
                

                if(isTrasactioHappen == true)
                {
                    isTrasactioHappen = false;
                    m_turnover++;
                    //pointers to daels
                    dealsTransLog newDeal = new dealsTransLog();
                    newDeal.DealLine = x;
                    newDeal.IndexInfile = linecounter;
                    m_trasactioLog.Add(newDeal);
                   
                }
                netPos = m_pos * x.Value;

                x.NetPos = netPos;

                
                //min max set up
                if (m_maxProfit < x.ChashFlow + x.NetPos)
                {
                    m_maxProfit = x.ChashFlow + x.NetPos;
                }
               
                
                if (m_minProfit > x.ChashFlow + x.NetPos)
                {
                    m_minProfit = x.ChashFlow + x.NetPos;
                  //  writeLinesl.Add(string.Format("{0},{1},{2},{3},{4},{5}", //for test only
                   //     x.NetPos, x.Pos * x.Value, x.Value, x.Date, x.Time, m_minProfit));
                    
                }
                //calc max DrowDown(DD)
                if (m_minlenFromCurrMaxPnl > (x.ChashFlow + x.NetPos) - m_maxProfit)
                {
                    m_minlenFromCurrMaxPnl = (x.ChashFlow + x.NetPos) - m_maxProfit;
                }

               // stop loss
                if (Math.Abs(m_pos) > m_MaxPos)
                {

                     m_isPassMaxPos = true;
                    
                    if (m_pos > 0)
                    {
                        x.OrderSize = -m_pos;
                        m_pos = m_pos + (int)x.OrderSize;
                        m_totalChashflow += (-x.OrderSize * x.Value);
                    }
                    else 
                    {
                        x.OrderSize = m_pos;
                        m_pos = m_pos - (int)x.OrderSize;
                        m_totalChashflow += (x.OrderSize * x.Value);
                    }// we check here if the m_pos is get to zero wicth mean we cash in all the deal
                    
                    
                   
                    x.ChashFlow = m_totalChashflow;
                    x.PnlUntilThisPoint = x.ChashFlow;
                    x.Pos = 0;
                    m_MaxPos = 0;
                   // m_isEndDateOrStopLoss = true;
                    isStopLoss = true;
                 
                }

                if (x.PnlUntilThisPoint == 0)
                {
                    x.PnlUntilThisPoint = x.ChashFlow + x.NetPos;
                }
            }



        }
private void learningLogFile()
{
    List<dataLineForMarkov> listOfDataLines = null;
    if (m_IsRegression == false)
    {
        listOfDataLines = (m_learner as MarkovLearner).ListOfDataLines;
    }
    string newName;
    string name = Path.GetFileNameWithoutExtension(m_logoutputfile);
    string directory = Path.GetDirectoryName(m_logoutputfile);

    List<string> lines = new List<string>();
    int index = 0;
    if(m_learner.DateAndValuesFromCalc == null )//sanety check
    {
        return;
    }
    lines.Add("Date,Value, Value Diff From Last Day, Std, diffDIVstd, state,Next Day State Forecast, Actuall Next Day State, hit is good enough, excellent hit!,f_Zero Buy,forecast precent for today,tomorrow precent for today,numeric diff between the last to 2 cols,abs(numeric diff),numeric diff divded by std, ,0=>0,0=>1,0=>2,0=>-1,0=>-2,1=>0,1=>1,1=>2,1=>-1,1=>-2,2=>0,2=>1,2=>2,2=>-1,2=>-2,-1=>0,-1=>1,-1=>2,-1=>-1,-1=>-2,-2=>0,-2=>1,-2=>2,-2=>-1,-2=>-2");

    foreach(dateAndValue x in m_learner.DateAndValuesFromCalc)
    {
        
        if (m_IsRegression == true)
        {
            lines.Add(x.Value + "," + x.Date + "," + (x.Value - ((m_learner as ReggLearner).Slope * index + 
                (m_learner as ReggLearner).Intercept) + "," + ((m_learner as ReggLearner).Slope * index + (m_learner as ReggLearner).Intercept)));
        }
        else
        {
            lines.Add(x.Date + "," + x.Value + "," +
            listOfDataLines[index].Difference.ToString() +","+
            listOfDataLines[index].Std.ToString()+","+
            listOfDataLines[index].diffDivStd.ToString()+","+
            (stateToScale(listOfDataLines[index].state)).ToString()+","+

            (stateToScale(listOfDataLines[index].NextDayStateForecast)).ToString()+","+
            (stateToScale(listOfDataLines[index].ActuallNextDayState)).ToString()+","+
            listOfDataLines[index].goodEnoughHit.ToString()+","+
            listOfDataLines[index].excellentHit.ToString()+","+
            listOfDataLines[index].EstimatedZeroBuy.ToString()+","+

            ((listOfDataLines[index].EstimatedZeroBuy / listOfDataLines[index].Value) * 100).ToString()+","+
            ((listOfDataLines[index].ActuallNextDayValue / listOfDataLines[index].Value) * 100).ToString()+","+
            (listOfDataLines[index].numericDiff*100).ToString()+","+
            ((Math.Abs(listOfDataLines[index].numericDiff))*100).ToString()+","+



            ((listOfDataLines[index].numericDiff) / ((m_learner as MarkovLearner).GetStdOfNumericDiff())).ToString() + "," + " ,"+

            listOfDataLines[index].givenStateProp[0].NoChangeProp.ToString()+","+
            listOfDataLines[index].givenStateProp[0].IncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[0].HighIncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[0].DecreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[0].highDecreaseProp.ToString()+","+

            listOfDataLines[index].givenStateProp[1].NoChangeProp.ToString()+","+
            listOfDataLines[index].givenStateProp[1].IncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[1].HighIncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[1].DecreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[1].highDecreaseProp.ToString()+","+

            listOfDataLines[index].givenStateProp[2].NoChangeProp.ToString()+","+
            listOfDataLines[index].givenStateProp[2].IncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[2].HighIncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[2].DecreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[2].highDecreaseProp.ToString()+","+

            listOfDataLines[index].givenStateProp[3].NoChangeProp.ToString()+","+
            listOfDataLines[index].givenStateProp[3].IncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[3].HighIncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[3].DecreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[3].highDecreaseProp.ToString()+","+

            listOfDataLines[index].givenStateProp[4].NoChangeProp.ToString()+","+
            listOfDataLines[index].givenStateProp[4].IncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[4].HighIncreaseProp.ToString()+","+
            listOfDataLines[index].givenStateProp[4].DecreaseProp.ToString() +","+
            listOfDataLines[index].givenStateProp[4].highDecreaseProp.ToString());
            index++;
        }

    }

    newName = directory + @"\" + name + "_learn" + "_" + m_startDate + ".csv";

    System.IO.File.WriteAllLines(newName, lines);
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

 private void summaryLogFile()
 {
     if(m_trasactioLog.Count == 0)//sanety check
     {
         throw new NoDealsExcption();
         
     }


                m_summarylines.Add(string.Format(
@"name of file {0} 
start date = {1}
end date = {2} 
nuber of deals = {3}
profit at end runing  = {4} 
min flow pnl = {5}
max flow pnl = {6}
turn over = {7}
maxDrowDown = {8}
end pos = {9}
STD = {10}
Max pos = {11}
distance = {12}
deal factor = {13}
Diffrance Ask Bid = {14}
profit per mil = {15}
zero buy  = {16}
max lost = {17}
is pass Max Pos = {18}
slope = {19}
inIntercept = {20}
(value - zeroBuy)/std = {21}"


, "multifiles", setDateToRegularFormat( m_startDate), setDateToRegularFormat(m_EndDate), m_trasactioLog.Count,//the item in the list is not a deal is the last line in the unifile
m_trasactioLog[m_trasactioLog.Count - 1].NetPos + m_trasactioLog[m_trasactioLog.Count - 1].ChashFlow, 
m_minProfit, m_maxProfit, m_turnover * m_order, m_minlenFromCurrMaxPnl * (-1)
,m_trasactioLog[m_trasactioLog.Count - 1].Pos,m_Std,m_MaxPos,m_distance,
m_dealFactor,m_DiffranceAskBid,m_MaxprofitPerunit,m_zeroBuy,
m_maxLost,m_isPassMaxPos == true ? 1 : 0, 0, 0, m_lenthZeroBuyFormFirstValue));

                string newName;
                string name = Path.GetFileNameWithoutExtension(m_logoutputfile);
                string directory = Path.GetDirectoryName(m_logoutputfile);



                newName = directory + @"\" + name + "_summary" + "_" + m_startDate + ".csv";           

                System.IO.File.WriteAllLines(newName, m_summarylines.ToArray());

            
        }



private void printFileAskBid()
        {
            List<string> lines = new List<string>();
            //AS 
            //order, price, P&L, Deal_cash_flow,overall_pos,flow P&L(chash flow + net pos),zero buy 
            lines.Add("date, time, Bid/ask, order, value, pos,net pos,my_buy, my_sell,cash flow ,flow P&L," + "total chashflow = " + m_totalChashflow);
            foreach (DealLine x in m_askBidList)
            {   
                
                lines.Add(string.Format(@"{0},{1},{2},{3},{4},{5},{6},{7},{8}, {9},{10}",
                x.Date, x.Time,x.DealOffer, x.OrderSize,
                x.Value, x.Pos, x.NetPos, x.Mybuy, x.Mysell, x.ChashFlow, x.PnlUntilThisPoint));
            }
            string newName;
            string name = Path.GetFileNameWithoutExtension(m_logoutputfile);
            string directory = Path.GetDirectoryName(m_logoutputfile);



            newName = directory + @"\" + name + "_flow" + "_" + m_startDate + ".csv";           


            System.IO.File.WriteAllLines(newName, lines);
        }

private void SetMyBuyAndMySell(double i_ask, double i_bid)
        {
                // m_mybuy = m_zeroBuy - (m_pos * m_dealFactor) - m_dealOrder;
               // m_mysell = m_zeroBuy - (m_pos * m_dealFactor) + m_DiffranceAskBid;
            //m_mybuy = m_zeroBuy - (m_pos * m_dealFactor) - (m_DiffranceAskBid * 0.5);
            //m_mysell = m_zeroBuy - (m_pos * m_dealFactor) + (m_DiffranceAskBid * 0.5);

            double alfha = m_pos * m_dealFactor;

            if (m_pos < 0)
            {
                m_mybuy = m_zeroBuy - alfha - m_DiffranceAskBid;
                m_mysell = m_zeroBuy - alfha + m_dealOrder;
            }

            if (m_pos == 0)
            {
                m_mybuy = m_zeroBuy - (m_dealOrder + m_DiffranceAskBid) / 2;
                m_mysell = m_zeroBuy + (m_dealOrder + m_DiffranceAskBid) / 2;
            }

            if (m_pos > 0)
            {
                m_mybuy = m_zeroBuy - alfha - m_dealOrder;
                m_mysell = m_zeroBuy - alfha + m_DiffranceAskBid;
            }

               
        }




private void readFromListOfDaysFile()
{
    string filePath = m_pathOffileToLearnFrom;

    using (StreamReader csvLearning = new StreamReader(filePath))
    {
        while (!csvLearning.EndOfStream)
        {
            string itemread = csvLearning.ReadLine();
            string date;
            setData(itemread, out date);
            m_listOfTradeDays.Add(date);
            
          
            

        }
    } // End Using


}

private void setData(string i_inputItem, out string i_date)
{
    int indexOfMark = i_inputItem.IndexOf(",", 0);
    string date = i_inputItem.Substring(0, indexOfMark);
    i_date = date.Substring(0, 4) + date.Substring(5, 2) + date.Substring(8, 2);
}

private bool isDayInHistory(string i_Day)
{
    bool isDayRecored = true;
    if (!(m_historyData.ContainsKey(i_Day)) == true)
    {
        isDayRecored = false;
    }

    if ((m_historyData.ContainsKey(i_Day)) == true)
    {
        if (m_historyData[i_Day] == " NaN")
        {
            isDayRecored = false;
        }

    }

    return isDayRecored;
}


private void UniedBidAskFile(int i_startLine)
        {
       
            string dateAsk, timeAsk, valueAsk, dateBid, timeBid, valueBid;
             int lenOfText = m_readBid.LenOfText > m_readAsk.LenOfText ? m_readAsk.LenOfText : m_readBid.LenOfText;
            List<string> lineOfNewFile = new List<string>();

            for (int i = i_startLine ; i <=  lenOfText / m_readAsk.LenOfLine ; i++)//#TODO Add the tail.......
            {
                //take the data
               bool readSuccessAsk =  m_readAsk.setLine(i, out dateAsk, out valueAsk, out timeAsk);
               bool readSuccessBid = m_readBid.setLine(i, out dateBid, out valueBid, out timeBid);

               if (readSuccessAsk == false || readSuccessBid == false)
                {
             
                    continue;
                }


                if (!isDayInHistory(dateAsk))
                {
                    continue;
                }
                if (!isDayInHistory(dateBid))
                {
                    continue;
                }
                //sort the data by date and time
                int currDateBid, currDateAsk, currTimeBid, currTimeAsk;
                DealLine firstLine, secondLine;
                firstLine = new DealLine();
                secondLine = new DealLine();
                

                try
                {
                    currDateAsk = int.Parse(dateAsk);
                    currDateBid = int.Parse(dateBid);
                    currTimeAsk = int.Parse(timeAsk);
                    currTimeBid = int.Parse(timeBid);
                }
                catch
                {
                    continue;//#TODO check the data
                }

               

                if (m_DateToEnd - 1 < currDateBid)//minus one day 
                {
                    m_isEndDateOrStopLoss = true;
                    return;
                }
            

                firstLine.Date = dateAsk;
                firstLine.Time = timeAsk;
                firstLine.Value = double.Parse(valueAsk);
                firstLine.DealOffer = MarketDeal.ASK;

                secondLine.Date = dateBid;
                secondLine.Time = timeBid;
                secondLine.Value = double.Parse(valueBid);
                secondLine.DealOffer = MarketDeal.BID;

                // date take praorety in this sort
                if (currDateBid > currDateAsk)
                {
                    DealLine temp;//swap beetwin pointers

                    temp = firstLine;
                    secondLine = firstLine;
                    firstLine = temp;
                }
                else
                {
                    if (currTimeBid > currTimeAsk)
                    {
                        DealLine temp;//swap beetwin pointers

                        temp = firstLine;
                        secondLine = firstLine;
                        firstLine = temp;
                    }

                }

                m_askBidList.Add(firstLine);
                m_askBidList.Add(secondLine);

           
               
            }

        }
    }
}
