// Sample application that demonstrates a simple shell context menu.
// Ralph Arvesen (www.vertigo.com / www.lostsprings.com)

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace log4jDigger
{
    /// <summary>
    /// Register and unregister simple shell context menus.
    /// </summary>
    public static class FileShellExtension
    {
        static string FileType = "*";
        static string KeyName = "LogViewContextMenu";
        static string MenuText = "Open with log4jDigger";

        public static void Register()
        {
            string menuCommand = string.Format("\"{0}\" W \"%1\"", Application.ExecutablePath);
            FileShellExtension.Register(FileType, KeyName, MenuText, menuCommand);
        }

        public static void UnRegister()
        {
            FileShellExtension.Unregister(FileType, KeyName);
        }

        public static bool IsRegistered()
        {
            return FileShellExtension.IsRegistered(FileType, KeyName);
        }

        public static void Register(
            string fileType, string shellKeyName,
            string menuText, string menuCommand)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileType) &&
                !string.IsNullOrEmpty(shellKeyName) &&
                !string.IsNullOrEmpty(menuText) &&
                !string.IsNullOrEmpty(menuCommand));

            string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(regPath))
            {
                key.SetValue(null, menuText);
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                string.Format(@"{0}\command", regPath)))
            {
                key.SetValue(null, menuCommand);
            }
        }


        public static void Unregister(string fileType, string shellKeyName)
        {
            if (!IsRegistered(fileType, shellKeyName))
                return;

            Debug.Assert(!string.IsNullOrEmpty(fileType) &&
                !string.IsNullOrEmpty(shellKeyName));

            string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);
            Registry.ClassesRoot.DeleteSubKeyTree(regPath);
        }

        public static bool IsRegistered(string fileType, string shellKeyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileType) &&
                !string.IsNullOrEmpty(shellKeyName));

            string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);

            return Registry.ClassesRoot.OpenSubKey(regPath) != null;
        }
    }

}
