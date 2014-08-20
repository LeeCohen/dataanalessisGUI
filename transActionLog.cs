using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    class transActionLog//#TODO ADD BOOL THAT SAY IF ALL THE VALUES R GOOD
    {
        private Transaction m_typeOfDeal;//type of transaction(buy or sell)
        private double m_openDeal;//the price that we get in postion
        private double m_closeDeal;//the we end the postion 
        private double m_targetPrice;
        private float m_precetToTarget;
        private int m_timeToClosePostions;
        private double m_profitInUnits;//the diffrecnt bitween open and close price
        private int m_orderOfDeal;
        private int m_lineOfStartTrans;
        private int m_lineOfEndTrans;
        private double m_profitPerDeal;//m_profitInUnits(diff open close) * m_orderOfDeal (the size of the deal )  
        private string m_resonOfClose;
        
        
        public void sealTheDeal()//#TODO THROW EXP WHEN NEEDED
        {
            if(m_typeOfDeal == Transaction.BUY)
            {
                m_profitInUnits = m_closeDeal - m_openDeal;
            }

            if (m_typeOfDeal == Transaction.SELL)
            {
                m_profitInUnits = m_openDeal - m_closeDeal ;

            }

            
            
            m_profitPerDeal = m_profitInUnits * m_orderOfDeal;
        }


        public int LineOfEndTrans
        {
            get { return m_lineOfEndTrans; }
            set { m_lineOfEndTrans = value; }
        }

        public double ProfitPerDeal
        {
            get { return m_profitPerDeal; }
        }

        public double ProfitInUnits
        {
            get { return m_profitInUnits; }

        }


      

        public int OrderOfDeal
        {
            get { return m_orderOfDeal; }
            set { m_orderOfDeal = value; }
        }

     

        public float PrecetToTarget 
        {
            get 
            {
                return m_precetToTarget;
            }
            set
            {
                m_precetToTarget = value;
            }

        }

        public int TimeToClosePostions
        {
            get { return m_timeToClosePostions; }
            set { m_timeToClosePostions = value; }
        }

        public double CloseDeal
        {
            set
            {
                m_closeDeal = value;
            }
            get
            {
                return m_closeDeal;
            }
        }
        
        
        public string isPassTarget
        {
            get
            {
                return  m_resonOfClose;
            }
            set
            {
                m_resonOfClose = value;
            }
        }


        public double OpenDeal
        {
            get 
            {
                return m_openDeal;
            }
            set
            {
                m_openDeal = value;
            }
        }


        public int LineOfStartTrans
        {
            get 
            {
                return m_lineOfStartTrans;
            }
            set 
            {
                 m_lineOfStartTrans = value;
            }
        }

        public Transaction TypeOfDeal
        {
            set
            {
                m_typeOfDeal = value;
            }
            get
            {
                return m_typeOfDeal;
            }
        }
  

        public double TargetPrice
        {
            set
            {
                m_targetPrice= value;
            }

            get 
            {
                return m_targetPrice;
            }
        }

    }
}
