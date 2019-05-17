#region File Header

// Filename: BuildPlayerCommandBase.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using System;
using System.Linq;
using UnityEditor;
using UBuildPipeline = UnityEditor.BuildPipeline;

#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif

namespace UnityCommandLine.BuildPipeline
{
    /// <inheritdoc />
    /// <summary>
    /// The base class for all build-player-related commands executable through Unity's command line interface.
    /// </summary>
    public abstract class BuildPlayerCommandBase : CommandBase
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Builds a player.
        /// </summary>
        /// <param name="settings">The build settings.</param>
        /// <returns>Either a BuildReport object or a string depending on the editor version used.</returns>
        private static
#if UNITY_2018_1_OR_NEWER
                BuildReport
#else
                string
#endif
                BuildPlayer(Settings settings)
        {
            return UBuildPipeline.BuildPlayer(settings.Levels, settings.OutputPath, settings.Target, settings.Options);
        }

        /// <summary>
        /// Gets all the included and enabled scenes in the project.
        /// </summary>
        /// <returns>The enabled scenes path.</returns>
        private static string[] GetEnabledScenes()
        {
            return EditorBuildSettings.scenes
                                      .Where(scene => scene.enabled && !string.IsNullOrEmpty(scene.path))
                                      .Select(scene => scene.path)
                                      .ToArray();
        }
        
        /// <summary>
        /// Checks whether the editor is currently building a player.
        /// </summary>
        /// <returns>Returns <c>true</c> if the editor is busy, otherwise <c>false</c>.</returns>
        private static bool IsBuildPipelineBusy()
        {
            return UBuildPipeline.isBuildingPlayer;
        }
        
        /// <summary>
        /// Checks whether the editor supports the build target.
        /// </summary>
        /// <param name="targetGroup">The build target group.</param>
        /// <param name="target">The build target.</param>
        /// <returns>Returns <c>true</c> if the build target is supported, otherwise <c>false</c>.</returns>
        private static bool IsBuildTargetSupported(BuildTargetGroup targetGroup, BuildTarget target)
        {
#if UNITY_2018_1_OR_NEWER
            return UBuildPipeline.IsBuildTargetSupported(targetGroup, target);
#else
            return true;
#endif
        }

        /// <summary>
        /// Prints a build report.
        /// </summary>
        /// <param name="report">The build report.</param>
        /// <returns>The stringified report.</returns>
#if UNITY_2018_1_OR_NEWER
        private static string PrintReport(BuildReport report)
        {
            return BuildReportUtils.StringifyReport(report, Values.DEFAULT_BUILD_REPORT_VERBOSE);
        }
#else
        private static string PrintReport(string report)
        {
            return report;
        }
#endif

        /// <summary>
        /// Print a <see cref="Settings"/> object.
        /// </summary>
        /// <param name="settings">The settings object.</param>
        /// <param name="title">The title.</param>
        private static void PrintSettings(Settings settings, string title = null)
        {
            PrintSeparator();
            
            if (!string.IsNullOrEmpty(title))
                PrintLine(title);
            
            Print(settings.ToString());
            
            PrintSeparator();

            PrintLine();
        }

        #endregion

        #endregion

        #region Fields
        
        /// <summary>
        /// The settings to use when building the player.
        /// </summary>
        protected readonly Settings Settings;
        
        private readonly Settings _backupSettings;

        #endregion

        #region Constructors & Destructors

        /// <inheritdoc />
        /// <summary>
        /// Creates an instance of <see cref="T:UnityCommandLine.BuildPipeline.BuildPlayerCommandBase" />.
        /// </summary>
        /// <param name="target">The build target.</param>
        protected BuildPlayerCommandBase(BuildTarget target)
        {
            _backupSettings = Settings.Create();
            Settings = Settings.Create();

            Settings.Target = target;
            
            if (!IsBuildTargetSupported(Settings.TargetGroup, Settings.Target))
                throw new Exception(string.Format("Build target '{0}' is not supported on this editor.", target));
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Run()
        {
            try
            {
                PrintSettings(_backupSettings, "Saved settings:");
                
                InitArgs();
                
                if (IsBuildPipelineBusy())
                    throw new Exception("BuildPipeline is busy.");
            
                var report = BuildPlayer(Settings);
                
                PrintLine();
                PrintSeparator();
                Print(PrintReport(report));
                PrintSeparator();
                PrintLine();
            }
            finally
            {
                _backupSettings.Apply();
                
                PrintSettings(_backupSettings, "Reverted settings:");
            }
        }

        /// <summary>
        /// Gets the output path.
        /// </summary>
        /// <param name="outputFileName">The output file name.</param>
        /// <returns>Returns the output path.</returns>
        protected virtual string GetOutputPath(string outputFileName)
        {
            var fileExtension = BuildTargetUtils.GetFileExtension(Settings.Target);

            return CommandUtils.PathCombine(Values.DEFAULT_BUILD_FOLDER_NAME, Settings.Target.ToString(),
                    string.Format("{0}{1}", outputFileName, fileExtension));
        }

        /// <summary>
        /// Initializes the arguments used by this command.
        /// </summary>
        private void InitArgs()
        {
            // Set the build name if there is one.
            string buildName;
            if (!GetArgumentValue(Values.ARG_BUILD_NAME, out buildName))
                buildName = Values.DEFAULT_BUILD_NAME;

            // Set the build output path.
            Settings.OutputPath = GetOutputPath(buildName);

            // Set the levels to include in the build.
            Settings.Levels = GetEnabledScenes();

            // Set the initial build option.
            Settings.Options = Values.DEFAULT_BUILD_OPTIONS;

            // Set additional build options to use in the build.
            if (HasArgument(Values.ARG_OPTION_DEVELOPMENT))
                Settings.Options |= BuildOptions.Development;

            if (HasArgument(Values.ARG_OPTION_ALLOW_DEBUGGING))
                Settings.Options |= BuildOptions.AllowDebugging;

            if (HasArgument(Values.ARG_OPTION_SYMLINK_LIBRARIES))
                Settings.Options |= BuildOptions.SymlinkLibraries;

            if (HasArgument(Values.ARG_OPTION_FORCE_ENABLE_ASSERTIONS))
                Settings.Options |= BuildOptions.ForceEnableAssertions;

            if (HasArgument(Values.ARG_OPTION_COMPRESS_WITH_LZ4))
                Settings.Options |= BuildOptions.CompressWithLz4;

#if UNITY_2017_2_OR_NEWER
            if (HasArgument(Values.ARG_OPTION_COMPRESS_WITH_LZ4_HC))
                Settings.Options |= BuildOptions.CompressWithLz4HC;
#endif

            if (HasArgument(Values.ARG_OPTION_STRICT_MODE))
                Settings.Options |= BuildOptions.StrictMode;

#if UNITY_2018_1_OR_NEWER
            if (HasArgument(Values.ARG_INCLUDE_TEST_ASSEMBLIES))
                Settings.Options |= BuildOptions.IncludeTestAssemblies;
#endif
            
            // Set the application identifier if there is one.
            string applicationIdentifier;
            if (GetArgumentValue(Values.ARG_APPLICATION_IDENTIFIER, out applicationIdentifier))
                Settings.ApplicationIdentifier = applicationIdentifier;

            // Set the bundle version if there is one.
            string bundleVersion;
            if (GetArgumentValue(Values.ARG_BUNDLE_VERSION, out bundleVersion))
                Settings.BundleVersion = bundleVersion;

            // Apply the new settings.
            Settings.Apply();
                
            PrintSettings(Settings, "Overwrote settings:");
        }

        #endregion
    }
}