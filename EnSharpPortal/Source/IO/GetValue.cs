using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnSharpPortal.Source.IO
{
    class GetValue
    {
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

        public bool[] TimeOfClass(string lectureTime)
        {
            bool[] timeOfClass = new bool[24];
            Array.Clear(timeOfClass, 0, timeOfClass.Length);

            int lectureStartTime, lecureEndTime;
            int indexToStartFillTrue, indexToFinish;

            for (int i = 0; i < lectureTime.Length; i++)
            {
                if (lectureTime[i] == '-')
                {
                    lectureStartTime = CharToInt(lectureTime[i - 5], lectureTime[i - 4]);
                    lecureEndTime = CharToInt(lectureTime[i + 1], lectureTime[i + 2]);
                    
                    indexToStartFillTrue = 2 * (lectureStartTime - 9);
                    if (lectureTime[i - 2] == '3') indexToStartFillTrue++;

                    indexToFinish = 2 * (lecureEndTime - 9);
                    if (lectureTime[i + 4] == '3') indexToFinish++;
                    
                    for (int index = indexToStartFillTrue; index < indexToFinish; index++)
                        timeOfClass[index] = true;
                }
            }

            return timeOfClass;
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
                    room2.Remove(0, i);

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
    }
}
