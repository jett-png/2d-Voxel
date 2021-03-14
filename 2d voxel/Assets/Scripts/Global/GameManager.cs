using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instancing

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    private void OnDisable() => instance = null;
    #endregion
}
