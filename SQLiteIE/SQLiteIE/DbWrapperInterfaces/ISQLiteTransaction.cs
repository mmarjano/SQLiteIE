using System;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrapperInterfaces
{
    /// <summary>
    /// SQLite transaction
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    [Guid("84607573-47EB-4E93-9F6B-4B150FBB4EFA")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [ComVisible(true)]
    public interface ISQLiteTransaction : IDisposable
    {
        /// <summary>
        /// Executes the SQL statement.
        /// </summary>
        /// <param name="query">The query that needs to be executed.</param>
        /// <param name="parameters">The parameters for the query.</param>
        /// <param name="successCallback">The success callback. Invoked when query execution succeeded.</param>
        /// <param name="errorCallback">The error callback. Invoked when query execution failed.</param>
        [DispId(1)]
        void ExecuteSql(string query, object parameters = null, object successCallback = null, object errorCallback = null);
    }
}
