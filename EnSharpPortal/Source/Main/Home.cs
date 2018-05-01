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
        
        private List<ClassVO> lectureSchedule = new List<ClassVO>();
        private List<ClassVO> basket = new List<ClassVO>();
        private List<ClassVO> enrolledLecture = new List<ClassVO>();

        /// <summary>
        /// 포탈 프로그램을 실행하는 메소드입니다.
        /// </summary>
        public void RunPortalWithoutLogIn()
        {
            bool isFirstLoop = true;

            // 데이터 로드(시간표)
            dataManager.LoadData();
            lectureSchedule = dataManager.Classes;
            
            while (true)
            {
                if(isFirstLoop)
                {
                    // 메뉴 출력
                    print.UserVersionMenu();

                    // 기능 선택
                    Console.SetCursorPosition(8, 10);
                    Console.Write('▷');

                    isFirstLoop = false;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(8, 10, 7, 2, '▷');                  // 위로 커서 이동        
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(8, 10, 7, 2, '▷');         // 아래로 커서 이동
                else if (keyInfo.Key == ConsoleKey.Enter)                                                 // 기능 선택
                {
                    GoNextFunction((Console.CursorTop / 2) - 5);
                    isFirstLoop = true;
                    if (tools.IsEnd(Console.CursorTop)) { Console.SetCursorPosition(4, Console.CursorTop + 2); return; }
                }
                else print.BlockCursorMove(8, "▷");                                                     // 입력 무시
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
                case Constants.INQUIRE_LECTURE_SCHEDULE:
                    lecturePlanManage.InquireLectureSchedule(Constants.LECTURE_SEARCH, lectureSchedule, basket);
                    return;
                case Constants.PUT_INTO_BASKET:
                    basket = lecturePlanManage.InquireLectureSchedule(Constants.PUT_LECTURE_IN_BASKET, lectureSchedule, basket);
                    return;
                case Constants.MANAGE_PRE_ENROLLED_LECTURE:
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
                case Constants.CLOSE_PROGRAM:
                    return;
            }
        }
    }
}
