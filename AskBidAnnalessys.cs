using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dataanalessisGUI
{
    class AskBidAnnalessys
    {

        private double m_profitPerunit;
        private double m_minProfit;
        private double m_minlenFromCurrMaxPnl;
        private double m_maxProfit;
        private const double c_unit = 1000000;
        private List<dealsTransLog> m_trasactioLog = new List<dealsTransLog>();
        private List<DealLine> m_askBidList = new List<DealLine>();
        private double m_mybuy;
        private double m_mysell;
        private double m_pos;
        private double m_dealFactor;
        private double m_dealOrder;
        private double m_zeroBuy;
        private string m_outFileAskBid;
        private ReadFile m_readAsk;
        private ReadFile m_readbid;
        private double m_order;
        private double m_pnl;
        private double m_totalChashflow;
        private string m_logoutpotfile;
        private string m_summaryfile;//make up a uniq name for it
        private  long  m_turnover;
        private bool m_isMultifile;
        private List<string> m_nameOfFiles = new List<string>();
        private int m_howManyFile;
        private int m_currentPairOfFiles;
        private double sumOfProfits;
        private int m_numberOfDeals;
        private string m_startDate;
        private string m_EndDate;

        List<string> writeLinesl = new List<string>();




        public double ProfitPerunit 
        {
            set { m_profitPerunit = value; }
        }

        public double Order
        {
            set { m_order = value; }
        }

        public AskBidAnnalessys(string i_bidFileSrouce, string i_AskFileSrouce, string i_newFilePath,string i_outPotFile,string i_summaryFile)
        {
            m_outFileAskBid = i_newFilePath;
            m_pos = 0;
            m_logoutpotfile = i_outPotFile;
            m_summaryfile = i_summaryFile;
            m_minProfit = 10000;//we want to find the min flow of p&l
            m_turnover = 0;//no need but to make things clear its a sum of all pos in absolute value we add one for evey pos and mult it by m_order
            m_minlenFromCurrMaxPnl = 0;//we asuming that any flow p&l - maxP&l < 0
            m_howManyFile = 1;
            m_readAsk = new ReadFile(i_AskFileSrouce);
            m_readbid = new ReadFile(i_bidFileSrouce);
 
        }

        public AskBidAnnalessys(string[] i_bidFileSrouces, string[] i_AskFileSrouces, string i_outPotFile, string i_summaryFile)
        {//for mutiplay files work ,no unifile its just too big to work with

            m_isMultifile = true;
            m_pos = 0;
            m_logoutpotfile = i_outPotFile;
            m_summaryfile = i_summaryFile;
            m_minProfit = 10000;//we want to find the min flow of p&l
            m_turnover = 0;//no need but to make things clear its a sum of all pos in absolute value we add one for evey pos and mult it by m_order
            m_minlenFromCurrMaxPnl = 0;//we asuming that any flow p&l - maxP&l < 0

            if (i_AskFileSrouces.Length != i_bidFileSrouces.Length)
            {
                //throw (new Exception("must have eqwal number of files"));
            }

            //for (int i = 0; i < i_AskFileSrouces.Length ; i++ )
            //{
            //    m_nameOfFiles.Add(i_AskFileSrouces[i] + i_bidFileSrouces[i]);
            //}
            //m_howManyFile = i_AskFileSrouces.Length > i_bidFileSrouces.Length ? i_bidFileSrouces.Length : i_AskFileSrouces.Length;
            m_howManyFile = 24;
            m_readAsk = new ReadFile(i_AskFileSrouces);
            m_readbid = new ReadFile(i_bidFileSrouces);

        }


      



        private void UniedBidAskFile()
        {
       
            string dateAsk, timeAsk, valueAsk, dateBid, timeBid, valueBid;
         
            int lenOfText = m_readbid.LenOfText > m_readAsk.LenOfText ? m_readAsk.LenOfText : m_readbid.LenOfText;
            List<string> lineOfNewFile = new List<string>();

            for (int i = 0; i <= lenOfText / 27 ; i++)//#TODO Add the tail.......
            {
                //take the data
                m_readAsk.setLine(i, out dateAsk, out valueAsk, out timeAsk);
                m_readbid.setLine(i, out dateBid, out valueBid, out timeBid);
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
                    continue;
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
        
        public void RunByAskAndBid(double i_ask, double i_bid, double i_zeroPos, double i_dealOrder, double i_factor)
        {
            m_zeroBuy = i_zeroPos;
            m_dealFactor = i_factor;
            m_dealOrder = i_dealOrder;
            m_pos = 0;

            //find the file to start with
            //double firstAsk, firstBid;
            // do ---> while
            UniedBidAskFile();
            SetMyBuyAndMySell(i_ask, i_bid);
            tradByAskAndBid();
            LogfileOfDeals();
            sumOfProfits += m_askBidList[m_askBidList.Count - 1].ChashFlow + m_askBidList[m_askBidList.Count - 1].NetPos; 
            m_numberOfDeals += m_trasactioLog.Count - 1;
            m_startDate = m_askBidList[0].Date;

            //  writeLinesl.Add(string.Format("net pos,pos*value ,value, date,time, chash - net pos"));
            for (int i = 0; i < m_howManyFile - 1 ; i++ )
            {
                m_trasactioLog.Clear();
                m_askBidList.Clear();
                nextFile();
                UniedBidAskFile();
                tradByAskAndBid();
                //firstAsk = m_askBidList[0].Value;
                //firstBid = m_askBidList[1].Value;
                LogfileOfDeals();
                //if(i == m_howManyFile - 2)
                //{
                // printFileAskBid(@"c:\outTest24Month.csv");
                //}
                sumOfProfits +=  m_trasactioLog[m_trasactioLog.Count - 1].ChashFlow + m_trasactioLog[m_trasactioLog.Count - 1].NetPos;
                m_numberOfDeals += m_trasactioLog.Count - 1;
                m_currentPairOfFiles++; 
                //m_pos = 0;
              //  SetMyBuyAndMySell(i_ask: firstAsk,i_bid: firstBid);
            }

            if (m_isMultifile == false) 
            { 
                printFileAskBid(m_outFileAskBid);
            }
            m_EndDate = m_askBidList[m_askBidList.Count - 1].Date;
            summaryLogFile();
            
          //  System.IO.File.WriteAllLines(@"c:\outmin2.csv", writeLinesl);
        }
        private void nextFile()
        {
            m_readAsk.NextFile();
            m_readbid.NextFile();
        }
        private void printFileAskBid(string i_outpot)
        {
            List<string> lines = new List<string>();
            //AS 
            //order, price, P&L, Deal_cash_flow,overall_pos,flow P&L(chash flow + net pos),zero buy 
            lines.Add("date, time, Bid/ask, order, value, pos,net pos,my_buy, my_sell,cash flow ,flow P&L," + "total chashflow = " + m_totalChashflow);
            foreach (DealLine x in m_askBidList)
            {
                lines.Add(string.Format(@"{0},{1},{2},{3},{4},{5},{6},{7},{8}, {9},{10}",
                x.Date, x.Time,x.DealOffer, x.OrderSize,
                x.Value, x.Pos, x.NetPos, x.Mybuy, x.Mysell, x.ChashFlow, x.ChashFlow + x.NetPos));
            }

            System.IO.File.WriteAllLines(i_outpot, lines);
        }

        private void SetMyBuyAndMySell(double i_ask, double i_bid)
        {

            double DiffranceAskBid;
            //this.myBuy = this.zeroBuy - (this.pos * this.dealFactor) - this.dealOrder;
            m_mybuy = m_zeroBuy - (m_pos * m_dealFactor) - m_dealOrder;
            DiffranceAskBid = (m_profitPerunit / c_unit) * ((i_ask + i_bid) / 2.0f);
            m_mysell = m_zeroBuy - (m_pos * m_dealFactor) + DiffranceAskBid;


        }


        private void LogfileOfDeals()
        {
            List<string> lines = new List<string>();
            // header of the log file

            lines.Add(@"index in file, date, time, Bid/ask, order, value, pos,net pos,my_buy, my_sell,cash flow ,flow P&L," + "total chashflow = " + m_totalChashflow);


            foreach (dealsTransLog  x in m_trasactioLog)
            {
                lines.Add(string.Format(@"{0},{1},{2},{3},{4},{5},{6},{7},{8}, {9},{10},{11}",
               x.IndexInfile , x.Date, x.Time, x.DealOffer, x.OrderSize,
               x.Value, x.Pos, x.NetPos, x.Mybuy, x.Mysell, x.ChashFlow, x.ChashFlow + x.NetPos));
               
            }

            string newName;
            string name = Path.GetFileNameWithoutExtension(m_logoutpotfile);
            string directory = Path.GetDirectoryName(m_logoutpotfile);
           


            newName = directory + @"\" + name + @"outputlog"+ m_currentPairOfFiles +".csv";
            System.IO.File.WriteAllLines(newName, lines.ToArray());
        }


        private void summaryLogFile()
        {
            List<string> lines = new List<string>();
            // header of the log file
            if (m_isMultifile == false)
            {
                lines.Add(string.Format(
    @" name of file {0} 
start date {1}
end date {2} 
nuber of deals {3}
profit at end runing {4} 
min flow pnl {5}
max flow pnl {6}
turn over {7}
maxDrowDown {8}"
    , m_outFileAskBid, m_askBidList[0].Date, m_askBidList[m_askBidList.Count - 1].Date, m_trasactioLog.Count - 1,//the item in the list is not a deal is the last line in the unifile
sumOfProfits, m_minProfit, m_maxProfit, m_turnover * m_order, m_minlenFromCurrMaxPnl * (-1)));

                System.IO.File.WriteAllLines(m_summaryfile, lines.ToArray());
            }
            else 
            {

                lines.Add(string.Format(
@" name of file {0} 
start date {1}
end date {2} 
nuber of deals {3}
profit at end runing {4} 
min flow pnl {5}
max flow pnl {6}
turn over {7}
maxDrowDown {8}"
, "multifiles", m_startDate, m_EndDate, m_numberOfDeals,//the item in the list is not a deal is the last line in the unifile
sumOfProfits, m_minProfit, m_maxProfit, m_turnover * m_order, m_minlenFromCurrMaxPnl * (-1)));


                System.IO.File.WriteAllLines(m_summaryfile, lines.ToArray());

            }
        }

        

        private void tradByAskAndBid()
        {
            double netPos = 0;
            int linecounter = 0;
            bool isTrasactioHappen = false;
          
            foreach (DealLine x in m_askBidList)
            {
                x.Pos = m_pos;// save last pos
                x.Mybuy = m_mybuy;
                x.Mysell = m_mysell;
                
                if (x.DealOffer == MarketDeal.BID)
                {
                    
                    if (m_mysell < x.Value)
                    {
                        isTrasactioHappen = true;
                        // x.Pos = m_pos;//save the current pos
                        m_pos = m_pos - m_order; //
                        
                        x.OrderSize = - m_order;
                        SetMyBuyAndMySell(x.Value, x.Value);
                        x.ChashFlow = m_order * x.Value;
                    }
                }

                if (x.DealOffer == MarketDeal.ASK)
                {
                    if (m_mybuy > x.Value)
                    {
                        isTrasactioHappen = true;
                       // x.Pos = m_pos;//save the current pos
                        m_pos = m_pos + m_order;
                        
                        x.OrderSize =  m_order;
                        SetMyBuyAndMySell(x.Value, x.Value);
                        x.ChashFlow = -m_order * x.Value;       

                    }
                   
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

                m_totalChashflow = m_totalChashflow + x.ChashFlow;
                x.ChashFlow = m_totalChashflow;//the cash flow until this point
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
                //clack max DrowDown(DD)
                if (m_minlenFromCurrMaxPnl > (x.ChashFlow + x.NetPos) - m_maxProfit)
                {
                    m_minlenFromCurrMaxPnl = x.Value - m_maxProfit;
                }
            }


            dealsTransLog lastDeal = new dealsTransLog();
            lastDeal.DealLine = m_askBidList[linecounter];
            lastDeal.IndexInfile = linecounter +1 ;
            linecounter++;
            m_trasactioLog.Add(lastDeal);
        }  

    }
}
