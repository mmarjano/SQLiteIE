using System;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrapperInterfaces
{
    /// <summary>
    /// Query execution result
    /// </summary>
    [Guid("33BBAF04-6C34-4EA5-8FE1-05A5BD798224")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [ComVisible(true)]
    public interface ISQLiteResultSet
    {
        /// <summary>
        /// Gets the last ID inserted by this statement.
        /// </summary>
        /// <value>
        /// The insert identifier.
        /// </value>
        long InsertID { get; }

        /// <summary>
        /// Returns number of rows that were affected by this sql statement.
        /// </summary>
        /// <value>
        /// The rows affected.
        /// </value>
        long RowsAffected { get; }

        /// <summary>
        /// Gets the rowlist.
        /// </summary>
        /// <value>
        /// The rowlist.
        /// </value>
        ISQLiteResultSetRowList Rowlist { get; }
    }
}
