using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using ConsoleApp2.DataModels;

namespace LaparoCommunicator
{
    class LaparoCommunicatorImpl : ILaparoCommunicator
    {
        private SerialPort port;
        private Thread pingerThread;
        private Thread receiverThread;
        private string targetId = "";
        private readonly InternalData internalData = new InternalData();

        internal LaparoCommunicatorImpl()
        {
            SetPort();

            Thread.Sleep(50);

            SendCommand(new OutCommand(OutCommandId.SET_STATE_SENDING));

            InitalizeThreads();
        }

        public void Dispose()
        {
            pingerThread.Abort();
            receiverThread.Abort();
        }

        public CartesianData GetDataInCartesian()
        {
            throw new NotSupportedException("Method not yet implemented");
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
            string sInstanceName = string.Empty;
            string sPortName = string.Empty;
            bool bFound = false;

            for (int i = 0; i < portNames.Length; i++)
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    sInstanceName = queryObj["InstanceName"].ToString();
                    if (sInstanceName.IndexOf(targetId) > -1)
                    {
                        Console.WriteLine("Laparo device found!");
                        sPortName = queryObj["PortName"].ToString();
                        port = new SerialPort(sPortName, 9600, Parity.None, 8, StopBits.One);
                        port.Open();
                        bFound = true;
                        break;
                    }
                }

                if (bFound)
                    break;
            }

            if(!bFound)
                throw new IOException("No Laparo device found, aborting");
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
            while (true)
            {
                SendCommand(new OutCommand(OutCommandId.PING));
                Thread.Sleep(1000);
            }
        }

        private void ReceiveData()
        {
            while (true)
            {
                byte[] bytes;
                lock (port)
                {
                    string s = port.ReadExisting();
                    bytes = Encoding.ASCII.GetBytes(s);
                }

                internalData.ProccesBytes(bytes);
                Thread.Sleep(10);
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
