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
            Console.WriteLine("선택  NO    개설학과전공   학수번호  분반       교과목명        이수구분  학년  학점        요일 및 강의시간          강의실       교수명        강의언어");
            Console.WriteLine(" ▷  100  디지털콘텐츠학과  009101   001  Capstone디자인(산...  전공필수   4    4.0  화목16:30-18:00,목18:00-20:00  율401/동401  Muhammad(1)...   한국어");
            
            //print.UserVersionMenu();
            //WaitUntilGetEnterKey();


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
