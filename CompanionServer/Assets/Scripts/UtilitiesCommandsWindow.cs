using UnityEngine;
using UnityEditor;


public class UtilitiesCommandsWindow : EditorWindow
{
    private string screenshotFileName = "screenshot.png";
    private string videoDuration = "5";
    private string videoFileName = "video.mp4";

    [MenuItem("Window/Utilities Commands")]
    public static void ShowWindow()
    {
        GetWindow<UtilitiesCommandsWindow>("Utilities Commands");
    }

    private void OnGUI()
    {
        // Screenshot Section
        GUILayout.Label("Screenshot", EditorStyles.boldLabel);
        GUILayout.Label("File Name");
        screenshotFileName = EditorGUILayout.TextField(screenshotFileName);

        if (GUILayout.Button("Take Screenshot"))
        {
            SendCommand("TakeScreenshot," + screenshotFileName);
        }

        // Video Recording Section
        GUILayout.Space(10); // Add some space between sections
        GUILayout.Label("Record Video", EditorStyles.boldLabel);
        GUILayout.Label("Duration");
        videoDuration = EditorGUILayout.TextField(videoDuration);

        GUILayout.Label("Video Name");
        videoFileName = EditorGUILayout.TextField(videoFileName);

        if (GUILayout.Button("Record Video"))
        {
            SendCommand("RecordVideo," + videoDuration + "," + videoFileName);
        }
    }

    private void SendCommand(string command)
    {
        if (EditorCompanionServer.Instance != null)
        {
            EditorCompanionServer.Instance.SendCommandToClient(command);
        }
        else
        {
            Debug.LogError("EditorCompanionServer instance is not available.");
        }
    }
}