using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QACommandsWindow : EditorWindow
{
    [MenuItem("Window/QA Commands")]
    public static void ShowWindow()
    {
        GetWindow<QACommandsWindow>("QA Commands");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Enter Playmode"))
        {
            SendCommand("EnterPlaymode");
        }

        if (GUILayout.Button("Return Console Log Line"))
        {
            SendCommand("ReturnConsoleLogLine");
        }

        if (GUILayout.Button("Read First Error In Log"))
        {
            SendCommand("ReadFirstErrorInLog");
        }

        if (GUILayout.Button("Clear Console Log"))
        {
            SendCommand("ClearConsoleLog");
        }

        if (GUILayout.Button("Set PauseMode"))
        {
            SendCommand("SetPauseMode");
        }

        if (GUILayout.Button("Set StepMode"))
        {
            SendCommand("SetStepMode");
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
