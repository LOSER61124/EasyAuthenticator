using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyAuthenticator.Ext
{
    public static class EasyAES
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="password">用户传入密钥字符串，可以任意长度</param>
        /// <param name="plainText">明文</param>
        /// <returns>Base64密文（内部包含随机IV）</returns>
        public static string AesEncrypt(string password, string plainText)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            // PBKDF2 派生得到AES‑256(32字节)密钥
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] aesKey = pbkdf2.GetBytes(32);

            byte[] iv = new byte[16];
            RandomNumberGenerator.Fill(iv);

            using Aes aes = Aes.Create();
            aes.Key = aesKey;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] input = Encoding.UTF8.GetBytes(plainText);

            using var ms = new MemoryStream();
            ms.Write(salt);       //16字节salt
            ms.Write(iv);         //16字节iv
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="password">用户传入密钥字符串，和加密时一致</param>
        /// <param name="cipherBase64">AesEncrypt返回的base64密文</param>
        /// <returns>原始明文</returns>
        /// <exception cref="CryptographicException">密码错误或者密文被篡改抛出异常</exception>
        public static string AesDecrypt(string password, string cipherBase64)
        {
            byte[] allData = Convert.FromBase64String(cipherBase64);
            using var ms = new MemoryStream(allData);

            byte[] salt = new byte[16];
            ms.Read(salt, 0, salt.Length);

            byte[] iv = new byte[16];
            ms.Read(iv, 0, iv.Length);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] aesKey = pbkdf2.GetBytes(32);

            using Aes aes = Aes.Create();
            aes.Key = aesKey;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor();
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.UTF8);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 自动处理：传入明文返回密文；传入密文返回明文
        /// 存在极小概率误判，正式业务优先分开调用Encrypt/Decrypt
        /// </summary>
        /// <param name="password">加密解密密码</param>
        /// <param name="input">明文或者base64密文</param>
        /// <returns>处理结果</returns>
        public static string AesAutoEncodeOrDecode(string password, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            try
            {
                //先尝试当做密文解密
                string decrypted = AesDecrypt(password, input);
                //解密没有抛异常，代表是密文，返回解密后的明文
                return decrypted;
            }
            catch (FormatException)
            {
                //不是合法Base64，判定为明文，执行加密
                return AesEncrypt(password, input);
            }
            catch (CryptographicException)
            {
                //base64合法，但密码不对/不是本工具生成的密文，当做明文加密
                return AesEncrypt(password, input);
            }
        }

    }
}
