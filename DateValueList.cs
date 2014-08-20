using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{

    public class DateValueList
    {
        public class DateValue
        {
            public double m_Open;
            public DateTime m_date;
            public double m_High;
            public double m_Low;
            public double m_Diff;
        }

        public List<DateValue> m_DateValueList = new List<DateValue>();
    }
}
