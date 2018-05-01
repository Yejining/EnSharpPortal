using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;
using EnSharpPortal.Source.Data;
using EnSharpPortal.Source.Function;

namespace EnSharpPortal.Source.IO
{
    class GetValue
    {
        Print print = new Print();
        Tools tools = new Tools();

        /// <summary>
        /// 생년월일로부터 년도를 구하는 메소드입니다.
        /// </summary>
        /// <param name="date">생년월일</param>
        /// <returns>년도</returns>
        public int Year(string date)
        {
            StringBuilder year = new StringBuilder(date);
            year.Remove(4, 4);

            return Int32.Parse(year.ToString());
        }

        /// <summary>
        /// 생년월일로부터 월을 구하는 메소드입니다.
        /// </summary>
        /// <param name="date">생년월일</param>
        /// <returns>월</returns>
        public int Month(string date)
        {
            StringBuilder month = new StringBuilder(date);
            month.Remove(6, 2);
            month.Remove(0, 4);
            
            return Int32.Parse(month.ToString());
        }

        /// <summary>
        /// 생년월일로부터 일을 구하는 메소드입니다.
        /// </summary>
        /// <param name="date">생년월일</param>
        /// <returns>일</returns>
        public int Day(string date)
        {
            StringBuilder day = new StringBuilder(date);
            day.Remove(0, 6);

            return Int32.Parse(day.ToString());
        }

        /// <summary>
        /// string형의 이수구분을 int형으로 변환해 반환하는 메소드입니다.
        /// </summary>
        /// <param name="courseDivision">이수구분</param>
        /// <returns>이수구분</returns>
        public int CourseDivision(string courseDivision)
        {
            switch (courseDivision)
            {
                case "전공선택":
                    return Constants.MAJOR_SELECTION;
                case "전공필수":
                    return Constants.MAJOR_ESSENTIAL;
                case "중핵필수":
                    return Constants.CORE_ESSENTIAL;
            }

            return 0;
        }

        /// <summary>
        /// string형의 요일 및 강의시간에서 요일 정보만 추출하는 메소드입니다.
        /// </summary>
        /// <param name="lectureTime">요일 및 강의시간</param>
        /// <returns>강의 요일 정보</returns>
        public List<int> DaysOfClass(string lectureTime)
        {
            List<int> daysOfClass = new List<int>();

            foreach (char day in lectureTime)
            {
                switch (day)
                {
                    case '월':
                        daysOfClass.Add(Data.Constants.MONDAY);
                        break;
                    case '화':
                        daysOfClass.Add(Data.Constants.TUESDAY);
                        break;
                    case '수':
                        daysOfClass.Add(Data.Constants.WEDNESDAY);
                        break;
                    case '목':
                        daysOfClass.Add(Data.Constants.THURSDAY);
                        break;
                    case '금':
                        daysOfClass.Add(Data.Constants.FRIDAY);
                        break;
                    default:
                        break;
                }
            }

            return daysOfClass;
        }

        /// <summary>
        /// string형태의 요일 및 강의시간을 배열 형식으로 변환해 반환하는 메소드입니다.
        /// </summary>
        /// <param name="lectureTime">요일 및 강의시간</param>
        /// <returns>배열 형태의 강의 시간</returns>
        public bool[,] TimeOfClass(string lectureTime)
        {
            bool[,] schedule = new bool[5, 24];
            bool[] timeOfClass = new bool[24];
            bool[] practiceClass = new bool[24];
            Array.Clear(schedule, 0, schedule.Length);

            int turn = 0;
            char day = 'a';

            // timeOfClass 배열에 저장
            for (int i = 0; i < lectureTime.Length; i++)
            {
                if (lectureTime[i] == '-')
                {
                    turn++;

                    if (turn == 1) timeOfClass = SetTimeInArray(lectureTime, i);
                    if (turn == 2)
                    {
                        day = lectureTime[i - 6];
                        practiceClass = SetTimeInArray(lectureTime, i);
                    }
                }
            }

            // 해당 요일에 시간표 저장
            for (int i = 0; i < 2; i++)
            {
                if (lectureTime[i] == '월') schedule = CopyArray(timeOfClass, schedule, 0);
                else if (lectureTime[i] == '화') schedule = CopyArray(timeOfClass, schedule, 1);
                else if (lectureTime[i] == '수') schedule = CopyArray(timeOfClass, schedule, 2);
                else if (lectureTime[i] == '목') schedule = CopyArray(timeOfClass, schedule, 3);
                else if (lectureTime[i] == '금') schedule = CopyArray(timeOfClass, schedule, 4);
            }

            if (turn == 2) schedule = AppendArray(practiceClass, schedule, day);

            return schedule;
        }

