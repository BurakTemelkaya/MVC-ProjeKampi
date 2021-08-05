using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BusinessLayer.Utilities
{
    class HashingHelper
    {
        //Admin bilgilerini Hashleme
        public static void AdminCreatePasswordHash(string adminPassword, out byte[] adminPasswordHash, out byte[] adminPasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                adminPasswordSalt = hmac.Key;
                adminPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminPassword));                
            }
        }
        public static void AdminCreateMailHash(string adminMail, out byte[] adminMailHash, out byte[] adminMailSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                adminMailSalt = hmac.Key;
                adminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
            }            
        }
        //login control
        public static bool AdminVerifyPasswordHash(string adminPassword, byte[] adminPasswordHash, byte[] adminPasswordSalt)
        {
            using (var hmac = new HMACSHA512(adminPasswordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminPassword));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != adminPasswordHash[i])
                    {
                        return false;
                    }
                }
                /*var computedAdminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
                for (int i = 0; i < computedAdminMailHash.Length; i++)
                {
                    if (computedAdminMailHash[i] != adminMailHash[i])
                    {
                        return false;
                    }
                }*/
                return true;
            }
        }

        public static bool AdminVerifyMailHash(string adminMail, byte[] adminMailHash , byte[] adminMailSalt)
        {
            using (var hmac = new HMACSHA512(adminMailSalt))
            {
                var computedAdminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
                for (int i = 0; i < computedAdminMailHash.Length; i++)
                {
                    if (computedAdminMailHash[i] != adminMailHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static string AdminPasswordDecode(string adminPassword)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512Hasher = new SHA512Managed();
            byte[] hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(adminPassword));
            return Convert.ToBase64String(hashedDataBytes);
        }

        //WRITER bilgilerini hashleme

        public static void WriterCreatePasswordHash(string adminPassword, out byte[] adminPasswordHash, out byte[] adminPasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                adminPasswordSalt = hmac.Key;
                adminPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminPassword));
                // writerMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(writerMail));
            }
        }

        public static bool WriterVerifyPasswordHash(string adminPassword, byte[] adminPasswordHash, byte[] adminPasswordSalt)
        {
            using (var hmac = new HMACSHA512(adminPasswordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminPassword));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != adminPasswordHash[i])
                    {
                        return false;
                    }
                }

                //var computedWriterMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(writerMail));
                //for (int i = 0; i < computedWriterMailHash.Length; i++)
                //{
                //    if (computedWriterMailHash[i] != writerMailHash[i])
                //    {
                //        return false;
                //    }
                //}
                return true;
            }
        }
    }
}
