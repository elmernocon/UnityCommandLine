#region File Header

// Filename: Values.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using System;

namespace UnityCommandLine
{
    /// <summary>
    /// A container class for frequently used values in commands.
    /// </summary>
    public static class Values
    {
        #region Constants
        
        /// <summary>
        /// The separator string.
        /// </summary>
        public const string SEPARATOR = "-------------------------------------------------------------------------------";

        /// <summary>
        /// The default string comparison.
        /// </summary>
        public const StringComparison DEFAULT_STRING_COMPARISON = StringComparison.InvariantCultureIgnoreCase;

        #endregion
    }
}