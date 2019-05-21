#region File Header

// Filename: BuildReportUtils.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

#if UNITY_2018_1_OR_NEWER

using System.IO;
using System.Linq;
using System.Text;
using UnityEditor.Build.Reporting;

namespace UnityCommandLine.BuildPipeline
{
    /// <summary>
    /// A utility class container for <see cref="BuildReport"/>-related methods.
    /// </summary>
    public static class BuildReportUtils
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Stringifies a <see cref="BuildReport"/> instance.
        /// </summary>
        /// <param name="report">The build report.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        /// <param name="verbose">Whether or not the output is verbose.</param>
        public static void StringifyReport(BuildReport report, StringBuilder stringBuilder,
                                           bool verbose = false)
        {
            stringBuilder.AppendLine("Build Report:");
            StringifySummary(report.summary, stringBuilder);
            StringifyStrippingInfo(report.strippingInfo, stringBuilder);
            StringifySteps(report.steps, stringBuilder, verbose);
            StringifyFiles(report.files, stringBuilder, verbose);
        }

        /// <summary>
        /// Stringifies a <see cref="BuildSummary"/> instance.
        /// </summary>
        /// <param name="summary">The build summary.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        private static void StringifySummary(BuildSummary summary, StringBuilder stringBuilder,
                                             int indentLevel = 0, string indent = Values.DEFAULT_INDENT)
        {
            var ind = CreateIndent(indentLevel, indent);
            
            stringBuilder.AppendLine($"{ind}- Summary:");
            stringBuilder.AppendLine($"{ind} - Result: {summary.result}");
            stringBuilder.AppendLine($"{ind} - GUID: {summary.guid}");
            stringBuilder.AppendLine($"{ind} - Platform: {summary.platform}");
            stringBuilder.AppendLine($"{ind} - PlatformGroup: {summary.platformGroup}");
            stringBuilder.AppendLine($"{ind} - OutputPath: {summary.outputPath}");
            stringBuilder.AppendLine($"{ind} - Options: {summary.options}");
            stringBuilder.AppendLine($"{ind} - StartedAt: {summary.buildStartedAt:s}");
            stringBuilder.AppendLine($"{ind} - EndedAt: {summary.buildEndedAt:s}");
            stringBuilder.AppendLine($"{ind} - TotalWarnings: {summary.totalWarnings}");
            stringBuilder.AppendLine($"{ind} - TotalErrors: {summary.totalErrors}");
            stringBuilder.AppendLine($"{ind} - TotalSize: {summary.totalSize / 1000} KB");
            stringBuilder.AppendLine($"{ind} - TotalTime: {(int) summary.totalTime.TotalMilliseconds} ms");
        }

        /// <summary>
        /// Stringifies a <see cref="StrippingInfo"/> instance.
        /// </summary>
        /// <param name="strippingInfo">The stripping info.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        private static void StringifyStrippingInfo(StrippingInfo strippingInfo, StringBuilder stringBuilder,
                                                   int indentLevel = 0, string indent = Values.DEFAULT_INDENT)
        {
            var ind = CreateIndent(indentLevel, indent);

            if (strippingInfo == null)
                return;
            
            stringBuilder.AppendLine($"{ind}- Stripping Info:");

            foreach (var includedModule in strippingInfo.includedModules)
                stringBuilder.AppendLine($"{ind} - {includedModule}");
        }

        /// <summary>
        /// Stringifies <see cref="BuildStep"/> instances.
        /// </summary>
        /// <param name="steps">The build step instances.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        /// <param name="verbose">Whether or not the output is verbose.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        private static void StringifySteps(BuildStep[] steps, StringBuilder stringBuilder,
                                           bool verbose = false,
                                           int indentLevel = 0, string indent = Values.DEFAULT_INDENT)
        {
            if (steps == null || steps.Length == 0)
                return;
            
            var ind = CreateIndent(indentLevel, indent);

            var duration = steps.Sum(step => step.duration.TotalMilliseconds);
            
            stringBuilder.AppendLine($"{ind}- Steps ({steps.Length} steps | {(int) duration} ms):");

            foreach (var step in steps)
                StringifyStep(step, stringBuilder, verbose, indentLevel + 1);
        }

        /// <summary>
        /// Stringifies a <see cref="BuildStep"/> instance.
        /// </summary>
        /// <param name="step">The build step instance.</param>
        /// <param name="stringBuilder">The string builder instance.</param>
        /// <param name="verbose">Whether or not the output is verbose.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        private static void StringifyStep(BuildStep step, StringBuilder stringBuilder,
                                          bool verbose = false,
                                          int indentLevel = 0, string indent = Values.DEFAULT_INDENT)
        {
            var ind = CreateIndent(indentLevel, indent);

            if (verbose)
            {
                stringBuilder.AppendLine($"{ind}- {step.name}");
                stringBuilder.AppendLine($"{ind} - Depth: {step.depth}");
                stringBuilder.AppendLine($"{ind} - Duration: {(int) step.duration.TotalMilliseconds} ms");
            }
            else
            {
                stringBuilder.AppendLine($"{ind}- {step.name} ({step.depth} depth | {(int) step.duration.TotalMilliseconds} ms)");
            }

            if (step.messages == null || step.messages.Length <= 0)
                return;
            
            stringBuilder.AppendLine($"{ind} - Messages: {step.messages.Length}");

            foreach (var message in step.messages)
                stringBuilder.AppendLine($"{ind}  - {message.content}");
        }

        /// <summary>
        /// Stringifies <see cref="BuildFile"/> instances.
        /// </summary>
        /// <param name="files">The build file instances.</param>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="verbose">Whether or not the output is verbose.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        private static void StringifyFiles(BuildFile[] files, StringBuilder stringBuilder,
                                           bool verbose = false,
                                           int indentLevel = 0, string indent = Values.DEFAULT_INDENT)
        {
            if (files == null || files.Length == 0)
                return;
            
            var ind = CreateIndent(indentLevel, indent);

            var size = files.Sum(file => (double) file.size);
            
            stringBuilder.AppendLine($"{ind}- Files ({files.Length} files | {(int) size / 1000} KB):");

            foreach (var file in files)
                StringifyFile(file, stringBuilder, verbose, indentLevel + 1);
        }

        /// <summary>
        /// Stringifies a <see cref="BuildFile"/> instance.
        /// </summary>
        /// <param name="file">The build file instance.</param>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="verbose">Whether or not the output is verbose.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        private static void StringifyFile(BuildFile file, StringBuilder stringBuilder,
                                          bool verbose = false,
                                          int indentLevel = 0, string indent = Values.DEFAULT_INDENT)
        {
            var ind = CreateIndent(indentLevel, indent);

            if (verbose)
            {
                stringBuilder.AppendLine($"{ind}- {file.path}");
                stringBuilder.AppendLine($"{ind} - Role: {file.role}");
                stringBuilder.AppendLine($"{ind} - Size: {file.size / 1000} KB");
            }
            else
            {
                stringBuilder.AppendLine($"{ind}- {Path.GetFileName(file.path)} ({file.role} | {file.size / 1000} KB)");
            }
        }

        /// <summary>
        /// Creates an indent string.
        /// </summary>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="indent">The indent string.</param>
        /// <returns>The new indent string.</returns>
        private static string CreateIndent(int indentLevel, string indent)
        {
            var ind = string.Empty;

            for (var i = 0; i < indentLevel; i++)
                ind += indent;

            return ind;
        }

        #endregion

        #endregion
    }
}

#endif