#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.ASPNETServer.
// File Name   : IMessageParser.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 21:28
// Description :

#endregion

using System.Buffers;
using IMDotNet.Shared.Message;

namespace IMDotNet.ASPNETServer.Services;

public interface IMessageParser
{
    public bool TryParseMessage(in ReadOnlySequence<byte> buffer, out Message message);
}