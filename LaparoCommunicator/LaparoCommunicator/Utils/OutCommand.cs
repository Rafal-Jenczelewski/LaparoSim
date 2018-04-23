namespace LaparoCommunicator
{
    enum OutCommandId : byte
    {
        PING = (byte)'P',
        SET_STATE_SENDING = (byte)'S'
    }

    class OutCommand
    {
        public byte id;
        public byte crc;

        internal OutCommand(OutCommandId ID)
        {
            id = (byte)ID;
            calculateCrc();
        }

        private void calculateCrc()
        {
            crc = (byte)('C' + 'M' + id);
        }
    }
}
