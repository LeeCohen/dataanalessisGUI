using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
     interface ImotherDataLearner
    {

        bool Start();
        //string FilePath
        //{
            
        //    set;
        //}

        //int Daysforword
        //{
            
        //    set;
        //}


        //double Diffrence
        //{
        //    get;
     
        //}

        //int CurrentDate
        //{
        //    get;
            
        //}

        double ZeroBuy
        {
            get;
            
        }
        double Std
        {
            get;
            set;
        }

        //int IndexEnded
        //{
        //    get;
            
        //}

        double lastPoint
        {
            get;

        }

        //double lastValue(DateTime i_CurrDate);

        //double Slope
        //{
        //    get;
        //}
        //double Intercept
        //{
        //    get;
        //}

        List<dateAndValue> DateAndValuesFromCalc
        {
            get;

        }

    }
}
