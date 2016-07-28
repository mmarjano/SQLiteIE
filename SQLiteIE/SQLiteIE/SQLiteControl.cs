using SQLiteIE.DbWrapperInterfaces;
using SQLiteIE.DbWrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteIE
{
    [Guid("1BA9A53F-7B8F-494A-B778-5B99F925B563")]
    [ProgId("SQLiteIE.SQLiteControl")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ISQLiteIEControl))]
    [ComVisible(true)]
    public class SQLiteControl : ISQLiteIEControl
    {
        // Holds open connections so that we can avoid opening a connection each time a transaction is started
        private static Dictionary<string, SQLiteDB> _dbConnections = new Dictionary<string, SQLiteDB>();

        /// <summary>
        /// Opens SQLite database
        /// </summary>
        /// <param name="parameters">Parameters for opening the database. Valid properties are <code>name</code> and <code>databaseLocation</code></param>
        /// <returns>
        /// Returns instance of <see cref="ISQLiteDB" /> implementation.
        /// </returns>
        public ISQLiteDB OpenDatabase(dynamic parameters)
        {
            string dbPath = Path.Combine(parameters.databaseLocation, parameters.name);

            SQLiteDB ret;

            if (!_dbConnections.TryGetValue(dbPath, out ret))
            {
                ret = new SQLiteDB(dbPath);
                _dbConnections.Add(dbPath, ret);
            }
            else
            {
                // reusing existing connection
            }
            
            return ret;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var connection in _dbConnections)
            {
                connection.Value.Dispose();
            }

            _dbConnections.Clear();
        }
    }
}
