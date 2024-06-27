using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace GameCreator.Editor.Installs
{
    public static class UninstallUniqueGameObjects
    {
        const string UninstallTitle = "Are you sure you want to uninstall Unique Game Objects";

        const string UninstallMsg =
            "** MAKE SURE YOU HAVE A BACKUP **"
            + "\n\rThis operation cannot be undone. This will not remove reference to Instance Guid components and the components themselves.";

        [MenuItem("Game Creator/Uninstall/Unique Game Objects", false, UninstallManager.PRIORITY)]
        static void Uninstall()
        {
            if (PackageInfo.GetAllRegisteredPackages().Any(x => x.name == "com.legi.unique_game_objects"))
            {
                if (EditorUtility.DisplayDialog(UninstallTitle, UninstallMsg, "Yes", "Cancel"))
                {
                    RemovePackage();
                }
            }
            else
            {
                UninstallManager.EventBeforeUninstall -= WillUninstall;
                UninstallManager.EventBeforeUninstall += WillUninstall;
            }
        }

        static void WillUninstall(string name)
        {
            UninstallManager.EventBeforeUninstall -= WillUninstall;
        }

        static void RemovePackage()
        {
            Client.Remove("com.legi.unique_game_objects");
        }
    }
}
