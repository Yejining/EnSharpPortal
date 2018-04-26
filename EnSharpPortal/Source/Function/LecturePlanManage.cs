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
            foreach (ClassVO lecture in classes)
                Console.WriteLine(lecture.ToString());
        }
    }
}
