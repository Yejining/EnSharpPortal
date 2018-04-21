using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using EnSharpPortal.Data;

namespace EnSharpPortal.Source.IO
{
    class Print
    {
        public void PortalLogIn()
        {
            Console.SetWindowSize(45, 30);
            SetBackgroundColor(ConsoleColor.Gray);
        }

        public void SetBackgroundColor(ConsoleColor color)
        {
            for (int height = 0; height < Console.WindowHeight; height++)
            {
                Console.BackgroundColor = color;
                Console.Write(new string(' ', Console.WindowWidth));
                if (height != Console.WindowHeight - 1) Console.SetCursorPosition(0, Console.CursorTop + 1);
                else Console.SetCursorPosition(0, 0);
            }
        }
    }
}
