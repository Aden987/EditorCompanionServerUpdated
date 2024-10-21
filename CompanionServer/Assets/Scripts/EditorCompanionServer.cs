using UnityEngine;
using System;
using Fleck;
using System.Threading.Tasks;
using UnityEditor;

public class EditorCompanionServer : MonoBehaviour
{
    private WebSocketServer server;
    private IWebSocketConnection clientSocket;

    public static EditorCompanionServer Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        string connection = "ws://0.0.0.0:8080";
        server = new WebSocketServer(connection);
        server.Start(socket =>
        {
            clientSocket = socket;
            socket.OnOpen = () =>
            {
                Debug.Log("Connection opened");
                socket.Send("ReturnConsoleLogLine,1");
                // Notify Unity editor about the connection
                EditorApplication.delayCall += () => ClientCommandsWindow.NotifyClientConnected();
            };
            socket.OnClose = () =>
            {
                Debug.Log("Connection closed");
                clientSocket = null;
            };
            socket.OnMessage = async (message) =>
            {
                Debug.Log("Server received: " + message);
                await HandleCommand(socket, message);
            };
        });
    }

    public void SendCommandToClient(string command)
    {
        if (clientSocket != null && clientSocket.IsAvailable)
        {
            clientSocket.Send(command);
        }
        else
        {
            Debug.LogError("No client connected or socket is not available.");
        }
    }

    //private async Task HandleCommand(IWebSocketConnection socket, string message)
    //{
    //    var parts = message.Split(new[] { '|' }, 2); // Change split character to '|'
    //    if (parts.Length > 0)
    //    {
    //        string command = parts[0];
    //        string arguments = parts.Length > 1 ? parts[1] : string.Empty;

    //        try
    //        {
    //            switch (command)
    //            {
    //                case "ReturnConsoleLogLine":
    //                    await socket.Send($"ReturnConsoleLogLine|{arguments}");
    //                    break;
    //                case "EnterPlaymode":
    //                    await socket.Send("EnterPlaymode");
    //                    break;
    //                case "ReadScriptContent": // Handle ScriptContent directly
    //                    HandleScriptContent(arguments);
    //                    break;
    //                default:
    //                    Debug.LogWarning("Unknown command received: " + message);
    //                    break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.LogError("Error handling command: " + ex.Message);
    //            await socket.Send("Error: " + ex.Message);
    //        }
    //    }
    //}

    private async Task HandleCommand(IWebSocketConnection socket, string message)
    {
        var parts = message.Split(new[] { '|' }, 2); // Change split character to '|'
        if (parts.Length > 0)
        {
            string command = parts[0];
            string arguments = parts.Length > 1 ? parts[1] : string.Empty;

            try
            {
                switch (command)
                {
                    case "ReturnConsoleLogLine":
                        await socket.Send($"ReturnConsoleLogLine|{arguments}");
                        break;
                    case "EnterPlaymode":
                        await socket.Send("EnterPlaymode");
                        break;
                    case "ReadScriptContent": // Handle ScriptContent directly
                        HandleScriptContent(arguments);
                        break;
                    case "ScreenshotTaken":
                        Debug.Log("Screenshot taken command received from client.");
                        break;
                    default:
                        Debug.LogWarning("Unknown command received: " + message);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling command: " + ex.Message);
                await socket.Send("Error: " + ex.Message);
            }
        }
    }
    private void HandleScriptContent(string arguments)
    {
        var args = arguments.Split(new[] { '|' }, 2);
        if (args.Length == 2)
        {
            string scriptName = args[0];
            string content = args[1];
            Debug.Log($"Content of script {scriptName}:\n{content}");
        }
        else
        {
            Debug.LogWarning("Invalid arguments for ScriptContent");
        }
    }
}
