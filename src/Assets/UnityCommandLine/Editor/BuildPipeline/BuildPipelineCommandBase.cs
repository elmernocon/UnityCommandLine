#region File Header

// Filename: BuildPipelineCommandBase.cs
// Author: Elmer Nocon
// Date Created: 2019/05/20
// License: MIT

#endregion

using UnityEditor;
using UBuildPipeline = UnityEditor.BuildPipeline;

namespace UnityCommandLine.BuildPipeline
{
    /// <summary>
    /// /// <inheritdoc />
    /// <summary>
    /// The base class for all build-pipeline-related commands executable through Unity's command line interface.
    /// </summary>
    /// </summary>
    public abstract class BuildPipelineCommandBase : CommandBase
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Checks whether the editor is currently building a player.
        /// </summary>
        /// <returns>Returns <c>true</c> if the editor is busy, otherwise <c>false</c>.</returns>
        protected static bool IsBuildPipelineBusy()
        {
            return UBuildPipeline.isBuildingPlayer;
        }

        /// <summary>
        /// Checks whether the editor supports the build target.
        /// </summary>
        /// <param name="targetGroup">The build target group.</param>
        /// <param name="target">The build target.</param>
        /// <returns>Returns <c>true</c> if the build target is supported, otherwise <c>false</c>.</returns>
        protected static bool IsBuildTargetSupported(BuildTargetGroup targetGroup, BuildTarget target)
        {
#if UNITY_2018_1_OR_NEWER
            return UBuildPipeline.IsBuildTargetSupported(targetGroup, target);
#else
            return true;
#endif
        }

        #endregion

        #endregion
    }
}