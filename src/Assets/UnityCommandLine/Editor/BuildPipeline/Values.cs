#region File Header

// Filename: Values.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using UnityEditor;

namespace UnityCommandLine.BuildPipeline
{
    /// <summary>
    /// A container class for frequently used values in build-player-related commands.
    /// </summary>
    public static class Values
    {
        #region Constants

        /// <summary>
        /// The default indent string.
        /// </summary>
        public const string DEFAULT_INDENT = " ";

        /// <summary>
        /// The default build folder name.
        /// </summary>
        public const string DEFAULT_BUILD_FOLDER_NAME = "Builds";

        /// <summary>
        /// The default build file name.
        /// </summary>
        public const string DEFAULT_BUILD_NAME = "build";

        /// <summary>
        /// The default build options.
        /// </summary>
        public const BuildOptions DEFAULT_BUILD_OPTIONS = BuildOptions.None;

        /// <summary>
        /// The default value of print report verbose.
        /// </summary>
        public const bool DEFAULT_BUILD_REPORT_VERBOSE = false;

        /// <summary>
        /// The argument key for the build target.
        /// </summary>
        public const string ARG_BUILD_TARGET = "-buildTarget";

        /// <summary>
        /// The argument key for the build file name.
        /// </summary>
        public const string ARG_BUILD_NAME = "-buildName";

        /// <summary>
        /// The argument key for <see cref="PlayerSettings.applicationIdentifier"/>.
        /// </summary>
        public const string ARG_APPLICATION_IDENTIFIER = "-applicationIdentifier";

        /// <summary>
        /// The argument key for <see cref="PlayerSettings.bundleVersion"/>.
        /// </summary>
        public const string ARG_BUNDLE_VERSION = "-bundleVersion";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.Development"/>.
        /// </summary>
        public const string ARG_OPTION_DEVELOPMENT = "-optionDevelopment";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.AllowDebugging"/>.
        /// </summary>
        public const string ARG_OPTION_ALLOW_DEBUGGING = "-optionAllowDebugging";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.SymlinkLibraries"/>.
        /// </summary>
        public const string ARG_OPTION_SYMLINK_LIBRARIES = "-optionSymlinkLibraries";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.ForceEnableAssertions"/>.
        /// </summary>
        public const string ARG_OPTION_FORCE_ENABLE_ASSERTIONS = "-optionForceEnableAssertions";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.CompressWithLz4"/>.
        /// </summary>
        public const string ARG_OPTION_COMPRESS_WITH_LZ4 = "-optionCompressWithLz4";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.CompressWithLz4HC"/>.
        /// </summary>
        public const string ARG_OPTION_COMPRESS_WITH_LZ4_HC = "-optionCompressWithLz4HC";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.StrictMode"/>.
        /// </summary>
        public const string ARG_OPTION_STRICT_MODE = "-optionStrictMode";

        /// <summary>
        /// The argument switch for <see cref="BuildOptions.IncludeTestAssemblies"/>.
        /// </summary>
        public const string ARG_INCLUDE_TEST_ASSEMBLIES = "-optionIncludeTestAssemblies";

        #endregion
    }
}