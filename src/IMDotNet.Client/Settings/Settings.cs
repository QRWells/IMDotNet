#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Client.
// File Name   : Settings.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 21:08
// Description :

#endregion

namespace IMDotNet.Client.Settings;

public record Settings(int Port, string Address);