#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : Packet.cs
// Author      : Qirui Wang
// Created at  : 2022/03/03 20:16
// Description :

#endregion

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IMDotNet.Shared.Message;

public struct Message
{
    public readonly MessageHeader Header;
    public readonly string? Body;

    public Message(MessageHeader header, string? body)
    {
        Header = header;
        Body = body;
    }

    public Message(ref ReadOnlySpan<byte> byteBuffer)
    {
        if (byteBuffer.Length < 16)
            throw new ArgumentException
                ($"{nameof(byteBuffer)} is incomplete.");
        var header = MemoryMarshal.Cast<byte, ushort>(byteBuffer[..16]);
        Header = new MessageHeader(header);
        Body = Encoding.UTF8.GetString(byteBuffer[16..]);
    }

    public byte[] WriteBuffer()
    {
        var header = Header.WriteBytes();
        if (Body == null)
            return header;
        var body = Encoding.UTF8.GetBytes(Body);
        return header.ToArray().Concat(body).ToArray();
    }
}