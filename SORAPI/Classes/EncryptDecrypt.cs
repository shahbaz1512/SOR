using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Serilog;
using SORAPI.Interface;

namespace SORAPI.Classes
{
    public class EncryptDecrypt :Crypto
    {
        string certPath = "E:\\Documents\\MaximusNew.pfx";
        string certPassword = "P@ss1234";
        //public static string EncryptString(string plainText)
        //{
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        //        aesAlg.IV = Encoding.UTF8.GetBytes(iv);

        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    swEncrypt.Write(plainText);
        //                }
        //                return Convert.ToBase64String(msEncrypt.ToArray());
        //            }
        //        }
        //    }
        //}

        //public static string DecryptString(string cipherText)
        //{
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        //        aesAlg.IV = Encoding.UTF8.GetBytes(iv);

        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {
        //                    return srDecrypt.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}

        public async Task<string> SSLDecrypt(string encryptedMessage)
        {
            string decryptedMessage = string.Empty;
            try
            {
                X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
                // Get the public key
                using (RSA privateKey = cert.GetRSAPrivateKey())
                {
                    // Decrypt the message using the private key
                    decryptedMessage = EncryptDecrypt.Decrypt(encryptedMessage, privateKey);
                    Console.WriteLine("Decrypted Message: \n" + decryptedMessage);
                }
                return decryptedMessage;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during SSLDecrypt.");
                return encryptedMessage;
            }
        }

        public async Task<string> SSLEncrypt(string PlainMessage)
        {
            string encryptedMessage = string.Empty;
            try
            {
                X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
                // Get the public key
                using (RSA publicKey = cert.GetRSAPublicKey())
                {
                    // Encrypt the message using the public key
                    encryptedMessage = EncryptDecrypt.Encrypt(PlainMessage, publicKey);
                    Console.WriteLine("Encrypted Message: \n" + encryptedMessage);
                }
                return encryptedMessage;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during SSLencrypt.");
                return PlainMessage;
            }
        }

        public static string Encrypt(string text, RSA publicKey)
        {
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(text);
            byte[] encryptedData = publicKey.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string encryptedText, RSA privateKey)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(encryptedText);
            byte[] decryptedData = privateKey.Decrypt(dataToDecrypt, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedData);
        }

    }
}
