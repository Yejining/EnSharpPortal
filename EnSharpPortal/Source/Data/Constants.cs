﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnSharpPortal.Source.Data
{
    class Constants
    {
        public const int NOBODY = 0;

        public static string[] HOME_SENTENCES =
        {
            "","","","",
            "엔샵 포탈", "", "ENJOY C SHARP!", "","","",
            "아이디(ID)                              ", "",
            "비밀번호(PW)                            ",
            "", "", "", "",
            "+ 학생은 학번, 관리자는 포털 아이디로     ",
            "  로그인이 가능합니다.                    ", "",
            "+ 초기 비밀번호는 회원의 생년월일이며     ",
            "  반드시 변경 후 사용하여 주시기 바랍니다.", "",
            "+ 장애문의                                ",
            "  관리자 : 010-5110-1996                  "
        };

        public static string[] ENSHARP_TITLE =
        {
            "┏                  ┓",
            "엔 샵     포 탈",
            " ENJOY C SHARP!",
            "┗                  ┛"
        };

        public static string[] ENSHARP_TITLE_IN_SEARCH_MODE =
        {
            "┏                                ┓",
            "   엔 샵  포 탈 | ENJOY C SHARP!",
            "┗                                ┛"
        };

        public static string[] USER_VERSION_MENU =
        {
            "강 의 시 간 표 조회","",
            "관 심 과 목   담 기","",
            "관 심 과 목   관 리","",
            "수   강     신   청","",
            "내  시 간 표  관 리","",
            "포 탈  이 용  안 내","",
            "종               료"
        };

        public static string[] SEARCHING_MENU =
        {
            "개설학과전공 | ",
            "선택",
            "학수번호 | ",
            "6자리 숫자 입력",
            "교과목명 | ",
            "입력",
            "학    년 | ",
            "선택",
            "교 수 명 | ",
            "입력"
        };

        public static string[] SEARCHING_MENU_IN_SEARCHING_MODE =
        {
            "개 설  학 과  전 공 | ",
            "학수번호 | ",
            "교   과    목   명  | ",
            "학    년 | ",
            "교      수      명  | "
        };

        public static string[] DEPARTMENT =
        {
            "전체",
            "디지털콘텐츠학과",
            "정보보호학과",
            "컴퓨터공학과",
            "소프트웨어학과"
        };

        public static string[] GRADE = 
        {
            "전체",
            "1",
            "2",
            "3",
            "4"
        };

        public static string[] SIGN_UP_CLASSES_MENU =
        {
            "수강신청 검색 | ",
            "선택",
        };

        public static string[] SIGN_UP_CLASSES_SELECTION =
        {
            "개설학과전공",
            "학수번호",
            "교과목명",
            "학년",
            "교수명",
            "관심과목"
        };

        public static string[] LECTURE_SEARCH_MENU =
        {
            "-강 의 시 간 표 조 회-",
            "- 관 심 과 목   담 기-",
            "- 수   강     신   청-"
        };

        public static string[] LECTURE_SCHEDULE_GUIDELINE =
        {
            "-----------------------------------------------------------------------------------------------------------------------------------------------------------",
            " 선택   개설학과전공    학수번호  분반          교과목명           이수구분  학년 학점          요일 및 강의시간          강의실       교수명      강의언어",
            "-----------------------------------------------------------------------------------------------------------------------------------------------------------"
        };

        public static string[] TEMPLATE1 =
        {
            "------------------------------------------------------------------------------------------------------------------",
            "    ː                    ː                    ː                    ː                    ː                    "
        };

        public static string TEMPLATE2 = "               월                    화                    수                    목                    금";


        public static string[] MY_SCHEDULE_MENU =
        {
            "-내 시간표 관리-", "", "", "",
            "시간표 열람", "", "",
            "시간표 저장", "", "",
            "시간표 관리"
        };

        public static string[] TIMES =
        {
            "09:00-09:30",
            "09:30-10:00",
            "10:00-10:30",
            "10:30-11:00",
            "11:00-11:30",
            "11:30-12:00",
            "12:00-12:30",
            "12:30-13:00",
            "13:00-13:30",
            "13:30-14:00",
            "14:00-14:30",
            "14:30-15:00",
            "15:00-15:30",
            "15:30-16:00",
            "16:00-16:30",
            "16:30-17:00",
            "17:00-17:30",
            "17:30-18:00",
            "18:00-18:30",
            "18:30-19:00",
            "19:00-19:30",
            "19:30-20:00",
            "21:00-21:30",
            "21:30-22:00"
        };

        public static string[] DAYS =
        {
            "월",
            "화",
            "수",
            "목",
            "금"
        };

        public static string[] ERROR_MESSAGE =
        {
            "검색 조건과 일치하는 강의가 없습니다!",
            "관심과목으로 지정한 강의가 없습니다!",
            "수강신청한 강의가 없습니다!"
        };

        public static ConsoleColor[] COLORS =
        {
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkYellow
        };

        public const string NUMBER_PATTERN = "[0-9]";
        public const string ENGLISH_PATTERN = "[a-zA-Z]";
        public const string KOREAN_PATTERN = "[ㄱ-ㅎㅏ-ㅣ가-힣]";
        public const string SPECIAL_LETTER = "[`~!@#$%^&*()\\-_=+\\{\\}\\[\\]\\\\\\|:;\"\'<>,.?/]";
        public const string VALID_SPECIAL_LETTER = "[`~!@#$%^&()\\-_=+\\{\\}\\[\\];\\\\',.]";
        public const string INVALID_SPECIAL_LETTER = "[\\\\/:*?\"<>|]";

        public const int NONE = -5;

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

        public const int All_GRADE = 0;
        public const int FRESHMAN = 1;
        public const int SOPHOMORE = 2;
        public const int JUNIOR = 3;
        public const int SENIOR = 4;

        public const int LECTURE_SEARCH = 0;
        public const int PUT_LECTURE_IN_BASKET = 1;
        public const int SIGN_UP_CLASS = 2;

        public const int SERIAL_NUMBER = 3;
        public const int LECTURE_NAME = 5;
        public const int PROFESSOR = 9;
        public const int FILE_NAME = 10;

        public const int SELECT_DEPARTMENT = 0;
        public const int SELECT_GRADE = 1;
        public const int SELECT_SEARCH_METHOD = 2;

        public const int INQUIRE_LECTURE_SCHEDULE = 0;
        public const int PUT_INTO_BASKET = 1;
        public const int MANAGE_PRE_ENROLLED_LECTURE = 2;
        public const int REGISTER_LECTURE = 3;
        public const int CHECK_MY_SCHEDULE = 4;
        public const int INFORMATION_ABOUT_PORTAL = 5;
        public const int CLOSE_PROGRAM = 6;

        public const int MANAGE_BASKET = 5;

        public const int PUT = 0;
        public const int DELETE = 1;
        public const int FAIL = 2;

        public const int ALL = -1;
        public const int MAJOR = 0;
        public const int NUMBER = 1;
        public const int NAME = 2;
        public const int YEAR = 3;
        public const int PROFESSOR_NAME = 4;
        public const int BASKET = 5;

        public const int INQUIRE_MY_LECTURE_SCHEDULE = 0;
        public const int SAVE_MY_LECTURE_SCHEDULE = 1;
        public const int MANAGE_MY_LECTURE_SCHEDULE = 2;

        public const int TAB = 1;
        public const int ENTER = 2;
        public const int ESCAPE = 3;

        public const int CHECK = 10;

        public const int ERROR_THERE_IS_NO_CLASS = 0;
        public const int ERROR_EMPTY_BASKET = 1;
        public const int ERROR_NO_ENROLLED_LECTURE = 2;
    }
}
