using UnityEditor;
using UnityEngine;

public class PrefabCommandsWindow : EditorWindow
{
    private string prefabName = "";
    private string gameObjectName = "";
    private string parentName = "";
    private string resultMessage = "";

    [MenuItem("Window/Prefab Commands")]
    public static void ShowWindow()
    {
        GetWindow<PrefabCommandsWindow>("Prefab Commands");
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Commands", EditorStyles.boldLabel);

        // Create Prefab Section
        GUILayout.Label("Create Prefab", EditorStyles.boldLabel);
        GUILayout.Label("Prefab Name");
        prefabName = EditorGUILayout.TextField(prefabName);
        GUILayout.Label("GameObject Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        if (GUILayout.Button("Create Prefab"))
        {
            SendCommand($"CreatePrefab,{prefabName},{gameObjectName}");
        }

        GUILayout.Space(10);

        // Instantiate Prefab Section
        GUILayout.Label("Instantiate Prefab", EditorStyles.boldLabel);
        GUILayout.Label("Prefab Name");
        prefabName = EditorGUILayout.TextField(prefabName);
        GUILayout.Label("Parent Name");
        parentName = EditorGUILayout.TextField(parentName);

        if (GUILayout.Button("Instantiate Prefab"))
        {
            SendCommand($"InstantiatePrefab,{prefabName},{parentName}");
        }

        GUILayout.Space(10);

        // Update Prefab Section
        GUILayout.Label("Update Prefab", EditorStyles.boldLabel);
        GUILayout.Label("Prefab Name");
        prefabName = EditorGUILayout.TextField(prefabName);
        GUILayout.Label("GameObject Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        if (GUILayout.Button("Update Prefab"))
        {
            SendCommand($"UpdatePrefab,{prefabName},{gameObjectName}");
        }

        GUILayout.Space(10);

        // Break Prefab Instance Section
        GUILayout.Label("Break Prefab Instance", EditorStyles.boldLabel);
        GUILayout.Label("GameObject Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        if (GUILayout.Button("Break Prefab Instance"))
        {
            SendCommand($"BreakPrefabInstance,{gameObjectName}");
        }

        GUILayout.Space(10);

        // Result Message
        GUILayout.Label("Result", EditorStyles.boldLabel);
        GUILayout.Label(resultMessage, EditorStyles.wordWrappedLabel);
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
