using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region Set Up
    public static PlayerInputs instance;
    public PlayerInputActions PIA;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("Instance already exists, destroying object");
            Destroy(this);
        }

        PIA = new PlayerInputActions();
    }

    private void Start()
    {
        WorldManager.instance.OnToggleDC += ToggleDC;
        WorldManager.instance.OnToggleInv += ToggleInv;
        PIA.standard.Disable();
    }

    private void OnEnable() => PIA.Enable();
    private void OnDisable()
    {
        PIA.Disable();
        instance = null;
    }
    #endregion


    #region Data
    public Transform cursor;

    [System.NonSerialized]
    public int run;
    #endregion


    #region Behavior
    public void Initialize()
    {
        PIA.standard.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject player = transform.GetChild(1).gameObject;
        player.SetActive(true);
        player.GetComponent<PlayerCtrl>().Initialize();

        GameRef.cursor = cursor;
        GameRef.player = player.transform;
    }

    private void Update()
    {
        if (!PIA.standard.enabled) return;

        run = (int)PIA.standard.Run.ReadValue<float>();

        Vector3 mouseV = PIA.standard.MouseVelocity.ReadValue<Vector2>();
        mouseV *= 0.015f;

        cursor.position = mouseV + cursor.position;
    }

    private void ToggleDC(bool DCtoggled)
    {
        if (DCtoggled) PIA.standard.Disable();
        else if(GameRef.init) PIA.standard.Enable();
    }

    private void ToggleInv(bool invToggled)
    {
        if (invToggled)
        {
            PIA.standard.Run.Disable();
            PIA.standard.Jump.Disable();
            PIA.debug.Disable();
        }
        else if (GameRef.init)
        {
            PIA.standard.Enable();
            PIA.debug.Enable();
        }
    }
    #endregion
}
