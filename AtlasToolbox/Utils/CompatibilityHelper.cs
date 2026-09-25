using System.Configuration;
using Microsoft.Win32;
using System;

namespace AtlasToolbox.Utils
{
    public class CompatibilityHelper
    {
        /// <summary>
        /// Check compatibility with app's version.
        /// Scans all applied AME playbooks (not just the standard Atlas GUID)
        /// to support forks with different UniqueIds (e.g. LTSC forks).
        /// Returns true if any applied playbook version matches a compatible version,
        /// or if the AtlasOS registry key exists (indicating Atlas is installed).
        /// </summary>
        /// <returns></returns>
        public static bool IsCompatible()
        {
            string[] compatibleVersions = ConfigurationManager.AppSettings.Get("AtlasVersion").Split(',');

            // First, try the standard Atlas playbook GUID
            string atlasVersion = (string)RegistryHelper.GetValue("HKLM\\SOFTWARE\\AME\\Playbooks\\Applied\\{00000000-0000-4000-6174-6C6173203A33}", "version");
            if (!string.IsNullOrEmpty(atlasVersion))
            {
                foreach (string version in compatibleVersions)
                {
                    if (atlasVersion.Trim() == version.Trim()) return true;
                }
                // Standard GUID found but version didn't match - still check other playbooks
            }

            // Scan all applied playbooks (supports forks with custom UniqueIds like LTSC forks)
            try
            {
                using RegistryKey appliedKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\AME\Playbooks\Applied");
                if (appliedKey != null)
                {
                    foreach (string subKeyName in appliedKey.GetSubKeyNames())
                    {
                        using RegistryKey playbookKey = appliedKey.OpenSubKey(subKeyName);
                        if (playbookKey != null)
                        {
                            string version = playbookKey.GetValue("version") as string;
                            if (!string.IsNullOrEmpty(version))
                            {
                                foreach (string compatVersion in compatibleVersions)
                                {
                                    if (version.Trim() == compatVersion.Trim()) return true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Registry access failed - fall through to AtlasOS key check
            }

            // Fallback: if AtlasOS registry key exists, the system has Atlas installed
            // (covers cases where playbook version registry wasn't written, e.g. older installs)
            if (RegistryHelper.KeyExists(@"HKLM\SOFTWARE\AtlasOS"))
            {
                return true;
            }

            return false;
        }
    }
}
