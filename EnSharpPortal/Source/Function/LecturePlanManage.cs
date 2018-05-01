using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.IO;
using EnSharpPortal.Source.Data;
using EnSharpPortal.Source.Main;

namespace EnSharpPortal.Source.Function
{
    class LecturePlanManage
    {
        Print print = new Print();
        GetValue getValue = new GetValue();
        Tools tools = new Tools();
        FileIOManager fileIOManager = new FileIOManager();

        /// <summary>
        /// 강의 시간표를 조회하는 메소드입니다.
        /// 강의 시간표 조회, 관심과목 담기 기능에서 사용됩니다.
        /// </summary>
        /// <param name="mode">기능</param>
        /// <param name="lectureSchedule">강의 시간표</param>
        /// <param name="basket">관심과목</param>
        /// <returns>검색된 강의 목록</returns>
        public List<ClassVO> InquireLectureSchedule(int mode, List<ClassVO> lectureSchedule, List<ClassVO> basket)
        {
            List<ClassVO> searchedLecture = new List<ClassVO>();
            int next;
            int department = 0;
            string serialNumber;
            string lectureName;
            int grade;
            string professor;

            print.LectureSearchMenu(mode);

            while (true)
            {
                Console.SetCursorPosition(0, 8);
                print.PrintSentence(Constants.LECTURE_SEARCH_MENU[mode]);
                print.PrintLectureSearchMenuAndOption(mode);

                // 강의시간표 조회 혹은 관심과목 담기 모드
                // - 정보 수집
                Console.SetCursorPosition(0, 22);
                print.ClearCurrentConsoleLine();

                print.ColorMenu(Constants.PROFESSOR, 19, Constants.SELECT_DEPARTMENT + 1, 11);
                department = getValue.DropBox(21, 11, Constants.SELECT_DEPARTMENT); if (department == -1) return basket;
                print.ColorMenu(Constants.SELECT_DEPARTMENT + 1, 11, Constants.SERIAL_NUMBER, 13);
                serialNumber = getValue.Information(17, 13, Constants.SERIAL_NUMBER, 6); if (string.Compare(serialNumber, "@입력취소@") == 0) return basket;
                print.ColorMenu(Constants.SERIAL_NUMBER, 13, Constants.LECTURE_NAME, 15);
                lectureName = getValue.Information(17, 15, Constants.LECTURE_NAME, 10); if (string.Compare(lectureName, "@입력취소@") == 0) return basket;
                print.ColorMenu(Constants.LECTURE_NAME, 15, Constants.SELECT_GRADE + 6, 17);
                grade = getValue.DropBox(17, 17, Constants.SELECT_GRADE); if (grade == -1) return basket;
                print.ColorMenu(Constants.SELECT_GRADE + 6, 17, Constants.PROFESSOR, 19);
                professor = getValue.Information(17, 19, Constants.PROFESSOR, 8); if (string.Compare(professor, "@입력취소@") == 0) return basket;

                print.ColorMenu(Constants.PROFESSOR, 19, Constants.CHECK, 22);

                // 조건 검색
                searchedLecture = getValue.SearchLectureByCondition(mode, lectureSchedule, basket, department, serialNumber, lectureName, grade, professor);

                next = tools.EnterOrTab();
                if (next == Constants.ENTER && searchedLecture.Count != 0)
                {
                    Console.SetCursorPosition(0, 25);
                    print.ClearCurrentConsoleLine();
                    break;
                }
                if (next == Constants.ENTER && searchedLecture.Count == 0) print.ErrorMessage(Constants.ERROR_THERE_IS_NO_CLASS, 22);
                if (next == Constants.ESCAPE) return basket;
            }

            // 강의 출력
            print.SearchedLectureSchedule(mode, Constants.ALL, searchedLecture, department, serialNumber, lectureName, grade, professor);
            
            if (mode == Constants.LECTURE_SEARCH) { tools.WaitUntilGetEscapeKey(); return lectureSchedule; }
            else return PutLectureInBasketOrSignUpLecture(Constants.PUT_LECTURE_IN_BASKET, searchedLecture);
        }

