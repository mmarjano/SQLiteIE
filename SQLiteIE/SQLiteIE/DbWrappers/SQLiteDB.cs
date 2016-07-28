using SQLiteIE.DbWrapperInterfaces;
using SQLiteIE.Helpers;
using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrappers
{
    /// <summary>
    /// Represents SQLite database
    /// </summary>
    /// <seealso cref="SQLiteIE.DbWrapperInterfaces.ISQLiteDB" />
    [Guid("E8EE36D9-5D7E-4105-A902-41F57071FA47")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ISQLiteDB))]
    [ComVisible(true)]
    public class SQLiteDB : ISQLiteDB
    {
        private DbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDB"/> class.
        /// </summary>
        /// <param name="dbPath">The database path.</param>
        internal SQLiteDB(string dbPath)
        {
            string connString = string.Format("Data Source={0};Version=3;", dbPath);
            _connection = new SQLiteConnection(connString);
            _connection.Open();
        }

        /// <summary>
        /// Creates a SQLite transaction.
        /// </summary>
        /// <param name="callback">The callback. Executed when transaction is created. Transaction will be passed in as parameter.</param>
        /// <param name="errorCallback">The error callback. Invoked when transaction failed. Transaction and ErrorMessage will be passed in as parameters.</param>
        /// <param name="successCallback">The success callback. Invoked when transaction succeeded. Transaction and ResultSet will be passed in as parameters.</param>
        public void Transaction(object callback, object errorCallback = null, object successCallback = null)
        {
            try
            {
                using (ISQLiteTransaction tran = new SQLiteTransaction(_connection))
                {
                    callback.Execute(tran);
                }

                if (successCallback != null)
                {
                    successCallback.Execute();
                }
            }
            catch (Exception ex)
            {
                if (errorCallback != null)
                {
                    errorCallback.Execute(ex.Message);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
