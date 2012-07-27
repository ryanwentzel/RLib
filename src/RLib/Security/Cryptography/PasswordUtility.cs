using System;
using System.Security.Cryptography;
using System.Text;

namespace RLib.Security.Cryptography
{
    /// <summary>
    /// A utility class that salts and hashes passwords.
    /// </summary>
    /// <remarks>
    /// See http://www.dijksterhuis.org/creating-salted-hash-values-in-c/ and 
    /// http://www.obviex.com/samples/hash.aspx for more information and examples.
    /// </remarks>
    public class PasswordUtility : IDisposable
    {
        /// <summary>
        /// The default salt length in bytes.
        /// </summary>
        public const int DefaultSaltLength = 4;

        private HashAlgorithm _hashProvider;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordUtility"/> class 
        /// using the <see cref="SHA256Managed"/> hash algorithm.
        /// </summary>
        public PasswordUtility()
            : this(new SHA256Managed())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordUtility"/> class 
        /// using the supplied <see cref="HashAlgorithm"/>.
        /// </summary>
        /// <param name="hashProvider">Hash algorithm.</param>
        public PasswordUtility(HashAlgorithm hashProvider)
        {
            if (hashProvider == null)
            {
                throw new ArgumentNullException("hashProvider");
            }

            _hashProvider = hashProvider;
        }

        /// <summary>
        /// Computes the hash value of the specified password.
        /// </summary>
        /// <param name="password">The plain-text password.</param>
        /// <param name="salt">A string that contains the generated salt.</param>
        /// <returns>A string representing the hash value of the password.</returns>
        public string HashPassword(string password, out string salt)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("PasswordUtility");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password");
            }

            byte[] saltAsBytes = GenerateSalt();
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);

            byte[] hash = ComputeHash(passwordAsBytes, saltAsBytes);
            salt = Convert.ToBase64String(saltAsBytes);
            Array.Clear(passwordAsBytes, 0, passwordAsBytes.Length);
            Array.Clear(saltAsBytes, 0, saltAsBytes.Length);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifies that the provided password is correct.
        /// </summary>
        /// <param name="password">A UTF-8 encoded string representing the password to verify.</param>
        /// <param name="salt">A base-64 encoded string containing the salt associated with this password.</param>
        /// <param name="hashedPassword">A base-64 encoded string containing the previously stored hashed password.</param>
        /// <returns>True if the password is valid; otherwise false.</returns>
        public bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("PasswordUtility");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password");
            }

            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("salt");
            }

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentException("hashedPassword");
            }

            byte[] hashToVerify = Convert.FromBase64String(hashedPassword);
            byte[] passwordToVerify = Encoding.UTF8.GetBytes(password);
            byte[] saltToVerify = Convert.FromBase64String(salt);

            bool result = VerifyPassword(passwordToVerify, saltToVerify, hashToVerify);
            Array.Clear(passwordToVerify, 0, passwordToVerify.Length);

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool VerifyPassword(byte[] password, byte[] salt, byte[] hashedPassword)
        {
            byte[] newHash = ComputeHash(password, salt);

            return ArraysAreEqual(hashedPassword, newHash);
        }

        private bool ArraysAreEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length) return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }

        private byte[] ComputeHash(byte[] password, byte[] salt)
        {
            byte[] passwordAndSalt = CombinePasswordAndSalt(password, salt);
            byte[] hash = _hashProvider.ComputeHash(passwordAndSalt);
            Array.Clear(passwordAndSalt, 0, passwordAndSalt.Length);

            return hash;
        }

        private byte[] CombinePasswordAndSalt(byte[] password, byte[] salt)
        {
            byte[] passwordAndSalt = new byte[password.Length + salt.Length];

            Array.Copy(password, passwordAndSalt, password.Length);
            Array.Copy(salt, 0, passwordAndSalt, password.Length, salt.Length);

            return passwordAndSalt;
        }

        private byte[] GenerateSalt(int length = DefaultSaltLength)
        {
            // TODO: validate length parameter value

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[length];
            rng.GetNonZeroBytes(buffer);

            return buffer;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_hashProvider != null)
                {
                    _hashProvider.Clear();
                }
            }

            _hashProvider = null;
            _disposed = true;
        }
    }
}
