using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;

namespace EnSharpPortal.Source.Main
{
    class Home
    {
        Print print = new Print();

        public void RunPortal()
        {
            print.PortalLogIn();
        }
    }
}
