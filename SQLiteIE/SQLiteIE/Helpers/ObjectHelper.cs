using System;
using System.Reflection;

namespace SQLiteIE.Helpers
{
    /// <summary>
    /// Helper methods for object
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Executes object as a function. Used for executing JS callback functions.
        /// </summary>
        /// <param name="obj">An object/function to execute</param>
        /// <param name="parameters">Parameters to be passed to the function</param>
        /// <returns>Returns return value of the invoked function.</returns>
        public static object Execute(this object obj, params object[] parameters)
        {
            object ret = obj.GetType().InvokeMember(
                String.Empty,
                BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                null,
                obj,
                parameters
            );

            return ret;
        }
    }
}
