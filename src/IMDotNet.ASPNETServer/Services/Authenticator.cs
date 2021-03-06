#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.ASPNETServer.
// File Name   : Authenticator.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 21:58
// Description :

#endregion

using IMDotNet.Shared.Data;

namespace IMDotNet.ASPNETServer.Services;

public class Authenticator : IAuthentication<User>
{
    public bool Authenticate(User principle)
    {
        throw new NotImplementedException();
    }
}