        /// <summary>
        /// 배열에 강의시간 정보를 저장하는 메소드입니다.
        /// </summary>
        /// <param name="lectureTime">강의 시간 및 요일</param>
        /// <param name="index">강의 시간 및 요일에서 '-' 문자가 나오는 인덱스</param>
        /// <returns>강의시간이 저장된 배열</returns>
        public bool[] SetTimeInArray(string lectureTime, int index)
        {
            bool[] timeOfClass = new bool[24];
            Array.Clear(timeOfClass, 0, timeOfClass.Length);

            int lectureStartTime, lecureEndTime;
            int indexToStartFillTrue, indexToFinish;

            // 강의 시작시간, 끝나는 시간 저장
            lectureStartTime = CharToInt(lectureTime[index - 5], lectureTime[index - 4]);
            lecureEndTime = CharToInt(lectureTime[index + 1], lectureTime[index + 2]);

            // 배열에서 저장 위치의 시작점 구함
            indexToStartFillTrue = 2 * (lectureStartTime - 9);
            if (lectureTime[index - 2] == '3') indexToStartFillTrue++;

            // 배열에서 저장 위치의 끝점 구함
            indexToFinish = 2 * (lecureEndTime - 9);
            if (lectureTime[index + 4] == '3') indexToFinish++;

            // 배열에 저장
            for (int i = indexToStartFillTrue; i < indexToFinish; i++)
                timeOfClass[i] = true;

            return timeOfClass;
        }

        /// <summary>
        /// 배열을 복사하는 메소드입니다.
        /// </summary>
        /// <param name="sourceArray">복사할 배열</param>
        /// <param name="destinationArray">복사될 배열</param>
        /// <param name="destination">복사 위치</param>
        /// <returns>복사된 배열</returns>
        public bool[,] CopyArray(bool[] sourceArray, bool[,] destinationArray, int destination)
        {
            for (int index = 0; index < sourceArray.Length; index++)
                destinationArray[destination, index] = sourceArray[index];

            return destinationArray;
        }

