using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dataanalessisGUI
{
    class dataAnalessis
    {

        private string m_text;
        

        private List<GrafPoint> m_graf = new List<GrafPoint>();
      
        private List<transActionLog> m_logOfTransactions = new List<transActionLog>();
        private string m_srouce;
        private string m_outpotfile;
        private string[] m_headers;
        private double m_a ;
        private  double m_b;
        private int m_numberOfmovingAverage;
        private Queue<bool> m_isprofit = new Queue<bool>();
        private string m_logoutpotfile;
        private double m_PnlOfAllDeals;//sum of all deals 
        private int m_numberOfLinesUntilExit;
        private float m_percentOfTarget;
        private int m_chashperDeal;//#TODO MAKE PARAMS TO CHANING INVESMENT
        private int m_minStantadValueToBuy;
        private int m_minStantadValueToSell;
        private int  m_mulStd;
        private int m_lookforward;
        private bool m_isUsStandardValue;
        private float m_hitRate;
        private ReadFile m_read;

        //#TODO  replace i++ to ++i if need

        //#TODO  add exptions to unset members

       public float HitRate
        {
            get { return m_hitRate; }
        }
        public double PnlOfAllDeals
        {
            get { return m_PnlOfAllDeals; }
        }

        public bool IsUsStandardValue 
        {
            set { m_isUsStandardValue = value; }
        }

        public int MulStdForBuyOrSell
        {
            set { m_mulStd = value; }
        }

        public int LookforwardForBuyOrSell
        {
            set { m_lookforward = value; }
        }

        public int MinStantadvalueOfBuy
        {
            set { m_minStantadValueToBuy = value; }
        }

        public int MinStantadvalueOfSell 
        {
            set 
            {
                m_minStantadValueToSell = value;
            }
        }


        public int NumberOfLinesUntilExit 
        {
          set { m_numberOfLinesUntilExit = value; }
        }

        public float PercentToTargetPrice
        {
            set {  m_percentOfTarget = value; }

        }
        public int ChashperDeal
        {
            set { m_chashperDeal = value; }
        }

         public double Slope 
         {
             get{return m_a;}
         }
        public  double Intercept
        {
            get
            {
                return m_b;
            }
        }

        public List<GrafPoint> Graf
        {
            get{return m_graf;}
        }


     

        public void RunByStd(int i_indexInFile,int i_numberTimeUnit, int i_timeUnit, int i_numberOfMovingAverage)
        {//#TODO make dinamic dimand to fuctions
            m_numberOfmovingAverage = i_numberOfMovingAverage;
           if(i_indexInFile <= 0)
           {
               throw new Exception("none logic index in file");
           }
            avargePerTimeunit(i_indexInFile, m_text.Length, i_numberTimeUnit,i_timeUnit ,m_text);
            //Regression();
            MovingAvareg(i_numberOfMovingAverage);
            clackStdOfSperdFromMovingAverage(i_numberOfMovingAverage);
            if (m_isUsStandardValue == true) 
            {
                setBuySellByStandartValue(m_minStantadValueToBuy, m_minStantadValueToSell);
            }
            else
            {
                setMyBuyMySellByStd(m_mulStd, m_lookforward);
            }
            openAndClosePostion(m_numberOfLinesUntilExit, m_percentOfTarget, m_chashperDeal);
            clackPnlAndProfitPerPoint();

        
        }



        public void MakeCsvHeaders(params string[] i_headers)
        {
            m_headers = i_headers;
        }

        public void WriteToCsvFile()
        {
            List<string> lines = new List<string>();
            string firstLine = null;
            foreach(string x in m_headers)
            {
                firstLine = firstLine + x + "," ; 
            }
            lines.Add(firstLine);
            int i = 0;
            foreach (GrafPoint x in m_graf)
            {
                i++;
                lines.Add(string.Format(@"{0} ,{1} ,{2} ,{3} ,{4} ,{5} ,{6} ,{7} ,{8} ,{9} ,{10} ,{11} , {12}", //#TODO put data in objecet array for dinamik changes
i, x.StartTime, x.EndTime, x.Highvalue, x.Lowvalue, 
(float)x.Avareg, (float)x.MovingAvarege ,
x.MovingAvaregDiffrence,x.StdOfLastestDiffrence, 
x.Standardized, x.MyBuy, x.MySell , x.EnumTransaction));
            }
            System.IO.File.WriteAllLines(m_outpotfile, lines.ToArray());


        }

        public void LogfileOfDeals()
        {
            List<string> lines = new List<string>();
            // header of the log file
            lines.Add("start line index ," + "end line index," + "start buy/sell," + "target price, "+ "enter price,"+
                "type of closing," + "out price," + "p&L in points," + "p&l in chash fallow," + 
               " if it profit ," + "sum of all deals ~=" + (float)m_PnlOfAllDeals);
            

         
            foreach (transActionLog x in m_logOfTransactions)
            {
                
                lines.Add(string.Format(@"{0} ,{1} ,{2} ,{3} ,{4} ,{5} ,{6} ,{7} ,{8} ,{9}",
                  x.LineOfStartTrans,x.LineOfEndTrans, x.TypeOfDeal, x.TargetPrice, x.OpenDeal ,
                  x.isPassTarget, x.CloseDeal,x.ProfitInUnits,x.ProfitPerDeal, m_isprofit.Dequeue() ? 1 : 0));
            }
            System.IO.File.WriteAllLines(m_logoutpotfile, lines.ToArray());
        }

        public dataAnalessis(string i_sourcFile,string i_outpotFile)
        {
            
            m_read = new ReadFile (i_sourcFile);
            m_outpotfile = i_outpotFile;
            string name = Path.GetFileNameWithoutExtension(m_outpotfile);
            string directory = Path.GetDirectoryName(m_outpotfile);
            m_logoutpotfile = directory +@"\"+ name + @"outputlog.csv" ;

            /*    var mlr = new MultinomialLogisticRegression(inputs: 2, categories: 2);
                LowerBoundNewtonRaphson lbnr = new LowerBoundNewtonRaphson(mlr);


                double[][] inputs = new double[100][];
                double[][] outputs = new double[100][];

                foreach(GrafPoint x in graf)
                {

                }

                double delta;
                int iteration = 0;

                do
                {
                    // Perform an iteration
                    delta = lbnr.Run(inputs, outputs);
                    iteration++;

                } while (iteration < 100 && delta > 1e-6);*/
        }



        private void setLine(int i_lineNumber, out string i_date, out string i_value, out string i_time)
        {
            m_read.setLine(i_lineNumber,out i_date,out i_value,out i_time);
        }



        private void clackPnlAndProfitPerPoint()
        {
            int countPositive = 0;

            foreach (transActionLog x in m_logOfTransactions)
            {
                m_PnlOfAllDeals += x.ProfitPerDeal;

                if(x.ProfitPerDeal >= 0)
                {
                    m_isprofit.Enqueue(true);
                    countPositive++;
                }

                if (x.ProfitPerDeal < 0)
                {
                    m_isprofit.Enqueue(false);
                }
            }
            m_hitRate = (float)countPositive / (float)m_logOfTransactions.Count;
           
        }

        private void setMyBuyMySellByStd(int i_mulStd, int i_lookforward)
        {
     
            for (int i = m_numberOfmovingAverage * 2 + 1; i < m_graf.Count ; i++ )
            {
                m_graf[i].MyBuy  = m_graf[i-1].MovingAvarege - (i_mulStd * m_graf[i - 1].StdOfLastestDiffrence);
                m_graf[i].MySell = m_graf[i-1].MovingAvarege + (i_mulStd * m_graf[i - 1].StdOfLastestDiffrence);

                if(i + i_lookforward < m_graf.Count){

                    if (m_graf[i].MyBuy > m_graf[i + i_lookforward].Lowvalue)
                    {
                        m_graf[i].EnumTransaction = Transaction.BUY; 
                    }

                    if (m_graf[i].MySell < m_graf[i + i_lookforward].Highvalue)
                    {
                        m_graf[i].EnumTransaction = Transaction.SELL;
                    }
                }

            }
        }


        private void setBuySellByStandartValue(int i_minvalueOfBuy, int i_minvalueOfSell)
        {
              
            foreach(GrafPoint x in m_graf )
            {
                if(x.Standardized > i_minvalueOfBuy)
                {
                    x.EnumTransaction = Transaction.BUY;
                }

                if(x.Standardized < -i_minvalueOfSell)
                {
                    x.EnumTransaction = Transaction.SELL;
                }
            }
        }

        private void openAndClosePostion(int i_nuberOfLines, float i_percentOfTarget,int i_chashin)
        {
            bool isPassTarget = false;
            Transaction typeOfTrans = Transaction.NONE;
            for (int i = 0; i < m_graf.Count ; i++ )
            {
               if(isOpenPostion(i,out typeOfTrans) == true)
               {
                   transActionLog newTrans = new transActionLog();
                   newTrans.OrderOfDeal = i_chashin;
                   newTrans.PrecetToTarget = i_percentOfTarget; 
                   if (typeOfTrans == Transaction.BUY)
                   {
                       if (m_isUsStandardValue == true)
                       {
                           newTrans.OpenDeal = m_graf[i].Avareg;
                       }
                       else
                       {
                           newTrans.OpenDeal = m_graf[i].MyBuy;
                       }
                       newTrans.TypeOfDeal = Transaction.BUY;
                       newTrans.TargetPrice = ((1f+i_percentOfTarget) * m_graf[i].Avareg);
                       newTrans.LineOfStartTrans = i + 1;
                       newTrans.CloseDeal = isClosePostionBuy(ref i, i_nuberOfLines, newTrans.TargetPrice, out isPassTarget);
                       newTrans.LineOfEndTrans = i + 1;
                       newTrans.isPassTarget = isPassTarget ? "exit target" : " exit time";
                   }
                   else
                   {
                               if (typeOfTrans == Transaction.SELL)
                               {
                                   if (m_isUsStandardValue == true)
                                   {
                                       newTrans.OpenDeal = m_graf[i].Avareg;
                                   }
                                   else
                                   {
                                       newTrans.OpenDeal = m_graf[i].MySell;
                                   }
                           newTrans.LineOfStartTrans = i + 1;
                           newTrans.TypeOfDeal = Transaction.SELL;
                           newTrans.TargetPrice = ((1f - i_percentOfTarget) * m_graf[i].Avareg);
                           newTrans.CloseDeal = isClosePostionSell(ref i, i_nuberOfLines, newTrans.TargetPrice, out isPassTarget);
                           newTrans.LineOfEndTrans = i + 1;
                           newTrans.isPassTarget = isPassTarget ? "exit target" : " exit time"; 
                       }
                   }
                   newTrans.sealTheDeal();
                   m_logOfTransactions.Add(newTrans);
                  
               }

            }
        }

        //#TODO MAKE IT INTO ONE FUNCTION
        private double isClosePostionBuy( ref int i_index, int i_nuberOfLines, double i_targetPrice, out bool i_isPassTarget)
        {
            int i;
            int counter = 0; 
            
            for (i = i_index; i < i_index + i_nuberOfLines &&  i < m_graf.Count - 1 ; i++)
            {
                counter++;
                if (m_graf[i].Avareg >= i_targetPrice)
                {
                    i_isPassTarget = true;
                    i_index += counter;
                    return m_graf[i].Avareg;
                }
            }
            i_index += counter;
            i_isPassTarget = false;
            return m_graf[i].Avareg;
        }


        private double isClosePostionSell(ref int i_index, int i_nuberOfLines, double i_targetPrice, out bool i_isPassTarget)
        {
            int i;
            int counter = 0;
            for (i = i_index; i < i_index + i_nuberOfLines && i < m_graf.Count - 1; i++)
            {
                counter++;
                if(m_graf[i].Avareg <= i_targetPrice )
                {
                    i_index += counter;
                    i_isPassTarget = true;
                    return m_graf[i].Avareg;
                }
            }
            i_index += counter;
            i_isPassTarget = false;
            return m_graf[i].Avareg;
        }

        private bool isOpenPostion(int i_index,out Transaction i_typeOfDeal)
        {
            bool isOpen = false;
            i_typeOfDeal = Transaction.NONE;
            if(i_index - 1 > 0)
            {
                if (m_graf[i_index - 1].EnumTransaction == Transaction.NONE && (m_graf[i_index].EnumTransaction == Transaction.BUY || m_graf[i_index].EnumTransaction == Transaction.SELL))
                {
                    i_typeOfDeal = m_graf[i_index].EnumTransaction;
                    isOpen = true;
                }
            }

            return isOpen;
 
        }
    
/*
        private void makeTransaction(int i_multiStd) //broken need rework
        {
            
            for (int i = m_numberOfmovingAverage * 2 ; i < m_graf.Count - 1; i++)
            {
                Transaction flag = Transaction.NONE;

                m_graf[i].MyBuy = m_graf[i].Avareg  -  i_multiStd * m_graf[i].StdOfLastestDiffrence;
                m_graf[i].MySell = m_graf[i].Avareg +  i_multiStd * m_graf[i].StdOfLastestDiffrence;

                if (m_graf[i].MyBuy > m_graf[i].Lowvalue)
                {
                    flag = Transaction.BUY;
                }

                if (m_graf[i].MySell < m_graf[i].Highvalue)
                {
                    if (flag == Transaction.BUY)
                    {
                        flag = Transaction.BUYANDSELL;
                    }
                    else
                    {
                        flag = Transaction.SELL;
                    }
                }

                m_graf[i].EnumTransaction = flag;
                
            }
        }*/

     /*   private void BackwordStd(int i_numberOfstdLookBack)
        {
            List<double> sumOfStd = new List<double>();
            double sum = 0;
            // make the first sum std , m_numberOfmovingAverage * 2 is the line that std start
            for (int i = m_numberOfmovingAverage * 2; i < m_numberOfmovingAverage * 2 + i_numberOfstdLookBack; i++)
            {
                sum += m_graf[i].StdOfLastestDiffrence;
            }

            sumOfStd.Add(sum);

            for (int i = m_numberOfmovingAverage * 2 + i_numberOfstdLookBack ; i < m_graf.Count; i++)
            {
                sum = (sum - m_graf[i - i_numberOfstdLookBack].StdOfLastestDiffrence) + m_graf[i].StdOfLastestDiffrence;
                sumOfStd.Add(sum);
            }

            int counter = 0;
            for (int i = m_numberOfmovingAverage * 2 + i_numberOfstdLookBack  ; i < m_graf.Count - 1; i++)
            {
                Transaction flag = Transaction.NONE;

                m_graf[i].MyBuy = m_graf[i].MovingAvarege - sumOfStd[counter];
                m_graf[i].MySell = m_graf[i].MovingAvarege + sumOfStd[counter];

                if (m_graf[i].MyBuy > m_graf[i].Lowvalue)
                {
                    flag = Transaction.BUY;
                }

                if(m_graf[i].MySell > m_graf[i].Highvalue)
                {
                    if (flag == Transaction.BUY)
                    {
                        flag = Transaction.BUYANDSELL;
                    }
                    else {
                        flag = Transaction.SELL;
                    }
                }

                m_graf[i].EnumTransaction = flag;
                counter++;
            }
        }*/

        private void MovingAvareg(int i_numberOfMoving)
        {
            double sum = 0;
            double avarege = 0;
            //make the first avarge
            for (int i = 0; i < i_numberOfMoving && i < m_graf.Count; i++)
            {
                sum += m_graf[i].Avareg;
            }

            avarege = sum / i_numberOfMoving;
            m_graf[i_numberOfMoving].MovingAvarege = avarege;
            //make the rest of the avarege

            for (int i = i_numberOfMoving + 1; i < m_graf.Count; i++)
            {
                avarege = (avarege - ((m_graf[i - i_numberOfMoving].Avareg) / i_numberOfMoving)) + (m_graf[i].Avareg / i_numberOfMoving);
                m_graf[i].MovingAvarege = avarege;

            }
        }

        private double StandardScoreOfDiffMovingAverage(double i_std, double i_average, double i_rawValue)
        {
            double StandardScore = 0;

            StandardScore = (i_rawValue - i_average) / i_std;

            return StandardScore;
        }


        private bool clackStdOfSperdFromMovingAverage(int i_numberOfMoving)//may be add index
        {


            /* if(i_index < i_numberOfMoving * 2)//senety chack
             {
                 return false;
             }*/
            /*double average = someDoubles.Average();
        double sumOfSquaresOfDifferences = someDoubles.Select(val => (val - average) * (val - average)).Sum();
        double sd = Math.Sqrt(sumOfSquaresOfDifferences / someDoubles.Length);*/

            double average;
            Queue<double> listOfDiffMovingAvarege = new Queue<double>();

            for (int i = i_numberOfMoving; i < i_numberOfMoving * 2 - 1; i++)
            {
                listOfDiffMovingAvarege.Enqueue(m_graf[i].MovingAvaregDiffrence);
            }

            for (int i = i_numberOfMoving * 2; i < m_graf.Count; i++)
            {
                listOfDiffMovingAvarege.Enqueue(m_graf[i].MovingAvaregDiffrence);
                average = listOfDiffMovingAvarege.Average();//#TODO improve effisensy
                double sumOfSquaresOfDifferences = listOfDiffMovingAvarege.Select(val => (val - average) * (val - average)).Sum();
                double sd = Math.Sqrt(sumOfSquaresOfDifferences / listOfDiffMovingAvarege.Count);
                m_graf[i].StdOfLastestDiffrence = sd;
                m_graf[i].Standardized = StandardScoreOfDiffMovingAverage(sd,m_graf[i].MovingAvarege,m_graf[i].Avareg);
                listOfDiffMovingAvarege.Dequeue();
            }

            return true;
        }



        private void avargePerTimeunit(int i_lineTostart, int i_lenOftext, int i_numOTimeUnit, int i_timeUnit ,string i_srouce)
        {
            int counter = 0;
             string date = null;
             string value = null; 
             string time = null;

            int i, previus_time, current_time;
            int currentday;
            setLine(i_lineTostart, out date, out value, out time);//#TODO sub this to get time only

                // #TODO THROW EX AND FIX THE DATA
                currentday = int.Parse(date);
                previus_time = int.Parse(time);
            
           

            ArrayList arrayOfValues = new ArrayList();
            List<int> times = new List<int>();
            times.Add(previus_time);

            for (i = i_lineTostart; i < i_lenOftext / 27; i++)
            {

                setLine(i, out date, out value, out time);

                current_time = int.Parse(time);
                arrayOfValues.Add(double.Parse(value));
                times.Add(current_time);

                if (int.Parse(date) != currentday)
                {
                    previus_time = 0;
                    currentday = int.Parse(date);
                }

                if (current_time > previus_time + i_timeUnit)
                {
                    
                    updateAvaregPerMin(arrayOfValues, current_time - previus_time, times.Max(), times.Min());
                    // Console.Write("correnct = " + current + " pre =" + pre + "time " + i_time + "\n");
                    previus_time = current_time;
                    arrayOfValues.Clear();
                    counter++;
                    times.Clear();
                }

                if (counter == i_numOTimeUnit)
                {
                    return;
                }
            }

        }

        private void updateAvaregPerMin(ArrayList i_arrayOfValues, int i_roundTime, int i_maxTime, int i_minTIme)
        {
            double sum = 0;
            int counter = 0;
            double maxvlaue = 0;
            double minvlaue = (double)i_arrayOfValues[0];
            foreach (double i in i_arrayOfValues)
            {
                counter++;
                if (i > maxvlaue)
                {
                    maxvlaue = i;
                }
                if (minvlaue > i)
                {
                    minvlaue = i;
                }
                sum = sum + i;
            }
            GrafPoint newPoint = new GrafPoint();
            newPoint.RuondTime = i_roundTime;
            newPoint.Highvalue = maxvlaue;
            newPoint.Lowvalue = minvlaue;
            newPoint.Avareg = sum / counter;
            newPoint.StartTime = i_minTIme;
            newPoint.EndTime = i_maxTime;
            m_graf.Add(newPoint);
        }


        private void Regression()
        {


            double xAvg = 0;
            double yAvg = 0;
            int index = 1;

            foreach (GrafPoint x in m_graf)
            {
                index++;
                yAvg += x.Avareg;
                xAvg += index;

            }


            xAvg = xAvg / index;//it the same count any way
            yAvg = yAvg / index;

            double v1 = 0;
            double v2 = 0;

            int counter = 1;
            foreach (GrafPoint x in m_graf)
            {
                v1 += (xAvg - counter) * (yAvg - x.Avareg);
                v2 += Math.Pow(yAvg - x.Avareg, 2);
                counter++;
            }


            m_a = v1 / v2;
            m_b = yAvg - m_a * xAvg;

            //Console.WriteLine("y = ax + b");
            //Console.WriteLine("a = {0}, the slope of the trend line.", Math.Round(a, 2));
            //Console.WriteLine("b = {0}, the intercept of the trend line.", Math.Round(b, 2));

            
        }
    }
}
