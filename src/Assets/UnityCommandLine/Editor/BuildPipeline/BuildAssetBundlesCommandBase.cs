#region File Header

// Filename: BuildAssetBundlesCommandBase.cs
// Author: Elmer Nocon
// Date Created: 2019/05/20
// License: MIT

#endregion

using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UBuildPipeline = UnityEditor.BuildPipeline;

namespace UnityCommandLine.BuildPipeline
{
    /// <inheritdoc />
    /// <summary>
    /// The base class for all build-asset-bundles-related commands executable through Unity's command line interface.
    /// </summary>
    public class BuildAssetBundlesCommandBase : BuildPipelineCommandBase
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Builds asset bundles.
        /// </summary>
        /// <param name="settings">The build asset bundles settings.</param>
        /// <returns>The asset bundle manifest.</returns>
        private static AssetBundleManifest BuildAssetBundles(BuildAssetBundlesSettings settings)
        {
            return UBuildPipeline.BuildAssetBundles(settings.OutputPath, settings.Options, settings.TargetPlatform);
        }

        /// <summary>
        /// Print a <see cref="Settings"/> object.
        /// </summary>
        /// <param name="settings">The asset bundles settings object.</param>
        /// <param name="title">The title.</param>
        private static void PrintSettings(BuildAssetBundlesSettings settings, string title = null)
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.AppendLine(UnityCommandLine.Values.SEPARATOR);
            if (!string.IsNullOrEmpty(title)) stringBuilder.AppendLine(title);
            BuildAssetBundlesSettings.Print(settings, stringBuilder);
            stringBuilder.AppendLine(UnityCommandLine.Values.SEPARATOR);
            
            PrintLine(stringBuilder.ToString());
        }

        #endregion

        #endregion

        #region Fields

        /// <summary>
        /// The build asset bundles settings to use when building the asset bundles.
        /// </summary>
        protected readonly BuildAssetBundlesSettings Settings;

        #endregion

        #region Constructors & Destructors

        /// <summary>
        /// Creates an instance of <see cref="BuildAssetBundlesCommandBase"/>.
        /// </summary>
        public BuildAssetBundlesCommandBase()
        {
            Settings = new BuildAssetBundlesSettings();

            // Gets the build target string.
            string buildTargetString;
            if (!GetArgumentValue(Values.ARG_BUILD_TARGET, out buildTargetString))
                throw new Exception(string.Format("Argument '{0}' is required.", Values.ARG_BUILD_TARGET));
            
            var buildTarget = buildTargetString.ToBuildTarget();
            
            // Sets the build target.
            Settings.TargetPlatform = buildTarget;
            
            var targetGroup = BuildTargetUtils.GetBuildTargetGroup(Settings.TargetPlatform);
            
            if (!IsBuildTargetSupported(targetGroup, Settings.TargetPlatform))
                throw new Exception(string.Format("Build target '{0}' is not supported on this editor.", Settings.TargetPlatform));
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Run()
        {
            InitArgs();
            
            PrintSettings(Settings, "Building:");
            
            BuildAssetBundles(Settings);
        }

        /// <summary>
        /// Gets the output path.
        /// </summary>
        /// <param name="outputFolderName">The output folder name.</param>
        /// <returns>Returns the output path.</returns>
        protected virtual string GetOutputPath(string outputFolderName)
        {
            var directoryPath = CommandUtils.PathCombine(Path.GetDirectoryName(Application.dataPath), Values.DEFAULT_BUNDLE_FOLDER_NAME, outputFolderName);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            
            return directoryPath;
        }

        /// <summary>
        /// Initializes the arguments used by this command.
        /// </summary>
        private void InitArgs()
        {
            string bundleName;
            if (!GetArgumentValue(Values.ARG_BUNDLE_NAME, out bundleName))
                bundleName = Values.DEFAULT_BUNDLE_NAME;
            
            // Set the asset bundle output path.
            Settings.OutputPath = GetOutputPath(bundleName);

            // Set the initial bundle option.
            Settings.Options = Values.DEFAULT_BUNDLE_OPTIONS;
            
            // Set additional bundle options to use in the build.
            if (HasArgument(Values.ARG_OPTION_UNCOMPRESSED_ASSET_BUNDLE))
                Settings.Options |= BuildAssetBundleOptions.UncompressedAssetBundle;
            
            if (HasArgument(Values.ARG_OPTION_DISABLE_WRITE_TYPE_TREE))
                Settings.Options |= BuildAssetBundleOptions.DisableWriteTypeTree;
            
            if (HasArgument(Values.ARG_OPTION_DETERMINISTIC_ASSET_BUNDLE))
                Settings.Options |= BuildAssetBundleOptions.DeterministicAssetBundle;
            
            if (HasArgument(Values.ARG_OPTION_FORCE_REBUILD_ASSET_BUNDLE))
                Settings.Options |= BuildAssetBundleOptions.ForceRebuildAssetBundle;
            
            if (HasArgument(Values.ARG_OPTION_IGNORE_TYPE_TREE_CHANGES))
                Settings.Options |= BuildAssetBundleOptions.IgnoreTypeTreeChanges;
            
            if (HasArgument(Values.ARG_OPTION_APPEND_HASH_TO_ASSET_BUNDLE_NAME))
                Settings.Options |= BuildAssetBundleOptions.AppendHashToAssetBundleName;

#if UNITY_5_3_OR_NEWER
            if (HasArgument(Values.ARG_OPTION_CHUNK_BASED_COMPRESSION))
                Settings.Options |= BuildAssetBundleOptions.ChunkBasedCompression;
#endif

#if UNITY_5_4_OR_NEWER
            if (HasArgument(Values.ARG_OPTION_STRICT_MODE))
                Settings.Options |= BuildAssetBundleOptions.StrictMode;
#endif

#if UNITY_5_5_OR_NEWER
            if (HasArgument(Values.ARG_OPTION_DRY_RUN_BUILD))
                Settings.Options |= BuildAssetBundleOptions.DryRunBuild;
#else // UNITY_5_4_OR_OLDER
#if UNITY_5_4_OR_NEWER // UNITY_5_4_ONLY
            if (HasArgument(Values.ARG_OPTION_OMIT_CLASS_VERSIONS))
                Settings.Options |= BuildAssetBundleOptions.OmitClassVersions;
#endif
#endif
            
#if UNITY_2017_1_OR_NEWER
            if (HasArgument(Values.ARG_OPTION_DISABLE_LOAD_ASSET_BY_FILE_NAME))
                Settings.Options |= BuildAssetBundleOptions.DisableLoadAssetByFileName;
            
            if (HasArgument(Values.ARG_OPTION_DISABLE_LOAD_ASSET_BY_FILE_NAME_WITH_EXTENSION))
                Settings.Options |= BuildAssetBundleOptions.DisableLoadAssetByFileNameWithExtension;
#endif
        }

        #endregion
    }
}