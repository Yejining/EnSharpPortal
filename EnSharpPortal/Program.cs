using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;
using EnSharpPortal.Source.IO;

namespace EnSharpPortal
{
    class Program
    {
        static void Main(string[] args)
        {
            Home home = new Home();

            //home.RunPortal();
            
            home.RunPortalWithoutLogIn();
        }
    }
}
