using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [Header("Target Follow")]
    private Transform target;

    public Vector3 offset;
    private Vector3 smoothSpeed;

    public float timeOffset;


    [Header("Spectator")]
    private Rigidbody2D rb;

    private Vector2 movInput;

    public float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
    }

    void Update()
    {
        if (WorldManager.instance.spectating)
        {
            MoveInput();
            return;
        }
    }

    private void FixedUpdate()
    {
        if (WorldManager.instance.spectating)
        {
            MoveAction();
            return;
        }

        FollowTarget();
    }


    #region Spectator - Free Move

    private float curSpeed;

    private void MoveInput()
    {
        movInput.x = Input.GetAxis("Horizontal");
        movInput.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            curSpeed = speed * 2;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            curSpeed = speed;
    }

    private void MoveAction()
    {
        rb.velocity = movInput * curSpeed;
    }

    #endregion

    #region Target Follow - Locked Move

    public void FollowTarget()
    {
        if (target == null)
        {
            target = WorldManager.instance.player;
            return;
        }

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref smoothSpeed, timeOffset);
    }

    public void SnapToTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = target.position + offset;
    }

    #endregion

}
