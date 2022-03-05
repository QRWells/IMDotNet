#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : UserNonExistsException.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:52
// Description :

#endregion

namespace IMDotNet.Shared.Exceptions;

public class UserNonExistsException : UserException
{
    public UserNonExistsException(string message) : base(message)
    {
    }

    public UserNonExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}