﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Command.Bot.Core.Runner
{
    public class BatchFile : BaseCommandLineRunner, IRunner
    {
        #region Implementation of IRunner

        public string Extension {
            get { return ".bat"; }
        }

        public FileRunner GetRunner(string filePath)
        {
            if (this.IsExtensionMatch(filePath))
            {
                var command = Path.GetFileNameWithoutExtension(filePath);
                return new FileRunner(command, string.Format("run {0} command.", Path.GetFileName(filePath)),ProcessFile(filePath), filePath);
            }
            return null;
        }

        private Func<IMessageContext, IEnumerable<string>> ProcessFile(string filePath)
        {
            return (context) => {
                ExecuteCommand(context,filePath);
                return new string[0];
            };
        }

        #endregion

        void ExecuteCommand(IMessageContext context, string command)
        {
            RunCommand(context, command);
        }
    }
}