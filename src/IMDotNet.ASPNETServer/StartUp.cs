#region FileInfo

// Copyright (c) 2022 Wang Qirui. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// This file is part of Project IMDotNet.ASPNETServer.
// File Name   : StartUp.cs
// Author      : Qirui Wang
// Created at  : 2022/03/05 17:11
// Description :

#endregion

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IMDotNet.ASPNETServer;

public class StartUp
{
    /// <summary>
    ///     This method gets called by the runtime. Use this method to add services to the container.
    ///     For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    ///     This method gets called by the runtime.
    ///     Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.Run(async context => { await context.Response.WriteAsync("Unexpected protocol!"); });
    }
}