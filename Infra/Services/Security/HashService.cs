using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Infra.Services.Security;

public class HashService
{
    private readonly int SaltSize;
    private readonly int HashSize;
    private readonly int Iterations;
    private readonly IConfiguration _configuration;

    public HashService(IConfiguration configuration)
    {
        _configuration = configuration;

        SaltSize = _configuration.GetValue<int>("Security:SaltSize");
        HashSize = _configuration.GetValue<int>("Security:HashSize");
        Iterations = _configuration.GetValue<int>("Security:Iterations");
    }

    public string HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        var hash = DeriveHash(password, salt);

        return $"{ToHex(salt)}:{ToHex(hash)}";
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            var salt = FromHex(parts[0]);
            var expectedHash = FromHex(parts[1]);

            var actualHash = DeriveHash(password, salt);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch
        {
            return false;
        }
    }

    private byte[] DeriveHash(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512);
        return pbkdf2.GetBytes(HashSize);
    }

    private static string ToHex(byte[] bytes) =>
        BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();

    private static byte[] FromHex(string hex)
    {
        var result = new byte[hex.Length / 2];
        for (int i = 0; i < result.Length; i++)
            result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        return result;
    }
}
