#region File Header

// Filename: CommandBase.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using System;
using System.Linq;
using UnityEngine;

namespace UnityCommandLine
{
    /// <summary>
    /// The base class for all commands executable through Unity's command line interface.
    /// </summary>
    /// <para>
    /// Example:
    /// <code>-executeMethod CommandClassName.Execute</code>
    /// </para>
    public abstract class CommandBase
    {
        #region Statics

        #region Static Fields

        private static readonly Func<string, string, bool> StringComparer = (s1, s2) => s1.Contains(s2);

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets the arguments passed along with this command.
        /// </summary>
        /// <returns></returns>
        protected static string[] GetArguments()
        {
            return Environment.GetCommandLineArgs();
        }

        /// <summary>
        /// Tries to get the value (the next argument) of the given argument [key].
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="argumentKey">The argument to use as a key.</param>
        /// <param name="argumentValue">The argument value to retrieve.</param>
        /// <param name="comparisonType">The string comparison to use.</param>
        /// <returns>Returns <c>true</c> if a valid argument value candidate was found, otherwise <c>false</c>.</returns>
        protected static bool GetArgumentValue(string[] arguments, string argumentKey, out string argumentValue,
                                               StringComparison comparisonType = Values.DEFAULT_STRING_COMPARISON)
        {
            var argumentsLength = arguments.Length;
            
            for (var i = 0; i < argumentsLength; i++)
            {
                var arg = arguments[i];
                
                if (!StringComparer(arg, argumentKey))
                    continue;

                var search = $"{argumentKey}=";

                if (arg.Contains(search))
                {
                    var idx = arg.IndexOf(search, StringComparison.Ordinal);
                    var val = arg.Substring(idx + search.Length);

                    if (!string.IsNullOrEmpty(val))
                    {
                        argumentValue = val;
                        return true;
                    }
                }
                
                if (i < argumentsLength - 1)
                {
                    argumentValue = arguments[i + 1];
                    return true;
                }
                
                break;
            }
            
            argumentValue = null;
            return false;
        }

        /// <summary>
        /// Checks whether the given argument was passed along with this command.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="argument">The argument to check.</param>
        /// <param name="comparisonType">The string comparison to use.</param>
        /// <returns>Returns <c>true</c> if the given argument was found, otherwise <c>false</c>.</returns>
        protected static bool HasArgument(string[] arguments, string argument, StringComparison comparisonType = Values.DEFAULT_STRING_COMPARISON)
        {
            return arguments.Any(arg => StringComparer(arg, argument));
        }

        /// <summary>
        /// Prints the message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected static void Print(string message = "")
        {
            Debug.Log(message);
        }

        /// <summary>
        /// Prints the message followed by a line terminator.
        /// </summary>
        /// <param name="message">The message.</param>
        protected static void PrintLine(string message = "")
        {
            Debug.Log(message);
        }

        /// <summary>
        /// Prints a separator.
        /// </summary>
        protected static void PrintSeparator()
        {
            PrintLine(Values.SEPARATOR);
        }

        #endregion

        #endregion

        #region Fields

        private readonly string[] _arguments;

        #endregion

        #region Constructors & Destructors

        /// <summary>
        /// Creates an instance of <see cref="CommandBase"/>.
        /// </summary>
        protected CommandBase()
        {
            // Get the arguments.
            _arguments = GetArguments();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The method to run when the command is executed.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Tries to get the value (the next argument) of the given argument [key].
        /// </summary>
        /// <param name="argumentKey">The argument to use as a key.</param>
        /// <param name="argumentValue">The argument value to retrieve.</param>
        /// <param name="comparisonType">The string comparison to use.</param>
        /// <returns>Returns <c>true</c> if a valid argument value candidate was found, otherwise <c>false</c>.</returns>
        protected bool GetArgumentValue(string argumentKey, out string argumentValue,
                                        StringComparison comparisonType = Values.DEFAULT_STRING_COMPARISON)
        {
            return GetArgumentValue(_arguments, argumentKey, out argumentValue, comparisonType);
        }

        /// <summary>
        /// Checks whether the given argument was passed along with this command.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="comparisonType">The string comparison to use.</param>
        /// <returns>Returns <c>true</c> if the given argument was found, otherwise <c>false</c>.</returns>
        protected bool HasArgument(string argument, StringComparison comparisonType = Values.DEFAULT_STRING_COMPARISON)
        {
            return HasArgument(_arguments, argument, comparisonType);
        }

        #endregion
    }
}