using System.Security.Cryptography;
using System.Text;

namespace Wemuda_book_app.Helpers
{
    public interface IPasswordHelper
    {
        (byte[] passwordHash, byte[] passwordSalt) CreateHash(string password);
        string GenerateRandomString(int length);
    }

    public class PasswordHelper : IPasswordHelper
    {
        public string GenerateRandomString(int length)
        {
            return new(Enumerable.
                Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
                .Select(x =>
                {
                    var cryptoResult = new byte[4];
                    using (var cryptoProvider = new RNGCryptoServiceProvider())
                    {
                        cryptoProvider.GetBytes(cryptoResult);
                    }

                    return x[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(x.Length)];
                }).ToArray());
        }

        public (byte[] passwordHash, byte[] passwordSalt) CreateHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            using var hmac = new HMACSHA512();

            return (
                passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes((password))),
                passwordSalt: hmac.Key
            );
        }
    }
}
