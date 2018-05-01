using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using EnSharpPortal.Source.Data;

namespace EnSharpPortal.Source.Main
{
    class DataManager
    {
        private List<ClassVO> classes;

        ClassVO classVO = new ClassVO();

        /// <summary>
        /// 데이터를 로드하는 메소드입니다. 엑셀파일로부터 강의 리스트를 로드합니다.
        /// </summary>
        public void LoadData()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            classes = classVO.Load(path + "\\Data\\2018-1_lecture schedule");
        }

        public List<ClassVO> Classes
        {
            get { return classes; }
        }
    }
}
