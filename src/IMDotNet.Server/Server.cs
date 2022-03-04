#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Server.
// File Name   : Server.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 17:16
// Description :

#endregion

using System;
using System.Net;
using System.Net.Sockets;
using IMDotNet.Shared.Network;

namespace IMDotNet.Server;

public class Server : TcpServer
{
    public Server(IPAddress address, int port) : base(address, port)
    {
    }

    protected override TcpSession CreateSession()
    {
        return new Session(this);
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
    }
}