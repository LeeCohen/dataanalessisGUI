using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataanalessisGUI
{
   public abstract class absReggLearner : ImotherDataLearner// not actually working, I only filled it by copy the memebers from datalearner and filling the methods
    {

     public abstract  bool Start();

     public abstract string FilePath
     {

         set;
     }

     public abstract int Daysforword
        {
            get;
            set;
        }



     public abstract double Diffrence
        {
            get;

        }

     public abstract int CurrentDate
        {
            get;

        }

     public abstract double ZeroBuy
        {
            get;

        }
     public abstract double Std
        {
            get;
            set;
        }

     public abstract int IndexEnded
     {
         get;

     }

     public abstract double lastPoint
     {
         get;

     }



     public abstract double Slope
     {
         get;
     }
     public double Intercept
     {
         get;
         set;
     }

     public abstract List<dateAndValue> DateAndValuesFromCalc
     {
         get;

     }


    }
}
