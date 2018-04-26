using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;
using EnSharpPortal.Source.Data;
using EnSharpPortal.Source.Function;

namespace EnSharpPortal.Source.Main
{
    class Home
    {
        Print print = new Print();
        GetValue getValue = new GetValue();
        LogIn logIn = new LogIn();
        ClassVO data = new ClassVO();
        DataManager dataManager = new DataManager();

        private int userNumber;

        public void RunPortal()
        {
            userNumber = Constants.NOBODY;
            print.PortalLogIn();

            //userNumber = LogIn.GetIn();

            


            print.LogInButton();
        }

        public void RunPortalWithoutLogIn()
        {
            // 데이터 로드(시간표)
            //dataManager.LoadData();
            
            print.UserVersionMenu();
            WaitUntilGetEnterKey();

            // 강의시간표 조회
            int department;
            string serialNumber;
            string lectureName;
            int grade;
            string professor;

            print.LectureSearchMenu(Data.Constants.LECTURE_SEARCH);
            department = getValue.DropBox(21, 11, Constants.SELECT_DEPARTMENT); if (department == -1) return;
            serialNumber = getValue.Information(17, 13, Constants.SERIAL_NUMBER, 6); if (string.Compare(serialNumber, "@입력취소@") == 0) return;
            lectureName = getValue.Information(17, 15, Constants.LECTURE_NAME, 10); if (string.Compare(lectureName, "@입력취소@") == 0) return;
            grade = getValue.DropBox(17, 17, Constants.SELECT_GRADE); if (grade == -1) return;
            professor = getValue.Information(17, 19, Constants.PROFESSOR, 8); if (string.Compare(professor, "@입력취소@") == 0) return;
            Console.SetCursorPosition(0, 23);
            print.PrintSentence("강의시간표 조회하기");

            
        }

        /// <summary>
        /// 엔터 키를 받을 때까지 기다리는 메소드입니다.
        /// </summary>
        public void WaitUntilGetEnterKey()
        {
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Enter) return;
            }
        }
    }
}
