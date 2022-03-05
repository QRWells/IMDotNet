#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : MessageExtension.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 21:24
// Description :

#endregion

using System;
using System.Buffers.Binary;
using IMDotNet.Shared.Message;

namespace IMDotNet.Shared.Extensions;

public static class MessageExtension
{
    public static bool TryReadHeader(this ReadOnlySpan<byte> buffer, out MessageHeader header)
    {
        header = new MessageHeader();
        if (buffer.Length < MessageHeader.Size) return false;
        if (BitConverter.IsLittleEndian)
        {
            header.SenderId = BitConverter.ToUInt32(buffer);
            header.ReceiverId = BitConverter.ToUInt32(buffer[0x4..]);
            header.Flag = (MessageFlag)BitConverter.ToUInt16(buffer[0x8..]);
            header.SequenceId = BitConverter.ToUInt16(buffer[0xA..]);
            header.PayloadLength = BitConverter.ToUInt32(buffer[0xC..]);
        }
        else
        {
            header.SenderId = BinaryPrimitives.ReadUInt32LittleEndian(buffer[..]);
            header.ReceiverId = BinaryPrimitives.ReadUInt32LittleEndian(buffer[0x4..]);
            header.Flag = (MessageFlag)BinaryPrimitives.ReadUInt16LittleEndian(buffer[0x8..]);
            header.SequenceId = BinaryPrimitives.ReadUInt16LittleEndian(buffer[0xA..]);
            header.PayloadLength = BinaryPrimitives.ReadUInt32LittleEndian(buffer[0xC..]);
        }

        return true;
    }
}