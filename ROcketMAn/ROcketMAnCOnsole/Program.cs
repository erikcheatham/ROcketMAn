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
            var env = ROcketMAnCOnsole.AppConfig.ROcketMAnEnvironment.Mocked;
            //var env = ROcketMAnCOnsole.AppConfig.ROcketMAnEnvironment.Dev;
            AppConfig.InitEnv(env);

            //throw new NotImplementedException();
            cl = new DiscordSocketClient();
            cmd = new CommandService();

            cl.Log += Log;

            await cl.LoginAsync(TokenType.Bot, AppConfig.DiscordAPIKey);
            await cl.StartAsync();

            //Block this task until the program is closed.
            await Task.Delay(-1);
        }

        //private async Task Mess

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
