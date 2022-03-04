#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : RoomNonExistsException.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:38
// Description :

#endregion

using System;

namespace IMDotNet.Shared.Exceptions;

public class RoomNonExistsException : RoomException
{
    public RoomNonExistsException(string message) : base(message)
    {
    }

    public RoomNonExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}