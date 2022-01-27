using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroHub
{
    class Global
    {
        public static string username;
        public static string password;
        public static string version = "1.0.1";
        public static int progress;



       


        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }


            private static string itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        /*/
            public static void Main(string[] args)
            {
                string StrPassword = "Push@123";
                string expected = "$P$BGW0cKLlkN6VlZ7OqRUvIY1Uvo/Bh9/";

                string computed = MD5Encode(StrPassword, expected);

                Console.WriteLine(StrPassword);
                Console.WriteLine(computed);
                Console.WriteLine("Are equal? " + expected.Equals(computed));
            }
        /**/
            public static string MD5Encode(string password, string hash)
            {
                string output = "*0";
                if (hash == null)
                {
                    return output;
                }

                if (hash.StartsWith(output))
                    output = "*1";

                string id = hash.Substring(0, 3);
                // We use "$P$", phpBB3 uses "$H$" for the same thing
                if (id != "$P$" && id != "$H$")
                    return output;

                // get who many times will generate the hash
                int count_log2 = itoa64.IndexOf(hash[3]);
                if (count_log2 < 7 || count_log2 > 30)
                    return output;

                int count = 1 << count_log2;

                string salt = hash.Substring(4, 8);
                if (salt.Length != 8)
                    return output;

                byte[] hashBytes = { };
                using (MD5 md5Hash = MD5.Create())
                {
                    hashBytes = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(salt + password));
                    byte[] passBytes = Encoding.ASCII.GetBytes(password);
                    do
                    {
                        hashBytes = md5Hash.ComputeHash(hashBytes.Concat(passBytes).ToArray());
                    } while (--count > 0);
                }

                output = hash.Substring(0, 12);
                string newHash = Encode64(hashBytes, 16);

                return output + newHash;
            }

            static string Encode64(byte[] input, int count)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                do
                {
                    int value = (int)input[i++];
                    sb.Append(itoa64[value & 0x3f]); // to uppercase
                    if (i < count)
                        value = value | ((int)input[i] << 8);
                    sb.Append(itoa64[(value >> 6) & 0x3f]);
                    if (i++ >= count)
                        break;
                    if (i < count)
                        value = value | ((int)input[i] << 16);
                    sb.Append(itoa64[(value >> 12) & 0x3f]);
                    if (i++ >= count)
                        break;
                    sb.Append(itoa64[(value >> 18) & 0x3f]);
                } while (i < count);

                return sb.ToString();
            }
        }

    }

