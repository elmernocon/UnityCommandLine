#region File Header

// Filename: Values.cs
// Author: Elmer Nocon
// Date Created: 2019/05/17
// License: MIT

#endregion

using UnityEditor;

namespace UnityCommandLine.AssetDatabase
{
    /// <summary>
    /// A container class for frequently used values in export-package-related commands.
    /// </summary>
    public static class Values
    {
        #region Constants

        /// <summary>
        /// The default package folder name.
        /// </summary>
        public const string DEFAULT_PACKAGE_FOLDER_NAME = "UnityPackages";

        /// <summary>
        /// The default package file name.
        /// </summary>
        public const string DEFAULT_PACKAGE_NAME = "package";

        /// <summary>
        /// The default export package options.
        /// </summary>
        public const ExportPackageOptions DEFAULT_PACKAGE_OPTIONS = ExportPackageOptions.Default;

        /// <summary>
        /// The argument key for the package contents.
        /// </summary>
        public const string ARG_PACKAGE_CONTENTS = "-packageContents";

        /// <summary>
        /// The argument key for the package file name.
        /// </summary>
        public const string ARG_PACKAGE_NAME = "-packageName";

        /// <summary>
        /// The argument switch for <see cref="ExportPackageOptions.Recurse"/>.
        /// </summary>
        public const string ARG_RECURSE = "-optionRecurse";

        /// <summary>
        /// The argument switch for <see cref="ExportPackageOptions.IncludeDependencies"/>.
        /// </summary>
        public const string ARG_INCLUDE_DEPENDENCIES = "-optionIncludeDependencies";

        /// <summary>
        /// The argument switch for <see cref="ExportPackageOptions.IncludeLibraryAssets"/>.
        /// </summary>
        public const string ARG_INCLUDE_LIBRARY_ASSETS = "-optionIncludeLibraryAssets";

        #endregion
    }
}