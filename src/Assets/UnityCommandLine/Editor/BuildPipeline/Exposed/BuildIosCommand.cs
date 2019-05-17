#region File Header

// Filename: BuildIosCommand.cs
// Author: Elmer Nocon
// Date Created: 2019/05/17
// License: MIT

#endregion

using UnityCommandLine.BuildPipeline;
using JetBrains.Annotations;
using UnityCommandLine;
using UnityEditor;
using Values = UnityCommandLine.BuildPipeline.Values;

/// <summary>
/// Builds an iOS player using the given <see cref="T:UnityEditor.BuildTarget" />.
/// </summary>
/// <para>
/// Example:
/// <code>-executeMethod BuildIosCommand.Execute</code>
/// </para>
public class BuildIosCommand : BuildPlayerCommandBase
{
    #region Statics

    #region Static Methods

    /// <summary>
    /// Executes this command.
    /// </summary>
    [UsedImplicitly]
    public static void Execute()
    {
        var command = new BuildIosCommand(BuildTarget.iOS);
        
        command.Run();
    }

    #endregion

    #endregion

    #region Constructors & Destructors

    /// <summary>
    /// Creates an instance of <see cref="BuildIosCommand"/>.
    /// </summary>
    /// <param name="target">The build target.</param>
    protected BuildIosCommand(BuildTarget target) : base(target)
    {
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    protected override string GetOutputPath(string outputFileName)
    {
        return CommandUtils.PathCombine(Values.DEFAULT_BUILD_FOLDER_NAME, Settings.Target.ToString());
    }

    #endregion
}