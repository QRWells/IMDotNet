#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : User.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:23
// Description :

#endregion

namespace IMDotNet.Shared.Data;

[Serializable]
public class User : IComparable<User>
{
    public User(string login, string password, Room chatroom = null)
    {
        Chatroom = chatroom;
        Login = login;
        Password = password;
    }

    public string Login { get; set; }

    public string Password { get; set; }

    public Room Chatroom { get; set; }

    public int CompareTo(User other)
    {
        if (Login == other.Login && Password == other.Password)
            return 0;

        return -1;
    }

    public override string ToString()
    {
        return Login;
    }
}