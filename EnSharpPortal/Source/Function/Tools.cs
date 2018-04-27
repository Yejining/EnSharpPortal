using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;

namespace EnSharpPortal.Source.Function
{
    class Tools
    {
        Print print = new Print();
        GetValue getValue = new GetValue();

        public void UpArrow(int cursorLocation, int startingLine, int interval, char pointer)
        {
            print.ClearOneLetter(cursorLocation);
            if (Console.CursorTop > startingLine) Console.SetCursorPosition(cursorLocation, Console.CursorTop - interval);
            Console.Write(pointer);
        }

        public void DownArrow(int cursorLocation, int startingLine, int countOfOption, int interval, char pointer)
        {
            print.ClearOneLetter(cursorLocation);
            if (Console.CursorTop < startingLine + (interval * (countOfOption - 1))) Console.SetCursorPosition(cursorLocation, Console.CursorTop + interval);
            Console.Write(pointer);
        }
    }
}
