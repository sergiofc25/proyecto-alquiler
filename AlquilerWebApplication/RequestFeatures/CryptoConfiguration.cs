using System.Security.Cryptography;
using System.Text;


namespace AlquilerWebApplication.RequestFeatures
{

    public interface ICryptoConfiguration
    {
        string GenerateSalt();

        string Encriptar(string texto, string EncryptionKey, string salt);

        string Desencriptar(string textoEncriptado, string EncryptionKey, string salt);
    }

    public class CryptoConfiguration : ICryptoConfiguration
    {
        public string GenerateSalt()
        {
            const int saltLength = 32; // Tamaño de la sal en bytes

            var salt = new byte[saltLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string Encriptar(string texto, string EncryptionKey, string salt)
        {
            var clearBytes = Encoding.Unicode.GetBytes(texto);

            using var aes = Aes.Create();

            var keyDerivationFunction = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes(salt), 10000, HashAlgorithmName.SHA256);
            aes.Key = keyDerivationFunction.GetBytes(32);
            aes.IV = keyDerivationFunction.GetBytes(16);

            using var memoryStream = new MemoryStream();

            using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                cryptoStream.Close();
            }

            var encryptedBytes = memoryStream.ToArray();
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Desencriptar(string textoEncriptado, string EncryptionKey, string salt)
        {
            var cipherBytes = Convert.FromBase64String(textoEncriptado);

            using var aes = Aes.Create();
            var keyDerivationFunction = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes(salt), 10000, HashAlgorithmName.SHA256);
            aes.Key = keyDerivationFunction.GetBytes(32);
            aes.IV = keyDerivationFunction.GetBytes(16);

            using var memoryStream = new MemoryStream();

            using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.Close();
            }

            var decryptedBytes = memoryStream.ToArray();
            return Encoding.Unicode.GetString(decryptedBytes);
        }
    }
}
