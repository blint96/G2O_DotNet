
namespace G2O_Framework
{
    public interface IPacket
    {
        void Reset();

        void WriteBool(bool value);

        void WriteChar(char value);

        void WriteInt16(int value);

        void WriteInt32(int value);

        void WriteFloat(float value);

        void WriteString(string value);

        bool ReadBool();

        char ReadChar();

        short ReadInt16();

        int ReadInt32();

        float ReadFloat();

        string ReadString();
    }
}
