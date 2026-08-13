using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyAuthenticator.Ext
{
    public static class TotpHelper
    {
        /// <summary>
        /// 获取TOTP三组验证码 + 距离下一次刷新剩余秒数（GitHub适用）
        /// </summary>
        /// <param name="base32Secret">GitHub的base32密钥</param>
        /// <returns>prev,current,next,remainSeconds</returns>
        public static TOTPDetailsDto GetTotpWindowCodes(string base32Secret)
        {
            byte[] key = Base32Decode(base32Secret);
            long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long counter = unixTime / 30;
            //计算距离下一轮刷新剩余秒数
            int remainSeconds = 30 - (int)(unixTime % 30);

            var prev = ComputeTotpCode(key, counter - 1);
            var current = ComputeTotpCode(key, counter);
            var next = ComputeTotpCode(key, counter + 1);

            return new TOTPDetailsDto
            {
                PrePWD = prev,
                CurrentPDW = current,
                NextPDW = next,
                RemainTime = remainSeconds
            };
        }

        /// <summary>
        /// 根据counter计算单组TOTP
        /// </summary>
        private static string ComputeTotpCode(byte[] key, long counter)
        {
            byte[] counterBytes = BitConverter.GetBytes(counter);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(counterBytes);

            using var hmac = new HMACSHA1(key);
            byte[] hash = hmac.ComputeHash(counterBytes);

            int offset = hash[hash.Length - 1] & 0x0F;
            int binary =
                ((hash[offset] & 0x7F) << 24)
                | ((hash[offset + 1] & 0xFF) << 16)
                | ((hash[offset + 2] & 0xFF) << 8)
                | (hash[offset + 3] & 0xFF);

            int code = binary % 1000000;
            return code.ToString("D6"); // D6 保证6位，前面补0
        }

        /// <summary>
        /// RFC4648 Base32解码，谷歌Authenticator / GitHub TOTP专用
        /// </summary>
        private static byte[] Base32Decode(string input)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            input = input.ToUpperInvariant().Replace("=", "");
            int bitCount = input.Length * 5;
            byte[] output = new byte[(bitCount + 7) / 8];

            int bitPos = 0;
            foreach (char c in input)
            {
                int val = alphabet.IndexOf(c);
                if (val < 0)
                    throw new FormatException($"非法Base32字符：{c}");

                for (int i = 4; i >= 0; i--)
                {
                    if ((val & (1 << i)) != 0)
                    {
                        int byteIndex = bitPos / 8;
                        int bitIndex = 7 - (bitPos % 8);
                        output[byteIndex] |= (byte)(1 << bitIndex);
                    }
                    bitPos++;
                }
            }
            return output;
        }

        /// <summary>
        /// 生成TOTP规范随机Base32密钥（仅本地测试）
        /// </summary>
        public static string CreateRandomBase32Secret(int length = 16)
        {
            const string base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            Random random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = base32Alphabet[random.Next(base32Alphabet.Length)];
            }
            return new string(result);
        }


        public class TOTPDetailsDto
        {

            /// <summary>
            /// 上一个密码
            /// </summary>
            public string PrePWD { get; set; } = string.Empty;

            /// <summary>
            /// 当前密码
            /// </summary>
            public string CurrentPDW { get; set; } = string.Empty;

            /// <summary>
            /// 下一个密码
            /// </summary>
            public string NextPDW { get; set; } = string.Empty;

            /// <summary>
            /// 当前密码剩余时间（s）
            /// </summary>
            public int RemainTime { get; set; }
        }

    }


}
