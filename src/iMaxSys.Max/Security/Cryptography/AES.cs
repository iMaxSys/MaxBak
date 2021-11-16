//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AES.cs
//摘要: AES
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

namespace iMaxSys.Max.Security.Cryptography;

public class AES
{
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string Encrypt(string value, string key, string? iv = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
        if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
        if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
        if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
        if (!string.IsNullOrEmpty(iv))
        {
            if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
        }

        var _keyByte = Encoding.UTF8.GetBytes(key);
        var _valueByte = Encoding.UTF8.GetBytes(value);
        using var aes = new RijndaelManaged();
        aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
        aes.Key = _keyByte;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        var cryptoTransform = aes.CreateEncryptor();
        var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string Decrypt1(string value, string key, string? iv = null)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
        if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
        if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
        if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
        if (!string.IsNullOrEmpty(iv))
        {
            if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
        }

        var _keyByte = Encoding.UTF8.GetBytes(key);
        var _valueByte = Convert.FromBase64String(value);
        using var aes = new RijndaelManaged();
        aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
        aes.Key = _keyByte;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        var cryptoTransform = aes.CreateDecryptor();
        var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
        return Encoding.UTF8.GetString(resultArray);
    }


    public static string? Decrypt(string value, string key, string? iv = null)
    {
        try
        {
            string i = iv.Replace(" ", "+");
            string k = key.Replace(" ", "+");
            string v = value.Replace(" ", "+");
            byte[] encryptedData = Convert.FromBase64String(v);

            RijndaelManaged rijndaelCipher = new()
            {
                Key = Convert.FromBase64String(k), // Encoding.UTF8.GetBytes(AesKey);  
                IV = Convert.FromBase64String(i),// Encoding.UTF8.GetBytes(AesIV);  
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            string result = Encoding.UTF8.GetString(plainText);

            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
