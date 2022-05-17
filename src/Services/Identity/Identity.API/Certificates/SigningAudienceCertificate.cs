using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Identity.API.Certificates;

public sealed class SigningAudienceCertificate : IDisposable
{
    private readonly RSA _rsa;

    public SigningAudienceCertificate()
    {
        _rsa = RSA.Create();
    }

    public SigningCredentials GetAudienceSigningKey()
    {
        string privateXmlKey = File.ReadAllText("./Keys/private_key.xml");
        _rsa.FromXmlString(privateXmlKey);

        return new SigningCredentials(
            key: new RsaSecurityKey(_rsa),
            algorithm: SecurityAlgorithms.RsaSha256);
    }

    public void Dispose()
    {
        _rsa?.Dispose();
    }
}