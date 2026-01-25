using Microsoft.AspNetCore.DataProtection;
using OtpNet;
using QRCoder;
using System.Security.Cryptography;
using System.Text;

namespace Zuhid.Auth.Tools;

public class SecurityTool {

  public static string Protect() {
    // https://github.com/dotnet/aspnetcore/blob/main/src/DataProtection/Extensions/src/TimeLimitedDataProtector.cs
    // Create a data protection provider
    var provider = DataProtectionProvider.Create("MyApp");
    // Create a protector for a specific purpose
    var protector = provider.CreateProtector("EmailConfirmation");
    // Convert to TimeLimitedDataProtector
    var timeLimitedProtector = protector.ToTimeLimitedDataProtector();

    var originalData = "EmailConfirmation:testUserId:securityStamp";
    var token = timeLimitedProtector.Protect(originalData, lifetime: TimeSpan.FromSeconds(10));
    return token;
  }

  public static string VerifyProtect(string token) {
    // Create a data protection provider
    var provider = DataProtectionProvider.Create("MyApp");
    // Create a protector for a specific purpose
    var protector = provider.CreateProtector("EmailConfirmation");
    // Convert to TimeLimitedDataProtector
    var timeLimitedProtector = protector.ToTimeLimitedDataProtector();

    var unprotectedByte = timeLimitedProtector.Unprotect(token);
    return unprotectedByte;
  }

  public static string HashPassword(string password) {
    // Generate a random salt
    var salt = new byte[16];
    using (var rng = RandomNumberGenerator.Create()) {
      rng.GetBytes(salt);
    }

    // Use PBKDF2 to hash the password with the salt
    using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
    var hash = pbkdf2.GetBytes(32); // 256-bit hash
    var hashBytes = new byte[48]; // 16 bytes salt + 32 bytes hash
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 32);

    // Convert to Base64 for storage
    return Convert.ToBase64String(hashBytes);
  }


  public static bool VerifyPassword(string password, string storedHash) {
    var hashBytes = Convert.FromBase64String(storedHash);
    var salt = new byte[16];
    Array.Copy(hashBytes, 0, salt, 0, 16);

    using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
    var hash = pbkdf2.GetBytes(32);
    for (var i = 0; i < 32; i++) {
      if (hashBytes[i + 16] != hash[i]) {
        return false;
      }
    }
    return true;
  }

  public static string GenerateBase32Secret(int length) {
    var bytes = new byte[length];
    RandomNumberGenerator.Create().GetBytes(bytes);
    const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
    var sb = new StringBuilder();
    foreach (var b in bytes) {
      sb.Append(alphabet[b % alphabet.Length]);
    }
    return sb.ToString();
  }

  public static string GenerateTotp(string secret, long counter, int totpSize) {
    var keyBytes = Base32Decode(secret);
    var counterBytes = BitConverter.GetBytes(counter);
    if (BitConverter.IsLittleEndian) {
      Array.Reverse(counterBytes);
    }

    using var hmac = new HMACSHA1(keyBytes);
    var hash = hmac.ComputeHash(counterBytes);
    var offset = hash[^1] & 0x0F;
    var binaryCode = ((hash[offset] & 0x7F) << 24)
                   | ((hash[offset + 1] & 0xFF) << 16)
                   | ((hash[offset + 2] & 0xFF) << 8)
                   | (hash[offset + 3] & 0xFF);

    var otp = binaryCode % (int)Math.Pow(10, totpSize);
    return otp.ToString(new string('0', totpSize));
  }

  static byte[] Base32Decode(string base32) {

    const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
    base32 = base32.TrimEnd('=').ToUpperInvariant();
    var byteCount = base32.Length * 5 / 8;
    var bytes = new byte[byteCount];
    var bitBuffer = 0;
    var bitsLeft = 0;
    var index = 0;

    foreach (var c in base32) {
      var val = alphabet.IndexOf(c);
      if (val < 0) {
        throw new ArgumentException("Invalid Base32 character.");
      }

      bitBuffer = (bitBuffer << 5) | val;
      bitsLeft += 5;

      if (bitsLeft >= 8) {
        bytes[index++] = (byte)(bitBuffer >> (bitsLeft - 8));
        bitsLeft -= 8;
      }
    }
    return bytes;
  }

  public static byte[] GenerateQrCode(string issuer, string account, string secret) {
    var otpauthUri = $"otpauth://totp/{issuer}:{account}?secret={secret}&issuer={issuer}&algorithm=SHA1&digits=6&period=30";
    using var qRCodeGenerator = new QRCodeGenerator();
    var qrCodeData = qRCodeGenerator.CreateQrCode(otpauthUri, QRCodeGenerator.ECCLevel.Q);
    var qrCode = new PngByteQRCode(qrCodeData);
    return qrCode.GetGraphic(20); // PNG as byte[]
  }

  public static string GenerateTotp_New() {
    var secretKey = KeyGeneration.GenerateRandomKey(20);
    var base32Secret = Base32Encoding.ToString(secretKey);

    return new Totp(secretKey, mode: OtpHashMode.Sha256, step: 60, totpSize: 8)
      .ComputeTotp();
  }


  public static string GenerateTotp(string secretKey, int timeStep = 30, int totpSize = 6) {
    // Convert secret key to bytes (Base32 or Base64 depending on your storage)
    var keyBytes = Convert.FromBase64String(secretKey);

    // Get current Unix time
    var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    var counter = unixTime / timeStep;

    // Convert counter to byte array (big-endian)
    var counterBytes = BitConverter.GetBytes(counter);
    if (BitConverter.IsLittleEndian) {
      Array.Reverse(counterBytes);
    }

    // Compute HMAC-SHA1
    using var hmac = new HMACSHA1(keyBytes);
    var hash = hmac.ComputeHash(counterBytes);

    // Dynamic truncation
    var offset = hash[^1] & 0x0F;
    var binaryCode = ((hash[offset] & 0x7F) << 24) | ((hash[offset + 1] & 0xFF) << 16) | ((hash[offset + 2] & 0xFF) << 8) | (hash[offset + 3] & 0xFF);

    var otp = binaryCode % (int)Math.Pow(10, totpSize);
    return otp.ToString(new string('0', totpSize)); // zero-padded
  }
}
