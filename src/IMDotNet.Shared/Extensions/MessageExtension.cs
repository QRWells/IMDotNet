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
using IMDotNet.Shared.Message;

namespace IMDotNet.Shared.Extensions;

public static class MessageExtension
{
    public static MessageHeader ReadHeader(this ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < MessageHeader.Size) throw new ArgumentException();
        return new MessageHeader();
    }
}