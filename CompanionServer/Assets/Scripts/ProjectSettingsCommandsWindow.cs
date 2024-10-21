using UnityEditor;
using UnityEngine;

public class ProjectSettingsCommandsWindow : EditorWindow
{
    private string settingName = "";
    private string settingValue = "";
    private string resultMessage = "";

    [MenuItem("Window/Project Settings Commands")]
    public static void ShowWindow()
    {
        GetWindow<ProjectSettingsCommandsWindow>("Project Settings Commands");
    }

    private void OnGUI()
    {
        GUILayout.Label("Project Settings Commands", EditorStyles.boldLabel);

        // Get Project Setting Section
        GUILayout.Label("Get Project Setting", EditorStyles.boldLabel);
        GUILayout.Label("Setting Name");
        settingName = EditorGUILayout.TextField(settingName);

        if (GUILayout.Button("Get Setting"))
        {
            GetProjectSetting(settingName);
        }

        GUILayout.Space(10);

        // Set Project Setting Section
        GUILayout.Label("Set Project Setting", EditorStyles.boldLabel);
        GUILayout.Label("Setting Name");
        settingName = EditorGUILayout.TextField(settingName);

        GUILayout.Label("Setting Value");
        settingValue = EditorGUILayout.TextField(settingValue);

        if (GUILayout.Button("Set Setting"))
        {
            SetProjectSetting(settingName, settingValue);
        }

        GUILayout.Space(10);

        // List All Project Settings Section
        GUILayout.Label("List All Project Settings", EditorStyles.boldLabel);
        if (GUILayout.Button("List All Settings"))
        {
            ListAllProjectSettings();
        }

        GUILayout.Space(10);

        // Result Message
        GUILayout.Label("Result", EditorStyles.boldLabel);
        GUILayout.Label(resultMessage, EditorStyles.wordWrappedLabel);
    }

    private void GetProjectSetting(string settingName)
    {
        // Call command on client to get project setting
        var command = $"GetProjectSetting,{settingName}";
        SendCommand(command);
    }

    private void SetProjectSetting(string settingName, string settingValue)
    {
        // Call command on client to set project setting
        var command = $"SetProjectSetting,{settingName},{settingValue}";
        SendCommand(command);
    }

    private void ListAllProjectSettings()
    {
        // Call command on client to list all project settings
        var command = "ListAllProjectSettings";
        SendCommand(command);
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
