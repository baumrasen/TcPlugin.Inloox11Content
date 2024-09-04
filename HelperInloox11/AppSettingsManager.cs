using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace TcPlugins.HelperInloox11
{

    public static class AppSettingsManager
    {

        private static Boolean showDebug = false;

        // save the settings to the registry

        private static RegistryKey GetAppSettingsKey()
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TcPluginsInloox11Content", RegistryKeyPermissionCheck.ReadWriteSubTree);
            return registryKey;
        }

        public static void SetValueString(string name, string value)
        {
            if (showDebug)
                Debug.WriteLine(String.Format(" ############################# AppSettingsManager / SetValueString {0}", name));

            try
            {
                GetAppSettingsKey().SetValue(name, value, RegistryValueKind.String);
            }
            catch (Exception)
            {
            }
        }

        public static string GetValueString(string name, string defaultV)
        {
            if (showDebug)
                Debug.WriteLine(String.Format(" ############################# AppSettingsManager / GetValueString {0}", name)); 

            string r1 = null;
            try
            {
                r1 = (string)GetAppSettingsKey().GetValue(name, null);
            }
            catch (Exception)
            {
            }

            if (r1 == null)
            {
                SetValueString(name, defaultV);
                return defaultV;
            }
            return r1;
        }

        public static void SetValueInt32(string name, int value)
        {
            if (showDebug)
                Debug.WriteLine(String.Format(" ############################# AppSettingsManager / SetValueInt32 {0}", name));

            try
            {
                GetAppSettingsKey().SetValue(name, value, RegistryValueKind.DWord);
            }
            catch (Exception)
            {
            }
        }

        public static int GetValueInt32(string name, int defaultV)
        {
            if (showDebug)
                Debug.WriteLine(String.Format(" ############################# AppSettingsManager / GetValueInt32 {0}", name));

            int r1 = defaultV;
            try
            {
                var k = GetAppSettingsKey();
                r1 = (int)(k.GetValue(name, defaultV));
            }
            catch (Exception)
            {
            }

            if (r1 == defaultV)
            {
                SetValueInt32(name, defaultV);
                return defaultV;
            }
            return r1;

        }

        public static void SetValueBool(string name, bool value)
        {
            if (showDebug)
                Debug.WriteLine(String.Format(" ############################# AppSettingsManager / SetValueBool {0}", name));

            SetValueInt32(name, value ? 1 : 0);
        }

        public static bool GetValueBool(string name, bool defaultV)
        {
            if (showDebug)
                Debug.WriteLine(String.Format(" ############################# AppSettingsManager / GetValueBool {0}", name));

            return (GetValueInt32(name, (defaultV ? 1 : 0))) == 1;
        }
    }
}
