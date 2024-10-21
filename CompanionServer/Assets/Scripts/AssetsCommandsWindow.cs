using UnityEditor;
using UnityEngine;

public class AssetsCommandsWindow : EditorWindow
{
    private string assetPath = "";
    private string newAssetPath = "";
    private string propertyName = "";
    private string propertyValue = "";
    private string resultMessage = "";

    private Vector2 scrollPosition; // For handling the scrollbar position

    [MenuItem("Window/Assets Commands")]
    public static void ShowWindow()
    {
        GetWindow<AssetsCommandsWindow>("Assets Commands");
    }

    private void OnGUI()
    {
        // Start ScrollView
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));

        GUILayout.Label("Assets Commands", EditorStyles.boldLabel);

        // Import Asset Section
        GUILayout.Label("Import Asset", EditorStyles.boldLabel);
        GUILayout.Label("Asset Path");
        assetPath = EditorGUILayout.TextField(assetPath);

        if (GUILayout.Button("Import Asset"))
        {
            SendCommand($"ImportAsset|{assetPath}");
        }

        GUILayout.Space(10);

        // Delete Asset Section
        GUILayout.Label("Delete Asset", EditorStyles.boldLabel);
        GUILayout.Label("Asset Name");
        assetPath = EditorGUILayout.TextField(assetPath);

        if (GUILayout.Button("Delete Asset"))
        {
            SendCommand($"DeleteAsset|{assetPath}");
        }

        GUILayout.Space(10);

        // Rename Asset Section
        GUILayout.Label("Rename Asset", EditorStyles.boldLabel);
        GUILayout.Label("Current Asset Name");
        assetPath = EditorGUILayout.TextField(assetPath);

        GUILayout.Label("New Asset Name");
        newAssetPath = EditorGUILayout.TextField(newAssetPath);

        if (GUILayout.Button("Rename Asset"))
        {
            SendCommand($"RenameAsset|{assetPath}|{newAssetPath}");
        }

        GUILayout.Space(10);

        // Move Asset Section
        GUILayout.Label("Move Asset", EditorStyles.boldLabel);
        GUILayout.Label("Asset Name");
        assetPath = EditorGUILayout.TextField(assetPath);

        GUILayout.Label("Move to Folder:");
        newAssetPath = EditorGUILayout.TextField(newAssetPath);

        if (GUILayout.Button("Move Asset"))
        {
            SendCommand($"MoveAsset|{assetPath}|{newAssetPath}");
        }

        GUILayout.Space(10);

        // Get Asset Properties Section
        GUILayout.Label("Get Asset Properties", EditorStyles.boldLabel);
        GUILayout.Label("Asset Name");
        assetPath = EditorGUILayout.TextField(assetPath);

        if (GUILayout.Button("Get Asset Properties"))
        {
            SendCommand($"GetAssetProperties|{assetPath}");
        }

        GUILayout.Space(10);

        // Set Asset Property Section
        GUILayout.Label("Set Asset Property", EditorStyles.boldLabel);
        GUILayout.Label("Asset Name");
        assetPath = EditorGUILayout.TextField(assetPath);

        GUILayout.Label("Property");
        propertyName = EditorGUILayout.TextField(propertyName);

        GUILayout.Label("Property Value");
        propertyValue = EditorGUILayout.TextField(propertyValue);

        if (GUILayout.Button("Set Asset Property"))
        {
            SendCommand($"SetAssetProperty|{assetPath}|{propertyName}|{propertyValue}");
        }

        GUILayout.Space(10);

        // Result Message
        GUILayout.Label("Result", EditorStyles.boldLabel);
        GUILayout.Label(resultMessage, EditorStyles.wordWrappedLabel);

        // End ScrollView
        EditorGUILayout.EndScrollView();
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
