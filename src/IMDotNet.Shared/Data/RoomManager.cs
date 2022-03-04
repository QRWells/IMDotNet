#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.Shared.
// File Name   : RoomManager.cs
// Author      : Qirui Wang
// Created at  : 2022/03/04 18:23
// Description :

#endregion

using System.Text.Json;
using IMDotNet.Shared.Exceptions;

namespace IMDotNet.Shared.Data;

public class RoomManager
{
    public List<Room> RoomList { get; set; } = new();

    public void AddChatroom(Room newRoom)
    {
        var chatroomToDelete = RoomList.FirstOrDefault(chatroom => chatroom.Name == newRoom.Name);
        if (chatroomToDelete != null) throw new RoomExistsException(newRoom.Name);

        RoomList.Add(newRoom);
    }

    public void RemoveChatroom(string name)
    {
        var chatroomToDelete = RoomList.FirstOrDefault(chatroom => chatroom.Name == name);
        if (chatroomToDelete == null) throw new RoomNonExistsException(name);

        RoomList.Remove(chatroomToDelete);
    }

    public async void LoadAsync(string path)
    {
        try
        {
            await using Stream stream = File.Open(path, FileMode.Open);
            var chatRooms = await JsonSerializer.DeserializeAsync<List<Room>>(stream) ?? new List<Room>();
            RoomList = chatRooms;
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async void SaveAsync(string path)
    {
        try
        {
            await using Stream stream = File.Open(path, FileMode.Create);
            await JsonSerializer.SerializeAsync(stream, RoomList);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}