#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : JsonMessage.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 14:53
// Description :

#endregion

using System.Runtime.Serialization;

namespace IMDotNet.Shared.Message;

public class JsonMessage : Message
{
    public JsonMessage(ISerializable obj)
    {
        _header.Flag |= MessageFlag.Json;
    }
}