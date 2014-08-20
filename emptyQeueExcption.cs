using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    class emptyQueueExcption : masterExcption
    {
        private string m_message;

        public emptyQueueExcption(string i_message)
        {
            this.m_message = i_message;
        }

        public override string Message
        {
            get
            {
                return m_message;
            }
        }
    }
}
