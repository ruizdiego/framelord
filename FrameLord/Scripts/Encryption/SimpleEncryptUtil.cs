
// FrameLord
using FrameLord.String;

namespace FrameLord.Encryption
{
    public static class SimpleEncryptUtil
    {
        const byte key = 200;

        /// <summary>
        /// Encrypt
        /// </summary>
        public static string EncryptString(string plainText, string passPhrase = null)
        {
            System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
            byte[] input = encoding.GetBytes(plainText);
            byte[] output = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (byte) (input[i] ^ key);
            }

            return StringUtil.EncodeTo64(output);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        public static string DecryptString(string cipherTextB64, string passPhrase = null)
        {
            byte[] input = StringUtil.DecodeFrom64ToByteArray(cipherTextB64);

            byte[] output = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (byte) (input[i] ^ key);
            }

            System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
            return encoding.GetString(output);
        }
    }
}