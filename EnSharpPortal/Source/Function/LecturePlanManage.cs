﻿using System;
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
        /// 강의 시간표 조회, 관심과목 담기, 수강신청 기능에서 사용됩니다.
        /// </summary>
        /// <param name="mode">기능</param>
        /// <param name="classes">강의 시간표</param>
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

            while(true)
            {
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

                next = getValue.EnterOrTab();
                if (next == Constants.ENTER)
                {
                    Console.SetCursorPosition(0, 25);
                    print.ClearCurrentConsoleLine();
                    break;
                }
                if (next == Constants.ESCAPE) return basket;
            }

            // - 조건 검색 후 관심과목 담기
            searchedLecture = getValue.SearchLectureByCondition(mode, lectureSchedule, basket, department, serialNumber, lectureName, grade, professor);
            print.SearchedLectureSchedule(mode, Constants.ALL, searchedLecture, department, serialNumber, lectureName, grade, professor);
            
            if (mode == Constants.LECTURE_SEARCH) return lectureSchedule;
            else return PutLectureInBasketOrSignUpLecture(Constants.PUT_LECTURE_IN_BASKET, searchedLecture);
        }

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
                searchMethod = getValue.DropBox(22, 11, Constants.SIGN_UP_CLASS);
                if (searchMethod == -1) return enrolledLecture;

                print.ColorMenu("수강신청 검색 | ", 11, Constants.NONE, Constants.NONE);
                Console.SetCursorPosition(0, Console.CursorTop + 2);
                menu = getValue.MenuWord(searchMethod);

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
                    case Constants.BASKET:
                        print.SearchedLectureSchedule(Constants.SIGN_UP_CLASS, searchMethod, basket, department, serialNumber, lectureName, grade, professor);
                        return PutLectureInBasketOrSignUpLecture(Constants.SIGN_UP_CLASS, basket);
                }
                
                print.ColorMenu(menu, 13, Constants.CHECK, 16);

                next = getValue.EnterOrTab();
                if (next == Constants.ENTER)
                {
                    Console.SetCursorPosition(0, 25);
                    print.ClearCurrentConsoleLine();
                    break;
                }
                if (next == Constants.ESCAPE) return enrolledLecture;
                if (next == Constants.TAB)
                {
                    Console.SetCursorPosition(0, 13);
                    print.ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, 16);
                    print.ClearCurrentConsoleLine();
                }
            }
            
            searchedLecture = getValue.SearchLectureByCondition(Constants.SIGN_UP_CLASS, lectureSchedule, enrolledLecture, department, serialNumber, lectureName, grade, professor);
            
            print.SearchedLectureSchedule(Constants.SIGN_UP_CLASS, searchMethod, searchedLecture, department, serialNumber, lectureName, grade, professor);
            return PutLectureInBasketOrSignUpLecture(Constants.SIGN_UP_CLASS, searchedLecture);
        }

        /// <summary>
        /// 관심과목을 담기 혹은 수강신청 기능을 수행하는 메소드입니다.
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
            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                
                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(2, cursorTop, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(2, cursorTop, searchedLecture.Count, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(2, "▷"); return selectedLecture; }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (getValue.IsValidLecture(searchedLecture[Console.CursorTop - cursorTop], selectedLecture, mode))
                    {
                        selectedLecture.Add(searchedLecture[Console.CursorTop - cursorTop]);
                        print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.PUT);
                    }
                    else print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.FAIL);
                }
                else print.BlockCursorMove(2, "▷");

                if (!getValue.IsValidLecture(searchedLecture[Console.CursorTop - cursorTop], selectedLecture, mode))
                    print.NonAvailableLectureMark(1, Console.CursorTop);
            }
        }

        /// <summary>
        /// 관심과목으로 담은 강의 리스트를 보여주고, 삭제하는 기능을 가진 메소드입니다.
        /// </summary>
        /// <param name="basket">관심과목에 담은 강의 리스트</param>
        /// <returns>갱신된 관심과목 강의 리스트</returns>
        public List<ClassVO> ManageSelectedLecture(int mode, List<ClassVO> selectedLecture)
        {
            ConsoleKeyInfo keyInfo;
            bool isFirstLoop = true;

            Console.SetWindowSize(160, 35);
            Console.Clear();

            print.SelectedLecture(mode, selectedLecture);

            while (true)
            {
                if (isFirstLoop)
                {
                    Console.SetCursorPosition(2, 9);
                    Console.Write('▷');
                    isFirstLoop = false;
                }

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(2, 9, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(2, 9, selectedLecture.Count, 1, '▷');
                else if (keyInfo.Key == ConsoleKey.Escape) { print.BlockCursorMove(2, "▷"); break; }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    print.CompletePutOrDeleteLectureInBasket(1, Console.CursorTop, Constants.DELETE);
                    for (int count = 0; count < selectedLecture.Count; count++) { Console.SetCursorPosition(0, 9 + count); print.ClearCurrentConsoleLine(); }
                    selectedLecture.RemoveAt(Console.CursorTop - 10);
                    print.Lectures(selectedLecture, 9);
                    isFirstLoop = true;
                }
                else print.BlockCursorMove(2, "▷");
            }

            return selectedLecture;
        }

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

                if (keyInfo.Key == ConsoleKey.UpArrow) tools.UpArrow(10, 12, 3, '▷');
                else if (keyInfo.Key == ConsoleKey.DownArrow) tools.DownArrow(10, 12, 3, 3, '▷');
                else if (keyInfo.Key == ConsoleKey.Escape) return enrolledLecture;
                else if (keyInfo.Key == ConsoleKey.Enter) { enrolledLecture = GoNextFunction((Console.CursorTop - 12) / 3, enrolledLecture); isFirstLoop = true; }
                else print.BlockCursorMove(10, "▷");
            }
        }

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

        public List<ClassVO> SaveMyLectureSchedule(List<ClassVO> enrolledLecture)
        {
            string fileName;
            string[,] excelFile = new string[25, 6];

            print.SaveLectureIntoFileBackground();

            fileName = getValue.Information(23, 11, Constants.FILE_NAME, 10);
            if (string.Compare(fileName, "@입력취소@") == 0) return enrolledLecture;

            excelFile = getValue.LectureInExcelForm(enrolledLecture, excelFile);

            fileIOManager.CreateExcelFile(fileName, excelFile);

            Console.Write("끝내려면 엔터키를 누르세요 : ");
            tools.WaitUntilGetEscapeKey();

            return enrolledLecture;
        }
     }
}
