using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esoft.Model
{
    public class Checkers
    {
        public static bool IsDouble(string d)
        {
            double result;
            d = d.Replace('.', ',');
            bool isDouble = Double.TryParse(d, out result);
            return isDouble ? !(result < 0) : false || String.IsNullOrWhiteSpace(d);
        }
        public static bool IsDouble(string d, int minLim, int maxLim)
        {
            double result;
            d = d.Replace('.', ',');
            bool isDouble = Double.TryParse(d, out result);
            return isDouble ? !(result < minLim || result > maxLim) : false || String.IsNullOrWhiteSpace(d);
        }
        public static bool IsUInt(string d)
        {
            UInt32 result;
            bool isUInt32 = UInt32.TryParse(d, out result);
            return isUInt32 ? true : false || String.IsNullOrEmpty(d);
        }
    }
}
