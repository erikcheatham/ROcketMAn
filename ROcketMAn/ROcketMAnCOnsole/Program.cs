using System;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ROcketMAnCOnsole.Modules;
using System.Collections.Generic;

namespace ROcketMAnCOnsole
{
    public class Program
    {
        private CommandService cmd;
        private DiscordSocketClient cl;
        private IServiceProvider serv;
        private ConfigurationSection ccs;

        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();
        //{
        //    Console.WriteLine("Hello World!");
        //}

        public async Task Start()
        {
            //throw new NotImplementedException();

            var env = ROcketMAnCOnsole.AppConfig.ROcketMAnEnvironment.Mocked;
            //var env = ROcketMAnCOnsole.AppConfig.ROcketMAnEnvironment.Dev;
            AppConfig.InitEnv(env);

            cl = new DiscordSocketClient();
            cmd = new CommandService();

            //cl.Log += Log;

            serv = new ServiceCollection()
                .AddSingleton(cl)
                .AddSingleton(cmd)
                .BuildServiceProvider();

            await InstallCommandsAsync();

            await cl.LoginAsync(TokenType.Bot, AppConfig.DiscordAPIKey);
            await cl.StartAsync();

            //Block this task until the program is closed.
            await Task.Delay(-1);
        }

        //public async Task InstallAsync()
        //{
        //    cl.Log += Log;
        //    // Hook the MessageReceived Event into our Command Handler
        //    cl.MessageReceived += HandleCommandAsync;
        //    // Here, we will inject the ServiceProvider with 
        //    // all of the services our client will use.
        //    serv = new ServiceCollection()
        //        .AddSingleton(cl)
        //        .AddSingleton(cmd)
        //        // You can pass in an instance of the desired type
        //        .AddSingleton(new NotificationService())
        //        // ...or by using the generic method.
        //        //.AddSingleton<DatabaseService>()
        //        .BuildServiceProvider();
        //    // ...
        //    await cmd.AddModulesAsync(Assembly.GetEntryAssembly());
        //}


        private async Task InstallCommandsAsync()
        {
            cl.Log += Log;
            // Hook the MessageReceived Event into our Command Handler
            cl.MessageReceived += HandleCommandAsync;
            // Discover all of the commands in this assembly and load them.
            await cmd.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('r', ref argPos) || message.HasMentionPrefix(cl.CurrentUser, ref argPos))) return;
            {
                //if (message.Content.ToString() == "rha")
                //{
                //    //Weather wc = new Weather();
                //    //await wc.SayMyWeather("weather bitch!");
                //    // Create a Command Context
                var context = new SocketCommandContext(cl, message);
                //    //var context = new SocketCommandContext(cl, message);

                //    //SocketUserMessage bot = new SocketUserMessage();

                //    message.MentionedUsers = "IRishShark";


                //    bot.MentionedUsers = "IRishShark";


                //var context = new SocketCommandContext(cl, message =>
                //{
                //    IMessage = "@IRishShark you a sucka g";
                //        //SocketUser user = user.Username = "";
                //    });
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully)
                var result = await cmd.ExecuteAsync(context, argPos, serv);
                if (!result.IsSuccess)
                {
                    var currentUser = context.Client.CurrentUser;
                    //List<SocketGuildUser> us = new List<SocketGuildUser>();
                    //ulong g = 0;
                    //if(ulong.TryParse(AppConfig.DiscordClientID, out g))
                    //{
                    //    var users = context.Client.DownloadUsersAsync(g).toList();
                    //}

                    var guild = cl.GetGuild(ulong.Parse(AppConfig.DiscordClientID));

                    //var users = context.Client.UserJoined().Id = "";

                    //var users = await context.User


                    var guilds = context.Client.Guilds;
                    var us = await context.Client.DownloadUsersAsync(guilds);

                    if (context.User.Username == "IrishShark")
                    //context.User.Username = "IrishShark";
                    //context.Guild.GetUser = "Irishshark";
                    //context.Message = "@IRishShark you a sucka g";
                    foreach (var u in context.Client.DownloadUsersAsync)
                    context.User.Username = "IrishShark";
                    //var u = context.Message.MentionedUsers = { "Irish" };
                    var user = context.Client.GetUser("IrishShark", "@");
                    //foreach (var user in users)

                    await context.Channel.SendMessageAsync(user, "you a sucka g");
                }
                //await context.Channel.SendMessageAsync(result.ErrorReason);
                //else
                //    await context.Channel.SendMessageAsync("weather bitch!");
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}