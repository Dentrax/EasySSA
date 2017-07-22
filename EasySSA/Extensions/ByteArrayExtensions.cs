#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EasySilkroadSecurityApi.Extensions {
    public static class ByteArrayExtensions {
        public static string HexDump(this byte[] buffer) =>
        buffer.HexDump(0, buffer.Length);

        public static string HexDump(this byte[] buffer, int offset, int count) {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            int num = count;
            if ((num % 0x10) > 0) {
                num += 0x10 - (num % 0x10);
            }
            for (int i = 0; i <= num; i++) {
                if ((i % 0x10) == 0) {
                    if (i > 0) {
                        builder.AppendFormat("  {0}{1}", builder2.ToString(), Environment.NewLine);
                        builder2.Clear();
                    }
                    if (i != num) {
                        builder.AppendFormat("{0:d10}   ", i);
                    }
                }
                if (i < count) {
                    builder.AppendFormat(buffer[offset + i].ToString("X2") + " ", new object[0]);
                    char c = (char)buffer[offset + i];
                    builder2.Append(char.IsControl(c) ? '.' : c);
                } else {
                    builder.Append("   ");
                    builder2.Append('.');
                }
            }
            return builder.ToString();
        }

        public static Type ToStruct<Type>(this byte[] buffer) {
            IntPtr destination = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, destination, buffer.Length);
            Type local2 = default(Type);
            Type local = (Type)Marshal.PtrToStructure(destination, local2.GetType());
            Marshal.FreeHGlobal(destination);
            return local;
        }

    }
}