        /// <summary>
        /// 수강 신청 기능을 담당하는 메소드입니다.
        /// </summary>
        /// <param name="lectureSchedule">강의 시간표</param>
        /// <param name="basket">관심과목으로 지정된 강의 리스트</param>
        /// <param name="enrolledLecture">수강신청된 강의 리스트</param>
        /// <returns>갱신된 수강신청 강의 리스트</returns>
        public List<ClassVO> SignUpLecture(List<ClassVO> lectureSchedule, List<ClassVO> basket, List<ClassVO> enrolledLecture)
        {
            List<ClassVO> searchedLecture = new List<ClassVO>();
            int next;
            string menu = "";
            int department = 0;
            string serialNumber = "0";
            string lectureName = "";
            int grade = 0;
            string professor = "";
            int searchMethod = -1;

            print.LectureSearchMenu(Constants.SIGN_UP_CLASS);

            while (true)
            {
                // 검색 기준 선택받음
                searchMethod = getValue.DropBox(22, 11, Constants.SIGN_UP_CLASS);
                if (searchMethod == -1) return enrolledLecture;

                print.ColorMenu("수강신청 검색 | ", 11, Constants.NONE, Constants.NONE);
                Console.SetCursorPosition(0, Console.CursorTop + 2);
                menu = getValue.MenuWord(searchMethod);

                // 검색 조건 정보 입력받음
                switch (searchMethod)
                {
                    case Constants.MAJOR:
                        print.ColorMenu(Constants.NONE, Constants.NONE, Constants.SELECT_DEPARTMENT + 1, Console.CursorTop);
                        department = getValue.DropBox(Console.CursorLeft, Console.CursorTop, Constants.SELECT_DEPARTMENT);
                        if (department == -1) return enrolledLecture;
                        break;
                    case Constants.NUMBER:
                        print.ColorMenu(Constants.NONE, Constants.NONE, Constants.SERIAL_NUMBER, Console.CursorTop);
                        serialNumber = getValue.Information(Console.CursorLeft, Console.CursorTop, Constants.SERIAL_NUMBER, 6);
                        if (string.Compare(serialNumber, "@입력취소@") == 0) return enrolledLecture;
                        break;
                    case Constants.NAME:
                        print.ColorMenu(Constants.NONE, Constants.NONE, Constants.LECTURE_NAME, Console.CursorTop);
                        lectureName = getValue.Information(Console.CursorLeft, Console.CursorTop, Constants.LECTURE_NAME, 10);
                        if (string.Compare(lectureName, "@입력취소@") == 0) return enrolledLecture;
                        break;
                    case Constants.YEAR:
                        print.ColorMenu(Constants.NONE, Constants.NONE, Constants.SELECT_GRADE + 6, Console.CursorTop);
                        grade = getValue.DropBox(Console.CursorLeft, Console.CursorTop, Constants.SELECT_GRADE);
                        if (grade == -1) return enrolledLecture;
                        break;
                    case Constants.PROFESSOR:
                        print.ColorMenu(Constants.NONE, Constants.NONE, Constants.PROFESSOR, Console.CursorTop);
                        professor = getValue.Information(Console.CursorLeft, Console.CursorTop, Constants.PROFESSOR, 8);
                        if (string.Compare(professor, "@입력취소@") == 0) return enrolledLecture;
                        break;                        
                }
                
                // 강의시간표 조회버튼 출력
                print.ColorMenu(menu, 13, Constants.CHECK, 16);

                // '관심과목 담기'로 검색하지 않는 경우 해당 조건에 따라 강의 검색
                searchedLecture = getValue.SearchLectureByCondition(Constants.SIGN_UP_CLASS, lectureSchedule, enrolledLecture, department, serialNumber, lectureName, grade, professor);

                // 다음단계 결정
                next = tools.EnterOrTab();

                Console.SetCursorPosition(0, 13);
                print.ClearCurrentConsoleLine();

                if (next == Constants.TAB) { Console.SetCursorPosition(0, 16); print.ClearCurrentConsoleLine(); }
                if ((next == Constants.ENTER && searchedLecture.Count == 0)) print.ErrorMessage(Constants.ERROR_THERE_IS_NO_CLASS, 16);
                else if (next == Constants.ENTER && searchMethod == Constants.BASKET && basket.Count == 0) print.ErrorMessage(Constants.ERROR_EMPTY_BASKET, 16);
                else if (next == Constants.ENTER) break;
                else if (next == Constants.ESCAPE) return enrolledLecture;
                else continue;
            }
            
            // '관심과목 담기'로 강의 시간표 검색할 경우
            if (searchMethod == Constants.BASKET)
            {
                print.SearchedLectureSchedule(Constants.SIGN_UP_CLASS, searchMethod, basket, department, serialNumber, lectureName, grade, professor);
                return PutLectureInBasketOrSignUpLecture(Constants.SIGN_UP_CLASS, basket);
            }
            
            print.SearchedLectureSchedule(Constants.SIGN_UP_CLASS, searchMethod, searchedLecture, department, serialNumber, lectureName, grade, professor);
            return PutLectureInBasketOrSignUpLecture(Constants.SIGN_UP_CLASS, searchedLecture);
        }

