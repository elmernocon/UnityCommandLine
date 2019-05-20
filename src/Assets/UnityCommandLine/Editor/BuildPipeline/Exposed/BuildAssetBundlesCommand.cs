#region File Header

// Filename: BuildAssetBundlesCommand.cs
// Author: Elmer Nocon
// Date Created: 2019/05/20
// License: MIT

#endregion

using JetBrains.Annotations;
using UnityCommandLine.BuildPipeline;

/// <summary>
/// Builds all asset bundles using the given <see cref="T:UnityEditor.BuildTarget" />.
/// </summary>
/// <para>
/// Example:
/// <code>-executeMethod BuildAssetBundlesCommand.Execute -buildTarget Win64</code>
/// </para>
public class BuildAssetBundlesCommand : BuildAssetBundlesCommandBase
{
    #region Statics

    #region Static Methods

    /// <summary>
    /// Executes this command.
    /// </summary>
    [UsedImplicitly]
    public static void Execute()
    {
        var command = new BuildAssetBundlesCommand();
        
        command.Run();
    }

    #endregion

    #endregion
}