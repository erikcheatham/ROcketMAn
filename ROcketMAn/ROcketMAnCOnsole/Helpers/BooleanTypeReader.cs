﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// Note: This example is obsolete, a boolean type reader is bundled with Discord.Commands
using Discord;
using Discord.Commands;

namespace ROcketMAnCOnsole.Helpers
{
    public class BooleanTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            bool result;
            if (bool.TryParse(input, out result))
                return Task.FromResult(TypeReaderResult.FromSuccess(result));

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as a boolean."));
        }
    }
}