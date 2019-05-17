#region File Header

// Filename: BuildTargetUtils.cs
// Author: Elmer Nocon
// Date Created: 2019/05/16
// License: MIT

#endregion

using System;
using UnityEditor;

// ReSharper disable CyclomaticComplexity

namespace UnityCommandLine.BuildPipeline
{
    /// <summary>
    /// A utility class container for <see cref="BuildTarget"/>-related methods.
    /// </summary>
    public static class BuildTargetUtils
    {
        #region Statics

        #region Static Methods

        /// <summary>
        /// Converts a string into a <see cref="BuildTarget"/>.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Returns the build target.</returns>
        public static BuildTarget ConvertStringToBuildTarget(string str, BuildTarget defaultValue = BuildTarget.NoTarget)
        {
            if (Enum.IsDefined(typeof(BuildTarget), str))
                return (BuildTarget) Enum.Parse(typeof(BuildTarget), str, true);

            var strLower = str.ToLower();
            
            switch (strLower)
            {
                case "win":
                    return BuildTarget.StandaloneWindows;
                
                case "linux":
                    return BuildTarget.StandaloneLinux;
                
                case "win64":
                    return BuildTarget.StandaloneWindows64;
                    
                case "windowsstoreapps":
                    return BuildTarget.WSAPlayer;
                
                case "linux64":
                    return BuildTarget.StandaloneLinux64;
                
                case "linuxuniversal":
                    return BuildTarget.StandaloneLinuxUniversal;

#if UNITY_2017_3_OR_NEWER
                case "osxuniversal":
                    return BuildTarget.StandaloneOSX;
                
#else // UNITY_2017_2_OR_OLDER
                case "osxuniversal":
                    return BuildTarget.StandaloneOSXUniversal;
                
                case "osxintel":
                    return BuildTarget.StandaloneOSXIntel;
                
                case "osxintel64":
                    return BuildTarget.StandaloneOSXIntel64;
#endif

                    
#if UNITY_5_4_OR_NEWER
#else // UNITY_5_3_OR_OLDER
                case "web":
                    return BuildTarget.WebPlayer;
                case "webstreamed:
                    return BuildTarget.WebPlayerStreamed;
#endif
                    
                default:
                    return defaultValue;
            }
        }

        /// <summary>
        /// Converts a string into a <see cref="BuildTarget"/>.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Returns the build target.</returns>
        public static BuildTarget ToBuildTarget(this string str, BuildTarget defaultValue = BuildTarget.NoTarget)
        {
            return ConvertStringToBuildTarget(str, defaultValue);
        }

        /// <summary>
        /// Gets the best matching file extensions for the given <see cref="BuildTarget"/>.
        /// </summary>
        /// <param name="target">The build target.</param>
        /// <returns>The best matching file extension for the given build target.</returns>
        public static string GetFileExtension(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                    return ".exe";
                    
                case BuildTarget.iOS:
                    return ".ipa";
                    
                case BuildTarget.Android:
                    return ".apk";
                    
                case BuildTarget.StandaloneLinux:
                    return ".x86";
                
                case BuildTarget.StandaloneWindows64:
                    return ".exe";
                    
                case BuildTarget.WebGL:
                    return ".html";
                    
                case BuildTarget.StandaloneLinux64:
                    return ".x64";
                
                case BuildTarget.StandaloneLinuxUniversal:
                    return ".x86_64";
                 
#if UNITY_2017_3_OR_NEWER
                case BuildTarget.StandaloneOSX:
                    return ".app";
                
#else // UNITY_2017_2_OR_OLDER
                case BuildTarget.StandaloneOSXUniversal:
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                    return ".app";
#endif
                           
#if UNITY_5_4_OR_NEWER
#else // UNITY_5_3_OR_OLDER
                case BuildTarget.WebPlayer:
                case BuildTarget.WebPlayerStreamed:
                    return ".html";
#endif

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the best matching <see cref="BuildTargetGroup"/> for the given <see cref="BuildTarget"/>.
        /// </summary>
        /// <param name="target">The build target.</param>
        /// <returns>The best matching build target group for the given build target.</returns>
        public static BuildTargetGroup GetBuildTargetGroup(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                    return BuildTargetGroup.Standalone;
                    
                case BuildTarget.iOS:
                    return BuildTargetGroup.iOS;
                    
                case BuildTarget.Android:
                    return BuildTargetGroup.Android;
                    
                case BuildTarget.StandaloneLinux:
                case BuildTarget.StandaloneWindows64:
                    return BuildTargetGroup.Standalone;
                    
                case BuildTarget.WebGL:
                    return BuildTargetGroup.WebGL;
                    
                case BuildTarget.WSAPlayer:
                    return BuildTargetGroup.WSA;
                    
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneLinuxUniversal:
                    return BuildTargetGroup.Standalone;
                    
                case BuildTarget.PS4:
                    return BuildTargetGroup.PS4;
                    
                case BuildTarget.XboxOne:
                    return BuildTargetGroup.XboxOne;
                    
                case BuildTarget.tvOS:
                    return BuildTargetGroup.tvOS;
                    
#if UNITY_2018_3_OR_NEWER
#else // UNITY_2018_2_OR_OLDER
                case BuildTarget.PSP2:
                    return BuildTargetGroup.PSP2; // 5.0 ~ 2018.2
    
                case BuildTarget.WiiU:
                    return BuildTargetGroup.WiiU; // 5.2 ~ 2018.2
    
#if UNITY_5_5_OR_NEWER
                case BuildTarget.N3DS:
                    return BuildTargetGroup.N3DS; // 5.5 ~ 2018.2
#endif
#endif
                    
#if UNITY_2018_2_OR_NEWER
#else // UNITY_2018_1_OR_OLDER
                case BuildTarget.Tizen:
                    return BuildTargetGroup.Tizen; // 5.1 ~ 2018.1
#endif
    
#if UNITY_2017_3_OR_NEWER
                case BuildTarget.StandaloneOSX:
                    return BuildTargetGroup.Standalone;
                
#else // UNITY_2017_2_OR_OLDER
                case BuildTarget.StandaloneOSXUniversal:
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                    return BuildTargetGroup.Standalone; // 5.0 ~ 2017.2
    
                case BuildTarget.SamsungTV:
                    return BuildTargetGroup.SamsungTV; // 5.0 ~ 2017.2
#endif
                    
#if UNITY_5_6_OR_NEWER
                case BuildTarget.Switch:
                    return BuildTargetGroup.Switch;
#else
#endif
                    
#if UNITY_5_5_OR_NEWER
#else // UNITY_5_4_OR_OLDER
                case BuildTarget.PS3:
                    return BuildTargetGroup.PS3; // 5.0 ~ 5.4
    
                case BuildTarget.XBOX360:
                    return BuildTargetGroup.XBOX360; // 5.0 ~ 5.4
    
                case BuildTarget.Nintendo3DS:
                    return BuildTargetGroup.Nintendo3DS; // 5.0 ~ 5.4
#endif
                    
#if UNITY_5_4_OR_NEWER
#else // UNITY_5_3_OR_OLDER
                case BuildTarget.WebPlayer:
                case BuildTarget.WebPlayerStreamed:
                    return BuildTargetGroup.WebPlayer; // 5.3 ~ 5.3
#endif
                    
                default:
                    return BuildTargetGroup.Unknown;
            }
        }

        #endregion

        #endregion
    }
}