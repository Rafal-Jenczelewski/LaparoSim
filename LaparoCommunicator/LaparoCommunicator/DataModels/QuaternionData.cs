namespace LaparoCommunicator
{
    public class QuaternionData
    {
        public float[] LeftQuaternion { get; set; }
        public float[] RightQuaternion { get; set; }
        public override string ToString()
        {
            return $"Left: {LeftQuaternion}, Right: {RightQuaternion}\n";
        }
    }
}
