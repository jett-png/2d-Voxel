using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region Set Up
    public static WorldManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    private void OnDisable() => instance = null;
    #endregion


    #region Data
    public string worldName;
    #endregion


    #region Event System

    #region Initialize Event
    public List<Initializer> initList;
    private int initIndex;

    public void Initialize()
    {
        initIndex = 0;
        StartCoroutine("InitQueue");
    }

    private IEnumerator InitQueue()
    {
        foreach(MonoBehaviour obj in initList[initIndex].objs)
        {
            obj.Invoke("Initialize", 0f);
        }

        yield return new WaitUntil(() => initList[initIndex].complete);

        initIndex++;

        if (initIndex >= initList.Count)
            GameRef.init = true;
        else
            StartCoroutine("InitQueue");
    }
    #endregion


    public event Action OnSave;
    public void Save() => OnSave?.Invoke();


    [NonSerialized]
    public bool DCToggled;
    public event Action<bool> OnToggleDC;
    public void ToggleDC()
    {
        if (OnToggleDC != null)
        {
            DCToggled = !DCToggled;
            OnToggleDC(DCToggled);
        }
    }


    [NonSerialized]
    public bool InvToggled;
    public event Action<bool> OnToggleInv;
    public void ToggleInv()
    {
        if (OnToggleInv != null)
        {
            InvToggled = !InvToggled;
            OnToggleInv(InvToggled);
        }
    }
    #endregion
}

#region Custom Types
[Serializable]
public struct Initializer
{
    public string title;
    public List<MonoBehaviour> objs;
    public bool complete;
}
#endregion