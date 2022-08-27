//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SHA1.cs
//摘要: SHA1
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;
using System.Collections;

namespace iMaxSys.Max.Security.Cryptography;

/// <summary>
/// AES
/// </summary>
public static class AES
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Encrypt(string plainText, string key, string iv)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));
        if (key.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(key));
        if (iv.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(iv));
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = System.Text.Encoding.Default.GetBytes(key);
            aesAlg.IV = System.Text.Encoding.Default.GetBytes(iv);

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return Encoding.UTF8.GetString(encrypted);
    }

    /// <summary>
    /// Decrypt
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Decrypt(string cipherText, string key, string iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));
        byte[] ciphers = Encoding.UTF8.GetBytes(cipherText);
        if (key.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(key));
        if (iv.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(iv));

        // Declare the string used to hold
        // the decrypted text.
        string? plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = System.Text.Encoding.Default.GetBytes(key);
            aesAlg.IV = System.Text.Encoding.Default.GetBytes(iv);

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(ciphers))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}

