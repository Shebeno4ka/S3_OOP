using System.Security.Cryptography;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;

public class PinCode
{
    public string Hash { get; init; }

    public PinCode(string pin)
    {
        if (pin.Length < 4)
            throw new ArgumentException("PIN code stroke must be at least 4 characters long");

        Hash = HashPin(pin);
    }

    public bool Verify(string pin)
    {
        return HashPin(pin) == Hash;
    }

    public bool Verify(PinCode pin)
    {
        return pin.Hash == Hash;
    }

    // Size of stroke is always 44 (32 from SHA-256 + Base64 encoding)
    private static string HashPin(string pin)
    {
        byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(pin));
        return Convert.ToBase64String(hashBytes);
    }
}