﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hans
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;

            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if(msg == null)
            {
                return;
            }

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;

            if(msg.HasMentionPrefix(_client.CurrentUser, ref argPos)) //if(msg.HasStringPrefix("Hans", ref argPos))  //if(msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync("Ich verstehe nur ordentlich Deutsch.");
                }
            }
        }

    }
}
