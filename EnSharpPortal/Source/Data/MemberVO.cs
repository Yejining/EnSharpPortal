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
        
        private string name;        // 학생 이름
        private int number;         // 학번
        private string password;    // 암호
        private int grade;          // 학년
        private bool isAbsence;     // 휴학여부(T/F)
        private DateTime birthDate; // 생일
        private string department;  // 소속

        /// <summary>
        /// MemberVO의 생성자 메소드입니다.
        /// 변수들을 파라미터로 받은 변수들로 설정해줍니다.
        /// </summary>
        /// <param name="name">학생 이름</param>
        /// <param name="number">학번</param>
        /// <param name="password">암호</param>
        /// <param name="grade">학년</param>
        /// <param name="isAbsence">휴학여부(T/F)</param>
        /// <param name="birthDate">생일</param>
        /// <param name="department">소속</param>
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

        /// <summary>
        /// 엑셀파일로부터 학생 정보를 불러와 리스트로 만들어주는 메소드입니다.
        /// </summary>
        /// <param name="path">파일 경로</param>
        public void Load(string path)
        {
            Array data = fileIOManager.OpenAndReadFile(path, "a","b");
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
