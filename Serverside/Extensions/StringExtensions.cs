﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Serverside.Extensions {
    public static class StringExtensions {
        public static string ToMD5Hash(this string input) {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
