using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.IO
{
    class Print
    {
        public void PortalLogIn()
        {
            Console.SetWindowSize(45, 30);
            Console.SetWindowPosition(5, 5);
            SetBackgroundColor(ConsoleColor.Black);
            PrintSentences(Constants.HOME_SENTENCES);
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

        public void PrintSentences(string[] sentences)
        {
            int leftCursor;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (string sentence in sentences)
            {
                leftCursor = GetLeftCursorForCenterArrangeMent(sentence);
                Console.SetCursorPosition(leftCursor, Console.CursorTop);
                Console.WriteLine(sentence);
            }
            Console.SetCursorPosition(0, 0);
        }

        public void AskIDOrPassword(int idOrPassword)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            switch (idOrPassword)
            {
                case Constants.ID:
                    Console.SetCursorPosition(16, 10);
                    Console.Write("8자리 숫자 입력");
                    break;
                case Constants.PASSWORD:
                    Console.SetCursorPosition(16, 12);
                    Console.Write("입력");
                    break;
            }
            
        }

        public void LogInButton()
        {

        }

        public void UserVersionMenu()
        {
            Console.WriteLine("학생기초정보");
            Console.WriteLine("비밀번호 변경");
            Console.WriteLine("학적변경 신청(휴학, 복학)");
            Console.WriteLine("강의시간표 조회");
            Console.WriteLine("관심과목 담기");
            Console.WriteLine("수강신청");
            Console.WriteLine("시간표 조회");
            Console.WriteLine("포탈 이용 안내");
            Console.WriteLine("로그아웃");

            Console.WriteLine();
            Console.WriteLine("엔터 누르면 강의시간표 조회");
        }

        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public int GetLeftCursorForCenterArrangeMent(string sentence)
        {
            int leftCursor;

            if (sentence.Length == 0) return 0;

            Console.Write(sentence);
            leftCursor = Console.WindowWidth / 2 - Console.CursorLeft / 2;
            ClearCurrentConsoleLine();

            return leftCursor;
        }
    }
}
