using System;
using System.Collections.Generic;
using System.Text;

namespace ROcketMAnCOnsole
{
    public static class AppConfig
    {
        public static string DiscordClientID;
        public static string DiscordAPIKey;
        public static string BaseServiceUrl;
        public static ROcketMAnEnvironment environment;
        //public static UserType MockedRole;
        //public static UserType UserRole;

        public static void InitEnv(ROcketMAnEnvironment env)
        {
            environment = env;
            switch (env)
            {
                case ROcketMAnEnvironment.Mocked:
                    {
                        DiscordClientID = "399970090084335627";
                        DiscordAPIKey = "Mzk5OTcwMDkwMDg0MzM1NjI3.DTVL7w.OGcA_opEPC14yNJkU5k4Mk0aYF4";
                        BaseServiceUrl = $"https://discordapp.com/oauth2/authorize?client_id={DiscordClientID}&scope=bot";
                        break;
                    }
                case ROcketMAnEnvironment.Dev:
                    {
                        break;
                    }
            }
        }

        public enum ROcketMAnEnvironment
        {
            Mocked,
            Dev
        }
    }
}