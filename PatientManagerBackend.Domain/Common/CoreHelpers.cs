using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PatientManagerBackend.Domain.Common
{
    public static class CoreHelpers
    {
        private static readonly long _baseDateTicks = new DateTime(1900, 1, 1).Ticks;
        private static readonly DateTime _epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly Random _random = new Random();

        public static Ulid CreateUlid(DateTimeOffset timestamp)
        {
            string randomness = RandomString(16, lower: false);
            Span<byte> randomnessBytes = stackalloc byte[10];
            randomnessBytes = GenerateRandomBytes(10);
            return Ulid.NewUlid(timestamp, randomnessBytes);
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }

        

        public static string RandomString(int length, bool alpha = true, bool upper = true, bool lower = true,
            bool numeric = true, bool special = false)
        {
            return RandomString(length, RandomStringCharacters(alpha, upper, lower, numeric, special));
        }

        

        public static string RandomString(int length, string characters)
        {
            return new string(Enumerable.Repeat(characters, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string SecureRandomString(int length, bool alpha = true, bool upper = true, bool lower = true,
            bool numeric = true, bool special = false)
        {
            return SecureRandomString(length, RandomStringCharacters(alpha, upper, lower, numeric, special));
        }

        // ref https://stackoverflow.com/a/8996788/1090359 with modifications
        public static string SecureRandomString(int length, string characters)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "length cannot be less than zero.");
            }

            if ((characters?.Length ?? 0) == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(characters), "characters invalid.");
            }

            const int byteSize = 0x100;
            if (byteSize < characters.Length)
            {
                throw new ArgumentException(
                    string.Format("{0} may contain no more than {1} characters.", nameof(characters), byteSize),
                    nameof(characters));
            }

            var outOfRangeStart = byteSize - (byteSize % characters.Length);
            using (var rng = RandomNumberGenerator.Create())
            {
                var sb = new StringBuilder();
                var buffer = new byte[128];
                while (sb.Length < length)
                {
                    rng.GetBytes(buffer);
                    for (var i = 0; i < buffer.Length && sb.Length < length; ++i)
                    {
                        // Divide the byte into charSet-sized groups. If the random value falls into the last group and the
                        // last group is too small to choose from the entire allowedCharSet, ignore the value in order to
                        // avoid biasing the result.
                        if (outOfRangeStart <= buffer[i])
                        {
                            continue;
                        }

                        sb.Append(characters[buffer[i] % characters.Length]);
                    }
                }

                return sb.ToString();
            }
        }

        private static string RandomStringCharacters(bool alpha, bool upper, bool lower, bool numeric, bool special)
        {
            var characters = string.Empty;
            if (alpha)
            {
                if (upper)
                {
                    characters += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                }

                if (lower)
                {
                    characters += "abcdefghijklmnopqrstuvwxyz";
                }
            }

            if (numeric)
            {
                characters += "0123456789";
            }

            if (special)
            {
                characters += "!@#$%^*&";
            }

            return characters;
        }

        

        public static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", string.Empty);
            return output;
        }

        public static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            // 62nd char of encoding
            output = output.Replace('-', '+');
            // 63rd char of encoding
            output = output.Replace('_', '/');
            // Pad with trailing '='s
            switch (output.Length % 4)
            {
                case 0:
                    // No pad chars in this case
                    break;
                case 2:
                    // Two pad chars
                    output += "=="; break;
                case 3:
                    // One pad char
                    output += "="; break;
                default:
                    throw new InvalidOperationException("Illegal base64url string!");
            }

            // Standard base64 decoder
            return Convert.FromBase64String(output);
        }

        
        static byte[] AesEncrypt(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("Encryption text");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Encryption Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Encryption IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

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
            return encrypted;
        }

    }
}
