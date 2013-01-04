// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Encryptor.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   The encryptor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Framework.Encyption.Impl
{
    /// <summary>
    /// The encryptor.
    /// </summary>
    public class Encryptor : IEncrypting
    {
        /// <summary>
        /// The encode.
        /// </summary>
        /// <param name="password">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Encode(string password)
        {
            // TODO: need to encrypt here
            return password;
        }
    }
}