using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;
using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.Function
{
    class Tools
    {
        Print print = new Print();

        /// <summary>
        /// 방향키를 이용하는 기능을 수행할 경우, 위 방향키를 누를 때 호출되는 메소드입니다.
        /// </summary>
        /// <param name="cursorLocation">들여쓰기를 위한 커서 정보</param>
        /// <param name="startingLine">첫 선택지가 위치한 줄</param>
        /// <param name="interval">줄간격</param>
        /// <param name="pointer">포인터</param>
        public void UpArrow(int cursorLocation, int startingLine, int countOfOption, int interval, char pointer)
        {
            print.ClearOneLetter(cursorLocation);
            if (Console.CursorTop > startingLine) Console.SetCursorPosition(cursorLocation, Console.CursorTop - interval);
            else if (Console.CursorTop == startingLine) Console.SetCursorPosition(cursorLocation, startingLine + (interval * (countOfOption - 1)));
            Console.Write(pointer);
        }

        /// <summary>
        /// 방향키를 이용하는 기능을 수행할 경우, 아래 방향키를 누를 때 호출되는 메소드입니다.
        /// </summary>
        /// <param name="cursorLocation">들여쓰기를 위한 커서 정보</param>
        /// <param name="startingLine">첫 선택지가 위치한 줄</param>
        /// <param name="countOfOption">선택지 개수</param>
        /// <param name="interval">줄간격</param>
        /// <param name="pointer">포인터</param>
        public void DownArrow(int cursorLocation, int startingLine, int countOfOption, int interval, char pointer)
        {
            print.ClearOneLetter(cursorLocation);
            if (Console.CursorTop < startingLine + (interval * (countOfOption - 1))) Console.SetCursorPosition(cursorLocation, Console.CursorTop + interval);
            else if (Console.CursorTop == startingLine + (interval * (countOfOption - 1))) Console.SetCursorPosition(cursorLocation, startingLine);
            Console.Write(pointer);
        }

        /// <summary>
        /// ESC키를 받을 때까지 키를 입력받는 메소드입니다.
        /// </summary>
        public void WaitUntilGetEscapeKey()
        {
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape) return;
                else if (Console.CursorLeft == 0) print.BlockCursorMove(Console.CursorLeft, " ");
                else print.BlockCursorMove(Console.CursorLeft - 1, " ");
            }
        }

        /// <summary>
        /// 엔터 또는 ESC키를 받을 때까지 키를 입력받는 메소드입니다.
        /// </summary>
        public void WaitUntilGetEnterOrEscapeKey()
        {
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape) return;
                else if (keyInfo.Key == ConsoleKey.Enter) return;
                else if (Console.CursorLeft == 0) print.BlockCursorMove(Console.CursorLeft, " ");
                else print.BlockCursorMove(Console.CursorLeft - 1, " ");
            }
        }

        /// <summary>
        /// 사용자로부터 엔터 혹은 탭키를 받을 때까지 기다리는 메소드입니다.
        /// </summary>
        /// <returns></returns>
        public int EnterOrTab()
        {
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Enter) return Constants.ENTER;
                else if (keyInfo.Key == ConsoleKey.Tab) return Constants.TAB;
                else if (keyInfo.Key == ConsoleKey.Escape) return Constants.ESCAPE;
            }
        }

        /// <summary>
        /// 프로그램의 종료 여부를 알려주는 메소드입니다.
        /// </summary>
        /// <param name="cursorTop">현재 커서가 위치한 줄 정보</param>
        /// <returns>프로그램 종료 여부</returns>
        public bool IsEnd(int cursorTop)
        {
            if (((cursorTop / 2) - 5) == Constants.CLOSE_PROGRAM) return true;
            else return false;
        }

        /// <summary>
        /// 입력받은 키가 유호한지 검사하는 메소드입니다.
        /// </summary>
        /// <param name="keyInfo">입력받은 키</param>
        /// <param name="mode">검색 모드</param>
        /// <returns>입력받은 키의 유효성</returns>
        public bool IsValid(ConsoleKeyInfo keyInfo, int mode)
        {
            // 엔터, 탭
            if (keyInfo.Key == ConsoleKey.Enter) return false;
            if (keyInfo.Key == ConsoleKey.Tab) return false;

            // 숫자
            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.NUMBER_PATTERN))
            {
                if (mode == Constants.PROFESSOR) return false;
                else return true;
            }
            if (mode == Constants.SERIAL_NUMBER) return false;

            // 한글, 영어
            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.ENGLISH_PATTERN)) return true;
            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.KOREAN_PATTERN)) return true;
            if (mode == Constants.PROFESSOR) return false;

            // 특수기호
            if (mode == Constants.LECTURE_NAME && (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.SPECIAL_LETTER))) return true;
            if (mode == Constants.LECTURE_NAME) return false;
            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.INVALID_SPECIAL_LETTER)) return false;
            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.VALID_SPECIAL_LETTER)) return true;
            if (keyInfo.Key == ConsoleKey.Spacebar) return true;

            return false;
        }

        /// <summary>
        /// 관심과목 담기 혹은 수강신청시 신청하고자 하는 과목이 유효한지의 여부를 검사해주는 메소드입니다.
        /// </summary>
        /// <param name="lecture">검사하고 싶은 수업</param>
        /// <param name="classes">신청한 수업들</param>
        /// <returns>신청 과목 유효 여부</returns>
        public bool IsValidLecture(ClassVO lecture, List<ClassVO> classes, int mode)
        {
            float sumOfCredit = 0;

            if (classes.Count == 0) return true;

            // 학수번호가 일치하는 경우, 강의시간이 겹치는 경우
            foreach (ClassVO selectedClass in classes)
            {
                sumOfCredit += selectedClass.Credit;
                if (lecture.SerialNumber == selectedClass.SerialNumber) return false;
                if (IsOverLapClass(lecture, selectedClass)) return false;
            }

            // 학점이 초과되는 경우
            if (mode != Constants.SIGN_UP_CLASS && sumOfCredit > 24) return false;
            if (mode == Constants.SIGN_UP_CLASS && sumOfCredit > 21) return false;

            return true;
        }

        /// <summary>
        /// 강의1과 강의2의 시간이 일치하는지 여부를 검사합니다.
        /// </summary>
        /// <param name="class1">강의1</param>
        /// <param name="class2">강의2</param>
        /// <returns>시간 겹침 여부</returns>
        public bool IsOverLapClass(ClassVO class1, ClassVO class2)
        {
            for (int row = 0; row < class1.TimeOfClass.GetLength(0); row++)
                for (int column = 0; column < class1.TimeOfClass.GetLength(1); column++)
                    if (class1.TimeOfClass[row, column] == class2.TimeOfClass[row, column] && class1.TimeOfClass[row, column] == true)
                        return true;
            return false;
        }

        /// <summary>
        /// 입력받은 키가 한국어인지 검사하는 메소드입니다.
        /// </summary>
        /// <param name="keyInfo">입력받은 키</param>
        /// <returns>한국어 여부</returns>
        public bool IsKoreanKey(ConsoleKeyInfo keyInfo)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.KOREAN_PATTERN))
                return true;
            else return false;
        }
    }
}
