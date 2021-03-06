﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KeyMapper.Classes;

namespace KeyMapper.Providers
{
    public static class LogProvider
    {
        static LogProvider()
        {
            _logFileName = Path.Combine(AppController.KeyMapperFilePath, ConsoleOutputFilename);
        }

        private const string ConsoleOutputFilename = "keymapper.log";

        // Redirect console output
        private static StreamWriter _consoleWriterStream;
        private static readonly string _logFileName;

        public static void ClearLogFile()
        {
            if (_consoleWriterStream != null)
            {
                _consoleWriterStream.BaseStream.SetLength(0);
                Console.WriteLine("Log file cleared: {0}", DateTime.Now);
            }
            else
            {
                Console.Write("Can't clear log in debug mode.");
            }
        }

        public static void RedirectConsoleOutput()
        {
            string path = _logFileName;
            string existingLogEntries = String.Empty;

            if (String.IsNullOrEmpty(path))
                return;

            if (File.Exists(path))
            {
                // In order to be able to clear the log, the streamwriter must be opened in create mode.
                // so read the contents of the log first.

                using (var sr = new StreamReader(path))
                {
                    existingLogEntries = sr.ReadToEnd();
                }
            }

            _consoleWriterStream = new StreamWriter(path, false, Encoding.UTF8);
            _consoleWriterStream.AutoFlush = true;
            _consoleWriterStream.Write(existingLogEntries);

            // Direct standard output to the log file.
            Console.SetOut(_consoleWriterStream);

            Console.WriteLine("Logging started: {0}", DateTime.Now);
        }

        public static void CloseConsoleOutput()
        {
            if (_consoleWriterStream != null)
            {
                _consoleWriterStream.Close();
            }
        }

        public static void ViewLogFile()
        {
            string logfile = _logFileName;
            if (string.IsNullOrEmpty(logfile))
                return;

            Process.Start(logfile);
        }

    }
}
