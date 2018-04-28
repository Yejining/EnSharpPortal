using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;
using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.Function
{
    class LecturePlanManage
    {
        Print print = new Print();
        GetValue getValue = new GetValue();
        Tools tools = new Tools();
        
        /// <summary>
        /// 강의 시간표를 조회하는 메소드입니다.
        /// 강의 시간표 조회, 관심과목 담기 기능에서 사용됩니다.
        /// </summary>
        /// <param name="mode">기능</param>
        /// <param name="classes">강의 시간표</param>
        public List<ClassVO> InquireLectureSchedule(int mode, List<ClassVO> classes)
        {
            int department;
            string serialNumber;
            string lectureName;
            int grade;
            string professor;
            
            print.LectureSearchMenu(Constants.LECTURE_SEARCH);
            department = getValue.DropBox(21, 11, Constants.SELECT_DEPARTMENT); if (department == -1) return null;
            serialNumber = getValue.Information(17, 13, Constants.SERIAL_NUMBER, 6); if (string.Compare(serialNumber, "@입력취소@") == 0) return null;
            lectureName = getValue.Information(17, 15, Constants.LECTURE_NAME, 10); if (string.Compare(lectureName, "@입력취소@") == 0) return null;
            grade = getValue.DropBox(17, 17, Constants.SELECT_GRADE); if (grade == -1) return null;
            professor = getValue.Information(17, 19, Constants.PROFESSOR, 8); if (string.Compare(professor, "@입력취소@") == 0) return null;
            Console.SetCursorPosition(0, 23);
            print.PrintSentence("강의시간표 조회하기");

            classes = getValue.SearchLectureByCondition(classes, department, serialNumber, lectureName, grade, professor);
            print.SearchedLectureSchedule(classes, department, serialNumber, lectureName, grade, professor);
            if (mode == Constants.PUT_LECTURE_IN_BASKET) return PutLectureInBasket(classes);
            return null;
        }

        /// <summary>
        /// 관심과목을 담는 메소드입니다.
        /// </summary>
        /// <param name="classes">강의 시간표</param>
        public List<ClassVO> PutLectureInBasket(List<ClassVO> classes)
        {
            List<ClassVO> classToPutBasket = new List<ClassVO>();

            Console.SetCursorPosition(2, 12);
            Console.Write('▷');

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(2, 12, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(2, 12, classes.Count, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(2, "▷"); break; }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (getValue.IsValidLecture(classes[Console.CursorTop - 12], classToPutBasket))
                    {
                        classToPutBasket.Add(classes[Console.CursorTop - 12]);
                        print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.PUT);
                    }
                    else print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.FAIL);
                }
                else print.BlockCursorMove(2, "▷");
            }

            return classToPutBasket;
        }

        public List<ClassVO> LookAroundBasket(List<ClassVO> basket)
        {
            ConsoleKeyInfo keyInfo;
            bool isFirstLoop = true;

            Console.SetWindowSize(160, 35);
            Console.Clear();

            print.LectureInBasket(basket);

            while (true)
            {
                if (isFirstLoop)
                {
                    Console.SetCursorPosition(2, 9);
                    Console.Write('▷');
                    isFirstLoop = false;
                }

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(2, 9, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(2, 9, basket.Count, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(2, "▷"); break; }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.DELETE);
                    for (int count = 0; count < basket.Count; count++) { Console.SetCursorPosition(0, 9 + count); print.ClearCurrentConsoleLine(); }
                    basket.RemoveAt(Console.CursorTop - 10);
                    print.Lectures(basket, 9);
                    isFirstLoop = true;
                }
                else print.BlockCursorMove(2, "▷");
            }

            return basket;
        }
    }
}
