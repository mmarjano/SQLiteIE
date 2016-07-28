using SQLiteIE.DbWrapperInterfaces;
using SQLiteIE.DbWrappers;
using System;
using System.Runtime.InteropServices;

namespace SQLiteIE
{
    /// <summary>
    /// SQLiteIE ActiveX Control
    /// </summary>
    [Guid("DC7A873E-9AB3-4521-BCDE-BB066FFDBD6F")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [ComVisible(true)]
    public interface ISQLiteIEControl : IDisposable
    {
        /// <summary>
        /// Opens SQLite database
        /// </summary>
        /// <param name="parameters">Parameters for opening the database. Valid properties are <code>name</code> and <code>databaseLocation</code></param>
        /// <returns>Returns instance of <see cref="ISQLiteDB"/> implementation.</returns>
        [DispId(1)]
        ISQLiteDB OpenDatabase(dynamic parameters);
    }
}
