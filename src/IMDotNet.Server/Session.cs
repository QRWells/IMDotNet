#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Server.
// File Name   : Session.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 20:20
// Description :

#endregion

using System;
using System.Net.Sockets;
using System.Text;
using IMDotNet.Shared.Network;

namespace IMDotNet.Server;

public class Session : TcpSession
{
    public Session(TcpServer server) : base(server)
    {
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} connected!");

        // Send invite message
        const string message = "Hello from TCP chat! Please send a message or '!' to disconnect the client!";
        SendAsync(message);
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} disconnected!");
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        Console.WriteLine("Incoming: " + message);

        // Multicast message to all connected sessions
        Server.Multicast(message);

        // If the buffer starts with '!' the disconnect the current session
        if (message == "!")
            Disconnect();
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP session caught an error with code {error}");
    }
}