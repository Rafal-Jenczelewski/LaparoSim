using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace LaparoCommunicator
{
    public class CartesianData
    {
        public double[] LeftCoordinats { get; set; }
        public double[] RightCoordinats { get; set; }
        public override string ToString()
        {
            return $"Left: {string.Join(" ", LeftCoordinats)}\tRight: {string.Join(" ", RightCoordinats)}";
        }
    }
}
