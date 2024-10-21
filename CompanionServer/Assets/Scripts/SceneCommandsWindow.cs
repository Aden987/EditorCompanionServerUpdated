using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneCommandsWindow : EditorWindow
{
    private string sceneName = "";
    private string resultMessage = "";

    [MenuItem("Window/Scene Commands")]
    public static void ShowWindow()
    {
        GetWindow<SceneCommandsWindow>("Scene Commands");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scene Commands", EditorStyles.boldLabel);

        // Open Scene Section
        GUILayout.Label("Open Scene", EditorStyles.boldLabel);
        GUILayout.Label("Scene Name");
        sceneName = EditorGUILayout.TextField(sceneName);

        if (GUILayout.Button("Open Scene"))
        {
            SendCommand($"OpenScene|{sceneName}");
        }

        GUILayout.Space(10);

        // Close Scene Section
        GUILayout.Label("Close Scene", EditorStyles.boldLabel);
        GUILayout.Label("Scene Name");
        sceneName = EditorGUILayout.TextField(sceneName);

        if (GUILayout.Button("Close Scene"))
        {
            SendCommand($"CloseScene|{sceneName}");
        }

        GUILayout.Space(10);

        // Save Scene Section
        GUILayout.Label("Save Scene", EditorStyles.boldLabel);
        GUILayout.Label("Scene Name");
        sceneName = EditorGUILayout.TextField(sceneName);

        if (GUILayout.Button("Save Scene"))
        {
            SendCommand($"SaveScene|{sceneName}");
        }

        GUILayout.Space(10);

        // Add Scene Section
        GUILayout.Label("Add Scene", EditorStyles.boldLabel);
        GUILayout.Label("Scene Name");
        sceneName = EditorGUILayout.TextField(sceneName);

        if (GUILayout.Button("Add Scene"))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SendCommand($"AddScene,{sceneName}");
            }
            else
            {
                Debug.LogWarning("Scene name is empty.");
            }
        }

        GUILayout.Space(10);

        // Remove Scene Section
        GUILayout.Label("Remove Scene", EditorStyles.boldLabel);
        GUILayout.Label("Scene Name");
        sceneName = EditorGUILayout.TextField(sceneName);

        if (GUILayout.Button("Remove Scene"))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SendCommand($"RemoveScene,{sceneName}");
            }
            else
            {
                Debug.LogWarning("Scene name is empty.");
            }
        }

        GUILayout.Space(10);

        // Result Message
        GUILayout.Label("Result", EditorStyles.boldLabel);
        GUILayout.Label(resultMessage, EditorStyles.wordWrappedLabel);
    }

    private void SendCommand(string command)
    {
        // Replace | with , to match the expected separator in HandleCommand
        command = command.Replace('|', ',');

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
