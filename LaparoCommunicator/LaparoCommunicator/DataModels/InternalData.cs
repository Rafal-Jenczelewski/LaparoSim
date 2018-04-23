﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Utils;

namespace ConsoleApp2.DataModels
{
    enum Side
    {
        Left = 1,
        Right = 2
    }
    
    class InternalData
    {
        private readonly Data leftData = new Data();
        private readonly Data rightData = new Data();

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
            if (bytes[0] != 0x43 || bytes[1] != 0x4D)
                throw new Exception("Wrong header");

            Logger.Log("Header correct");

            bytes = bytes.Skip(2).ToArray();
            Data currentData = new Data();

            //Założenie, że niezależnie od wszystkiego 4 bajt oznacza stronę...
            //if (bytes[2] == 'L')
            //{
                //currentData = leftData;
                Logger.Log("Got left side");
            //}
            //else if (bytes[2] == 'R')
            //{
            //   currentData = rightData;
            //    Logger.Log("Got right side");
            //}
            //else
            //{
            //    Logger.Log("Got unknown side: " + bytes[2]);
            //    throw new Exception("Unknown side at byte 4");             
            //}

            switch (bytes[1])
            {
                case 0x00:
                    Logger.Log("No command");
                    break;
                case 0x52:
                    Logger.Log("Main params");
                    bytes = bytes.Skip(2).ToArray();

                    currentData.Angles[0] = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();

                    for (int i = 0; i < 3; i++)
                    {
                        currentData.Quad[i] = BitConverter.ToSingle(bytes, 0);
                        bytes = bytes.Skip(4).ToArray();
                    }

                    currentData.Angles[1] = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();

                    currentData.Angles[2] = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
                case 0x4D:
                    bytes = bytes.Skip(2).ToArray();
                    break;
                case 0x41:
                    Logger.Log("Got acc");
                    bytes = bytes.Skip(2).ToArray();
                    currentData.Acc = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
                case 0x56:
                    Logger.Log("Got vel");
                    bytes = bytes.Skip(2).ToArray();
                    currentData.Vel = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
                case 0x4F:
                    Logger.Log("Got osc");
                    bytes = bytes.Skip(2).ToArray();
                    currentData.Osc = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
                case 0x44:
                    Logger.Log("Got dist");
                    bytes = bytes.Skip(2).ToArray();
                    currentData.Dist = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
                case 0x53:
                    Logger.Log("Clamps");
                    bytes = bytes.Skip(2).ToArray();
                    currentData.ClampsS = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
                case 0x43:
                    Logger.Log("Clamps");
                    bytes = bytes.Skip(2).ToArray();
                    currentData.Clamps = BitConverter.ToSingle(bytes, 0);
                    bytes = bytes.Skip(4).ToArray();
                    break;
            }
            Logger.Log("After switch");
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
