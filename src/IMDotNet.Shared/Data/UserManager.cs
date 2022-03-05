#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : UserManager.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:22
// Description :

#endregion

using System.Text.Json;
using IMDotNet.Shared.Exceptions;

namespace IMDotNet.Shared.Data;

public class UserManager
{
    /// <summary>
    ///     Store users in a list
    /// </summary>
    public List<User> UserList { get; set; } = new();


    /// <summary>
    ///     Add an user to the list. Make sure it does not already exist, using his login.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    public void AddUser(string login, string password)
    {
        if (UserList.ToList().Any(user => user.Login == login)) throw new UserExistsException(login);

        UserList.Add(new User(login, password));
    }

    /// <summary>
    ///     Remove the user from the list based on his login
    /// </summary>
    /// <param name="login"></param>
    public void RemoveUser(string login)
    {
        User? userToDelete = null;

        foreach (var user in UserList.ToList().Where(user => user.Login == login)) userToDelete = user;

        if (userToDelete == null) throw new UserNonExistsException(login);

        UserList.Remove(userToDelete);
    }

    /// <summary>
    ///     Find an user in the list
    /// </summary>
    /// <param name="other">User to look for</param>
    /// <returns>User found</returns>
    public User GetUser(User other)
    {
        User getUser = null;

        foreach (var user in UserList.ToList()
                     .Where(user => user.Login == other.Login && user.Password == other.Password))
            getUser = user;

        if (getUser == null) throw new UserNonExistsException(other.Login);

        return getUser;
    }

    /// <summary>
    ///     Auth the user based on his password.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    public bool Authenticate(string login, string password)
    {
        User? u = null;

        foreach (var user in
                 UserList.ToList().Where(user => user.Login == login && user.Password == password)) u = user;

        return u != null;
    }

    public async void Load(string path)
    {
        try
        {
            await using Stream stream = File.Open(path, FileMode.Open);
            var users = await JsonSerializer.DeserializeAsync<List<User>>(stream);
            UserList = users;
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async void Save(string path)
    {
        try
        {
            await using Stream stream = File.Open(path, FileMode.Create);
            await JsonSerializer.SerializeAsync(stream, UserList);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}