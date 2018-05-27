namespace LaparoCommunicator
{
    public class EulerData
    {
        public float[] LeftAngles { get; set; }
        public float[] RightAngles { get; set; }
        public override string ToString()
        {
            return $"Left: {LeftAngles}, Right: {RightAngles}\n";
        }
    }
}
