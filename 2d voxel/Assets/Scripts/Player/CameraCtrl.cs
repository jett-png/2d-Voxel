using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    #region Data
    [Header("Target Follow")]
    private Transform target;

    public Vector3 offset;
    private Vector3 smoothSpeed;

    public float timeOffset;


    [Header("Spectator")]
    public float speed;

    private bool movInput;
    private bool init;
    #endregion


    #region Behavior

    #region Initialize
    public void Initialize()
    {
        PlayerInputs.instance.PIA.standard.Run.performed += ctx => movInput = true;
        PlayerInputs.instance.PIA.standard.Run.performed += ctx => movInput = false;

        target = GameRef.player;

        curSpeed = speed;
        init = true;
    }
    #endregion


    #region Spectator - Free Move
    private float curSpeed;

    private void Update()
    {
        if (!init) return;

        if (movInput)
            FreeMove();
    }

    private void FreeMove()
    {
        //rb.velocity = PlayerInputs.instance.run * curSpeed;
    }
    #endregion


    #region Target Follow - Locked Move
    private void FixedUpdate()
    {
        if (!init) return;

        FollowTarget();
    }

    public void FollowTarget()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref smoothSpeed, timeOffset);
    }
    #endregion

    #endregion
}
