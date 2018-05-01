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
        LecturePlanManage lecturePlanManage = new LecturePlanManage();
        Tools tools = new Tools();

        private int userNumber;
        private List<ClassVO> lectureSchedule = new List<ClassVO>();
        private List<ClassVO> basket = new List<ClassVO>();
        private List<ClassVO> enrolledLecture = new List<ClassVO>();

        public void RunPortal()
        {
            userNumber = Constants.NOBODY;
            print.PortalLogIn();

            //userNumber = LogIn.GetIn();
            
            print.LogInButton();
        }

        /// <summary>
        /// 로그인하지 않고 바로 포탈 프로그램을 실행하는 메소드입니다.
        /// </summary>
        public void RunPortalWithoutLogIn()
        {
            bool isFirstLoop = true;

            // 데이터 로드(시간표)
            dataManager.LoadData();
            lectureSchedule= dataManager.Classes;
            
            while (true)
            {
                if(isFirstLoop)
                {
                    // 메뉴 출력
                    print.UserVersionMenu();

                    // 기능 선택
                    Console.SetCursorPosition(5, 8);
                    Console.Write('▷');

                    isFirstLoop = false;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(5, 8, 2, '▷');                     // 위로 커서 이동        
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(5, 8, 10, 2, '▷');        // 아래로 커서 이동
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(5, "▷"); break; }    // 나가기
                else if (keyInfo.Key == ConsoleKey.Enter)                                                // 기능 선택
                { GoNextFunction((Console.CursorTop / 2) - 4); isFirstLoop = true; }
                else print.BlockCursorMove(5, "▷");                                                     // 입력 무시
            }
        }

        /// <summary>
        /// 다음 기능을 선택하는 메소드입니다.
        /// </summary>
        /// <param name="cursorTop">커서 위치</param>
        public void GoNextFunction(int cursorTop)
        {
            switch (cursorTop)
            {
                case Constants.STUDENT_INFORMATION:
                    return;
                case Constants.CHANGE_PASSWORD:
                    return;
                case Constants.APPLICATION_FOR_CHANGING_REGISTER:
                    return;
                case Constants.INQUIRE_LECTURE_SCHEDULE:
                    lecturePlanManage.InquireLectureSchedule(Constants.LECTURE_SEARCH, lectureSchedule, basket);
                    return;
                case Constants.PUT_INTO_BASKET:
                    basket = lecturePlanManage.InquireLectureSchedule(Constants.PUT_LECTURE_IN_BASKET, lectureSchedule, basket);
                    return;
                case Constants.MANAGE_BASKET:
                    basket = lecturePlanManage.ManageSelectedLecture(Constants.MANAGE_BASKET, basket);
                    return;
                case Constants.REGISTER_LECTURE:
                    enrolledLecture = lecturePlanManage.SignUpLecture(lectureSchedule, basket, enrolledLecture);
                    return;
                case Constants.CHECK_MY_SCHEDULE:
                    enrolledLecture = lecturePlanManage.ManageEnrolledLecture(enrolledLecture);
                    return;
                case Constants.INFORMATION_ABOUT_PORTAL:
                    return;
                case Constants.LOG_OUT:
                    return;
            }
        }
    }
}
