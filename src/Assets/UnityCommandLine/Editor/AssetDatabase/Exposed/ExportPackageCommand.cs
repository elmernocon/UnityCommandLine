#region File Header

// Filename: ExportPackageCommand.cs
// Author: Elmer Nocon
// Date Created: 2019/05/17
// License: MIT

#endregion

using System;
using JetBrains.Annotations;
using UnityCommandLine.AssetDatabase;

/// <summary>
/// Export a package.
/// </summary>
/// <para>
/// Example:
/// <code>-executeMethod ExportPackageCommand.Execute -packageContents "Asset1.png,Asset2.png,Folder1,Folder2" -packageName HelloWorld</code>
/// </para>
public class ExportPackageCommand : ExportPackageCommandBase
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
        var command = new ExportPackageCommand();
        
        command.Run();
    }

    #endregion

    #endregion
}