        /// <summary>
        /// 관심과목 담기 혹은 수강신청 기능을 수행하는 메소드입니다.
        /// </summary>
        /// <param name="mode">관심과목 담기 혹은 수강신청</param>
        /// <param name="searchedLecture">검색된 강의</param>
        /// <returns>관심과목으로 지정된 강의 혹은 수강신청된 강의 목록</returns>
        public List<ClassVO> PutLectureInBasketOrSignUpLecture(int mode, List<ClassVO> searchedLecture)
        {
            List<ClassVO> selectedLecture = new List<ClassVO>();
            int cursorTop;

            if (mode != Constants.SIGN_UP_CLASS) cursorTop = 12;
            else cursorTop = 9;

            Console.SetCursorPosition(2, cursorTop);
            Console.Write('▷');
            
            // 방향키 및 엔터, ESC키를 이용해 기능 수행
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                
                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(2, cursorTop, searchedLecture.Count, 1, '▷');                                 // 위로 커서 옮김
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(2, cursorTop, searchedLecture.Count, 1, '▷'); // 밑으로 커서 옮김
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(2, "▷"); return selectedLecture; }       // 나가기
                else if (keyInfo.Key == ConsoleKey.Enter)                                                                    // 해당 강의 선택
                {
                    if (tools.IsValidLecture(searchedLecture[Console.CursorTop - cursorTop], selectedLecture, mode))         // - 강의 선택 성공
                    {
                        selectedLecture.Add(searchedLecture[Console.CursorTop - cursorTop]);
                        print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.PUT);
                    }
                    else print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.FAIL);                     // - 강의 선택 실패
                }
                else print.BlockCursorMove(2, "▷");

                if (!tools.IsValidLecture(searchedLecture[Console.CursorTop - cursorTop], selectedLecture, mode))            // 입력 무시 
                    print.NonAvailableLectureMark(1, Console.CursorTop);
            }
        }

        /// <summary>
        /// 관심과목으로 담긴 강의를 관리하거나 수강신청된 강의를 관리하는 메소드입니다.
        /// </summary>
        /// <param name="mode">기능 모드</param>
        /// <param name="selectedLecture">선택된 강의</param>
        /// <returns>갱신된 선택된 강의</returns>
        public List<ClassVO> ManageSelectedLecture(int mode, List<ClassVO> selectedLecture)
        {
            ConsoleKeyInfo keyInfo;
            bool isFirstLoop = true;

            Console.SetWindowSize(160, 35);
            Console.Clear();

            print.SelectedLecture(mode, selectedLecture);

            if (selectedLecture.Count == 0) { print.PrintSentence("나가기(ESC)"); tools.WaitUntilGetEscapeKey(); return selectedLecture; }

            // 방향키 및 엔터, ESC키를 이용해 기능 수행
            while (true)
            {
                if (isFirstLoop)
                {
                    Console.SetCursorPosition(2, 9);
                    Console.Write('▷');
                    isFirstLoop = false;
                }

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(2, 9, selectedLecture.Count, 1, '▷');              // 위로 커서 옮김
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(2, 9, selectedLecture.Count, 1, '▷');     // 밑으로 커서 내림
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(2, "▷"); break; }                    // 나가기
                else if (keyInfo.Key == ConsoleKey.Enter)                                                                // 리스트에서 강의 삭제
                {
                    print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.DELETE);
                    selectedLecture.RemoveAt(Console.CursorTop - 9);
                    for (int count = 0; count < selectedLecture.Count + 1; count++) { Console.SetCursorPosition(0, 9 + count); print.ClearCurrentConsoleLine(); }
                    if (selectedLecture.Count != 0) print.Lectures(selectedLecture, 9);
                    else { print.PrintSentence("나가기(ESC)"); tools.WaitUntilGetEscapeKey(); return selectedLecture; }
                    isFirstLoop = true;
                }
                else print.BlockCursorMove(2, "▷");                                                                     // 입력 무시
            }

            return selectedLecture;
        }

        /// <summary>
        /// '내 시간표 관리'기능을 수행하는 메소드입니다.
        /// 시간표 보기, 시간표 저장, 시간표 관리 기능을 수행합니다.
        /// </summary>
        /// <param name="enrolledLecture">수강신청된 강의 리스트</param>
        /// <returns>갱신된 수강신청 강의 리스트</returns>
        public List<ClassVO> ManageEnrolledLecture(List<ClassVO> enrolledLecture) 
        {
            ConsoleKeyInfo keyInfo;
            bool isFirstLoop = true;

            while (true)
            {
                if (isFirstLoop)
                {
                    // 메뉴 출력
                    print.ManageEnrolledLectureMenu();

                    // 기능 선택
                    Console.SetCursorPosition(10, 12);
                    Console.Write('▷');

                    isFirstLoop = false;
                }

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(10, 12, 3, 3, '▷');            // 위로 커서 이동
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(10, 12, 3, 3, '▷');   // 밑으로 커서 이동
                else if (keyInfo.Key == ConsoleKey.Escape) return enrolledLecture;                   // 나가기
                else if (keyInfo.Key == ConsoleKey.Enter)                                            // 기능 선택
                { enrolledLecture = GoNextFunction((Console.CursorTop - 12) / 3, enrolledLecture); isFirstLoop = true; }
                else print.BlockCursorMove(10, "▷");                                                // 입력 무시
            }
        }

        /// <summary>
        /// '내 시간표 관리'에서 다음 기능을 선택하는 메소드입니다.
        /// </summary>
        /// <param name="cursorTop">커서 정보(들여쓰기)</param>
        /// <param name="enrolledLecture">수강신청된 강의 리스트</param>
        /// <returns>갱신된 수강신청 강의 리스트</returns>
        public List<ClassVO> GoNextFunction(int cursorTop, List<ClassVO> enrolledLecture)
        {
            switch (cursorTop)
            {
                case Constants.INQUIRE_MY_LECTURE_SCHEDULE:
                    print.MyLectureSchedule(enrolledLecture);
                    tools.WaitUntilGetEscapeKey();
                    return enrolledLecture;
                case Constants.SAVE_MY_LECTURE_SCHEDULE:
                    return SaveMyLectureSchedule(enrolledLecture);
                case Constants.MANAGE_MY_LECTURE_SCHEDULE:
                    return ManageSelectedLecture(Constants.MANAGE_MY_LECTURE_SCHEDULE, enrolledLecture);
                default:
                    return enrolledLecture;
            }
        }

        /// <summary>
        /// 수강신청된 강의를 엑셀파일로 저장하는 메소드입니다.
        /// </summary>
        /// <param name="enrolledLecture">수강신청된 강의 리스트</param>
        /// <returns>수강신청된 강의 리스트</returns>
        public List<ClassVO> SaveMyLectureSchedule(List<ClassVO> enrolledLecture)
        {
            string fileName;
            string[,] excelFile = new string[25, 6];

            print.SaveLectureIntoFileBackground();

            // 파일 이름 입력 받음
            fileName = getValue.Information(23, 11, Constants.FILE_NAME, 10);
            if (string.Compare(fileName, "@입력취소@") == 0) return enrolledLecture;

            // 수강신청된 강의 리스트를 배열로 정리
            excelFile = getValue.LectureInExcelForm(enrolledLecture, excelFile);

            // 엑셀 파일로 저장
            fileIOManager.CreateExcelFile(fileName, excelFile);

            Console.Write("끝내려면 엔터키를 누르세요 : ");
            tools.WaitUntilGetEnterOrEscapeKey();

            return enrolledLecture;
        }
     }
}
