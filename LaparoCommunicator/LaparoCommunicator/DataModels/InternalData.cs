﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApp2.Utils;

namespace ConsoleApp2.DataModels
{
    enum Side
    {
        Left = 1,
        Right = 2
    }

    enum InCommand : byte
    {
        Empty = 0x00,
        ResultsVirtual = 0x52,
        Acc = 0x41,
        Vel = 0x56,
        Dist = 0x44,
        ClampsS = 0x53,
        Clamps = 0x43,
        Info = 0x49,
        InResultsCam = 0x4D,
        Osc = 0x4F
    }
    
    class InternalData
    {
        private Data leftData = new Data();
        private Data rightData = new Data();

        private class Data
        {
            public Data()
            {
                Quad = new double[4];
                Angles = new double[3];
            }

           public double[] Quad { get; set; }
            public double[] Angles { get; set; }
            public double Acc { get; set; }
            public double Vel { get; set; }
            public double Dist { get; set; }
            public double Osc { get; set; }
            public double Clamps { get; set; }
            public double ClampsS { get; set; }
        }

        internal InternalData()
        { }

        public void ProccesBytes(byte[] bytes)
        {
            Logger.Log("Proccessing");

            while (!String.IsNullOrEmpty(Encoding.ASCII.GetString(bytes)))
            {
                Logger.Log(System.Text.Encoding.ASCII.GetString(bytes.Take(4).ToArray()));

                for (int i = 0; i < 3; i++)
                {
                    Logger.Log(bytes[i].ToString());
                }

                Logger.Log("Checking header!");

                if (bytes[0] != 0x43 || bytes[1] != 0x4D)
                    throw new Exception("Wrong header");

                Logger.Log("Header correct");

                bytes = bytes.Skip(2).ToArray();
                Data currentData = new Data();

                byte side = bytes[1];

                InCommand id = (InCommand)bytes[0];
                bytes = bytes.Skip(2).ToArray();


                switch (id)
                {
                    case InCommand.Empty:
                        Logger.Log("No command");
                        break;
                    case InCommand.ResultsVirtual:
                        Logger.Log("Main params");

                        currentData.Angles[0] = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();

                        for (int i = 0; i < 4; i++)
                        {
                            currentData.Quad[i] = BitConverter.ToSingle(bytes, 0);
                            bytes = bytes.Skip(4).ToArray();
                        }

                        currentData.Angles[1] = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();

                        currentData.Angles[2] = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    case InCommand.InResultsCam:
                        Logger.Log("Got 0x4D");
                        break;
                    case InCommand.Acc:
                        Logger.Log("Got acc");
                        currentData.Acc = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    case InCommand.Vel:
                        Logger.Log("Got vel");
                        currentData.Vel = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    case InCommand.Osc:
                        Logger.Log("Got osc");
                        currentData.Osc = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    case InCommand.Dist:
                        Logger.Log("Got dist");
                        currentData.Dist = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    case InCommand.ClampsS:
                        Logger.Log("Clamps_S");
                        currentData.ClampsS = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    case InCommand.Clamps:
                        Logger.Log("Clamps");
                        currentData.Clamps = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                        break;
                    //IN_INFO, convert to hex
                    case InCommand.Info:
                        bytes = bytes.Skip(35).ToArray();
                        break;
                    default:
                        Logger.Log("Got unkown command: " + bytes[0]);
                        break;
                }

                switch(side)
                {
                    case (byte)'L':
                        leftData = currentData;
                        break;
                    case (byte)'R':
                        rightData = currentData;
                        break;
                }

                //We need to skip CRC
                bytes = bytes.Skip(1).ToArray();
                Logger.Log("After switch"); 
            }
        }

        public double[] GetQuad(Side s)
        {
            switch (s)
            {
                case Side.Left:
                    return leftData.Quad;
                case Side.Right:
                    return rightData.Quad;
            }

            return new double[0];
        }

        public double[] GetAngles(Side s)
        {
            switch (s)
            {
                case Side.Left:
                    return leftData.Angles;
                case Side.Right:
                    return rightData.Angles;
            }

            return new double[0];
        }
    }
}
