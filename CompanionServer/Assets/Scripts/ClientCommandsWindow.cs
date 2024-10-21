using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClientCommandsWindow : EditorWindow
{
    private static bool isClientConnected = false;

    [MenuItem("Window/Client Commands")]
    public static void ShowWindow()
    {
        GetWindow<ClientCommandsWindow>("Client Commands");
    }

    public static void NotifyClientConnected()
    {
        isClientConnected = true;
        ShowWindow();
    }

    private void OnGUI()
    {
        GUILayout.Label("Client Commands", EditorStyles.boldLabel);

        if (!isClientConnected)
        {
            GUILayout.Label("No client connected.", EditorStyles.helpBox);
            return;
        }

       
        if (GUILayout.Button("Scripting Commands"))
        {
            ScriptingCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Hierarchy Commands"))
        {
            HierarchyCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("QA Commands"))
        {
            QACommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Project Settings Commands"))
        {
            ProjectSettingsCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Assets Commands"))
        {
            AssetsCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Scene Commands"))
        {
            SceneCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Prefab Commands"))
        {
            PrefabCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("UI Commands"))
        {
            UICommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Debugging Commands"))
        {
            DebuggingCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Utilities Commands"))
        {
            UtilitiesCommandsWindow.ShowWindow();
        }
        if (GUILayout.Button("Git Commands"))
        {
            GitCommandsWindow.ShowWindow();
        }
    }
}
