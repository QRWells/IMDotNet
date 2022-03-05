﻿#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.ASPNETServer.
// File Name   : ConnectionHandler.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 17:06
// Description :

#endregion

using System.Text;
using IMDotNet.ASPNETServer.Services;
using IMDotNet.Shared.Data;
using IMDotNet.Shared.Message;
using Microsoft.AspNetCore.Connections;

namespace IMDotNet.ASPNETServer.Handlers;

public class TcpHandler : ConnectionHandler
{
    private readonly IAuthentication<User> _authenticator;
    private readonly ILogger<TcpHandler> _logger;
    private readonly IMessageParser _parser;

    public TcpHandler(ILogger<TcpHandler> logger, IMessageParser parser, IAuthentication<User> authenticator)
    {
        _logger = logger;
        _parser = parser;
        _authenticator = authenticator;
    }

    public override async Task OnConnectedAsync(ConnectionContext connection)
    {   
        // TODO: Authentication
        try
        {
            var buffer = new byte[1024];
            var mem = new Memory<byte>(buffer);
            _logger.LogInformation("{ConnectionId} connected", connection.ConnectionId);
            var input = connection.Transport.Input;
            while (true)
            {
                var res = await input.ReadAsync(connection.ConnectionClosed);
                var str = Encoding.UTF8.GetString(res.Buffer);
                // TODO: Parse
                _logger.LogInformation("Received: {}", str);
                
                if (str.Length == 0 || str == "!")
                    break;
                var bytes = Encoding.UTF8.GetBytes(Reverse(str));
                bytes.CopyTo(mem);
                
                await connection.Transport.Output.WriteAsync(mem[..bytes.Length], connection.ConnectionClosed);
                input.AdvanceTo(res.Buffer.End, res.Buffer.End);
            }
        }
        catch (Exception e)
        {
            _logger.LogTrace("Exception happened : {}", e.StackTrace ?? e.Message);
        }
        finally
        {
            _logger.LogInformation("{ConnectionId} disconnected", connection.ConnectionId);
        }
    }

    // Method for test
    private static string Reverse(string s)
    {
        var array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    private Task ProcessMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}