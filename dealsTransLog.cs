using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    class dealsTransLog
    {
        private DealLine m_dealLine = new DealLine();
        private int m_indexInfile;



        public DealLine DealLine {
            set { m_dealLine = value; } //shallow copy so set is gone for dealLine only
        }

        public int IndexInfile 
        {
            get { return m_indexInfile; }
            set { m_indexInfile = value; }
            
        }

        public double NetPos
        {
            get { return m_dealLine.NetPos; }
        }

        public double PnlUntilThisPoint
        {
            get { return m_dealLine.PnlUntilThisPoint; }
            set 
            {
                m_dealLine.PnlUntilThisPoint = value;
            }
        }

        public double ChashFlow
        {
            get { return m_dealLine.ChashFlow; }
           
        }

        public double Mybuy
        {
            get
            {
                return m_dealLine.Mybuy;
            }
         
        }


        public double Mysell
        {
            get { return m_dealLine.Mysell; }
            
        }

        public double OrderSize
        {
            get
            {
                return m_dealLine.OrderSize;
            }
            
        }

        public double Pos
        {
            get
            {
                return m_dealLine.Pos;
            }
        
        }
        public string Date
        {
            get { return m_dealLine.Date; }
            
        }
        public string Time
        {
            get { return m_dealLine.Time; }
          
        }

        public double Value
        {
            get { return m_dealLine.Value; }
           
        }
        public MarketDeal DealOffer
        {
            get { return m_dealLine.DealOffer; }
           
        }



    }
}
