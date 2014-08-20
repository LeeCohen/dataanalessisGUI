using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{

   public enum Transaction
    {
      NONE,
      SELL,
      BUYANDSELL,
      BUY
    };

 
    class GrafPoint
    {
       private double m_highValue;
       private double m_lowValue;
       private double m_avareg;
       private int m_roundTime;
       private int m_startTime;
       private int m_endTime;
       private double m_movingAvareg;
       private double m_movingAvaregDiffrence;
       private int m_numberOfMoving;
       private double m_std;
       private double m_Standardized;
       private double m_buy;
       private double m_sell;
       private Transaction e_trans;
  

       public double MyBuy
       {
           get { return m_buy; }
           set { m_buy = value; }

       }
       public double MySell
       {
           get { return m_sell; }
           set { m_sell = value; }
       }

       public Transaction EnumTransaction 
       {
           get { return e_trans; }
           set 
           {   
               e_trans = value;
           }
       }

       public double Standardized
       {
           get { return m_Standardized; }
           set { m_Standardized = value; }
       }
       public double StdOfLastestDiffrence
       {
           set { m_std = value; }
           get { return m_std;}
       }
       public double MovingAvaregDiffrence
       {
           
           get { return m_movingAvaregDiffrence; }
       }

       public double MovingAvarege
       {
           set { m_movingAvareg = value;
           m_movingAvaregDiffrence = m_movingAvareg - m_avareg;}
           get { return m_movingAvareg; }
       }

       public int NumberOfMoving
       {
           set { m_numberOfMoving = value; }
           get { return m_numberOfMoving; }
       }

       public int StartTime
       {
           set { m_startTime = value; }
           get { return m_startTime; }
       }


       public int EndTime
       {
           set { m_endTime = value; }
           get { return m_endTime; }
       }

     public   double Highvalue
        {
            get { return m_highValue; }
            set {m_highValue = value; }
        }

     public double Avareg
     {
         get { return m_avareg; }
         set { m_avareg = value; }
     }
     public double Lowvalue
        {
            get { return m_lowValue; }
            set { m_lowValue = value; }
        }

     public int RuondTime
        {
            get { return m_roundTime; }
            set { m_roundTime = value; }
        }

    }
}
