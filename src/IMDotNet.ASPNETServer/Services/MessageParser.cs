#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.ASPNETServer.
// File Name   : MessageParser.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 21:58
// Description :

#endregion

using System.Buffers;
using IMDotNet.Shared.Message;

namespace IMDotNet.ASPNETServer.Services;

public class MessageParser : IMessageParser
{
    private enum State
    {
        Empty,
        Header,
        Body
    }

    private State _currentState = State.Empty;
    private int _headerBufferCount;
    private readonly byte[] _headerBuffer = new byte[MessageHeader.Size];

    public bool TryParseMessage(in ReadOnlySequence<byte> buffer, out Message message)
    {
        if (buffer.IsEmpty)
        {
            message = null;
            return false;
        }

        foreach (var seq in buffer)
        {
            switch (_currentState)
            {
                case State.Empty:
                    HandleEmpty(seq);
                    break;
                case State.Header:
                    HandleHeader(seq);
                    break;
                case State.Body:
                    HandleBody(seq);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buffer));
            }
        }

        message = null;
        return false;
    }

    private void HandleEmpty(in ReadOnlyMemory<byte> buffer)
    {
        if (buffer.Length < MessageHeader.Size)
        {
            _currentState = State.Header;
            buffer.CopyTo(_headerBuffer);
            _headerBufferCount = buffer.Length;
        }
        else
            HandleBody(buffer);
    }

    private void HandleHeader(in ReadOnlyMemory<byte> buffer)
    {
        if (_headerBufferCount + buffer.Length >= MessageHeader.Size)
        {
            Array.Clear(_headerBuffer);
            _headerBufferCount = 0;
            HandleBody(buffer);
            return;
        }

        var rem = MessageHeader.Size - _headerBufferCount;
        Array.Copy(
            buffer.ToArray(),
            0,
            _headerBuffer,
            _headerBufferCount,
            Math.Min(rem, buffer.Length));
    }

    private void HandleBody(in ReadOnlyMemory<byte> buffer)
    {
        // TODO
    }
}