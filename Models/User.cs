using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace pruebasoftware.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Stored as base64 SHA256 hash (demo only)
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }

    public void SetPassword(string password)
    {
        PasswordHash = Hash(password);
    }

    public bool VerifyPassword(string password)
    {
        return PasswordHash == Hash(password);
    }

    private static string Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}
