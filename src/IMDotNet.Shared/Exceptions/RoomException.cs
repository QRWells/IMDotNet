#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : RoomException.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:36
// Description :

#endregion

using System;

namespace IMDotNet.Shared.Exceptions;

public abstract class RoomException : Exception
{
    protected RoomException(string message) : base(message)
    {
    }

    protected RoomException(string message, Exception innerException) : base(message, innerException)
    {
    }
}