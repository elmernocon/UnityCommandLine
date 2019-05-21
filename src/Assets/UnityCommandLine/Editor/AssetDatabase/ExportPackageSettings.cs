#region File Header

// Filename: Settings.cs
// Author: Elmer Nocon
// Date Created: 2019/05/17
// License: MIT

#endregion

using System.Text;
using UnityEditor;

namespace UnityCommandLine.AssetDatabase
{
    /// <summary>
    /// A container class for all values used when exporting a package.
    /// </summary>
    public class ExportPackageSettings
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Prints the values of a <see cref="ExportPackageSettings"/> instance.
        /// </summary>
        /// <param name="settings">The export package settings instance.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        public static void Print(ExportPackageSettings settings, StringBuilder stringBuilder)
        {
            const string listItemPrefix = "  - ";
            
            if (settings.AssetPathNames != null && settings.AssetPathNames.Length > 0)
                stringBuilder.AppendLine(string.Format("AssetPathNames:\n{0}{1}", listItemPrefix, string.Join(
                        string.Format("\n{0}", listItemPrefix), settings.AssetPathNames)));
            
            if (!string.IsNullOrEmpty(settings.OutputPath))
                stringBuilder.AppendLine(string.Format("OutputPath: {0}", settings.OutputPath));
            
            if (settings.Options != ExportPackageOptions.Default)
                stringBuilder.AppendLine(string.Format("Options: {0}", settings.Options));
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the list of asset path names to include in the package.
        /// </summary>
        public string[] AssetPathNames { get; set; }

        /// <summary>
        /// Gets and sets the output path.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets and sets the export package options to use when exporting the package.
        /// </summary>
        public ExportPackageOptions Options { get; set; }

        #endregion
    }
}