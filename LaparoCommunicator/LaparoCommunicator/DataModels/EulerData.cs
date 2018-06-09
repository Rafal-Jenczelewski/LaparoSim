namespace LaparoCommunicator
{
    public class EulerData
    {
        public float[] LeftAngles { get; set; }
        public float[] RightAngles { get; set; }
        public override string ToString()
        {
            string s = "";
            foreach(var c in LeftAngles)
            {
                s += c + " ";
            }
            s += " || ";
            foreach (var c in RightAngles)
            {
                s += c + " ";
            }

            return s;
        }
    }
}
