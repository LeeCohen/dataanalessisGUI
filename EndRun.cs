using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataanalessisGUI
{
    class EndRun : EventArgs
    {
        private bool m_isRunEnd;

        public bool IsRunEnd 
        {
            get { return m_isRunEnd; }
            set { m_isRunEnd = value; }
        }
    }
}
