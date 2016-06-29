using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace X509CertificateGenTool
{
  /// <summary>
  /// Reference at https://github.com/ElemarJR/LearningIdentityServer4/tree/master/LearningIdentityServer.X509CertificateGen
  /// </summary>
  internal class Program
  {
    private static void Main(string[] args)
    {
      var c2 = GenerateX509Certificate("args[0]");
      var b64 = Convert.ToBase64String(c2.Export(X509ContentType.Pkcs12, "password"));

      File.WriteAllText(args[1], b64);
    }

    private static X509Certificate2 GenerateX509Certificate(string certName)
    {
      var keypairgen = new RsaKeyPairGenerator();
      keypairgen.Init(new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), 2048));

      var keypair = keypairgen.GenerateKeyPair();

      var gen = new X509V3CertificateGenerator();

      var cn = new X509Name("CN=" + certName);
      var sn = BigInteger.ProbablePrime(120, new Random());
      gen.SetSerialNumber(sn);
      gen.SetSubjectDN(cn);
      gen.SetIssuerDN(cn);
      gen.SetNotAfter(DateTime.MaxValue);
      gen.SetNotBefore(DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)));
      gen.SetSignatureAlgorithm("MD5WithRSA");
      gen.SetPublicKey(keypair.Public);

      var newCert = gen.Generate(keypair.Private);

      var cert = DotNetUtilities.ToX509Certificate(newCert);
      var certBytes = cert.Export(X509ContentType.Pkcs12, "password");
      var cert2 = new X509Certificate2(certBytes, "password");

      var rsaPriv = DotNetUtilities.ToRSA(keypair.Private as RsaPrivateCrtKeyParameters);
      var csp = new CspParameters {KeyContainerName = "KeyContainer"};
      var rsaPrivate = new RSACryptoServiceProvider(csp);

      rsaPrivate.ImportParameters(rsaPriv.ExportParameters(true));

      cert2.PrivateKey = rsaPrivate;

      return cert2;
    }
  }
}