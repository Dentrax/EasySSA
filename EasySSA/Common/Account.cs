﻿#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Common {
    public struct Account {

        public string Username { get; private set; }

        public string Password { get; private set; }

        public Account(string username, string password) {
            this.Username = username;
            this.Password = password;
        }

    }
}
