using System;

namespace LaparoCommunicator
{
    public interface ILaparoCommunicator : IDisposable
    {
        CartesianData GetDataInCartesian();

        EulerData GetDataInEuler();

        QuaternionData GetDataInQuaternion();
    }
}
