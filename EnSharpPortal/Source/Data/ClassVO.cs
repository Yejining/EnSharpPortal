using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;
using EnSharpPortal.Source.IO;

namespace EnSharpPortal.Source.Data
{
    class ClassVO
    {
        FileIOManager fileIOManager = new FileIOManager();
        
        int number;                             // 강의 번호
        private string department;              // 개설학과전공
        private string serialNumber;            // 학수번호
        private string divisionClassNumber;     // 분반
        private string lectureName;             // 교과목명
        private string courseDivision;          // 이수구분
        private int grade;                      // 학년
        private float credit;                   // 학점
        private string lectureSchedule;         // 요일 및 강의시간
        private bool[,] timeOfClass;            // 강의시간
        private string classRoom;               // 강의실
        private List<string> classRooms;        // 강의실
        private string professor;               // 교수명
        private string lectureLanguage;         // 강의언어

        /// <summary>
        /// ClassVO의 생성자 메소드입니다.
        /// </summary>
        public ClassVO()
        {

        }

        /// <summary>
        /// ClassVO의 생성자 메소드입니다.
        /// 변수들을 파라미터로 받은 변수들로 설정해줍니다.
        /// </summary>
        /// <param name="number">강의 번호</param>
        /// <param name="department">개설학과전공</param>
        /// <param name="serialNumber">학수번호</param>
        /// <param name="divisionClassNumber">분반</param>
        /// <param name="lectureName">강의명</param>
        /// <param name="courseDivision">이수구분</param>
        /// <param name="grade">학년</param>
        /// <param name="credit">학점</param>
        /// <param name="lectureTime">요일 및 강의시간</param>
        /// <param name="classRoom">강의실</param>
        /// <param name="professor">교수명</param>
        /// <param name="lectureLanguage">강의언어</param>
        public ClassVO(int number, string department, string serialNumber, string divisionClassNumber, string lectureName, string courseDivision, int grade,
            float credit, string lectureTime, string classRoom, string professor, string lectureLanguage)
        {
            GetValue getValue = new GetValue();

            this.number = number;
            this.department = department;
            this.serialNumber = serialNumber;
            this.divisionClassNumber = divisionClassNumber;
            this.lectureName = lectureName;
            this.courseDivision = courseDivision;
            this.grade = grade;
            this.credit = credit;
            lectureSchedule = lectureTime;
            timeOfClass = getValue.TimeOfClass(lectureTime);
            this.classRoom = classRoom;
            classRooms = getValue.ClassRoom(classRoom);
            this.professor = professor;
            this.lectureLanguage = lectureLanguage;
        }

        public bool[,] TimeOfClass
        {
            get { return timeOfClass; }
        }

        public int Number
        {
            get { return number; }
        }

        public string Department
        {
            get { return department; }
        }

        public int Grade
        {
            get { return grade; }
        }

        public string SerialNumber
        {
            get { return serialNumber; }
        }

        public string DivisionClassNumber
        {
            get { return divisionClassNumber; }
        }

        public string LectureName
        {
            get { return lectureName; }
        }

        public string CourseDivision
        {
            get { return courseDivision; }
        }

        public float Credit
        {
            get { return credit; }
        }

        public string LectureSchedule
        {
            get { return lectureSchedule; }
        }

        public string ClassRoom
        {
            get { return classRoom; }
        }

        public List<string> ClassRooms
        {
            get { return classRooms; }
        }

        public string Professor
        {
            get { return professor; }
        }

        public string LectureLanguage
        {
            get { return lectureLanguage; }
        }

        /// <summary>
        /// ClassVO의 string변수들을 한 번에 출력해주는 메소드입니다.
        /// </summary>
        /// <returns>ClassVO의 string변수</returns>
        public override string ToString()
        {
            return "개설학과전공 : " + department + ", 학수번호 : " + serialNumber + ", 분반 : " + divisionClassNumber +
                ", 교과목명 : " + lectureName + ", 이수구분 : " + courseDivision + ", 학년 : " + grade + ", 학점 : " + credit +
                 ", 교수명 : " + professor + ", 강의언어 : " + lectureLanguage;
        }

        /// <summary>
        /// 엑셀파일로부터 강의를 불러와 리스트로 만들어주는 메소드입니다.
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <returns>강의 리스트</returns>
        public List<ClassVO> Load(string path)
        {
            Array data = fileIOManager.OpenAndReadFile(path, "A2", "L167");
            List<ClassVO> classes = new List<ClassVO>();

            // 데이터 행, 열 구함
            int rows = data.GetUpperBound(0) - data.GetLowerBound(0) + 1;
            int columns = data.GetUpperBound(1) - data.GetLowerBound(1) + 1;
            
            // 데이터에서 값들 불러와 ClassVO에 대입, 리스트로 만듦
            for (int row = 1; row <= rows; row++)
            {
                for (int column = 1; column <= columns; column++) if (data.GetValue(row, column) == null) data.SetValue("", row, column);

                classes.Add(new ClassVO(Int32.Parse(data.GetValue(row, 1).ToString()), data.GetValue(row, 2).ToString(), data.GetValue(row, 3).ToString(),
                    data.GetValue(row, 4).ToString(), data.GetValue(row, 5).ToString(),
                    data.GetValue(row, 6).ToString(), Int32.Parse(data.GetValue(row, 7).ToString()),
                    float.Parse(data.GetValue(row, 8).ToString()), data.GetValue(row, 9).ToString(),
                    data.GetValue(row, 10).ToString(), data.GetValue(row, 11).ToString(), data.GetValue(row, 12).ToString()));
            }

            return classes;
        }
    }
}
