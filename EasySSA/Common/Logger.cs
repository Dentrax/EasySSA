#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion


using System;
using System.IO;
using System.Text;

namespace EasySSA.Common {
    public sealed class Logger {

        public const string OUTPUT_DEBUG_DIRECTORY_NAME = "Logs";

        public const string OUTPUT_DEBUG_FILE_EXTENSION = "log";

        public const string DEBUG_LOG_HEADER = @"
        ====================================================

        EasySSA Copyright(C) 2017 Furkan Türkal
        This program comes with ABSOLUTELY NO WARRANTY; This is free software,
        and you are welcome to redistribute it under certain conditions; See
        file LICENSE, which is part of this source code package, for details.

        For more information plase visit : https://goo.gl/LnnkSn

        ====================================================
  

        This file contains a log of all EasySSA operations performed during the last run.

        Interrogate this file to track errors or to help track down game-related
        issues, packets, debug. You can do this by tracing the allocations performed by a specific owner
        or by tracking a specific address through a series of allocations and
        reallocations.

        There is a lot of useful information here which, when used creatively, can be
        extremely helpful.

        Note that the following guides are used throughout this file:

   
           [+] - Allocation
           [~] - Reallocation
           [-] - Deallocation
           [I] - Generic information
           [D] - Information used for debugging
           [W] - Generic warning
           [F] - Failure induced for the purpose of stress-testing your application 
           [!] - Error
           [?] - Unhandled log type

        ...so, to find all errors in the file, search for [!].

        
        Copyright : Silkroad Security API by Drew 'pushedx' Benton : https://goo.gl/wzpNfi 
        ";




        public static readonly Logger MAIN = new Logger("MAIN");

        public static readonly Logger SERVICE = new Logger("SERVICE");

        public static readonly Logger TCP = new Logger("TCP");

        public static readonly Logger NETWORK = new Logger("NETWORK");

        public static readonly Logger CLIENT = new Logger("CLIENT");

        public static readonly Logger PACKET = new Logger("PACKET");

        public bool WasInitialized { get; private set; }

        public static bool CanPrint { get; set; }

        private string m_name;

        private string m_path;

        private StreamWriter m_fileWriter;

        public Logger(string name) {
            this.m_name = name;
            CanPrint = true;
        }

        public void Print(LogLevel level, string message) {
            this.PrintToFile(level, message);
            this.PrintToConsole(level, message);
        }

        public void PrintToFile(string format, params object[] args) {
            this.PrintToFile(LogLevel.Debug, format, args);
        }
        public void PrintToFile(LogLevel level, string format, params object[] args) {
            this.PrintToFile(level, string.Format(format, args));
        }
        public void PrintToFile(LogLevel level, string message) {
            this.Initialize();
            if (this.m_fileWriter == null || !(CanPrint)) return;

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(DateTime.Today.ToShortDateString());
            sb.Append(" ");
            sb.Append(DateTime.Now.TimeOfDay.ToString());
            sb.Append("]-");
            sb.Append(GetLogLevelString(level));
            sb.Append(": ");
            sb.Append(message);
            this.m_fileWriter.WriteLine(sb.ToString());
        }

        public void PrintToConsole(string format, params object[] args) {
            this.PrintToConsole(LogLevel.Debug, format, args);
        }
        public void PrintToConsole(LogLevel level, string format, params object[] args) {
            this.PrintToConsole(level, string.Format(format, args));
        }
        public void PrintToConsole(LogLevel level, string message) {
            if (!(CanPrint)) return;
            string text = string.Format("[{0}]: {1}", this.m_name, message);
            switch (level) {
                case LogLevel.Debug:
                case LogLevel.Info:

                    Console.WriteLine(text);
                    break;
                case LogLevel.Warning:
                    Console.WriteLine(text);
                    break;
                case LogLevel.Error:
                    Console.WriteLine(text);
                    break;
            }
        }

        private void Initialize() {
            if (this.WasInitialized) return;

            this.m_fileWriter = null;
            string dir = OUTPUT_DEBUG_DIRECTORY_NAME;
            bool firstTime = false;

            if (!Directory.Exists(dir)) {
                try { Directory.CreateDirectory(dir); } catch { }
            }

            this.m_path = string.Format("{0}/{1}.{2}", dir, this.m_name, OUTPUT_DEBUG_FILE_EXTENSION);

            if (!File.Exists(this.m_path)) firstTime = true;
            try {
                this.m_fileWriter = new StreamWriter(this.m_path, true);
                this.m_fileWriter.AutoFlush = true;
                if (firstTime) RaiseOnLogFileCreated();
            } catch { }

            this.WasInitialized = true;
        }

        private void RaiseOnLogFileCreated() {
            StringBuilder sb = new StringBuilder();

            string fileCreatedTag = "Log file created at ";
            string fileCreatedDateLong = DateTime.Today.ToLongDateString().ToString() + " " + DateTime.Now.TimeOfDay.ToString();
            string fileCreatedDateShort = DateTime.Today.ToShortDateString().ToString() + " " + DateTime.Now.TimeOfDay.ToString();


            sb.AppendLine(fileCreatedTag + fileCreatedDateLong);

            sb.AppendLine();

            sb.AppendLine("----------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine();
            sb.AppendLine("\t\t" + this.m_name + ".log - EasySSA logging file created on " + fileCreatedDateShort);
            sb.AppendLine();
            sb.AppendLine("----------------------------------------------------------------------------------------------------------------------------------");

            sb.AppendLine();

            sb.AppendLine(DEBUG_LOG_HEADER);
            sb.AppendLine();
            sb.AppendLine("----------------------------------------------------------------------------------------------------------------------------------");

            sb.AppendLine();

            this.m_fileWriter.WriteLine(sb.ToString());
            this.m_fileWriter.Flush();
        }

        private string GetLogLevelString(LogLevel level) {
            switch (level) {
                case LogLevel.Allocation: return "[+]";
                case LogLevel.Reallocation: return "[~]";
                case LogLevel.Deallocation: return "[-]";
                case LogLevel.Info: return "[I]";
                case LogLevel.Debug: return "[D]";
                case LogLevel.Warning: return "[W]";
                case LogLevel.Failure: return "[W]";
                case LogLevel.Error: return "[!]";
                default: return "[?]";
            }
        }

    }
}
