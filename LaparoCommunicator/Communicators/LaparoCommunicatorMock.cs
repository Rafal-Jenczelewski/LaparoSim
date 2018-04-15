using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaparoCommunicator
{
    class LaparoCommunicatorMock : ILaparoCommunicator
    {
        private readonly string[] cartesianData;
        private readonly string[] eulerData;
        private readonly string[] quaternionData;
        private int position = 0;
        private static readonly char[] SideSeparator = {'|'};
        private static readonly char[] CoordSeparator = {' '};

        internal LaparoCommunicatorMock(string path)
        {
            cartesianData = File.ReadAllLines(path + "cartesian.txt");
            eulerData = File.ReadAllLines(path + "euler.txt");
            quaternionData = File.ReadAllLines(path + "quaternion.txt");
        }

        public CartesianData GetDataInCartesian()
        {
            if (cartesianData.Length == position - 1)
                position = 0;

            var splittedData = cartesianData[position].Split(SideSeparator, StringSplitOptions.RemoveEmptyEntries);
            position++;

            var leftCoords = splittedData[0].Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            var rightCoords = splittedData[1].Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            return new CartesianData {LeftCoordinats = leftCoords, RightCoordinats = rightCoords};
        }

        public EulerData GetDataInEuler()
        {
            if (eulerData.Length == position - 1)
                position = 0;

            var splittedData = eulerData[position].Split(SideSeparator, StringSplitOptions.RemoveEmptyEntries);
            position++;

            var leftCoords = splittedData[0].Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            var rightCoords = splittedData[1].Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            return new EulerData() { LeftAngles = leftCoords, RightAngles = rightCoords };
        }

        public QuaternionData GetDataInQuaternion()
        {
            if (quaternionData.Length == position - 1)
                position = 0;

            var splittedData = quaternionData[position].Split(SideSeparator, StringSplitOptions.RemoveEmptyEntries);
            position++;

            var leftCoords = splittedData[0].Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            var rightCoords = splittedData[1].Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            return new QuaternionData() { LeftQuaternion = leftCoords, RightQuaternion = rightCoords };
        }

        public void Dispose()
        {
        }
    }
}
