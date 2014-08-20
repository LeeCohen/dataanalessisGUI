using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace markovThatRecieveData
{
    class uninitialedMarkovChainException : Exception
    {
        public uninitialedMarkovChainException(string i_Msg) : base(i_Msg)
        {
        }
    }
}
