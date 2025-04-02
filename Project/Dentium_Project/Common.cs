using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentium_Project
{
    public static class Common
    {
        /// <summary>
        /// 셀 입력 바인딩용 리스트
        /// </summary>
        public static BindingList<ExcelData> insertlist = new BindingList<ExcelData>() { AllowNew = true }; // 자동 줄생성 설정
        //선택한 행 저장용
        public static int selectLow = 0;
        //입력한 ProductLot셀의 값 저장
        public static string newValue = "";

        //프린트 허용을 위한 변수 지정
        public static bool printFlag = true;
    }
    public class ExcelData
    {
        public string ProductLot { get; set; }
        public string ModelName { get; set; }
        public string QTY { get; set; }
        public string Holder { get; set; }
        public string UpJIG_LOT { get; set; }
        public string UpJIG_LOT2 { get; set; }
        public string DownJIG_LOT2 { get; set; }
        public string DownJIG_LOT { get; set; }
        public string Memo { get; set; }
    }
}
