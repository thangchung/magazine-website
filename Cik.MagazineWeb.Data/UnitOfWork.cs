namespace Cik.MagazineWeb.Data
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;

    using Cik.MagazineWeb.Data.Infras;

    internal class UnitOfWork : IUnitOfWork
    {
        private DbTransaction _transaction;
        private DbContext _dbContext;

        public UnitOfWork(DbContext context)
        {
            this._dbContext = context;
        }

        public bool IsInTransaction
        {
            get { return this._transaction != null; }
        }

        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (this._transaction != null)
            {
                throw new ApplicationException("Cannot begin a new transaction while an existing transaction is still running. " +
                                                "Please commit or rollback the existing transaction before starting a new one.");
            }
            this.OpenConnection();
            this._transaction = ((IObjectContextAdapter)this._dbContext).ObjectContext.Connection.BeginTransaction(isolationLevel);
        }        

        public void RollBackTransaction()
        {
            if (this._transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            if (this.IsInTransaction)
            {
                this._transaction.Rollback();
                this.ReleaseCurrentTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (this._transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            try
            {
                ((IObjectContextAdapter)this._dbContext).ObjectContext.SaveChanges();
                this._transaction.Commit();
                this.ReleaseCurrentTransaction();
            }
            catch
            {
                this.RollBackTransaction();
                throw;
            }            
        }

        public void SaveChanges()
        {
            if (this.IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call CommitTransaction instead.");
            }
            ((IObjectContextAdapter)this._dbContext).ObjectContext.SaveChanges();
        }

        public void SaveChanges(SaveOptions saveOptions)
        {
            if (this.IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call CommitTransaction instead.");
            }

            ((IObjectContextAdapter)this._dbContext).ObjectContext.SaveChanges(saveOptions);
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes off the managed and unmanaged resources used.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (this._disposed)
                return;

            this._disposed = true;
        }

        private bool _disposed;
        #endregion

        private void OpenConnection()
        {
            if (((IObjectContextAdapter)this._dbContext).ObjectContext.Connection.State != ConnectionState.Open)
            {
                ((IObjectContextAdapter)this._dbContext).ObjectContext.Connection.Open();
            }
        }

        /// <summary>
        /// Releases the current transaction
        /// </summary>
        private void ReleaseCurrentTransaction()
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction = null;
            }
        }
    }
}
