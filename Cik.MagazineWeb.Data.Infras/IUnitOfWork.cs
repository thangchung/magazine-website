// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the IUnitOfWork type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cik.MagazineWeb.Data.Infras
{
    using System;
    using System.Data;
    using System.Data.Objects;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether is in transaction.
        /// </summary>
        bool IsInTransaction { get; }

        /// <summary>
        /// The save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <param name="saveOptions">
        /// The save options.
        /// </param>
        void SaveChanges(SaveOptions saveOptions);

        /// <summary>
        /// The begin transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">
        /// The isolation level.
        /// </param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// The roll back transaction.
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// The commit transaction.
        /// </summary>
        void CommitTransaction();
    }
}