        /// <summary>
        /// 실습시간이 있는 강의의 경우 실습시간을 시간표 배열에 추가하는 메소드입니다.
        /// </summary>
        /// <param name="sourceArray">실습시간 정보가 담긴 배열</param>
        /// <param name="destinationArray">정보를 저장할 배열</param>
        /// <param name="dayOfWeek">실습시간 요일</param>
        /// <returns></returns>
        public bool[,] AppendArray(bool[] sourceArray, bool[,] destinationArray, char dayOfWeek)
        {
            int destination = -1;

            if (dayOfWeek == '월') destination = 0;
            else if (dayOfWeek == '화') destination = 1;
            else if (dayOfWeek == '수') destination = 2;
            else if (dayOfWeek == '목') destination = 3;
            else if (dayOfWeek == '금') destination = 4;

            for (int index = 0; index < sourceArray.Length; index++)
                if (sourceArray[index]) destinationArray[destination, index] = sourceArray[index];

            return destinationArray;
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

        /// <summary>
        /// 문자 2개를 이어붙인 후 숫자로 변환해 반환하는 메소드입니다.
        /// </summary>
        /// <param name="letter1">문자1</param>
        /// <param name="letter2">문자2</param>
        /// <returns>숫자</returns>
        public int CharToInt(char letter1, char letter2)
        {
            int numberToReturn;
            StringBuilder numbers = new StringBuilder(letter1.ToString());
            numbers.Append(letter2.ToString());

            numberToReturn = Int32.Parse(numbers.ToString());

            return numberToReturn;
        }

        /// <summary>
        /// 강의실 문자열에서 각각의 강의실을 추출해 리스트로 만드는 메소드입니다.
        /// </summary>
        /// <param name="classroom">강의실</param>
        /// <returns>강의실 리스트</returns>
        public List<string> ClassRoom(string classroom)
        {
            List<string> classRoom = new List<string>();
            StringBuilder room1 = new StringBuilder(classroom);
            StringBuilder room2 = new StringBuilder(classroom);

            for (int i = 0; i < classroom.Length; i++)
            {
                if (classroom[i] == '/')
                {
                    room1.Remove(i, classroom.Length - i);
                    room2.Remove(0, i + 1);

                    classRoom.Add(room1.ToString());
                    classRoom.Add(room2.ToString());

                    return classRoom;
                }
            }

            classRoom.Add(room1.ToString());
            return classRoom;
        }

        /// <summary>
        /// string 형태의 강의언어를 int형으로 변환하는 메소드입니다.
        /// </summary>
        /// <param name="lectureLanguage">강의언어</param>
        /// <returns>강의언어</returns>
        public int LectureLanguage(string lectureLanguage)
        {
            if (string.Compare(lectureLanguage, "한국어") == 0)
                return Constants.KOREAN;
            else return Constants.ENGLISH;
        }

        /// <summary>
        /// 드롭박스에서 원하는 옵션을 선택하는 메소드입니다.
        /// </summary>
        /// <param name="mode">사용자가 선택한 검색 모드</param>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>       
        /// <returns>사용자가 선택한 옵션</returns>
        public int DropBox(int cursorLeft, int cursorTop, int mode)
        {
            ConsoleKeyInfo keyInfo;

            int index = 0;
            string[] option;
            
            // 드롭박스 선택
            if (mode == Constants.SELECT_DEPARTMENT) option = Constants.DEPARTMENT;
            else if (mode == Constants.SELECT_GRADE) option = Constants.GRADE;
            else option = Constants.SIGN_UP_CLASSES_SELECTION;

            // 방향키 및 엔터, ESC키 통해 정보 선택 혹은 나가기
            while (true)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write(option[index]);
                
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        return -1;
                    case ConsoleKey.Enter:
                        if (mode == Constants.SIGN_UP_CLASS) print.Category(6, cursorTop + 2, index);
                        return index;
                    case ConsoleKey.Tab:
                        if (mode == Constants.SIGN_UP_CLASS) print.Category(6, cursorTop + 2, index);
                        return index;
                    case ConsoleKey.UpArrow:
                        if (index == 0) index = option.Length - 1;
                        else index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index == option.Length - 1) index = 0;
                        else index++;
                        break;
                    default:
                        print.BlockCursorMove(cursorLeft + 4, "");
                        keyInfo = Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// 사용자가 강의 검색방법을 선택하면 해당 검색방법을 문자열로 반환해주는 메소드입니다.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public string MenuWord(int mode)
        {
            switch (mode)
            {
                case Constants.MAJOR:
                    return Constants.SEARCHING_MENU[0];
                case Constants.NUMBER:
                    return Constants.SEARCHING_MENU[2];
                case Constants.NAME:
                    return Constants.SEARCHING_MENU[4];
                case Constants.YEAR:
                    return Constants.SEARCHING_MENU[6];
                case Constants.PROFESSOR:
                    return Constants.SEARCHING_MENU[8];
                default:
                    return "";
            }
        }

        /// <summary>
        /// 사용자가 검색창에 입력한 값을 반환해주는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        /// <param name="mode">검색 모드</param>
        /// <param name="limit">검색 입력 최대 길이</param>
        /// <returns>사용자가 입력한 검색어</returns>
        public string Information(int cursorLeft, int cursorTop, int mode, int limit)
        {
            int currentCursor = 0;
            bool isValid = false;
            StringBuilder answer = new StringBuilder();
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            // 질문 출력
            print.SearchGuideline(Constants.SEARCHING_MENU[mode], cursorLeft, cursorTop);
            Console.SetCursorPosition(cursorLeft, cursorTop);

            while (true)
            {
                currentCursor = Console.CursorLeft;
                keyInfo = Console.ReadKey();

                // 키값이 유효한지 검사
                isValid = tools.IsValid(keyInfo, mode);

                if (answer.Length == 0) print.DeleteGuideLine(cursorLeft, isValid, keyInfo);

                if (keyInfo.Key == ConsoleKey.Escape) return "@입력취소@";                                              // 나가기
                else if (keyInfo.Key == ConsoleKey.Backspace) answer = BackspaceInput(cursorLeft, cursorTop, answer);   // 지우기
                else if (isValid) answer = ValidInput(currentCursor, limit, keyInfo.KeyChar, answer);                   // 올바른 입력
                else if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key!= ConsoleKey.Tab) print.InvalidInput(keyInfo, currentCursor, cursorTop); // 입력 무시
                else if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Tab)                              // 검색 완료
                {
                    if (answer.Length == 0) print.SearchGuideline(Constants.SEARCHING_MENU[mode], cursorLeft, cursorTop);
                    if (answer.Length == 0 && mode == Constants.SERIAL_NUMBER) return "0";
                    else return answer.ToString();
                }

                // 검색어 글자가 0자일 경우 가이드라인 출력
                if (answer.Length == 0)
                {
                    if (mode == Constants.FILE_NAME) print.SearchGuideline("10자 이내 문자, 숫자", cursorLeft, cursorTop);
                    else print.SearchGuideline(Constants.SEARCHING_MENU[mode], cursorLeft, cursorTop);
                }
            }
        }

