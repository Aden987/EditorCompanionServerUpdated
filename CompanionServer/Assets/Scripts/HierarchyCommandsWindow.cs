using UnityEditor;
using UnityEngine;

public class HierarchyCommandsWindow : EditorWindow
{
    private string gameObjectName = "";
    private string parentName = "";
    private string componentName = "";
    private string propertyName = "";
    private string propertyValue = "";
    private string resultMessage = "";
    string oldName = "";
    string newName = "";
    private Vector2 scrollPosition;

    [MenuItem("Window/Hierarchy Commands")]
    public static void ShowWindow()
    {
        GetWindow<HierarchyCommandsWindow>("Hierarchy Commands");
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("Hierarchy Commands", EditorStyles.boldLabel);

        // Inspect Hierarchy Section
        if (GUILayout.Button("Inspect Hierarchy"))
        {
            SendCommand("InspectHierarchy");
        }

        GUILayout.Space(10);

        // Inspect Components Section
        GUILayout.Label("Inspect Components", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        if (GUILayout.Button("Inspect Components"))
        {
            SendCommand($"InspectComponents,{gameObjectName}");
        }

        GUILayout.Space(10);

        // Inspect Component Settings Section
        GUILayout.Label("Inspect Component Settings", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        GUILayout.Label("Component Name");
        componentName = EditorGUILayout.TextField(componentName);

        if (GUILayout.Button("Inspect Component Settings"))
        {
            SendCommand($"InspectComponentSettings,{gameObjectName},{componentName}");
        }

        GUILayout.Space(10);

        // Create GameObject Section
        GUILayout.Label("Create GameObject", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        GUILayout.Label("Parent Name (Optional)");
        parentName = EditorGUILayout.TextField(parentName);

        if (GUILayout.Button("Create GameObject"))
        {
            SendCommand($"CreateGameObject,{gameObjectName},{parentName}");
        }

        GUILayout.Space(10);

        // Delete GameObject Section
        GUILayout.Label("Delete GameObject", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        if (GUILayout.Button("Delete GameObject"))
        {
            SendCommand($"DeleteGameObject,{gameObjectName}");
        }

        GUILayout.Space(10);

        // Rename GameObject Section
        GUILayout.Label("Rename GameObject", EditorStyles.boldLabel);
        GUILayout.Label("Old Name");
        oldName = EditorGUILayout.TextField(oldName);

        GUILayout.Label("New Name");
        newName = EditorGUILayout.TextField(newName);

        if (GUILayout.Button("Rename GameObject"))
        {
            if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName))
            {
                SendCommand($"RenameGameObject,{oldName},{newName}");
            }
            else
            {
                Debug.LogWarning("Both old and new names must be provided.");
            }
        }

        GUILayout.Space(10);

        // Add Component Section
        GUILayout.Label("Add Component", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        GUILayout.Label("Component Name");
        componentName = EditorGUILayout.TextField(componentName);

        if (GUILayout.Button("Add Component"))
        {
            SendCommand($"AddComponent,{gameObjectName},{componentName}");
        }

        GUILayout.Space(10);

        // Remove Component Section
        GUILayout.Label("Remove Component", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        GUILayout.Label("Component Name");
        componentName = EditorGUILayout.TextField(componentName);

        if (GUILayout.Button("Remove Component"))
        {
            SendCommand($"RemoveComponent,{gameObjectName},{componentName}");
        }

        GUILayout.Space(10);

        // Set Component Property Section
        GUILayout.Label("Set Component Property", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        GUILayout.Label("Component Name");
        componentName = EditorGUILayout.TextField(componentName);

        GUILayout.Label("Property Name");
        propertyName = EditorGUILayout.TextField(propertyName);

        GUILayout.Label("Property Value");
        propertyValue = EditorGUILayout.TextField(propertyValue);

        if (GUILayout.Button("Set Component Property"))
        {
            SendCommand($"SetComponentProperty,{gameObjectName},{componentName},{propertyName},{propertyValue}");
        }

        GUILayout.Space(10);

        // Get Component Property Section
        GUILayout.Label("Get Component Property", EditorStyles.boldLabel);
        GUILayout.Label("Game Object Name");
        gameObjectName = EditorGUILayout.TextField(gameObjectName);

        GUILayout.Label("Component Name");
        componentName = EditorGUILayout.TextField(componentName);

        GUILayout.Label("Property Name");
        propertyName = EditorGUILayout.TextField(propertyName);

        if (GUILayout.Button("Get Component Property"))
        {
            SendCommand($"GetComponentProperty,{gameObjectName},{componentName},{propertyName}");
        }

        GUILayout.Space(10);

        // Result Message
        GUILayout.Label("Result", EditorStyles.boldLabel);
        GUILayout.Label(resultMessage, EditorStyles.wordWrappedLabel);

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
