using System.Buffers.Binary;
using System.Runtime.InteropServices;
using IMDotNet.Shared.Extensions;

namespace IMDotNet.Shared.Message;

[Flags]
public enum MessageFlag : ushort
{
    Text = 1 << 0,
    Json = 1 << 1,
    File = 1 << 2,
    Binary = 1 << 3,
    BroadCast = 1 << 13,
    Group = 1 << 14,
    Request = 1 << 15
}

[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Size = 0x10)]
public struct MessageHeader
{
    public const int Size = 0x10;

    [FieldOffset(0x0)] internal uint SenderId;
    [FieldOffset(0x4)] internal uint ReceiverId;
    [FieldOffset(0x8)] internal MessageFlag Flag;
    [FieldOffset(0xA)] internal ushort SequenceId;
    [FieldOffset(0xC)] internal uint PayloadLength;

    public MessageHeader(ReadOnlySpan<ushort> buffer)
    {
        if (buffer.Length < 5)
            throw new ArgumentException($"the ${nameof(buffer)} is insufficient");
        Flag = (MessageFlag)buffer[0];
        SequenceId = buffer[1];
        PayloadLength = buffer[2];
        SenderId = buffer[3];
        ReceiverId = buffer[4];
    }

    public IEnumerable<byte> WriteBytes()
    {
        var buf = new byte[Size];
        var span = buf.AsSpan();
        if (BitConverter.IsLittleEndian)
        {
            BitConverter.TryWriteBytes(span, SenderId);
            BitConverter.TryWriteBytes(span[0x4..], ReceiverId);
            BitConverter.TryWriteBytes(span[0x8..], (ushort)Flag);
            BitConverter.TryWriteBytes(span[0xA..], SequenceId);
            BitConverter.TryWriteBytes(span[0xC..], PayloadLength);
        }
        else
        {
            BinaryPrimitives.WriteUInt32LittleEndian(span[..], SenderId);
            BinaryPrimitives.WriteUInt32LittleEndian(span[0x4..], ReceiverId);
            BinaryPrimitives.WriteUInt16LittleEndian(span[0x8..], (ushort)Flag);
            BinaryPrimitives.WriteUInt16LittleEndian(span[0xA..], SequenceId);
            BinaryPrimitives.WriteUInt32LittleEndian(span[0xC..], PayloadLength);
        }

        return buf;
    }

    public static bool ReadFromBytes(ReadOnlySpan<byte> bytes, out MessageHeader header)
    {
        return bytes.TryReadHeader(out header);
    }
}