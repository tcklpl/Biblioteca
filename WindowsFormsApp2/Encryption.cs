using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApp2 {
    class Encryption {

        /*
         *  Daqui pra baixo é PBKDF2 e um gerador de salt ae
         */

        public const int SaltByteSize = 24;
        public const int HashByteSize = 20;

        public static byte[] GetPbkdf2Bytes(String pass, byte[] salt, int ite, int outb) {
            var p = new Rfc2898DeriveBytes(pass, salt);
            p.IterationCount = ite;
            return p.GetBytes(outb);
        }

        public static String GetPbkdf2String(String pass, byte[] salt, int ite, int outb) {
            return Convert.ToBase64String(GetPbkdf2Bytes(pass, salt, ite, outb));
        }

        public static bool SlowEquals(byte[] a, byte[] b) {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++) {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        public static bool ValidatePassword(String pass, String goodHash, String salt, int ite) {
            var saltb = Convert.FromBase64String(salt);
            var hashb = Convert.FromBase64String(goodHash);
            var th = GetPbkdf2Bytes(pass, saltb, ite, hashb.Length);
            return SlowEquals(hashb, th);
        }

        public static String GenerateSalt() {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[HashByteSize];
            cryptoProvider.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        /*
         *  Daqui pra baixo é AES-128
         */

        public static byte[] AES128EncryptBytes(byte[] data, byte[] pass) {
            byte[] encrypted, IV;
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = pass;
                aesAlg.GenerateIV();
                IV = aesAlg.IV;
                aesAlg.Mode = CipherMode.CBC;
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream()) {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                        using (var swEncrypt = new StreamWriter(csEncrypt)) {
                            swEncrypt.Write(data);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                var combinesIvCt = new byte[IV.Length + encrypted.Length];
                Array.Copy(IV, 0, combinesIvCt, 0, IV.Length);
                Array.Copy(encrypted, 0, combinesIvCt, IV.Length, encrypted.Length);
                return combinesIvCt;
            }
        }

        public static byte[] AES128DecryptBytes(byte[] data, byte[] pass) {
            byte[] plain = null;
            String ontime = null;
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = pass;
                byte[] IV = new byte[aesAlg.BlockSize / 8];
                byte[] cipherText = new byte[data.Length - IV.Length];
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherText)) {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (var srDecrypt = new StreamReader(csDecrypt)) {
                            ontime = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            plain = Convert.FromBase64String(ontime);
            return plain;
        }

        public static String GetAES128EncryptAsBase64String(String data, String pass) {
            return Convert.ToBase64String(AES128EncryptBytes(Convert.FromBase64String(data), Convert.FromBase64String(pass)));
        }

        public static String GetAES128DecryptAsBase64String(String data, String pass) {
            return Convert.ToBase64String(AES128DecryptBytes(Convert.FromBase64String(data), Convert.FromBase64String(pass)));
        }

        public static byte[] get16Key() {
            using (var random = new RNGCryptoServiceProvider()) {
                var key = new byte[16];
                random.GetBytes(key);
                return key;
            }
        }



    }

}