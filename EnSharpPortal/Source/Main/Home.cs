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
            dataManager.LoadData();
            List<ClassVO> classes = dataManager.Classes;

            print.UserVersionMenu();
            WaitUntilGetEnterKey();

            // 강의 시간표 조회
            lecturePlanManage.InquireLectureSchedule(Constants.PUT_LECTURE_IN_BASKET, classes);
            
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
