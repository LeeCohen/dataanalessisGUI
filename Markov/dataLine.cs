using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrovchain1
{
    public class dataLineForMarkov
    {
        public DateTime Date;
        public double Value;
        public double Difference;
        public int NextDayStateForecast;
        public int ActuallNextDayState;
        public double EstimatedZeroBuy;
        public double ActuallNextDayValue;
        public int state;
        public StateProp[] givenStateProp = new StateProp[5];//choose an index in order to get/ set props for the index given state 0= no change, 1 = increase, 2= high increase 3= decrease 4= high decrease
        public StateCounter[] givenStateCounter = new StateCounter[5];//choose an index in order to get/ set counter for the index given state  0= no change, 1 = increase, 2= high increase 3= decrease 4= high decrease
        public double Std;
        public double diffDivStd;
        public double numericDiff;
        public int goodEnoughHit;
        public int excellentHit;


        public dataLineForMarkov(DateTime i_Date, double i_Value) : this()
        {
            if (i_Date == null)
                throw new ArgumentNullException(i_Date.ToString());

            Date = i_Date;
            Value = i_Value;
        }

        public dataLineForMarkov()
        {
            for (int i=0; i<5; i++)
            {
                givenStateProp[i] = new StateProp();
                givenStateCounter[i] = new StateCounter();
            }
        }


        public void updateDiffLowerThan2()
        {
            int diff = Math.Abs(stateToScale(ActuallNextDayState) - stateToScale(NextDayStateForecast));

            if (diff < 2)
            {
                goodEnoughHit = 1;
                if (diff == 0)
                {
                    excellentHit = 1;
                }
                else
                {
                    excellentHit = 0;
                }
            }
            else
            {
                excellentHit = 0;
                goodEnoughHit = 0;
            }

        }

        public static int stateToScale(int i_State)
        {
            if (i_State == 3)
                return (-1);
            else if (i_State == 4)
                return (-2);
            else
                return i_State;
        }

        public int GetExpectedNextState(int i_GivenState)// fix- must be changed
        {
            //naive algorithm: find the highest prop
            // gives 40% succsses
            double res;
            int StateToReturn;
            res = Math.Max(givenStateProp[i_GivenState].highDecreaseProp, givenStateProp[i_GivenState].DecreaseProp);
            res = Math.Max(res, givenStateProp[i_GivenState].IncreaseProp);
            res = Math.Max(res, givenStateProp[i_GivenState].HighIncreaseProp);
            res = Math.Max(res, givenStateProp[i_GivenState].NoChangeProp);

            if(res == givenStateProp[i_GivenState].NoChangeProp)
            {
                StateToReturn = MarkovChain.NO_CHANGE;
            }

            else if (res == givenStateProp[i_GivenState].highDecreaseProp)
            {
                StateToReturn = MarkovChain.HIGH_DECREASE;
            }

            else if (res == givenStateProp[i_GivenState].DecreaseProp)
            {
                StateToReturn = MarkovChain.DECREASE;
            }

            else if (res == givenStateProp[i_GivenState].IncreaseProp)
            {
                StateToReturn = MarkovChain.INCREASE;
            }

            else// if (res == givenStateProp[i_GivenState].HighIncreaseProp)
            {
                StateToReturn = MarkovChain.HIGH_INCREASE;
            }

            return StateToReturn;
        }

        //second algorithm:  give highIncrease and highDecrese factor=2, the others get factor=1
            

    }
}
