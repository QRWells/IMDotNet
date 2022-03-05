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

using System.Text;

namespace IMDotNet.Shared.Message;

public class TextMessage : Message
{
    public TextMessage(string text)
    {
        Text = text;
        _header.Flag |= MessageFlag.Text;
    }

    internal TextMessage(MessageHeader header, string text)
    {
        _header = header;
        Text = text;
    }

    public string Text { get; }

    public override byte[] WriteBuffer()
    {
        _rawBody = Encoding.UTF8.GetBytes(Text);
        _header.PayloadLength = (uint)_rawBody.Length;
        var header = Header.WriteBytes();
        return header.ToArray().Concat(_rawBody).ToArray();
    }
}