#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.ASPNETServer.
// File Name   : IAuthentication.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 21:28
// Description :

#endregion

namespace IMDotNet.ASPNETServer.Services;

public interface IAuthentication<in TPrinciple>
{
    public bool Authenticate(TPrinciple principle);
}