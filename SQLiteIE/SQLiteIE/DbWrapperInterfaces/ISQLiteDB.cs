using System;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrapperInterfaces
{
    /// <summary>
    /// Defines mehods that can be called on Database level.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    [Guid("E364D2F0-1B7D-4E16-997B-8FCF4E12D2BC")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [ComVisible(true)]
    public interface ISQLiteDB : IDisposable
    {
        /// <summary>
        /// Transactions the specified callback.
        /// </summary>
        /// <param name="callback">The callback. Executed when transaction is created. Transaction will be passed in as parameter.</param>
        /// <param name="errorCallback">The error callback. Invoked when transaction failed. Transaction and ErrorMessage will be passed in as parameters.</param>
        /// <param name="successCallback">The success callback. Invoked when transaction succeeded. Transaction and ResultSet will be passed in as parameters.</param>
        [DispId(1)]
        void Transaction(object callback, object errorCallback = null, object successCallback = null);
    }
}
