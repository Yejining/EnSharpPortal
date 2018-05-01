using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;

namespace EnSharpPortal
{
    class Program
    {
        /// <summary>
        /// 포탈 프로그램을 시작합니다.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Home home = new Home();

            //home.RunPortal();

            home.RunPortalWithoutLogIn();
        }
    }
}
