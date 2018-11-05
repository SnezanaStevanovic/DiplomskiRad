using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Timesheet.BLL.Interfaces;

namespace Timesheet.BLL
{
    public class HashService : IHashService
    {
        public string GetMd5Hash(string cryptedText)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(cryptedText));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
