using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Benjaminhaldin_GambitChallange.Models
{
    public class ModbusConvert
    {
        public ModbusConvert(ushort val, string dataType, ushort val2 = 0, string unit = "0")
        {
            Value1 = val;
            Value2 = val2;
            DataType = dataType;
            Unit = unit;
        }

        public ushort Value1 { get; set; }
        public ushort Value2 { get; set; }
        public string DataType { get; set; }
        public string Unit { get; set; }
    }
}
