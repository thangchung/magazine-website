// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEncypting.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   The Encrypting interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Framework.Encyption
{
    /// <summary>
    /// The Encrypting interface.
    /// </summary>
    public interface IEncrypting
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
        string Encode(string password);
    }
}