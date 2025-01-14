//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;

namespace UnityGameFramework.Editor
{
    /// <summary>
    /// 脚本宏定义。
    /// </summary>
    public static class ScriptingDefineSymbols
    {
        private static readonly NamedBuildTarget[] NamedBuildTargets =
        {
            NamedBuildTarget.Standalone,
            NamedBuildTarget.iOS,
            NamedBuildTarget.Android,
            NamedBuildTarget.WindowsStoreApps,
            NamedBuildTarget.WebGL
        };

        /// <summary>
        /// 检查指定平台是否存在指定的脚本宏定义。
        /// </summary>
        /// <param name="buildTarget">要检查脚本宏定义的平台。</param>
        /// <param name="scriptingDefineSymbol">要检查的脚本宏定义。</param>
        /// <returns>指定平台是否存在指定的脚本宏定义。</returns>
        public static bool HasScriptingDefineSymbol(NamedBuildTarget buildTarget, string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
            {
                return false;
            }

            string[] scriptingDefineSymbols = GetScriptingDefineSymbols(buildTarget);
            foreach (string i in scriptingDefineSymbols)
            {
                if (i == scriptingDefineSymbol)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 为指定平台增加指定的脚本宏定义。
        /// </summary>
        /// <param name="buildTarget">要增加脚本宏定义的平台。</param>
        /// <param name="scriptingDefineSymbol">要增加的脚本宏定义。</param>
        public static void AddScriptingDefineSymbol(NamedBuildTarget buildTarget, string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
            {
                return;
            }

            if (HasScriptingDefineSymbol(buildTarget, scriptingDefineSymbol))
            {
                return;
            }

            List<string> scriptingDefineSymbols = new List<string>(GetScriptingDefineSymbols(buildTarget))
            {
                scriptingDefineSymbol
            };

            SetScriptingDefineSymbols(buildTarget, scriptingDefineSymbols.ToArray());
        }

        /// <summary>
        /// 为指定平台移除指定的脚本宏定义。
        /// </summary>
        /// <param name="buildTarget">要移除脚本宏定义的平台。</param>
        /// <param name="scriptingDefineSymbol">要移除的脚本宏定义。</param>
        public static void RemoveScriptingDefineSymbol(NamedBuildTarget buildTarget, string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
            {
                return;
            }

            if (!HasScriptingDefineSymbol(buildTarget, scriptingDefineSymbol))
            {
                return;
            }

            List<string> scriptingDefineSymbols = new List<string>(GetScriptingDefineSymbols(buildTarget));
            while (scriptingDefineSymbols.Contains(scriptingDefineSymbol))
            {
                scriptingDefineSymbols.Remove(scriptingDefineSymbol);
            }

            SetScriptingDefineSymbols(buildTarget, scriptingDefineSymbols.ToArray());
        }

        /// <summary>
        /// 为所有平台增加指定的脚本宏定义。
        /// </summary>
        /// <param name="scriptingDefineSymbol">要增加的脚本宏定义。</param>
        public static void AddScriptingDefineSymbol(string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
            {
                return;
            }

            foreach (NamedBuildTarget buildTarget in NamedBuildTargets)
            {
                AddScriptingDefineSymbol(buildTarget, scriptingDefineSymbol);
            }
        }

        /// <summary>
        /// 为所有平台移除指定的脚本宏定义。
        /// </summary>
        /// <param name="scriptingDefineSymbol">要移除的脚本宏定义。</param>
        public static void RemoveScriptingDefineSymbol(string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
            {
                return;
            }

            foreach (NamedBuildTarget buildTarget in NamedBuildTargets)
            {
                RemoveScriptingDefineSymbol(buildTarget, scriptingDefineSymbol);
            }
        }

        /// <summary>
        /// 获取指定平台的脚本宏定义。
        /// </summary>
        /// <param name="buildTarget">要获取脚本宏定义的平台。</param>
        /// <returns>平台的脚本宏定义。</returns>
        public static string[] GetScriptingDefineSymbols(NamedBuildTarget buildTarget)
        {
            return PlayerSettings.GetScriptingDefineSymbols(buildTarget).Split(';');
        }

        /// <summary>
        /// 设置指定平台的脚本宏定义。
        /// </summary>
        /// <param name="buildTarget">要设置脚本宏定义的平台。</param>
        /// <param name="scriptingDefineSymbols">要设置的脚本宏定义。</param>
        public static void SetScriptingDefineSymbols(NamedBuildTarget buildTarget, string[] scriptingDefineSymbols)
        {
            PlayerSettings.SetScriptingDefineSymbols(buildTarget, string.Join(";", scriptingDefineSymbols));
        }
    }
}