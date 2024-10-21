using UnityEditor;
using UnityEngine;

public class ScriptingCommandsWindow : EditorWindow
{
    private string scriptName = "";
    private string newScriptName = "";
    private string scriptContent = "";
    private string resultMessage = "";
    private string lineNumber = "";

    [MenuItem("Window/Scripting Commands")]
    public static void ShowWindow()
    {
        GetWindow<ScriptingCommandsWindow>("Scripting Commands");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scripting Commands", EditorStyles.boldLabel);

        // Read Script Content Section
        GUILayout.Label("Read Script Content", EditorStyles.boldLabel);
        GUILayout.Label("Script Name");
        scriptName = EditorGUILayout.TextField(scriptName);

        if (GUILayout.Button("Read Script Content"))
        {
            SendCommand($"ReadScriptContent,{scriptName}");
        }

        GUILayout.Space(10);

        // Edit Script Section
        GUILayout.Label("Edit Script", EditorStyles.boldLabel);
        GUILayout.Label("Script Name");
        scriptName = EditorGUILayout.TextField(scriptName);

        GUILayout.Label("Line Number");
        lineNumber = EditorGUILayout.TextArea(lineNumber);

        GUILayout.Label("New Script Content");
        scriptContent = EditorGUILayout.TextArea(scriptContent);

        if (GUILayout.Button("Edit Script"))
        {
            SendCommand($"EditScript,{scriptName}|{lineNumber}|{scriptContent}");
        }

        GUILayout.Space(10);

        // Add Script Section
        GUILayout.Label("Add Script", EditorStyles.boldLabel);
        GUILayout.Label("Script Name");
        scriptName = EditorGUILayout.TextField(scriptName);

        if (GUILayout.Button("Add Script"))
        {
            SendCommand($"AddScript,{scriptName}");
        }

        GUILayout.Space(10);

        // Delete Script Section
        GUILayout.Label("Delete Script", EditorStyles.boldLabel);
        GUILayout.Label("Script Name");
        scriptName = EditorGUILayout.TextField(scriptName);

        if (GUILayout.Button("Delete Script"))
        {
            SendCommand($"DeleteScript,{scriptName}");
        }

        GUILayout.Space(10);

        // Rename Script Section
        GUILayout.Label("Rename Script", EditorStyles.boldLabel);
        GUILayout.Label("Old Script Name");
        scriptName = EditorGUILayout.TextField(scriptName);

        GUILayout.Label("New Script Name");
        newScriptName = EditorGUILayout.TextField(newScriptName);

        if (GUILayout.Button("Rename Script"))
        {
            SendCommand($"RenameScript,{scriptName}|{newScriptName}");
        }

        GUILayout.Space(10);

        // List Scripts Section
        if (GUILayout.Button("List Scripts"))
        {
            SendCommand("ListScripts");
        }

        GUILayout.Space(10);

        // Compile Scripts Section
        if (GUILayout.Button("Compile Scripts"))
        {
            SendCommand("CompileScripts");
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
