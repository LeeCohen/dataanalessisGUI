using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    public class dateAndValue
    {
       private DateTime m_date;
       private double m_value;
       private double m_Precentian;

       public dateAndValue(string i_date,string i_value)
       {
           int year = int.Parse( i_date.Substring(0,4));
           int month =int.Parse( i_date.Substring(4, 2));
           int day =int.Parse (i_date.Substring(6, 2));
           m_date = new DateTime(year,month,day);
           m_value = double.Parse(i_value);
       }
       public dateAndValue(DateTime i_Date, double i_Value)
       {
           m_date = i_Date;
           m_value = i_Value;
       }
       public DateTime Date
       {
           get
           {
               return m_date;
           }
       }
       public double Value
       {
           get 
           {
               return m_value;
           }
       }

    }
}
