using SQLiteIE.DbWrapperInterfaces;
using System;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrappers
{
    /// <summary>
    /// SQL result set
    /// </summary>
    /// <seealso cref="SQLiteIE.DbWrapperInterfaces.ISQLiteResultSet" />
    [Guid("4D40302A-C9A4-42BD-B18A-DF81F760A196")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ISQLiteResultSet))]
    [ComVisible(true)]
    public class SQLiteResultSet : ISQLiteResultSet
    {
        private ISQLiteResultSetRowList _rowList;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteResultSet"/> class.
        /// </summary>
        /// <param name="rowList">The row list.</param>
        internal SQLiteResultSet(ISQLiteResultSetRowList rowList)
        {
            _rowList = rowList;
        }

        /// <summary>
        /// Gets the last ID inserted by this statement.
        /// </summary>
        /// <value>
        /// The insert identifier.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public long InsertID
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the rowlist.
        /// </summary>
        /// <value>
        /// The rowlist.
        /// </value>
        public ISQLiteResultSetRowList Rowlist
        {
            get
            {
                return _rowList;
            }
        }

        /// <summary>
        /// Returns number of rows that were affected by this sql statement.
        /// </summary>
        /// <value>
        /// The rows affected.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public long RowsAffected
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
