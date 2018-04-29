using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;
using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.IO
{
    class GetValue
    {
        Print print = new Print();
        DataManager dataManager = new DataManager();

        public int Year(string date)
        {
            StringBuilder year = new StringBuilder(date);
            year.Remove(4, 4);

            return Int32.Parse(year.ToString());
        }

        public int Month(string date)
        {
            StringBuilder month = new StringBuilder(date);
            month.Remove(6, 2);
            month.Remove(0, 4);
            
            return Int32.Parse(month.ToString());
        }

        public int Day(string date)
        {
            StringBuilder day = new StringBuilder(date);
            day.Remove(0, 6);

            return Int32.Parse(day.ToString());
        }

        public int CourseDivision(string courseDivision)
        {
            switch (courseDivision)
            {
                case "전공선택":
                    return Data.Constants.MAJOR_SELECTION;
                case "전공필수":
                    return Data.Constants.MAJOR_ESSENTIAL;
                case "중핵필수":
                    return Data.Constants.CORE_ESSENTIAL;
            }

            return 0;
        }

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

        public int CountOfDayOfWeek(string lectureTime)
        {
            int count = 0;
            string pattern = "[가-힣]";

            foreach (char day in lectureTime)
                if (System.Text.RegularExpressions.Regex.IsMatch(day.ToString(), pattern)) count++;

            return count;
        }

        public string[,] LectureInExcelForm(List<ClassVO> enrolledLecture, string[,] excelFile)
        {
            excelFile.Initialize();
            for (int column = 1; column <= 5; column++) excelFile[0, column] = Constants.DAYS[column - 1];
            for (int row = 1; row <= 24; row++) excelFile[row, 0] = Constants.TIMES[row - 1];

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

        public bool[] SetTimeInArray(string lectureTime, int index)
        {
            bool[] timeOfClass = new bool[24];
            Array.Clear(timeOfClass, 0, timeOfClass.Length);

            int lectureStartTime, lecureEndTime;
            int indexToStartFillTrue, indexToFinish;

            lectureStartTime = CharToInt(lectureTime[index - 5], lectureTime[index - 4]);
            lecureEndTime = CharToInt(lectureTime[index + 1], lectureTime[index + 2]);

            indexToStartFillTrue = 2 * (lectureStartTime - 9);
            if (lectureTime[index - 2] == '3') indexToStartFillTrue++;

            indexToFinish = 2 * (lecureEndTime - 9);
            if (lectureTime[index + 4] == '3') indexToFinish++;

            for (int i = indexToStartFillTrue; i < indexToFinish; i++)
                timeOfClass[i] = true;

            return timeOfClass;
        }

        public bool[,] CopyArray(bool[] sourceArray, bool[,] destinationArray, int destination)
        {
            for (int index = 0; index < sourceArray.Length; index++)
                destinationArray[destination, index] = sourceArray[index];

            return destinationArray;
        } 

        public int CharToInt(char letter1, char letter2)
        {
            int numberToReturn;
            StringBuilder numbers = new StringBuilder(letter1.ToString());
            numbers.Append(letter2.ToString());
            
            numberToReturn = Int32.Parse(numbers.ToString());

            return numberToReturn;
        }

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

        public int LectureLanguage(string lectureLanguage)
        {
            if (string.Compare(lectureLanguage, "한국어") == 0)
                return Data.Constants.KOREAN;
            else return Data.Constants.ENGLISH;
        }

        /// <summary>
        /// 드롭박스에서 원하는 옵션을 선택하는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        /// <param name="mode">사용자가 선택한 검색 모드</param>
        /// <returns>사용자가 선택한 옵션</returns>
        public int DropBox(int cursorLeft, int cursorTop, int mode)
        {
            ConsoleKeyInfo keyInfo;

            int index = 0;
            string[] option;

            if (mode == Constants.SELECT_DEPARTMENT) option = Constants.DEPARTMENT;
            else if (mode == Constants.SELECT_GRADE) option = Constants.GRADE;
            else option = Constants.SIGN_UP_CLASSES_SELECTION;

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
                    case ConsoleKey.DownArrow:
                        if (index == option.Length - 1) index = 0;
                        else index++;
                        break;
                    default:
                        keyInfo = Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// 사용자가 검색창에 입력한 값을 반환해주는 메소드입니다.
        /// </summary>
        /// <param name="cursorLeft">커서 설정 변수(들여쓰기)</param>
        /// <param name="cursorTop">커서 설정 변수(줄)</param>
        /// <param name="mode">검색 모드</param>
        /// <returns>사용자가 입력한 검색어</returns>
        public string Information(int cursorLeft, int cursorTop, int mode, int limit)
        {
            int currentCursor;
            bool isValid = false;
            StringBuilder answer = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            Console.SetCursorPosition(cursorLeft, cursorTop);

            while (true)
            {
                currentCursor = Console.CursorLeft;
                keyInfo = Console.ReadKey();

                isValid = IsValid(keyInfo, mode);

                if (answer.Length == 0) print.DeleteGuideLine(cursorLeft, isValid, keyInfo);

                if (keyInfo.Key == ConsoleKey.Escape) return "@입력취소@";
                else if (keyInfo.Key == ConsoleKey.Backspace) answer = BackspaceInput(cursorLeft, cursorTop, answer);
                else if (isValid) answer = ValidInput(currentCursor, limit, keyInfo.KeyChar, answer);
                else if (keyInfo.Key != ConsoleKey.Enter) print.InvalidInput(currentCursor, cursorTop);
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (answer.Length == 0 && mode == Data.Constants.SERIAL_NUMBER) return "0"; 
                    else return answer.ToString();
                }

                if (answer.Length == 0) print.SearchGuideline(Data.Constants.SEARCHING_MENU[mode], cursorLeft, cursorTop);
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
        public List<ClassVO> SearchLectureByCondition(List<ClassVO> classes, int department, string serialNumber, string lectureName, int grade, string professor)
        {
            classes = ClearancedClasses(classes, department, grade);
            classes = ClearancedClasses(classes, serialNumber, Constants.SERIAL_NUMBER);
            classes = ClearancedClasses(classes, lectureName, Constants.LECTURE_NAME);
            classes = ClearancedClasses(classes, professor, Constants.PROFESSOR);

            return classes;
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
        /// 사용자가 입력한 키가 숫자인지 검사하는 메소드입니다.
        /// </summary>
        /// <param name="keyInfo">사용자가 입력한 키</param>
        /// <returns>검색어 숫자 여부</returns>
        public bool IsNumber(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9) return true;
            else return false;
        }

        // 수정 필요, 입력값이 올바른지 학인하는 것
        public bool IsValid(ConsoleKeyInfo keyInfo, int mode)
        {
            if (mode == Data.Constants.SERIAL_NUMBER) return IsNumber(keyInfo);
            if (keyInfo.Key == ConsoleKey.Enter) return false;
            return true;
        }

        /// <summary>
        /// 관심과목 담기 혹은 수강신청시 신청하고자 하는 과목이 유효한지의 여부를 검사해주는 메소드입니다.
        /// </summary>
        /// <param name="lecture">검사하고 싶은 수업</param>
        /// <param name="classes">신청한 수업들</param>
        /// <returns>신청 과목 유효 여부</returns>
        public bool IsValidLecture(ClassVO lecture, List<ClassVO> classes, int mode)
        {
            float sumOfCredit = 0;

            if (classes.Count == 0) return true;

            foreach (ClassVO selectedClass in classes)
            {
                sumOfCredit += selectedClass.Credit;
                if (lecture.SerialNumber == selectedClass.SerialNumber) return false;
                if (IsOverLapClass(lecture, selectedClass)) return false;
            }

            if (mode != Constants.SIGN_UP_CLASS && sumOfCredit > 24) return false;
            if (mode == Constants.SIGN_UP_CLASS && sumOfCredit > 21) return false;

            return true;
        }

        /// <summary>
        /// 강의1과 강의2의 시간이 일치하는지 여부를 검사합니다.
        /// </summary>
        /// <param name="class1"></param>
        /// <param name="class2"></param>
        /// <returns></returns>
        public bool IsOverLapClass(ClassVO class1, ClassVO class2)
        {
            for (int row = 0; row < class1.TimeOfClass.GetLength(0); row++)
                for (int column = 0; column < class1.TimeOfClass.GetLength(1); column++)
                    if (class1.TimeOfClass[row, column] == class2.TimeOfClass[row, column] && class1.TimeOfClass[row, column] == true)
                        return true;
            return false;
        }

        
    }
}
