#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Client.
// File Name   : Client.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 21:09
// Description :

#endregion

using System.Net.Sockets;
using System.Text;
using TcpClient = IMDotNet.Shared.Network.TcpClient;

namespace IMDotNet.Client;

public class Client : TcpClient
{
    private bool _stop;

    public Client(string address, int port) : base(address, port)
    {
    }

    public void DisconnectAndStop()
    {
        _stop = true;
        DisconnectAsync();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");

        // Wait for a while...
        Thread.Sleep(1000);

        // Try to connect again
        if (!_stop)
            ConnectAsync();
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        Console.WriteLine(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP client caught an error with code {error}");
    }
}