using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.IO
{
    class Print
    {
        /// <summary>
        /// 프로그램을 처음 실행시켰을 떄 배경화면을 출력하는 메소드입니다.
        /// </summary>
        public void PortalLogIn()
        {
            SetWindowSmallSize();
            SetBackgroundColor(ConsoleColor.Black);
            PrintSentences(Constants.HOME_SENTENCES);
        }

        /// <summary>
        /// 콘솔창의 배경색을 바꿔주는 메소드입니다.
        /// </summary>
        /// <param name="color">바꿀 콘솔 배경색</param>
        public void SetBackgroundColor(ConsoleColor color)
        {
            for (int height = 0; height < Console.WindowHeight; height++)
            {
                Console.BackgroundColor = color;
                Console.Write(new string(' ', Console.WindowWidth));
                if (height != Console.WindowHeight - 1) Console.SetCursorPosition(0, Console.CursorTop + 1);
                else Console.SetCursorPosition(0, 0);
            }
        }

        /// <summary>
        /// 인자로 받은 문자열 배열들을 가운데정렬하여 출력하는 메소드입니다.
        /// </summary>
        /// <param name="sentences">출력할 문자열 배열</param>
        public void PrintSentences(string[] sentences)
        {
            int leftCursor;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (string sentence in sentences)
            {
                leftCursor = GetLeftCursorForCenterArrangeMent(sentence);
                Console.SetCursorPosition(leftCursor, Console.CursorTop);
                Console.WriteLine(sentence);
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 인자로 받은 문자열을 가운데정렬하여 출력하는 메소드입니다.
        /// </summary>
        /// <param name="sentence">출력할 문자열</param>
        public void PrintSentence(string sentence)
        {
            int leftCursor;

            Console.ForegroundColor = ConsoleColor.Gray;
            leftCursor = GetLeftCursorForCenterArrangeMent(sentence);
            Console.SetCursorPosition(leftCursor, Console.CursorTop);
            Console.WriteLine(sentence);
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 사용자에게 ID나 Password를 물어보는 메소드입니다.
        /// </summary>
        /// <param name="idOrPassword">아이디 혹은 패스워드</param>
        public void AskIDOrPassword(int idOrPassword)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            switch (idOrPassword)
            {
                case Constants.ID:
                    Console.SetCursorPosition(16, 10);
                    Console.Write("8자리 숫자 입력");
                    break;
                case Constants.PASSWORD:
                    Console.SetCursorPosition(16, 12);
                    Console.Write("입력");
                    break;
            }
            
        }

        public void LogInButton()
        {

        }

        /// <summary>
        /// 유저 버전으로 로그인했을 때 메뉴를 출력하는 메소드입니다.
        /// </summary>
        public void UserVersionMenu()
        {
            SetWindowSmallSize();

            Console.SetCursorPosition(0, 3);
            PrintSentences(Data.Constants.ENSHARP_TITLE);
            Console.SetCursorPosition(0, 8);
            PrintSentences(Data.Constants.USER_VERSION_MENU);
        }

        /// <summary>
        /// 지금 검색하는 검색 조건의 색을 칠하는 메소드입니다.
        /// 이전 검색 조건은 원래 색으로 바꾸어줍니다.
        /// </summary>
        /// <param name="modeToGray">원래 색으로 바꿀 검색 조건</param>
        /// <param name="grayCursorTop">원래 색으로 바꿀 텍스트의 줄 정보</param>
        /// <param name="modeToHighlight">색칠할 검색 조건</param>
        /// <param name="highlightCursorTop">색칠할 텍스트의 줄 정보</param>
        public void ColorMenu(int modeToGray, int grayCursorTop, int modeToHighlight, int highlightCursorTop)
        {
            // 원래 색으로 바꿀 검색 조건이 있을 경우 해당 텍스트의 색을 원래 색으로 변환
            if (modeToGray != Constants.NONE)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(6, grayCursorTop);
                Console.Write(Constants.SEARCHING_MENU[modeToGray - 1]);
            }

            // 강조할 검색 조건의 텍스트 색 변환
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(6, highlightCursorTop);
            if (modeToHighlight != Constants.CHECK) Console.Write(Constants.SEARCHING_MENU[modeToHighlight - 1]);
            else { Console.SetCursorPosition(10, highlightCursorTop); Console.Write("강의시간표 조회하기(엔터)"); }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// 지금 검색하는 검색 조건의 색을 칠하는 메소드입니다.
        /// 이전 검색 조건은 원래 색으로 바꾸어줍니다.
        /// </summary>
        /// <param name="menu">이전 검색 조건</param>
        /// <param name="grayCursorTop">원래 색으로 바꿀 텍스트의 줄 정보</param>
        /// <param name="modeToHighlight">색칠할 검색 조건</param>
        /// <param name="highlightCursorTop">색칠할 텍스트의 줄 정보</param>
        public void ColorMenu(string menu, int grayCursorTop, int modeToHighlight, int highlightCursorTop)
        {
            // 원래 색으로 바꿀 텍스트의 색을 원래 색으로 변환
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(6, grayCursorTop);
            Console.Write(menu);

            // 강조할 텍스트가 있을 경우 해당 텍스트 강조
            if (modeToHighlight != Constants.NONE)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(6, highlightCursorTop);
                if (modeToHighlight != Constants.CHECK) Console.Write(Constants.SEARCHING_MENU[modeToHighlight - 1]);
                else { Console.SetCursorPosition(10, highlightCursorTop); Console.Write("강의시간표 조회하기(엔터)"); }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        /// <summary>
        /// 강의시간표를 조회할 때의 메뉴를 출력하는 메소드입니다.
        /// </summary>
        /// <param name="mode">강의 시간표 조회, 관심과목 담기, 수강신청</param>
        public void LectureSearchMenu(int mode)
        {
            SetWindowSmallSize();

            Console.SetCursorPosition(0, 3);
            PrintSentences(Constants.ENSHARP_TITLE);
            Console.SetCursorPosition(0, 8);
            PrintSentence(Constants.LECTURE_SEARCH_MENU[mode]);
            PrintLectureSearchMenuAndOption(mode);
        }

        /// <summary>
        /// 강의 시간표 열람 혹은 관심과목 담기 실행시 검색할 강의들의 조건과 가이드라인을 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="mode">수행하는 기능 모드</param>
        public void PrintLectureSearchMenuAndOption(int mode)
        {
            Console.SetCursorPosition(6, 11);

            if (mode == Constants.SIGN_UP_CLASS)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("수강신청 검색 | ");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }

            for (int sentence = 0; sentence < Constants.SEARCHING_MENU.Length; sentence++)
            {
                if (sentence % 2 == 0)  // 조건 출력
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(Constants.SEARCHING_MENU[sentence]);
                }
                else                     // 가이드라인 출력
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(Constants.SEARCHING_MENU[sentence]);
                    Console.SetCursorPosition(6, Console.CursorTop + 1);
                }
            }
        }

        /// <summary>
        /// '내 시간표 관리' 에서 메뉴들을 출력해주는 메소드입니다. 
        /// </summary>
        public void ManageEnrolledLectureMenu()
        {
            SetWindowSmallSize();
            SetBackgroundColor(ConsoleColor.Black);
            Console.SetCursorPosition(0, 3);
            PrintSentences(Data.Constants.ENSHARP_TITLE);
            Console.SetCursorPosition(0, 8);
            PrintSentences(Constants.MY_SCHEDULE_MENU);
        }

        /// <summary>
        /// 시간표 저장 기능을 수행할 때 타이틀과 질문, 가이드라인을 출력해주는 메소드입니다.
        /// </summary>
        public void SaveLectureIntoFileBackground()
        {
            SetWindowSmallSize();
            Console.SetCursorPosition(0, 3);
            PrintSentences(Constants.ENSHARP_TITLE);
            Console.SetCursorPosition(0, 8);
            PrintSentence("-시간표 저장-");
            Console.SetCursorPosition(3, 11);
            Console.Write("저장할 시간표 이름| ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("10자 이내 문자, 숫자");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// 강의 시간표 검색 결과를 출력하는 메소드입니다.
        /// </summary>
        /// <param name="classes">검색된 시간표 결과</param>
        /// <param name="department">사용자가 검색시 입력한 개설학과전공</param>
        /// <param name="serialNumber">사용자가 검색시 입력한 학수번호</param>
        /// <param name="lectureName">사용자가 검색시 입력한 강의명</param>
        /// <param name="grade">사용자가 검색시 선택한 학년</param>
        /// <param name="professor">사용자가 검색시 입력한 교수명</param>
        public void SearchedLectureSchedule(int mode, int searchMethod, List<ClassVO> classes, int department, string serialNumber, string lectureName, int grade, string professor)
        {
            Console.SetWindowSize(160, 35);
            Console.Clear();

            LectureScheduleTitle(mode, searchMethod,department, serialNumber, lectureName, grade, professor);
            Console.SetCursorPosition(0, 9);
            if (mode == Constants.SIGN_UP_CLASS) Console.SetCursorPosition(0, 6);
            foreach (string guideline in Constants.LECTURE_SCHEDULE_GUIDELINE) Console.WriteLine(guideline);

            Lectures(classes, Console.CursorTop);
        }

        /// <summary>
        /// 강의들을 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="lectures">강의 리스트</param>
        /// <param name="cursorTop">출력 시작 커서 위치</param>
        public void Lectures(List<ClassVO> lectures, int cursorTop)
        {
            StringBuilder name = new StringBuilder();
            StringBuilder shortenProfessor = new StringBuilder();
            string credit;

            Console.SetCursorPosition(0, cursorTop);

            foreach (ClassVO lecture in lectures)
            {
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.Write(lecture.Department);
                Console.SetCursorPosition(25, Console.CursorTop);
                Console.Write(lecture.SerialNumber);
                Console.SetCursorPosition(34, Console.CursorTop);
                Console.Write(lecture.DivisionClassNumber);
                Console.SetCursorPosition(40, Console.CursorTop);
                name.Clear();
                name.Append(lecture.LectureName);
                if (lecture.LectureName.Length > 15) { name.Remove(15, name.Length - 15); name.Append("..."); }
                Console.Write(name);
                Console.SetCursorPosition(67, Console.CursorTop);
                Console.Write(lecture.CourseDivision);
                Console.SetCursorPosition(78, Console.CursorTop);
                Console.Write(lecture.Grade);
                Console.SetCursorPosition(82, Console.CursorTop);
                credit = string.Empty;
                credit = lecture.Credit.ToString("N1");
                Console.Write(credit);
                Console.SetCursorPosition(88, Console.CursorTop);
                Console.Write(lecture.LectureSchedule);
                Console.SetCursorPosition(120, Console.CursorTop);
                Console.Write(lecture.ClassRoom);
                Console.SetCursorPosition(134, Console.CursorTop);
                shortenProfessor.Clear();
                shortenProfessor.Append(lecture.Professor);
                if (lecture.Professor.Length > 8) { shortenProfessor.Remove(8, shortenProfessor.Length - 8); shortenProfessor.Append("..."); }
                Console.Write(shortenProfessor);
                Console.SetCursorPosition(148, Console.CursorTop);
                Console.WriteLine(lecture.LectureLanguage);
            }
        }

        /// <summary>
        /// 강의 시간표 검색 결과를 나타낼 때 배경을 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="mode">기능</param>
        /// <param name="searchMethod">mode가 수강신청일 경우 해당, 사용자가 선택한 검색 조건</param>
        /// <param name="department">사용자가 검색시 입력한 개설학과전공</param>
        /// <param name="serialNumber">사용자가 검색시 입력한 학수번호</param>
        /// <param name="lectureName">사용자가 검색시 입력한 강의명</param>
        /// <param name="grade">사용자가 검색시 선택한 학년</param>
        /// <param name="professor">사용자가 검색시 입력한 교수명</param>
        public void LectureScheduleTitle(int mode, int searchMethod, int department, string serialNumber, string lectureName, int grade, string professor)
        {
            List<string> searchingCondition = new List<string>();

            Console.SetCursorPosition(120, 2);
            foreach (string title in Constants.ENSHARP_TITLE_IN_SEARCH_MODE)
            {
                Console.WriteLine(title);
                Console.SetCursorPosition(120, Console.CursorTop);
            }

            // 검색 조건이 '전체'로 설정되어있거나 입력값이 없는 경우
            searchingCondition.Add(Constants.DEPARTMENT[department]);
            if (string.Compare(serialNumber, "0") == 0) serialNumber = string.Copy("전체 학수번호");
            searchingCondition.Add(serialNumber);
            if (string.Compare(lectureName, "") == 0) lectureName = string.Copy("전체");
            searchingCondition.Add(lectureName);
            searchingCondition.Add(Constants.GRADE[grade]);
            if (string.Compare(professor, "") == 0) professor = string.Copy("전체");
            searchingCondition.Add(professor);
            
            Console.SetCursorPosition(7, 3);
            if (mode == Constants.SIGN_UP_CLASS)
            {
                Console.Write("수강신청 검색 | " + Constants.SIGN_UP_CLASSES_SELECTION[searchMethod]);
                if (searchMethod != Constants.BASKET) Console.Write(" | " + searchingCondition[searchMethod]);
                return;
            }

            // 배경 출력
            Console.SetCursorPosition(7, 2);
            for (int item = 0; item < Constants.SEARCHING_MENU_IN_SEARCHING_MODE.Count(); item++)
            {
                if (item % 2 == 0)
                {
                    Console.Write(Constants.SEARCHING_MENU_IN_SEARCHING_MODE[item]);
                    Console.Write(searchingCondition[item]);
                }
                else
                {
                    Console.SetCursorPosition(55, Console.CursorTop);
                    Console.Write(Constants.SEARCHING_MENU_IN_SEARCHING_MODE[item]);
                    Console.WriteLine(searchingCondition[item]);
                    Console.SetCursorPosition(7, Console.CursorTop + 1);
                }
            }
        }

        /// <summary>
        /// 현재 커서가 위치한 강의가 선택 불가능한 강의일 경우
        /// 포인터를 선택 불가능 마크로 바꿔주는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 위치(들여쓰기)</param>
        /// <param name="cursorTop">커서 위치(줄)</param>
        public void NonAvailableLectureMark(int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(" X");
            Console.SetCursorPosition(cursorLeft + 1, cursorTop);
        }

        /// <summary>
        /// 사용자가 선택한 강의가 등록되거나 등록 거부될 경우 그 결과를 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 정보(들여쓰기)</param>
        /// <param name="cursorTop">커서 정보(줄)</param>
        /// <param name="mode">등록완료 혹은 등록거부</param>
        public void CompletePutOrDeleteLectureInBasket(int cursorLeft, int cursorTop, int mode)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            if (mode == Constants.PUT) Console.Write("선택");
            else if (mode == Constants.DELETE) Console.Write("삭제");
            else if (mode == Constants.FAIL) Console.Write(" X");
            System.Threading.Thread.Sleep(500);
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new string(' ', 4));
            Console.SetCursorPosition(cursorLeft + 1, cursorTop);
            Console.Write('▷');
        }

        /// <summary>
        /// 선택된 강의 시간표들을 출력하는 메소드입니다.
        /// </summary>
        /// <param name="mode">기능</param>
        /// <param name="selectedLecture">선택된 강의 리스트</param>
        public void SelectedLecture(int mode, List<ClassVO> selectedLecture)
        {
            // 엔샵 타이틀 출력
            Console.SetCursorPosition(120, 2);
            foreach (string title in Constants.ENSHARP_TITLE_IN_SEARCH_MODE)
            {
                Console.WriteLine(title);
                Console.SetCursorPosition(120, Console.CursorTop);
            }

            // 기능 타이틀 출력
            Console.SetCursorPosition(7, 3);
            if (mode == Constants.MANAGE_BASKET) Console.Write("| 관심과목 관리 |");
            else Console.Write("| 내 시간표 관리 |");
            
            Console.SetCursorPosition(0, 6);
            foreach (string guideline in Constants.LECTURE_SCHEDULE_GUIDELINE) Console.WriteLine(guideline);
            Console.SetCursorPosition(1, 7);
            Console.Write("삭제");

            // 강의 출력
            Lectures(selectedLecture, 9);
        }

        /// <summary>
        /// 수강신청시 사용자가 검색 조건을 선택하면 해당 검색 조건을 출력해주는 메소드입니다. 
        /// </summary>
        /// <param name="cursorLeft">커서 정보(들여쓰기)</param>
        /// <param name="cursorTop">커서 정보(줄)</param>
        /// <param name="index">검색 조건</param>
        public void Category(int cursorLeft, int cursorTop, int index)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(Constants.SIGN_UP_CLASSES_SELECTION[index] + " | ");
        }

        /// <summary>
        /// 사용자의 시간표를 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="enrolledLecture"></param>
        public void MyLectureSchedule(List<ClassVO> enrolledLecture)
        {
            int color = 0;
            ConsoleColor colorForLecture;

            // 표 출력
            Template();

            // 시간표 출력
            foreach (ClassVO lecture in enrolledLecture)
            {
                // 색 고르기
                colorForLecture = Constants.COLORS[color];
                if (color == Constants.COLORS.Length - 1) color = 0;
                else color++;
                
                // 색칠 및 수업명, 강의실 출력
                for (int row = 0; row < lecture.TimeOfClass.GetLength(0); row++)
                {
                    ColorLectureTimeTable(lecture, row, colorForLecture);
                    WriteLectureNameAndPlace(lecture, row);
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// 시간표 출력시 수업명과 강의실을 출력하는 메소드입니다.
        /// </summary>
        /// <param name="lecture">강의</param>
        /// <param name="row">수업 요일</param>
        public void WriteLectureNameAndPlace(ClassVO lecture, int row)
        {
            int cursorLeft, cursorTop;
            bool isNamePrinted;
            bool isFirstLectureRoomPrinted, isSecondLectureRoomPrinted;

            isNamePrinted = false;
            isFirstLectureRoomPrinted = false;
            isSecondLectureRoomPrinted = false;

            for (int column = 0; column < lecture.TimeOfClass.GetLength(1); column++)
            {
                if (lecture.TimeOfClass[row, column])
                {
                    // 출력할 위치 계산
                    cursorLeft = (22 * row) + 6;
                    if (column % 2 == 0) cursorTop = (5 * (column / 2)) + 10;
                    else cursorTop = (5 * ((column - 1) / 2)) + 12;

                    // 이름 및 강의실 출력
                    if (!isNamePrinted) { LectureName(cursorLeft, cursorTop, lecture.LectureName); isNamePrinted = true; }
                    if (isNamePrinted && column == 18) LectureName(cursorLeft, cursorTop, lecture.LectureName);
                    if (!isFirstLectureRoomPrinted) { LectureRoom(cursorLeft, Console.CursorTop + 1, lecture.ClassRooms[0]); isFirstLectureRoomPrinted = true; }
                    if (cursorTop > 52 && isFirstLectureRoomPrinted && !isSecondLectureRoomPrinted && lecture.ClassRooms.Count == 2) { LectureRoom(cursorLeft, Console.CursorTop + 1, lecture.ClassRooms[1]); isSecondLectureRoomPrinted = true; }
                }
            }
        }

        /// <summary>
        /// 시간표 출력시 수업 시간대의 배경을 색칠하는 메소드입니다.
        /// </summary>
        /// <param name="lecture">수업</param>
        /// <param name="row">요일</param>
        /// <param name="colorForLecture">색칠할 색</param>
        public void ColorLectureTimeTable(ClassVO lecture, int row, ConsoleColor colorForLecture)
        {
            int cursorLeft, cursorTop;

            for (int column = 0; column < lecture.TimeOfClass.GetLength(1); column++)
            {
                if (lecture.TimeOfClass[row, column])
                {
                    // 색칠할 위치 계산
                    cursorLeft = (22 * row) + 6;
                    if (column % 2 == 0) cursorTop = (5 * (column / 2)) + 10;
                    else cursorTop = (5 * ((column - 1) / 2)) + 12;

                    // 색칠
                    Console.BackgroundColor = colorForLecture;
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Console.WriteLine(new string(' ', 20));
                    Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                    Console.WriteLine(new string(' ', 20));

                    // 경계선 색칠
                    if (CountOfDayOfWeek(lecture.LectureSchedule) < 3 && lecture.TimeOfClass[row, column + 1]) 
                    {
                        Console.SetCursorPosition(cursorLeft, cursorTop + 2);
                        Console.WriteLine(new string(' ', 20));
                    }
                    else if (CountOfDayOfWeek(lecture.LectureSchedule) == 3 && lecture.TimeOfClass[row, column + 1] && cursorTop != 52)
                    {
                        Console.SetCursorPosition(cursorLeft, cursorTop + 2);
                        Console.WriteLine(new string(' ', 20));
                    }
                }
            }
        }

        /// <summary>
        /// 강의실을 출력하는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 정보(들여쓰기)</param>
        /// <param name="cursorTop">커서 정보(줄)</param>
        /// <param name="lectureRoom">강의실</param>
        public void LectureRoom(int cursorLeft, int cursorTop, string lectureRoom)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(lectureRoom);
        }

        /// <summary>
        /// 강의명을 출력하는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 정보(들여쓰기)</param>
        /// <param name="cursorTop">커서 정보(줄)</param>
        /// <param name="lectureName">강의명</param>
        public void LectureName(int cursorLeft, int cursorTop, string lectureName)
        {
            string name1 = lectureName;
            string name2 = lectureName;
            int byteOfName = System.Text.ASCIIEncoding.Unicode.GetByteCount(lectureName);
            int indexToCut = 0;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            if (byteOfName > 20) // 강의명이 길면 자른 후 출력
            {
                for (indexToCut = 0; indexToCut < lectureName.Length; indexToCut++)
                {
                    int byteOfFrontPartOfName = System.Text.ASCIIEncoding.Unicode.GetByteCount(name1.Remove(indexToCut, lectureName.Length - indexToCut));
                    if (byteOfFrontPartOfName > 20) break;
                }
                name1 = lectureName.Remove(indexToCut, lectureName.Length - indexToCut);
                name2 = lectureName.Remove(0, indexToCut);
                Console.Write(name1);
                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                Console.Write(name2);
            }
            else Console.Write(lectureName);
        }

        /// <summary>
        /// 시간표 출력시 표를 출력하는 메소드입니다.
        /// </summary>
        public void Template()
        {
            Console.SetWindowSize(160, 35);
            Console.Clear();

            Console.SetCursorPosition(120, 2);
            foreach (string title in Constants.ENSHARP_TITLE_IN_SEARCH_MODE)
            {
                Console.WriteLine(title);
                Console.SetCursorPosition(120, Console.CursorTop);
            }

            Console.SetCursorPosition(7, 3);
            Console.Write("| 시간표 열람 |");

            Console.SetCursorPosition(0, 7);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (string line in Constants.TEMPLATE1) Console.WriteLine(line);
            for (int count = 0; count < 12; count++)
            {
                foreach (string line in Constants.TEMPLATE1) Console.WriteLine(line);
                Console.WriteLine(Constants.TEMPLATE1[1] + "\n" + Constants.TEMPLATE1[1] + "\n" + Constants.TEMPLATE1[1]);
            }
            Console.WriteLine(Constants.TEMPLATE1[0]);

            Console.SetCursorPosition(0, 8);
            Console.Write(Constants.TEMPLATE2);

            Console.SetCursorPosition(2, 10);
            for (int clock = 9; clock < 21; clock++)
            {
                if (clock > 12) Console.Write(" " + (clock - 12));
                else { if (clock < 10) Console.Write(" " + clock); else Console.Write(clock); }
                Console.SetCursorPosition(2, Console.CursorTop + 5);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// 콘솔창을 작은모드로 설정해주는 메소드입니다.
        /// </summary>
        public void SetWindowSmallSize()
        {
            Console.Clear();
            Console.SetWindowSize(45, 30);
            //Console.SetWindowPosition(5, 5);
        }

        /// <summary>
        /// 콘솔에서 현재 커서가 위치한 줄을 비워줍니다.
        /// </summary>
        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// 콘솔에서 한 글자만을 삭제하는 메소드입니다.
        /// </summary>
        /// <param name="spaces">삭제할 글자의 왼쪽 커서 위치</param>
        public void ClearOneLetter(int spaces)
        {
            Console.SetCursorPosition(spaces, Console.CursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(spaces, Console.CursorTop);
        }

        /// <summary>
        /// 커서의 움직임을 막는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 위치(들여쓰기)</param>
        /// <param name="pointer">화살표</param>
        public void BlockCursorMove(int cursorLeft, string pointer)
        {
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            Console.Write(pointer + ' ');
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// 사용자가 검색창에 검색어를 입력하면 안내문을 지워주는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="isNumber">사용자가 입력한 문자가 유효한지 검사</param>
        /// <param name="letter">사용자가 입력한 문자</param>
        public void DeleteGuideLine(int cursorLeft, bool isValid, ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Tab) return;
            
            Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            if (isValid) Console.Write(keyInfo.KeyChar);
        }

        /// <summary>
        /// 사용자가 유효하지 않은 문자를 입력한 경우 그 문자를 콘솔창에서 지워주는 메소드입니다.
        /// </summary>
        /// <param name="currentCursor">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        public void InvalidInput(ConsoleKeyInfo keyInfo,int currentCursor, int cursorTop)
        {
            string space;

            if (System.Text.RegularExpressions.Regex.IsMatch(keyInfo.KeyChar.ToString(), Constants.KOREAN_PATTERN))
                space = "  ";
            else space = " ";

            Console.SetCursorPosition(currentCursor, cursorTop);
            Console.Write(space);
            Console.SetCursorPosition(currentCursor, cursorTop);
        }

        /// <summary>
        /// 검색창이 비었을 때 안내멘트를 출력해주는 메소드입니다.
        /// </summary>
        /// <param name="guideline">안내멘트</param>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        public void SearchGuideline(string guideline, int cursorLeft, int cursorTop)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(guideline);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        /// <summary>
        /// 가운데 정렬로 충렬하기 위해 들여쓰기할 공간을 계산하는 메소드입니다.
        /// </summary>
        /// <param name="sentence">출력할 문장</param>
        /// <returns>가운데 정렬을 위한 들여쓰기 공백</returns>
        public int GetLeftCursorForCenterArrangeMent(string sentence)
        {
            int leftCursor;

            if (sentence.Length == 0) return 0;

            Console.Write(sentence);
            leftCursor = Console.WindowWidth / 2 - Console.CursorLeft / 2;
            ClearCurrentConsoleLine();

            return leftCursor;
        }

        /// <summary>
        /// 검색창을 비우는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 정보(들여쓰기)</param>
        /// <param name="cursorTop">커서 정보(줄)</param>
        public void ClearSearchBar(int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
        }

        /// <summary>
        /// 사용자가 입력한 검색어를 출력하는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 정보(들여쓰기)</param>
        /// <param name="cursorTop">커서 정보(줄)</param>
        /// <param name="answer">사용자가 입력한 검색어</param>
        public void Answer(int cursorLeft, int cursorTop, string answer)
        {
            ClearSearchBar(cursorLeft, cursorTop);
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(answer);
        }

        /// <summary>
        /// string형의 요일 및 강의시간에 요일을 뜻하는 글자가 몇 글자인지 세는 메소드입니다.
        /// string에서 한글의 개수를 반환합니다.
        /// </summary>
        /// <param name="lectureTime">실습 시간 및 요일</param>
        /// <returns>요일 수</returns>
        public int CountOfDayOfWeek(string lectureTime)
        {
            int count = 0;
            string pattern = "[가-힣]";

            foreach (char day in lectureTime)
                if (System.Text.RegularExpressions.Regex.IsMatch(day.ToString(), pattern)) count++;

            return count;
        }
    }
}
