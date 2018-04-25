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

        private string department;
        private int serialNumber;
        private int divisionClassNumber;
        private string lectureName;
        private int courseDivision;
        private int grade;
        private float credit;
        private List<int> daysOfClass;
        private bool[] timeOfClass;
        private List<string> classRoom;
        private string professor;
        private int lectureLanguage;
        
        public ClassVO()
        {

        }

        public ClassVO(string department, int serialNumber, int divisionClassNumber, string lectureName, string courseDivision, int grade,
            float credit, string lectureTime, string classRoom, string professor, string lectureLanguage)
        {
            GetValue getValue = new GetValue();

            this.department = department;
            this.serialNumber = serialNumber;
            this.divisionClassNumber = divisionClassNumber;
            this.lectureName = lectureName;
            this.courseDivision = getValue.CourseDivision(courseDivision);
            this.grade = grade;
            this.credit = credit;
            daysOfClass = getValue.DaysOfClass(lectureTime);
            timeOfClass = getValue.TimeOfClass(lectureTime);
            this.classRoom = getValue.ClassRoom(classRoom);
            this.professor = professor;
            this.lectureLanguage = getValue.LectureLanguage(lectureLanguage);
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

                classes.Add(new ClassVO(data.GetValue(row, 1).ToString(), Int32.Parse(data.GetValue(row, 2).ToString()),
                    Int32.Parse(data.GetValue(row, 3).ToString()), data.GetValue(row, 4).ToString(),
                    data.GetValue(row, 5).ToString(), Int32.Parse(data.GetValue(row, 6).ToString()),
                    float.Parse(data.GetValue(row, 7).ToString()), data.GetValue(row, 8).ToString(),
                    data.GetValue(row, 9).ToString(), data.GetValue(row, 10).ToString(), data.GetValue(row, 11).ToString()));
            }

            return classes;
        }
    }
}
