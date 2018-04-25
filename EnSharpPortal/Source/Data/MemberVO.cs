using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnSharpPortal.Source.Main;

namespace EnSharpPortal.Source.Data
{
    class MemberVO
    {
        FileIOManager fileIOManager = new FileIOManager();
        
        private string name;
        private int number;
        private string password;
        private int grade;
        private bool isAbsence;
        private DateTime birthDate;
        private string department;

        public MemberVO(string name, int number, string password,
            int grade, bool isAbsence, DateTime birthDate, string department)
        {
            this.name = name;
            this.number = number;
            this.password = password;
            this.grade = grade;
            this.isAbsence = isAbsence;
            this.birthDate = birthDate;
            this.department = department;
        }

        public void Load(string path)
        {
            Array data = fileIOManager.OpenAndReadFile(path);
            List<MemberVO> members = new List<MemberVO>();

            foreach (string[] row in data)
            {
                bool checkAbsence = true;
                int year, month, day;

                if (string.Compare(row[4], "T") == 0) checkAbsence = true;
                else checkAbsence = false;

                //year = getVa

                //members.Add(new MemberVO(row[0], Int32.Parse(row[1]), row[2], Int32.Parse(row[3]), checkAbsence, )
            }
        }
    }
}
