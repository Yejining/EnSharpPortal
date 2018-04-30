using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;
using EnSharpPortal.Source.IO;
using EnSharpPortal.Source.Data;

namespace EnSharpPortal
{
    class Program
    {
        static void Main(string[] args)
        {
            Home home = new Home();

            //home.RunPortal();

            home.RunPortalWithoutLogIn();

            //ConsoleKeyInfo keyInfo;

            //while (true)
            //{
            //    keyInfo = Console.ReadKey();


            //    Console.Write(System.Text.ASCIIEncoding.Unicode.GetByteCount(keyInfo.KeyChar.ToString()));

            //    if (keyInfo.Key == ConsoleKey.Enter) break;
            //}



        }
    }
}
