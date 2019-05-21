#region File Header

// Filename: BuildAssetBundlesSettings.cs
// Author: Elmer Nocon
// Date Created: 2019/05/20
// License: MIT

#endregion

using System.Text;
using UnityEditor;

namespace UnityCommandLine.BuildPipeline
{
    /// <summary>
    /// A container class for all values used when building asset bundles.
    /// </summary>
    public class BuildAssetBundlesSettings
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Prints the values of a <see cref="BuildAssetBundlesSettings"/> instance.
        /// </summary>
        /// <param name="settings">The build asset bundles settings instance.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        public static void Print(BuildAssetBundlesSettings settings, StringBuilder stringBuilder)
        {
            if (!string.IsNullOrEmpty(settings.OutputPath))
                stringBuilder.AppendLine(string.Format("OutputPath: {0}", settings.OutputPath));
            
            if (settings.Options != BuildAssetBundleOptions.None)
                stringBuilder.AppendLine(string.Format("Options: {0}", settings.Options));
            
            stringBuilder.AppendLine(string.Format("TargetPlatform: {0}", settings.TargetPlatform));
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the output path.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets and sets the build asset bundles options to use when building asset bundles.
        /// </summary>
        public BuildAssetBundleOptions Options { get; set; }

        /// <summary>
        /// Gets and sets the target platform.
        /// </summary>
        public BuildTarget TargetPlatform { get; set; }

        #endregion
    }
}