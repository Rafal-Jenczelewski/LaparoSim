using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaparoCommunicator
{
    public class CommunicatorFactory
    {
        public static ILaparoCommunicator GetCommunicator()
        {
            return new LaparoCommunicatorImpl();
        }

        public static ILaparoCommunicator GetMock(string pathToData)
        {
            return new LaparoCommunicatorMock(pathToData);
        }
}
}
