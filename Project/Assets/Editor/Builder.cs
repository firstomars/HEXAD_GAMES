using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Threading;

public class Builder
{
    public static void BuildWindows()
    {
        string assetFolderPath = Application.dataPath;
        string pcFileName = assetFolderPath + $"/../Builds/Windows/{Application.productName}.exe";

        var scenes = EditorBuildSettings.scenes;
        var levels = scenes.Select(z => z.path).ToArray();

        Debug.Log("Starting Windows Build");
        BuildPipeline.BuildPlayer(levels, pcFileName, BuildTarget.StandaloneWindows, BuildOptions.None);
        Debug.Log("Finished Windows Build");

        Thread.Sleep(2000);
    }

    public static void BuildWeb()
    {
        string assetFolderPath = Application.dataPath;
        string webFileName = assetFolderPath + "/../Builds/Web/";

        var scenes = EditorBuildSettings.scenes;
        var levels = scenes.Select(z => z.path).ToArray();

        Debug.Log("Starting WebGL Build");
        BuildPipeline.BuildPlayer(levels, webFileName, BuildTarget.WebGL, BuildOptions.None);
        Debug.Log("Finished WebGL Build");

        Thread.Sleep(2000);
    }

    public static void BuildAndroid()
    {
        string assetFolderPath = Application.dataPath;
        string apkFileName = assetFolderPath + $"/../Builds/Android/{Application.productName}.apk";

        var scenes = EditorBuildSettings.scenes;
        var levels = scenes.Select(z => z.path).ToArray();

        Debug.Log("Starting Android Build");
        BuildPipeline.BuildPlayer(levels, apkFileName, BuildTarget.Android, BuildOptions.None);
        Debug.Log("Finished Android Build");

        Thread.Sleep(2000);
    }
}
