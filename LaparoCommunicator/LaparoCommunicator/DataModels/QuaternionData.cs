using System.Globalization;
using System.Linq;

namespace LaparoCommunicator
{
    public class QuaternionData
    {
        public float[] LeftQuaternion { get; set; }
        public float[] RightQuaternion { get; set; }
        public override string ToString()
        {
            string s = "";
            foreach (var f in LeftQuaternion)
            {
                s += f + " ";
            }
            s += " || ";
            foreach (var f in RightQuaternion)
            {
                s += f + " ";
            }

            return s;
            //return $"Left: {LeftQuaternion.Select(e => e.ToString(CultureInfo.InvariantCulture))}, Right: {RightQuaternion}\n";
        }
    }
}
