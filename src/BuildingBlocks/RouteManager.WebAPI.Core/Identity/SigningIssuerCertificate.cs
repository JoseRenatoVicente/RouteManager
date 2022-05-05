using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace RouteManager.WebAPI.Core.Identity;

public class SigningIssuerCertificate : IDisposable
{
    private readonly RSA _rsa;

    public SigningIssuerCertificate()
    {
        _rsa = RSA.Create();
    }

    public RsaSecurityKey GetIssuerSigningKey()
    {
        string publicXmlKey = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Identity//public_key.xml"));
        _rsa.FromXmlString(publicXmlKey);

        return new RsaSecurityKey(_rsa);
    }

    public void Dispose()
    {
        _rsa?.Dispose();
    }
}