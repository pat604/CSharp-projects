using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurrentProject.Class
{
    static class Helper
    {

        public static DateTime CurrentTimestamp()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        }

        public static string PasswordHash(string password, string extra)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password + extra);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }

        public static string Random()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        public static void SyncMessageBox(string errorString)
        {
            MessageBox.Show("Adatok feldolgozása folyamatban!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
