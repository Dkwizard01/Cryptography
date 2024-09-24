using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace Password_hashing_app
{
    public class Encryption
    {
        public string password;
        static byte[] conByte;
        public string dePass;
        static byte[] decByte;
        byte[] key = new byte[32];
        byte[] iv = new byte[16];
        string filePath = @"secret.txt";
        byte[] savedBytes;
        public Encryption(string pass)
        {
            password = pass;
            conByte = Encoding.UTF8.GetBytes(password);

        }

        public void Encrypt()
        {
            Aes encAes = Aes.Create();
            encAes.Key = key;
            encAes.IV = iv;

            using (MemoryStream mem = new MemoryStream())
            {
                using (CryptoStream crypto = new CryptoStream(mem, encAes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    crypto.Write(conByte, 0, conByte.Length);
                    
                   
                }
               
             File.WriteAllBytes(filePath, mem.ToArray());


            }


        }

        public void Decrypt()
        {

            savedBytes = File.ReadAllBytes(filePath);
            decByte = new byte[128];
            Aes decAes = Aes.Create();
            decAes.Key = key;
            decAes.IV = iv;
           

            using (MemoryStream dec = new MemoryStream(savedBytes))
            {
                using (CryptoStream decCrypt = new CryptoStream(dec, decAes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                     decCrypt.Read(decByte, 0, decByte.Length);
                  
                    dePass = Encoding.UTF8.GetString(decByte);
                }

            }


        
            
        }
    }
}
