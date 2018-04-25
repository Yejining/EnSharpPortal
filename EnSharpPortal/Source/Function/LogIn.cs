using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;
using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.Function
{
    class LogIn
    {
        Print print = new Print();

        public int GetIn()
        {
            

            print.AskIDOrPassword(Constants.ID);
            print.AskIDOrPassword(Constants.PASSWORD);

            return 1;
        }
    }
}
