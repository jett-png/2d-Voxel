using System;
using UnityEngine.Tilemaps;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region Instancing

    public static WorldManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    #endregion


    [Header("Generation Settings")]
    public Vector2Int worldSize = new Vector2Int(10, 10);
    public Vector2Int chunkSize = new Vector2Int(10, 10);

    [Range(0.1f,2f)]
    public float blockSize = 1;


    [Header("Global Lists")]
    public ItemData[] items;
    public Tile[] materials;


    [Header("States")]
    public bool gamePaused;

    //debug states
    public bool HUDShown, gizmosShown, locationShown, statsShown, spectating, godMode;


    #region Event System

    #region Events

    public event Action onSave;

    public void Save()
    {
        if (onSave != null)
        {
            onSave();
        }
    }

    #endregion

    #region HotKey Events

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gamePaused = !gamePaused;

        if (!Input.GetKey(KeyCode.Tab))
            return;

        if (Input.GetKeyDown(KeyCode.F1))
            HUDShown = !HUDShown;
        if (Input.GetKeyDown(KeyCode.F2))
            gizmosShown = !gizmosShown;
        if (Input.GetKeyDown(KeyCode.F3))
            locationShown = !locationShown;
        if (Input.GetKeyDown(KeyCode.F4))
            statsShown = !statsShown;
        if (Input.GetKeyDown(KeyCode.F5))
            spectating = !spectating;
        if (Input.GetKeyDown(KeyCode.F6))
            godMode = !godMode;
    }

    #endregion

    #endregion
}