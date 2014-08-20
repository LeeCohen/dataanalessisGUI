using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    public enum MarketDeal
    {
        NONE,
        BID,
        ASK
    };

  public  class DealLine
    {
        private string m_date;
        private string m_time;
        private double m_value;
        private double m_currentPos;
        private double m_dealOrder;
        private double m_pnlUntilThisPoint;
        private double m_mybuy;
        private double m_mysell;
        private double m_netPos;
        private MarketDeal e_deal;
        private double m_chashFlow;

        public double NetPos
        {
            get { return m_netPos; }
            set { m_netPos = value; }
        }

        public double PnlUntilThisPoint 
        {
            get { return m_pnlUntilThisPoint; }
            set { m_pnlUntilThisPoint = value; }
        }

        public double ChashFlow
        {
            get { return m_chashFlow; }
            set { m_chashFlow = value; }
        }

        public double Mybuy
        {
            get
            {
                return m_mybuy;
            }
            set 
            {
                m_mybuy = value;
            }
        }


        public double Mysell 
        {
            get { return m_mysell; }
            set { m_mysell = value; }
        }

        public double OrderSize 
        {
            get 
            {
                return m_dealOrder;
            }
            set { m_dealOrder = value; }
        }

        public double Pos 
        {
            get
            {
                return m_currentPos;
            }
            set 
            {
                m_currentPos = value;
            }
        }
        public string Date
        {
            get { return m_date; }
            set { m_date = value; }
        }
        public string Time 
        {
            get { return m_time; }
            set { m_time = value; }
        }

        public double Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
        public MarketDeal DealOffer
        {
            get { return e_deal; }
            set { e_deal = value; }
        }




    }
}
