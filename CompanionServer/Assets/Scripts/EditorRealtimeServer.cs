using Fleck;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class EditorRealtimeServer : MonoBehaviour
{
    private WebSocketServer server;
    private IWebSocketConnection clientSocket;
    private bool awaitingReconnection = false;

    public static EditorRealtimeServer Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartServer();
    }

    private void StartServer()
    {
        // Dispose of the existing server instance, if any, to free up the port
        if (server != null)
        {
            server.Dispose();
            server = null;
        }

        string connection = "ws://0.0.0.0:8083";
        server = new WebSocketServer(connection);
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                clientSocket = socket;
                awaitingReconnection = false;
                Debug.Log("Connection opened");

                // Notify Unity editor about the connection
                EditorApplication.delayCall += () => ClientCommandsWindow.NotifyClientConnected();
            };
            socket.OnClose = () =>
            {
                Debug.Log("Connection closed");
                clientSocket = null;
                awaitingReconnection = true;
            };
            socket.OnMessage = async (message) =>
            {
                Debug.Log("Server received: " + message);
                await OnMessageReceived(socket, message); // Handle the message
            };
        });

        Debug.Log("WebSocket server started at ws://0.0.0.0:8082");
    }

    void Update()
    {
        // Attempt reconnection if awaiting a reconnect
        if (awaitingReconnection && clientSocket == null)
        {
            awaitingReconnection = false; // Reset to avoid multiple calls
            StartCoroutine(RestartServerWithDelay(1.0f)); // Delay before restart
        }
    }

    private IEnumerator RestartServerWithDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StartServer();
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

    private async Task OnMessageReceived(IWebSocketConnection socket, string message)
    {
        Debug.Log("Received message: " + message);
    }

    private async Task HandleCommand(IWebSocketConnection socket, string message)
    {
        var parts = message.Split(new[] { '|' }, 2);
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
                    case "ReadScriptContent":
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

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            Debug.Log("Exiting play mode: Shutting down server.");
            ShutDownServer();
        }
    }

    private void ShutDownServer()
    {
        if (server != null)
        {
            try
            {
                if (clientSocket != null)
                {
                    clientSocket.Close(); // Close client connection if active
                    clientSocket = null;
                }

                server.Dispose(); // Dispose of the server instance
                server = null;

                Debug.Log("WebSocket server shut down successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while shutting down server: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Server is already null or not running.");
        }
    }


    void OnDestroy()
    {
        // Ensure server is shut down gracefully
        ShutDownServer();

        // Unregister play mode state change callback
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }
}
