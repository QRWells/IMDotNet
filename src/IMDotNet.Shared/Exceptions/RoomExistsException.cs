#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : RoomExistsException.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:35
// Description :

#endregion

using System;

namespace IMDotNet.Shared.Exceptions;

public class RoomExistsException : RoomException
{
    public RoomExistsException(string message) : base(message)
    {
    }

    public RoomExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}