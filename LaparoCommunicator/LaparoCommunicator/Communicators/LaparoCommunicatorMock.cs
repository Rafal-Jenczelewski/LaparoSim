using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LaparoCommunicator
{
    class LaparoCommunicatorMock : ILaparoCommunicator
    {
        private readonly string[] eulerData;
        private readonly string[] quaternionData;
        private int position = 0;
        private static readonly char[] CoordSeparator = {'\t'};
        private static readonly string fileExtension = ".tsv";

        internal LaparoCommunicatorMock(string path)
        {
            eulerData = File.ReadAllLines(path + "euler" + fileExtension);
            quaternionData = File.ReadAllLines(path + "quaternion" + fileExtension);
        }

        public EulerData GetDataInEuler()
        {
            if (eulerData.Length == position)
                position = 0;

            var data = eulerData[position];
            var leftCoords = data.Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

            data = eulerData[position + 1];
            var rightCoords = data.Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            position = position + 2;

            return new EulerData() { LeftAngles = leftCoords, RightAngles = rightCoords };
        }

        public QuaternionData GetDataInQuaternion()
        {
            if (quaternionData.Length == position)
                position = 0;

            var data = quaternionData[position];
            var leftCoords = data.Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

            data = quaternionData[position + 1];
            var rightCoords = data.Split(CoordSeparator, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            position = position + 2;

            return new QuaternionData() { LeftQuaternion = leftCoords, RightQuaternion = rightCoords };
        }

        public void Dispose()
        {
        }
    }
}
