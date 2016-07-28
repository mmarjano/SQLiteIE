using Microsoft.JScript;
using System;
using System.Runtime.InteropServices;

namespace SQLiteIE.DbWrapperInterfaces
{
    /// <summary>
    /// Row list
    /// </summary>
    [Guid("C7A77B6E-D8A1-4889-A705-A9F621D3693B")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [ComVisible(true)]
    public interface ISQLiteResultSetRowList
    {
        /// <summary>
        /// Gets the number of items returned by SQL query.
        /// </summary>
        /// <value>
        /// The number of items.
        /// </value>
        [DispId(1)]
        int Length { get; }

        /// <summary>
        /// Gets a sigle row determined by index parameter.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Returns a row at specified index.</returns>
        [DispId(2)]
        JSObject Item(int index);

        /// <summary>
        /// Gets the result set property names.
        /// </summary>
        /// <returns>Returns array of property names.</returns>
        [DispId(3)]
        ArrayObject GetPropertyNames();

    }
}
