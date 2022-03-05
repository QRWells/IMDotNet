#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : FileMessage.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 15:48
// Description :

#endregion

namespace IMDotNet.Shared.Message;

public class FileMessage : Message
{
    public FileMessage(FileStream file)
    {
        File = file;
        _rawBody = new byte[file.Length];
        file.Read(_rawBody);
    }

    internal FileMessage(MessageHeader header, FileStream file)
    {
        _header = header;
        File = file;
    }

    public FileStream File { get; }
}