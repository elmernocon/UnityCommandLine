#region File Header

// Filename: BuildPlayerCommand.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using System;
using UnityCommandLine.BuildPipeline;
using JetBrains.Annotations;
using UnityEditor;

/// <summary>
/// Builds a player using the given <see cref="T:UnityEditor.BuildTarget" />.
/// </summary>
/// <para>
/// Example:
/// <code>-executeMethod BuildPlayerCommand.Execute -buildTarget win64</code>
/// </para>
public class BuildPlayerCommand : BuildPlayerCommandBase
{
    #region Statics

    #region Static Methods

    /// <summary>
    /// Executes this command.
    /// </summary>
    /// <exception cref="Exception"></exception>
    [UsedImplicitly]
    public static void Execute()
    {
        var arguments = GetArguments();

        string buildTargetString;
        if (!GetArgumentValue(arguments, Values.ARG_BUILD_TARGET, out buildTargetString))
            throw new Exception(string.Format("Argument '{0}' is required.", Values.ARG_BUILD_TARGET));

        var buildTarget = buildTargetString.ToBuildTarget();
        
        var command = new BuildPlayerCommand(buildTarget);
        
        command.Run();
    }

    #endregion

    #endregion

    #region Constructors & Destructors

    /// <summary>
    /// Creates an instance of <see cref="BuildPlayerCommand"/>.
    /// </summary>
    /// <param name="target">The build target.</param>
    protected BuildPlayerCommand(BuildTarget target) : base(target)
    {
    }

    #endregion
}