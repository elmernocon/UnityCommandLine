#region File Header

// Filename: CommandUtils.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using System.IO;

#if !NET_4_6
using System.Linq;
#endif

namespace UnityCommandLine
{
    /// <summary>
    /// A utility class container for methods used by commands.
    /// </summary>
    public static class CommandUtils
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Combines two path strings.
        /// </summary>
        /// <param name="args">The paths.</param>
        /// <returns>The new combined path.</returns>
        public static string PathCombine(params string[] args)
        {
#if NET_4_6
            return Path.Combine(args);
#else
            return args.Aggregate(Path.Combine);
#endif
        }

        #endregion

        #endregion
    }
}