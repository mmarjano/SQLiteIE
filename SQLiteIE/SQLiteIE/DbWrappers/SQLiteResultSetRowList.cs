using SQLiteIE.DbWrapperInterfaces;
using System;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.JScript;
using System.Collections.Generic;

namespace SQLiteIE.DbWrappers
{
    /// <summary>
    /// ResultSet row list
    /// </summary>
    /// <seealso cref="SQLiteIE.DbWrapperInterfaces.ISQLiteResultSetRowList" />
    [Guid("6428A147-F83D-437D-8C8D-4F6E61557339")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ISQLiteResultSetRowList))]
    [ComVisible(true)]
    public class SQLiteResultSetRowList : ISQLiteResultSetRowList
    {
        private List<object> _items = new List<object>();
        private List<string> _propertyNames = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteResultSetRowList"/> class.
        /// </summary>
        /// <param name="rowData">The row data.</param>
        internal SQLiteResultSetRowList(DataTable rowData)
        {
            LoadFromDataTable(rowData);
        }

        /// <summary>
        /// Loads from data table.
        /// </summary>
        /// <param name="rowData">The row data.</param>
        private void LoadFromDataTable(DataTable rowData)
        {
            _items = new List<object>();
            _propertyNames = new List<string>();

            foreach (DataColumn dc in rowData.Columns)
            {
                _propertyNames.Add(dc.ColumnName);
            }

            foreach (DataRow dr in rowData.Rows)
            {
                Microsoft.JScript.JSObject o = new Microsoft.JScript.JSObject();
                System.Runtime.InteropServices.Expando.IExpando a = o;

                foreach (DataColumn dc in rowData.Columns)
                {
                    a.AddProperty(dc.ColumnName);
                    o.SetMemberValue2(dc.ColumnName, dr[dc.ColumnName]);
                }

                _items.Add(o);
            }
        }

        /// <summary>
        /// Gets the number of items returned by SQL query.
        /// </summary>
        /// <value>
        /// The number of items.
        /// </value>
        public int Length
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Gets the result set property names.
        /// </summary>
        /// <returns>
        /// Returns array of property names.
        /// </returns>
        public ArrayObject GetPropertyNames()
        {
            object[] array = new object[_propertyNames.Count];
            _propertyNames.ToArray().CopyTo(array, 0);
            ArrayObject ret = GlobalObject.Array.ConstructArray(array);

            return ret;
        }

        /// <summary>
        /// Gets a sigle row determined by index parameter.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// Returns a row at specified index.
        /// </returns>
        public JSObject Item(int index)
        {
            return _items[index] as JSObject;
        }
    }
}
