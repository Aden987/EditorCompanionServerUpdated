using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebuggingCommandsWindow : EditorWindow
{
    //SetBreakPoint
    private string setBreakPointscriptName = "Script";
    private int setBreakPointlineNumber = 1;

    //RemoveBreakpoint
    private string removeBreakPointscriptName = "Script";
    private int removeBreakPointlineNumber = 1;

    //EvaluateExpression
    private string expression = "Expression";

    //WatchVariable
    private string watchVariableName = "Variable Name";

    //RemoceWatch


    [MenuItem("Window/Debugging Commands")]
    public static void ShowWindow()
    {
        GetWindow<DebuggingCommandsWindow>("Debugging Commands");
    }

    private void OnGUI()
    {
      
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
