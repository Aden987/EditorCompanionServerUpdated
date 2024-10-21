using UnityEditor;
using UnityEngine;

public class UICommandsWindow : EditorWindow
{
    private string elementType = "";
    private string parentName = "";
    private string elementName = "";
    private string propertyName = "";
    private string propertyValue = "";
    private string resultMessage = "";

    [MenuItem("Window/UI Commands")]
    public static void ShowWindow()
    {
        GetWindow<UICommandsWindow>("UI Commands");
    }

    private void OnGUI()
    {
        GUILayout.Label("UI Commands", EditorStyles.boldLabel);

        // Create UI Element Section
        GUILayout.Label("Create UI Element", EditorStyles.boldLabel);
        GUILayout.Label("Element Type");
        elementType = EditorGUILayout.TextField(elementType);
        GUILayout.Label("Parent Name");
        parentName = EditorGUILayout.TextField(parentName);

        if (GUILayout.Button("Create UI Element"))
        {
            SendCommand($"CreateUIElement,{elementType},{parentName}");
        }

        GUILayout.Space(10);

        // Set UI Element Property Section
        GUILayout.Label("Set UI Element Property", EditorStyles.boldLabel);
        GUILayout.Label("Element Name");
        elementName = EditorGUILayout.TextField(elementName);
        GUILayout.Label("Property Name");
        propertyName = EditorGUILayout.TextField(propertyName);
        GUILayout.Label("Property Value");
        propertyValue = EditorGUILayout.TextField(propertyValue);

        if (GUILayout.Button("Set UI Element Property"))
        {
            SendCommand($"SetUIElementProperty,{elementName},{propertyName},{propertyValue}");
        }

        GUILayout.Space(10);

        // Get UI Element Property Section
        GUILayout.Label("Get UI Element Property", EditorStyles.boldLabel);
        GUILayout.Label("Element Name");
        elementName = EditorGUILayout.TextField(elementName);
        GUILayout.Label("Property Name");
        propertyName = EditorGUILayout.TextField(propertyName);

        if (GUILayout.Button("Get UI Element Property"))
        {
            SendCommand($"GetUIElementProperty,{elementName},{propertyName}");
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
