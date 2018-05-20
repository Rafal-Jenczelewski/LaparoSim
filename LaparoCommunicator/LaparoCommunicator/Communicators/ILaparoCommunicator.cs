using System;

namespace LaparoCommunicator
{
    public interface ILaparoCommunicator : IDisposable
    {
        EulerData GetDataInEuler();

        QuaternionData GetDataInQuaternion();
    }
}
