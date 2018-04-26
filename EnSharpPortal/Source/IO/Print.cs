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
        /// <summary>
        /// 프로그램을 처음 실행시켰을 떄 배경화면을 출력하는 메소드입니다.
        /// </summary>
        public void PortalLogIn()
        {
            SetWindowSmallSize();
            SetBackgroundColor(ConsoleColor.Black);
            PrintSentences(Constants.HOME_SENTENCES);
        }

        /// <summary>
        /// 콘솔창의 배경색을 바꿔주는 메소드입니다.
        /// </summary>
        /// <param name="color">바꿀 콘솔 배경색</param>
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

        /// <summary>
        /// 인자로 받은 문자열 배열들을 가운데정렬하여 출력하는 메소드입니다.
        /// </summary>
        /// <param name="sentences">출력할 문자열 배열</param>
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

        public void PrintSentence(string sentence)
        {
            int leftCursor;

            Console.ForegroundColor = ConsoleColor.Gray;
            leftCursor = GetLeftCursorForCenterArrangeMent(sentence);
            Console.SetCursorPosition(leftCursor, Console.CursorTop);
            Console.WriteLine(sentence);
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
            SetWindowSmallSize();

            Console.SetCursorPosition(0, 3);
            PrintSentences(Data.Constants.ENSHARP_TITLE);
            Console.SetCursorPosition(0, 8);
            PrintSentences(Data.Constants.USER_VERSION_MENU);
        }

        public void LectureSearchMenu(int mode)
        {
            SetWindowSmallSize();

            Console.SetCursorPosition(0, 3);
            PrintSentences(Data.Constants.ENSHARP_TITLE);
            Console.SetCursorPosition(0, 8);
            PrintSentence(Data.Constants.LECTURE_SEARCH_MENU[mode]);
            PrintLectureSearchMenuAndOption(mode);
        }
        
        public void PrintLectureSearchMenuAndOption(int mode)
        {
            Console.SetCursorPosition(6, 11);
            for (int sentence = 0; sentence < Constants.SEARCHING_MENU.Length; sentence++)
            {
                if (sentence % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(Constants.SEARCHING_MENU[sentence]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(Constants.SEARCHING_MENU[sentence]);
                    Console.SetCursorPosition(6, Console.CursorTop + 1);
                }
            }
        }

        /// <summary>
        /// 콘솔창을 작은모드로 설정해주는 메소드입니다.
        /// </summary>
        public void SetWindowSmallSize()
        {
            Console.Clear();
            Console.SetWindowSize(45, 30);
            Console.SetWindowPosition(5, 5);
        }

        /// <summary>
        /// 콘솔에서 현재 커서가 위치한 줄을 비워줍니다.
        /// </summary>
        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// 사용자가 검색창에 검색어를 입력하면 안내문을 지워주는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="isNumber">사용자가 입력한 문자가 유효한지 검사</param>
        /// <param name="letter">사용자가 입력한 문자</param>
        public void DeleteGuideLine(int cursorLeft, bool isValid, ConsoleKeyInfo keyInfo)
        {
            // 한글 입력되는 경우!

            if (keyInfo.Key == ConsoleKey.Enter) return;

            Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            if (isValid) Console.Write(keyInfo.KeyChar);
        }

        /// <summary>
        /// 사용자가 유효하지 않은 문자를 입력한 경우 그 문자를 콘솔창에서 지워주는 메소드입니다.
        /// </summary>
        /// <param name="currentCursor">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        public void InvalidInput(int currentCursor, int cursorTop)
        {
            // 한글 입력되는 경우!
            Console.SetCursorPosition(currentCursor, cursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(currentCursor, cursorTop);
        }

        /// <summary>
        /// 검색창이 비었을 때 안내멘트를 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="guideline">안내멘트</param>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        public void SearchGuideline(string guideline, int cursorLeft, int cursorTop)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(guideline);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        /// <summary>
        /// 가운데 정렬로 충렬하기 위해 들여쓰기할 공간을 계산하는 메소드입니다.
        /// </summary>
        /// <param name="sentence">출력할 문장</param>
        /// <returns>가운데 정렬을 위한 들여쓰기 공백</returns>
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
