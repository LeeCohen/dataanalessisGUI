using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrovchain1
{
    public class StateCounter
    {
        public double NoChangeCount = 0.0;
        public double IncreaseCount = 0.0;
        public double HighIncreaseCount = 0.0;
        public double DecreaseCount = 0.0;
        public double highDecreaseCount = 0.0;

        public double GetSum ()
        {
            return NoChangeCount + IncreaseCount + HighIncreaseCount + DecreaseCount + highDecreaseCount;
        }

    }
}
