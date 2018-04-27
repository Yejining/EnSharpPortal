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

        int number;
        private string department;
        private string serialNumber;
        private string divisionClassNumber;
        private string lectureName;
        private string courseDivision;
        private int grade;
        private float credit;
        private string lectureSchedule;
        private List<int> daysOfClass;
        private bool[] timeOfClass;
        private string classRoom;
        private List<string> classRooms;
        private string professor;
        private string lectureLanguage;

        public ClassVO()
        {

        }

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
            daysOfClass = getValue.DaysOfClass(lectureTime);
            timeOfClass = getValue.TimeOfClass(lectureTime);
            this.classRoom = classRoom;
            classRooms = getValue.ClassRoom(classRoom);
            this.professor = professor;
            this.lectureLanguage = lectureLanguage;
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

        public string Professor
        {
            get { return professor; }
        }

        public string LectureLanguage
        {
            get { return lectureLanguage; }
        }

        public override string ToString()
        {
            return "개설학과전공 : " + department + ", 학수번호 : " + serialNumber + ", 분반 : " + divisionClassNumber +
                ", 교과목명 : " + lectureName + ", 이수구분 : " + courseDivision + ", 학년 : " + grade + ", 학점 : " + credit +
                 ", 교수명 : " + professor + ", 강의언어 : " + lectureLanguage;
        }

        public List<ClassVO> Load(string path)
        {
            Array data = fileIOManager.OpenAndReadFile(path);
            List<ClassVO> classes = new List<ClassVO>();

            int rows = data.GetUpperBound(0) - data.GetLowerBound(0) + 1;
            int columns = data.GetUpperBound(1) - data.GetLowerBound(1) + 1;
            
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
