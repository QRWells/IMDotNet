#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : IMessage.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 14:46
// Description :

#endregion

namespace IMDotNet.Shared.Message;

public abstract class Message
{
    protected MessageHeader _header;
    protected byte[] _rawBody;

    public MessageHeader Header => _header;
    public int Size => MessageHeader.Size + _rawBody.Length;

    public virtual byte[] WriteBuffer()
    {
        var header = Header.WriteBytes();
        return header.ToArray().Concat(_rawBody).ToArray();
    }

    protected void ExtractRawMessage(ReadOnlySpan<byte> buffer)
    {
        if (!MessageHeader.ReadFromBytes(buffer, out _header)) return;
        if (buffer.Length < MessageHeader.Size + _header.PayloadLength) return;
        _rawBody = new byte[_header.PayloadLength];
        buffer[20..].CopyTo(_rawBody);
    }

    #region Set Basic Information

    public Message WithSenderId(uint senderId)
    {
        _header.SenderId = senderId;
        return this;
    }

    public Message WithReceiverId(uint receiverId)
    {
        _header.ReceiverId = receiverId;
        return this;
    }

    public Message WithPayloadLength(uint payloadLength)
    {
        _header.PayloadLength = payloadLength;
        return this;
    }

    public Message WithSequenceId(ushort sequenceId)
    {
        _header.SequenceId = sequenceId;
        return this;
    }

    #endregion

    #region Flag modification

    public Message ClearFlag()
    {
        _header.Flag &= 0;
        return this;
    }

    public Message IsBroadCast()
    {
        _header.Flag |= MessageFlag.BroadCast;
        return this;
    }

    public Message IsToGroup()
    {
        _header.Flag |= MessageFlag.Group;
        return this;
    }

    #endregion
}