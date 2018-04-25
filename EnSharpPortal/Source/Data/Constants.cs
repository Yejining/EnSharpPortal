using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnSharpPortal.Source.Data
{
    class Constants
    {
        public const int NOBODY = 0;

        public static string[] HOME_SENTENCES = { "","","","",
        "엔샵 포탈", "", "ENJOY C SHARP!", "","","",
        //"아이디(ID)    8자리 숫자     ┏       ┓",
        "아이디(ID)                              ",
        "",
        //"                                로그인  ",
        //"비밀번호(PW)  입력           ┗       ┛",
        "비밀번호(PW)                            ",
        "", "", "", "",
        "+ 학생은 학번, 관리자는 포털 아이디로     ",
        "  로그인이 가능합니다.                    ", "",
        "+ 초기 비밀번호는 회원의 생년월일이며     ",
        "  반드시 변경 후 사용하여 주시기 바랍니다.", "",
        "+ 장애문의                                ",
        "  관리자 : 010-5110-1996                  "
        };
        
        public const int ID = 1;
        public const int PASSWORD = 2;

        public const int MONDAY = 1;
        public const int TUESDAY = 2;
        public const int WEDNESDAY = 3;
        public const int THURSDAY = 4;
        public const int FRIDAY = 5;

        public const int KOREAN = 1;
        public const int ENGLISH = 2;

        public const int MAJOR_SELECTION = 1;
        public const int MAJOR_ESSENTIAL = 2;
        public const int CORE_ESSENTIAL = 3;
    }
}
