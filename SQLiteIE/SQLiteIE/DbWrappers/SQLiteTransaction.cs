using SQLiteIE.DbWrapperInterfaces;
using SQLiteIE.Helpers;
using System;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrappers
{
    /// <summary>
    /// Represents SQLite transaction
    /// </summary>
    /// <seealso cref="SQLiteIE.DbWrapperInterfaces.ISQLiteTransaction" />
    [Guid("AFED719B-AADD-40E3-AD1B-8E511E433772")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ISQLiteTransaction))]
    [ComVisible(true)]  
    public class SQLiteTransaction : ISQLiteTransaction
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        #region Helpers
        /// <summary>
        /// Converts the js array object to object[].
        /// </summary>
        /// <param name="jsArray">The js array.</param>
        /// <returns>Returns converted object.</returns>
        private static object[] ConvertJsArray(object jsArray)
        {
            int arrayLength = jsArray != null ? (int)jsArray.GetType().InvokeMember("length", BindingFlags.GetProperty, null, jsArray, new object[] { }) : 0;
            object[] array = new object[arrayLength];

            for (int index = 0; index < arrayLength; index++)
            {
                array[index] = jsArray.GetType().InvokeMember(index.ToString(), BindingFlags.GetProperty, null, jsArray, new object[] { });
            }

            return array;
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteTransaction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        internal SQLiteTransaction(IDbConnection connection)
        {
            _connection = connection;
            _transaction = _connection.BeginTransaction();
        }
        
        /// <summary>
        /// Executes the SQL statement.
        /// </summary>
        /// <param name="query">The query that needs to be executed.</param>
        /// <param name="parameters">The parameters for the query.</param>
        /// <param name="successCallback">The success callback. Invoked when query execution succeeded.</param>
        /// <param name="errorCallback">The error callback. Invoked when query execution failed.</param>
        public void ExecuteSql(string query, object parameters = null, object successCallback = null, object errorCallback = null)
        {
            object[] parametersArray = SQLiteTransaction.ConvertJsArray(parameters);

            ISQLiteResultSet ret = null;

            bool hasError = false;
            try
            {
                using (IDbCommand command = _connection.CreateCommand())
                {
                    command.Transaction = _transaction;

                    command.CommandText = query;
                    foreach (var param in parametersArray)
                    {
                        command.Parameters.Add(new SQLiteParameter { Value = param });
                    }
                    
                    IDataReader dataReader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    ISQLiteResultSetRowList r = new SQLiteResultSetRowList(dataTable);
                    ret = new SQLiteResultSet(r);
                }
            }
            catch (Exception ex)
            {
                hasError = true;
                if (errorCallback != null)
                {
                    errorCallback.Execute(this, ex.Message);
                }
                else
                {
                    throw;
                }
            }

            if (!hasError && successCallback != null)
            {
                successCallback.Execute(this, ret);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }
    }
}
