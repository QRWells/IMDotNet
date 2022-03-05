#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : BinaryMessage.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 15:48
// Description :

#endregion

namespace IMDotNet.Shared.Message;

public class BinaryMessage : Message
{
    public BinaryMessage(byte[] rawBytes)
    {
        _rawBody = rawBytes;
        _header.PayloadLength = (uint)rawBytes.Length;
        _header.Flag |= MessageFlag.Binary;
    }

    public BinaryMessage(Stream stream)
    {
        var size = stream.Read(_rawBody);
        _header.PayloadLength = (uint)size;
    }
}