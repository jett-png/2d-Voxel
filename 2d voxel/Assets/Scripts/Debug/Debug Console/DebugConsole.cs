using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    #region Commands
    public List<object> commands;

    public static DebugCommand<float[]> CommandTest;
    public static DebugCommand<bool> HelpMenu;
    public static DebugCommand Initialize;
    public static DebugCommand<float[]> NewWorld;
    public static DebugCommand<float[]> Marker;
    public static DebugCommand<float[]> Fill;

    private void Start()
    {
        PlayerInputs.instance.PIA.debug.ToggleDC.performed += ctx => ToggleDC();
        PlayerInputs.instance.PIA.debug.Enter.performed += ctx => ExecuteCommand();
        PlayerInputs.instance.PIA.debug.AnyKey.performed += ctx => anyKey = true;

        Fill = new DebugCommand<float[]>("/fill", "fills in a specified area with the chosen block", "/fill <blockType> <x1 y1> <x2 y2>", (value) =>
        {
            if (value[0] < 0 || value[0] >= 255)
                return;

            int width = (int)Mathf.Abs(value[1] - value[3]);
            int height = (int)Mathf.Abs(value[2] - value[4]);
            Vector2[,] pos = new Vector2[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pos[x, y] = new Vector2(value[1] + x + 0.01f, value[2] + y + 0.01f);
                }
            }

            BlockManager.instance.SetTiles(pos, (int)value[0]);
        });

        Marker = new DebugCommand<float[]>("/set marker", "creates a marker at the specified position", "/set marker <number> <x1 y1>", (value) =>
        {
            if (value[0] < 21 && value[0] > 0)
                markers[(int)value[0]] = new Vector2(value[1], value[2]);
        });

        Initialize = new DebugCommand("/initialize", "initializes the world", "/initialize", () =>
        {
            WorldManager.instance.worldName = "New World";
            WorldManager.instance.Initialize();
        });

        NewWorld = new DebugCommand<float[]>("/create world", "creates and initializes a new world", "/create world <world width> <world height> <chunk width> <chunk height>", (value) =>
        {
            WorldCreation.New(new Vector2Int((int)value[0], (int)value[1]), new Vector2Int((int)value[2], (int)value[3]));
        });

        HelpMenu = new DebugCommand<bool>("/help", "toggles the help menu", "/help <toggle>", (value) =>
        {
            showHelp = value;
        });

        CommandTest = new DebugCommand<float[]>("/commandtest", "tests a command", "/commandtest <amount>", (value) =>
        {
            Debug.Log($"command returns: {value[0]} {value[5]}");
        });


        commands = new List<object>
        {
            CommandTest,
            HelpMenu,
            NewWorld,
            Initialize,
            Marker,
            Fill
        };
    }

    private void ExecuteCommand()
    {
        if (!showConsole) return;

        for (int i = 0; i < commands.Count; i++)
        {
            CommandBase commandBase = commands[i] as CommandBase;

            if (commandInput.StartsWith(commandBase.commandId))
            {
                if (commands[i] as DebugCommand != null) (commands[i] as DebugCommand).Invoke();

                if (commandInput.Length < commandBase.commandId.Length + 1) break;
                string[] properties = commandInput.Remove(0, commandBase.commandId.Length + 1).Split(' ');

                if (commands[i] as DebugCommand<bool> != null && properties.Length >= 1)
                {
                    bool.TryParse(properties[0], out bool result);
                    (commands[i] as DebugCommand<bool>).Invoke(result);
                }
                else if (commands[i] as DebugCommand<float[]> != null)
                {
                    float[] T1 = new float[properties.Length];
                    for (int p = 0; p < properties.Length; p++)
                    {
                        float.TryParse(properties[p], out float result);
                        T1[p] = result;
                    }

                    (commands[i] as DebugCommand<float[]>).Invoke(T1);
                }
            }
        }

        ToggleDC();
    }
    #endregion


    #region Debug Console
    private bool showConsole, showHelp, anyKey;

    private string commandInput;

    private Vector2[] markers = new Vector2[20];

    private void ToggleDC()
    {
        showConsole = !showConsole;
        WorldManager.instance.ToggleDC();
        Cursor.visible = showConsole;
        Cursor.lockState = showConsole ? CursorLockMode.Confined : CursorLockMode.Locked;
        commandInput = "/";
    }

    private string commandHint()
    {
        anyKey = false;

        for (int i = 0; i < commands.Count; i++)
        {
            CommandBase commandBase = commands[i] as CommandBase;

            if (commandInput.Length > 1)
                if(commandInput.StartsWith(commandBase.commandId) || commandBase.commandId.StartsWith(commandInput))
                    return $"{commandBase.commandFormat}            {commandBase.commandDescription}";
        }

        return "";
    }

    private string hint;

    private void OnGUI()
    {
        if (!showConsole) return;
        if (anyKey) hint = commandHint();

        GUI.backgroundColor = Color.green;
        commandInput = GUI.TextField(new Rect(5, Screen.height - 30, Screen.width - 10, 25), commandInput);

        GUIStyle hintStyle = new GUIStyle();
        hintStyle.normal.textColor = Color.green;
        GUI.Label(new Rect(8f, Screen.height - 52, Screen.width - 10, 25), hint, hintStyle);
    }
    #endregion
}