#region File Header

// Filename: ExportPackageCommandBase.cs
// Author: Elmer Nocon
// Date Created: 2019/05/17
// License: MIT

#endregion

using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace UnityCommandLine.AssetDatabase
{
    /// <inheritdoc />
    /// <summary>
    /// The base class for all export-package-related commands executable through Unity's command line interface.
    /// </summary>
    public abstract class ExportPackageCommandBase : CommandBase
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Exports a package.
        /// </summary>
        /// <param name="settings">The export package settings.</param>
        private static void ExportPackage(ExportPackageSettings settings)
        {
            UnityEditor.AssetDatabase.ExportPackage(settings.AssetPathNames, settings.OutputPath, settings.Options);
        }

        /// <summary>
        /// Print a <see cref="Settings"/> object.
        /// </summary>
        /// <param name="settings">The export package settings object.</param>
        /// <param name="title">The title.</param>
        private static void PrintSettings(ExportPackageSettings settings, string title = null)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.AppendLine(UnityCommandLine.Values.SEPARATOR);
            if (!string.IsNullOrEmpty(title)) stringBuilder.AppendLine(title);
            ExportPackageSettings.Print(settings, stringBuilder);
            stringBuilder.AppendLine(UnityCommandLine.Values.SEPARATOR);
            
            PrintLine(stringBuilder.ToString());
        }

        #endregion

        #endregion

        #region Fields

        /// <summary>
        /// The export package settings to use when exporting the package.
        /// </summary>
        protected readonly ExportPackageSettings Settings;

        #endregion

        #region Constructors & Destructors

        /// <inheritdoc />
        /// <summary>
        /// Creates an instance of <see cref="T:UnityCommandLine.AssetDatabase.ExportPackageCommandBase" />.
        /// </summary>
        protected ExportPackageCommandBase()
        {
            Settings = new ExportPackageSettings();
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Run()
        {
            InitArgs();
            
            PrintSettings(Settings, "Exporting:");

            ExportPackage(Settings);
        }

        /// <summary>
        /// Gets the output path.
        /// </summary>
        /// <param name="outputFileName">The output file name.</param>
        /// <returns>Returns the output path.</returns>
        protected virtual string GetOutputPath(string outputFileName)
        {
            const string fileExtension = ".unitypackage";

            var directoryPath = CommandUtils.PathCombine(Path.GetDirectoryName(Application.dataPath), Values.DEFAULT_PACKAGE_FOLDER_NAME);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            
            return CommandUtils.PathCombine(directoryPath,
                    string.Format("{0}{1}", outputFileName, fileExtension));
        }

        /// <summary>
        /// Initializes the arguments used by this command.
        /// </summary>
        private void InitArgs()
        {
            // Set the package name if there is one.
            string packageName;
            if (!GetArgumentValue(Values.ARG_PACKAGE_NAME, out packageName))
                packageName = Values.DEFAULT_PACKAGE_NAME;
            
            // Set the package output path.
            Settings.OutputPath = GetOutputPath(packageName);

            string packageContentsString;
            string[] packageContents = null;
            if (GetArgumentValue(Values.ARG_PACKAGE_CONTENTS, out packageContentsString))
                packageContents = packageContentsString.Split(',')
                                                       .Where(content => File.Exists(content) || Directory.Exists(content))
                                                       .ToArray();

            if (packageContents == null || packageContents.Length == 0)
                throw new Exception("No package contents were selected.");

            // Set the asset path names.
            Settings.AssetPathNames = packageContents;
            
            // Set the initial build option.
            Settings.Options = Values.DEFAULT_PACKAGE_OPTIONS;

            // Set additional package options to use in the package.
            if (HasArgument(Values.ARG_RECURSE))
                Settings.Options |= ExportPackageOptions.Recurse;

            if (HasArgument(Values.ARG_INCLUDE_DEPENDENCIES))
                Settings.Options |= ExportPackageOptions.IncludeDependencies;

            if (HasArgument(Values.ARG_INCLUDE_LIBRARY_ASSETS))
                Settings.Options |= ExportPackageOptions.IncludeLibraryAssets;
        }

        #endregion
    }
}