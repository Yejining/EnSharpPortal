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

        // 강의 시간표 조회, 관심과목 담기
        public void InquireLectureSchedule(int mode, List<ClassVO> classes)
        {
            int department;
            string serialNumber;
            string lectureName;
            int grade;
            string professor;
            
            print.LectureSearchMenu(Constants.LECTURE_SEARCH);
            department = getValue.DropBox(21, 11, Constants.SELECT_DEPARTMENT); if (department == -1) return;
            serialNumber = getValue.Information(17, 13, Constants.SERIAL_NUMBER, 6); if (string.Compare(serialNumber, "@입력취소@") == 0) return;
            lectureName = getValue.Information(17, 15, Constants.LECTURE_NAME, 10); if (string.Compare(lectureName, "@입력취소@") == 0) return;
            grade = getValue.DropBox(17, 17, Constants.SELECT_GRADE); if (grade == -1) return;
            professor = getValue.Information(17, 19, Constants.PROFESSOR, 8); if (string.Compare(professor, "@입력취소@") == 0) return;
            Console.SetCursorPosition(0, 23);
            print.PrintSentence("강의시간표 조회하기");

            classes = getValue.SearchLectureByCondition(classes, department, serialNumber, lectureName, grade, professor);
            print.SearchedLectureSchedule(classes, department, serialNumber, lectureName, grade, professor);
            if (mode == Constants.PUT_LECTURE_IN_BASKET) PutLectureInBasket(classes);
        }

        public void PutLectureInBasket(List<ClassVO> classes)
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
                        print.CompletePutLectureInBasket(1, Console.CursorTop);
                    }
                    
                }
                else print.BlockCursorMove(2, "▷");
            }

            Console.SetCursorPosition(0, 30);
            foreach (ClassVO lecture in classToPutBasket) Console.WriteLine(lecture.Number);
        }
    }
}