        /// <summary>
        /// 사용자가 검색창 입력시 백스페이스 키를 누르면 한글자 지우기를 실행한 후
        /// 그동안 입력한 검색어를 반환하는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        /// <param name="answer">사용자가 입력한 검색어</param>
        /// <returns>지우기가 실행된 사용자가 입력한 검색어</returns>
        public StringBuilder BackspaceInput(int cursorLeft, int cursorTop, StringBuilder answer)
        {
            if (answer.Length > 0)
            {
                answer.Remove(answer.Length - 1, 1);
                Console.SetCursorPosition(cursorLeft, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
                Console.SetCursorPosition(cursorLeft, cursorTop);
                if (answer.Length != 0) Console.Write(answer);
            }
            else if (answer.Length == 0) Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);

            return answer;
        }

        /// <summary>
        /// 사용자가 검색창 입력시 유효한 문자를 입력하면
        /// 검색어 배열에 문자를 추가하는 메소드입니다.
        /// </summary>
        /// <param name="currentCursor">커서 설정 변수(들여쓰기)</param>
        /// <param name="limit">글자 제한 수</param>
        /// <param name="userInputLetter">사용자가 입력한 문자</param>
        /// <param name="answer">사용자가 입력한 검색어</param>
        /// <returns>갱신된 사용자가 입력한 검색어</returns>
        public StringBuilder ValidInput(int currentCursor, int limit, char userInputLetter, StringBuilder answer)
        {
            if (answer.Length < limit) answer.Append(userInputLetter);
            else
            {
                Console.SetCursorPosition(currentCursor, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(currentCursor, Console.CursorTop);
            }

            return answer;
        }

        /// <summary>
        /// 사용자가 검색조건에 따라 검색한 강의 시간표들 반환하는 메소드입니다.
        /// </summary>
        /// <param name="department">사용자가 선택한 개설학과전공</param>
        /// <param name="serialNumber">사용자가 입력한 학수번호</param>
        /// <param name="lectureName">사용자가 입력한 강의명</param>
        /// <param name="grade">사용자가 선택한 학년</param>
        /// <param name="professor">사용자가 입력한 교수명</param>
        /// <returns>검색된 강의 시간표</returns>
        public List<ClassVO> SearchLectureByCondition(int mode, List<ClassVO> lectureSchedule, List<ClassVO> selectedLecture, int department, string serialNumber, string lectureName, int grade, string professor)
        {
            List<ClassVO> searchedLecture = new List<ClassVO>();

            searchedLecture = ClearancedClasses(lectureSchedule, department, grade);
            searchedLecture = ClearancedClasses(searchedLecture, serialNumber, Constants.SERIAL_NUMBER);
            searchedLecture = ClearancedClasses(searchedLecture, lectureName, Constants.LECTURE_NAME);
            searchedLecture = ClearancedClasses(searchedLecture, professor, Constants.PROFESSOR);

            if (mode == Constants.LECTURE_SEARCH) return searchedLecture;
            else return ClearancedClasses(searchedLecture, selectedLecture);
        }

        /// <summary>
        /// 강의 리스트에서 검색어와 일치하지 않는 강의를 제거한 후 반환하는 메소드입니다.
        /// </summary>
        /// <param name="classes">강의 리스트</param>
        /// <param name="department">사용자가 선택한 개설학과전공</param>
        /// <param name="grade">사용자가 선택한 학년</param>
        /// <returns>정리된 강의 리스트</returns>
        public List<ClassVO> ClearancedClasses(List<ClassVO> classes, int department, int grade)
        {
            List<int> indexesToDelete = new List<int>();

            // 개설전공학과 검색
            if (department != 0)
            {
                for (int index = classes.Count - 1; index >= 0; index--)
                    if (string.Compare(classes[index].Department, Constants.DEPARTMENT[department]) != 0)
                        indexesToDelete.Add(index);
                for (int delete = 0; delete < indexesToDelete.Count; delete++) classes.RemoveAt(indexesToDelete[delete]);
                indexesToDelete.Clear();
            }

            // 학년 검색
            if (grade != 0)
            {
                for (int index = classes.Count - 1; index >= 0; index--)
                    if (classes[index].Grade != grade) indexesToDelete.Add(index);
                for (int delete = 0; delete < indexesToDelete.Count; delete++) classes.RemoveAt(indexesToDelete[delete]);
                indexesToDelete.Clear();
            }

            return classes;
        }

        /// <summary>
        /// 강의 리스트에서 선택된 강의들을 지우는 메소드입니다.
        /// </summary>
        /// <param name="lectureSchedule">강의 리스트</param>
        /// <param name="selectedLecture">선택된 강의 리스트</param>
        /// <returns>정리된 강의 리스트</returns>
        public List<ClassVO> ClearancedClasses(List<ClassVO> lectureSchedule, List<ClassVO> selectedLecture)
        {
            List<int> indexesToDelete = new List<int>();

            for (int lectureIndex = lectureSchedule.Count - 1; lectureIndex >= 0; lectureIndex--)
                foreach (ClassVO selected in selectedLecture)
                    if (string.Compare(lectureSchedule[lectureIndex].SerialNumber, selected.SerialNumber) == 0 &&
                        string.Compare(lectureSchedule[lectureIndex].DivisionClassNumber, selected.DivisionClassNumber) == 0)
                        indexesToDelete.Add(lectureIndex);

            for (int delete = 0; delete < indexesToDelete.Count; delete++) lectureSchedule.RemoveAt(indexesToDelete[delete]);

            return lectureSchedule;
        }

        /// <summary>
        /// 강의 리스트에서 검색어와 일치하지 않는 강의를 제거한 후 반환하는 메소드입니다.
        /// </summary>
        /// <param name="classes">강의 리스트</param>
        /// <param name="searchWord">검색어</param>
        /// <param name="mode">검색 모드</param>
        /// <returns>정리된 강의 리스트</returns>
        public List<ClassVO> ClearancedClasses(List<ClassVO> classes, string searchWord, int mode)
        {
            string searchTarget;
            List<int> indexesToDelete = new List<int>();

            if (searchWord.Length != 0)
            {
                for (int index = classes.Count - 1; index >= 0; index--)
                {
                    if (mode == Constants.SERIAL_NUMBER) searchTarget = classes[index].SerialNumber;
                    else if (mode == Constants.LECTURE_NAME) searchTarget = classes[index].LectureName;
                    else searchTarget = classes[index].Professor;

                    if (!System.Text.RegularExpressions.Regex.IsMatch(searchTarget, searchWord, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        indexesToDelete.Add(index);
                }
                for (int delete = 0; delete < indexesToDelete.Count; delete++) classes.RemoveAt(indexesToDelete[delete]);
            }

            return classes;
        }

        /// <summary>
        /// 수강신청된 강의를 엑셀파일 형식으로 저장하는 메소드입니다.
        /// </summary>
        /// <param name="enrolledLecture">수강신청된 강의</param>
        /// <param name="excelFile">저장할 배열</param>
        /// <returns>엑셀형식으로 저장된 수강신청 배열</returns>
        public string[,] LectureInExcelForm(List<ClassVO> enrolledLecture, string[,] excelFile)
        {
            excelFile.Initialize();
            for (int column = 1; column <= 5; column++) excelFile[0, column] = Constants.DAYS[column - 1];
            for (int row = 1; row <= 24; row++) excelFile[row, 0] = Constants.TIMES[row - 1];

            // 각 강의마다 배열에 저장
            foreach (ClassVO lecture in enrolledLecture)
                for (int row = 0; row < 5; row++)
                    for (int column = 0; column < 24; column++)
                        if (lecture.TimeOfClass[row, column])
                        {
                            if (CountOfDayOfWeek(lecture.LectureSchedule) == 3 && column >= 18)
                                excelFile[column + 1, row + 1] = lecture.LectureName + "(" + lecture.ClassRooms[1] + ")";
                            else excelFile[column + 1, row + 1] = lecture.LectureName + "(" + lecture.ClassRooms[0] + ")";
                        }

            return excelFile;
        }
    }
}
