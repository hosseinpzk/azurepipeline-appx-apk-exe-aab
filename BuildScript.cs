// Create this folder in your repository in Assets/Editor/ and modify accourding to your app

/////////////////////////

using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.Linq;


public static class BuildScript
{
//////////////////////////////////////////////////////////// SLN
    [MenuItem("Build/Build UWP")]
    public static void BuildUWP()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        string buildFolder = @"D:\agent\_work\1\a\UWPBuild";
        string slnFolder = Path.Combine(buildFolder, "YOURAPPNAME"); ###accordin to yaml file sln section

        if (!Directory.Exists(slnFolder))
        {
            Directory.CreateDirectory(slnFolder);
        }

        if (scenes.Length == 0)
        {
            Debug.LogError("❌ No scenes found in Build Settings!");
            return;
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = slnFolder,  
            target = BuildTarget.WSAPlayer,  
            options = BuildOptions.None
        };

        Debug.Log("🚀 Starting UWP Build...");
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("✅ UWP Build Completed Successfully: " + slnFolder);

            
            string slnFile = Path.Combine(slnFolder, "YOURAPP.sln"); ### Accourding to yaml file sln section
            if (File.Exists(slnFile))
            {
                Debug.Log("✅ Solution file created: " + slnFile);
            }
            else
            {
                Debug.LogError("❌ Solution file NOT found! Check Unity settings.");
            }
        }
        else
        {
            Debug.LogError("❌ Build Failed!");
            Debug.LogError($"Errors: {summary.totalErrors}");
            Debug.LogError($"Warnings: {summary.totalWarnings}");
        }
    }
//////////////////////////////////////////////////////// AAB GOOGLEPLAY
    [MenuItem("Build/Build AAB")]
    public static void BuildAAB()
    {
      
        // Configure the keystore for signing the AAB file
        PlayerSettings.Android.useCustomKeystore = true; //IF YOU'R USING DEBUGMODE, CHANGE IT TO FALSE AND COMMENT NEXT 3 LINES
        PlayerSettings.Android.keystoreName = @"C:\PATH-TO-KEY\NAME.keystore";
        PlayerSettings.Android.keystorePass = "KEYSTOREPASS";
        PlayerSettings.Android.keyaliasName = "KEYALIASNAME";
        PlayerSettings.Android.keyaliasPass = "KEYALIASPASS";

      
        EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Public; 

        
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        string buildFolder = @"C:\agent\_work\1\a\";
        string aabName = "APP-NAME-VERSION.aab"; // 
        string fullPath = Path.Combine(buildFolder, aabName);

        
        if (!Directory.Exists(buildFolder))
        {
            Directory.CreateDirectory(buildFolder);
        }

       
        if (scenes.Length == 0)
        {
            Debug.LogError("❌ No scenes found in Build Settings!");
            return;
        }

        
        EditorUserBuildSettings.buildAppBundle = true;

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = fullPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        
        Debug.Log("🚀 Starting AAB Build...");
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

       
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("✅ AAB Build Completed Successfully: " + fullPath);

            
            string desktopAabPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "YOURAPP-NAME-VERSION.aab");
            File.Copy(fullPath, desktopAabPath, true);
            Debug.Log($"✅ AAB copied to: {desktopAabPath}");

          
            string symbolsPath = Path.Combine(buildFolder, "symbols.zip");
            string desktopSymbolsPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "symbols.zip");

            if (File.Exists(symbolsPath))
            {
                File.Copy(symbolsPath, desktopSymbolsPath, true);
                Debug.Log($"✅ Symbols file copied to: {desktopSymbolsPath}");
            }
            else
            {
                Debug.LogWarning("⚠️ Symbols file not found! Maybe Unity didn't generate it.");
            }
        }
        else
        {
            Debug.LogError("❌ Build Failed!");
            Debug.LogError($"Errors: {summary.totalErrors}");
            Debug.LogError($"Warnings: {summary.totalWarnings}");
        }
    }
////////////////////////////////////////////////////////////////////////// APK
    [MenuItem("Build/Build APK")]
    public static void BuildAPK()
    {
        // Configure the keystore for signing the AAB file
        PlayerSettings.Android.useCustomKeystore = true; //IF YOU'R USING DEBUGMODE, CHANGE IT TO FALSE AND COMMENT NEXT 3 LINES
        PlayerSettings.Android.keystoreName = @"YOUR KEYSTORE FILE PATH C:\...";
        PlayerSettings.Android.keystorePass = "KEYSTOREPASS";
        PlayerSettings.Android.keyaliasName = "KEYALIASNAME";
        PlayerSettings.Android.keyaliasPass = "KEYALIASPASS";

        string [] scenes = EditorBuildSettings.scenes 
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();



        
        string buildFolder = @"C:\agent\_work\1\a\";
        string apkName = "YOURAPP-NAME-VERSION.apk"; //YOURAPP-NAME-VERSION
        string fullPath = Path.Combine(buildFolder, apkName);

        // بررسی وجود فولدر خروجی
        if (!Directory.Exists(buildFolder))
        {
            Directory.CreateDirectory(buildFolder);
        }

        
        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogError("❌ No scenes found in Build Settings!");
            return;
        }

        // تنظیمات بیلد
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes, 
            locationPathName = fullPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        
        Debug.Log("🚀 Starting APK Build...");
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

       
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("✅ APK Build Completed Successfully: " + fullPath);
        }
        else
        {
            Debug.LogError("❌ Build Failed!");
            Debug.LogError($"Errors: {summary.totalErrors}");
            Debug.LogError($"Warnings: {summary.totalWarnings}");
        }
    }
//////////////////////////////////////////////////////////////////////////// EXE
    [MenuItem("Build/Build Windows EXE")]
    public static void BuildWindows()
    {
        
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

       
        string buildFolder = @"C:\agent\_work\1\a\WindowsBuild";
        string exeFolder = Path.Combine(buildFolder, "YOURAPP-NAME-VERSION"); // YOURAPP-NAME-VERSION
        string exeName = "YOURAPP-NAME-VERSION.exe";  // YOURAPP-NAME-VERSION
        string fullPath = Path.Combine(exeFolder, exeName);

        
        if (!Directory.Exists(exeFolder))
        {
            Directory.CreateDirectory(exeFolder);
        }

       
        if (scenes.Length == 0)
        {
            Debug.LogError("❌ No scenes found in Build Settings!");
            return;
        }

        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = fullPath,  
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        
        Debug.Log("🚀 Starting Windows EXE Build...");
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("✅ Windows EXE Build Completed Successfully: " + fullPath);
        }
        else
        {
            Debug.LogError("❌ Build Failed!");
            Debug.LogError($"Errors: {summary.totalErrors}");
            Debug.LogError($"Warnings: {summary.totalWarnings}");
        }
    }    

}
