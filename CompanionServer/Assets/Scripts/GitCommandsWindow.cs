using UnityEditor;
using UnityEngine;

public class GitCommandsWindow : EditorWindow
{
    private string commitMessage = "";
    private string branchName = "";
    private string mergeBranch = "";
    private string resultMessage = "";
    private Vector2 scrollPosition;

    [MenuItem("Window/Git Commands")]
    public static void ShowWindow()
    {
        GetWindow<GitCommandsWindow>("Git Commands");
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("Git Commands", EditorStyles.boldLabel);

        // Check Git Repository Connection
        if (GUILayout.Button("Check Git Repository Connection"))
        {
            SendCommand("CheckGitRepositoryConnection");
        }

        GUILayout.Space(10);

        // Commit Section
        GUILayout.Label("Git Commit", EditorStyles.boldLabel);
        GUILayout.Label("Commit Message");
        commitMessage = EditorGUILayout.TextField(commitMessage);

        if (GUILayout.Button("Commit"))
        {
            SendCommand($"GitCommit,{commitMessage}");
        }

        GUILayout.Space(10);

        // Push Section
        if (GUILayout.Button("Push Commit"))
        {
            SendCommand("GitPush");
        }

        GUILayout.Space(10);

        // Pull Section
        if (GUILayout.Button("Pull Commit"))
        {
            SendCommand("GitPull");
        }

        GUILayout.Space(10);

        // Fetch Section
        if (GUILayout.Button("Fetch"))
        {
            SendCommand("GitFetch");
        }

        GUILayout.Space(10);

        // Branch List Section
        if (GUILayout.Button("List Branches"))
        {
            SendCommand("GitBranchList");
        }

        GUILayout.Space(10);

        // Checkout Branch Section
        GUILayout.Label("Checkout Branch", EditorStyles.boldLabel);
        GUILayout.Label("Branch Name");
        branchName = EditorGUILayout.TextField(branchName);

        if (GUILayout.Button("Checkout Branch"))
        {
            SendCommand($"GitCheckoutBranch,{branchName}");
        }

        GUILayout.Space(10);

        // Merge Branch Section
        GUILayout.Label("Merge Branch", EditorStyles.boldLabel);
        GUILayout.Label("Branch Name to Merge");
        branchName = EditorGUILayout.TextField(branchName);

        GUILayout.Label("Merge Target Branch Name");
        mergeBranch = EditorGUILayout.TextField(mergeBranch);

        if (GUILayout.Button("Merge Branch"))
        {
            SendCommand($"GitMergeBranch,{branchName},{mergeBranch}");
        }

        GUILayout.Space(10);

        // Create Branch Section
        GUILayout.Label("Create Branch", EditorStyles.boldLabel);
        GUILayout.Label("New Branch Name");
        branchName = EditorGUILayout.TextField(branchName);

        if (GUILayout.Button("Create Branch"))
        {
            SendCommand($"GitCreateBranch,{branchName}");
        }

        GUILayout.Space(10);

        // Result Message
        GUILayout.Label("Result", EditorStyles.boldLabel);
        GUILayout.Label(resultMessage, EditorStyles.wordWrappedLabel);

        EditorGUILayout.EndScrollView();
    }

    private void SendCommand(string command)
    {
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
