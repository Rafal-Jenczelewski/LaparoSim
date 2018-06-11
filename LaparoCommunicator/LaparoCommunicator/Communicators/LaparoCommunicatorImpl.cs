using System;
using System.Reflection;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace LaparoCommunicator
{
    class LaparoCommunicatorImpl : ILaparoCommunicator
    {
        private SerialPort port;
        private Thread pingerThread;
        private Thread receiverThread;
        private readonly InternalData internalData = new InternalData();

        private static string portFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "portName.txt");

        internal LaparoCommunicatorImpl()
        {
            Logger.Log("------");
            SetPort();

            Thread.Sleep(50);

            SendCommand(new OutCommand(OutCommandId.SET_STATE_SENDING));

            InitalizeThreads();
        }

        public void Dispose()
        {
            pingerThread.Abort();
            receiverThread.Abort();
            port.Close();
        }

        public EulerData GetDataInEuler()
        {
            return new EulerData()
            {
                LeftAngles = internalData.GetAngles(Side.Left),
                RightAngles = internalData.GetAngles(Side.Right)
            };
        }

        public QuaternionData GetDataInQuaternion()
        {
            return new QuaternionData()
            {
                LeftQuaternion = internalData.GetQuad(Side.Left),
                RightQuaternion = internalData.GetQuad(Side.Right)
            };
        }

        private void SetPort()
        {
            string[] portNames = SerialPort.GetPortNames();
            string portName = string.Empty;

            portName = File.ReadAllLines(portFilePath)[0];

            port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            
            Logger.Log("After open");
        }

        private void SendCommand(OutCommand command)
        {
            byte[] outBytes = new byte[8];

            outBytes[0] = (byte)'C';
            outBytes[1] = (byte)'M';
            outBytes[2] = command.id;
            outBytes[3] = 0;
            outBytes[4] = 0;
            outBytes[5] = 0;
            outBytes[6] = 0;
            outBytes[6] = command.crc;

            lock (port)
            {
                port.Write(outBytes, 0, 8);
            }
        }

        private void PingDevice()
        {
            Logger.Log("Starting pinging");
            while (true)
            {
                Logger.Log("Ping");
                SendCommand(new OutCommand(OutCommandId.PING));
                Thread.Sleep(1000);
            }
        }

        private void ReceiveData()
        {
            Logger.Log("Starting receiver");
            int i = 0;
            while (true)
            {
                Logger.Log("iteracja rec: " + i);
                byte[] bytes;
                lock (port)
                {
                    if (!port.IsOpen)
                        Logger.Log("Port not opened");
                    string s = port.ReadExisting();
                    Logger.Log("Received bytes: " + s.Length);
                    bytes = Encoding.ASCII.GetBytes(s);
                }
                if (bytes.Length != 0)
                    internalData.ProccesBytes(bytes);

                Thread.Sleep(10);
                i++;
            }
        }

        private void InitalizeThreads()
        {
            ThreadStart pingerStart = PingDevice;
            pingerThread = new Thread(pingerStart);
            pingerThread.Start();

            ThreadStart receiverStart = ReceiveData;
            receiverThread = new Thread(receiverStart);
            receiverThread.Start();
        }
    }
}
