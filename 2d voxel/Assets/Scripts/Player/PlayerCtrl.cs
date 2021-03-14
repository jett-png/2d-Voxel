using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MonoBehaviour
{
    #region Data
    //ref
    private Rigidbody2D rb;

    //running
    public float speed;

    //jumping
    public float jumpForce;
    private float jumpCooldown;
    private bool jumpTrigger;
    private bool jumping;

    //grounded
    public LayerMask Ground;
    public Vector2 groundSize;
    private Vector2 groundOffset;
    public float groundHeight;
    private bool grounded;

    //voxel editing
    private bool breaking;
    private bool building;
    #endregion


    #region Behavior

    #region Initialize
    private bool init;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        PlayerInputs.instance.PIA.standard.Jump.performed += ctx => JumpInput();
        PlayerInputs.instance.PIA.standard.Trigger1.performed += ctx => breaking = true;
        PlayerInputs.instance.PIA.standard.Trigger1.canceled += ctx => breaking = false;
        PlayerInputs.instance.PIA.standard.Trigger2.performed += ctx => building = true;
        PlayerInputs.instance.PIA.standard.Trigger2.canceled += ctx => building = false;
        
        groundOffset = new Vector2(0, groundHeight);

        init = true;
    }
    #endregion


    #region Updates
    private void Update()
    {
        if (!init) return;

        if (breaking) Break();
        if (building) Build();

        GroundDetection();
    }

    private void FixedUpdate()
    {
        if (!init) return;

        Run();
    }
    #endregion


    #region Player Capabilities

    #region Move
    private void JumpInput()
    {
        if (grounded && !jumping) jumpTrigger = true;
    }

    private void GroundDetection()
    {
        grounded = Physics2D.OverlapBox((Vector2)transform.position + groundOffset, groundSize, 0, Ground);

        if (jumping)
            jumpCooldown += Time.deltaTime;

        if(jumpCooldown >= 0.25f)
        {
            jumping = false;
            jumpCooldown = 0;
        }
    }

    private void OnDrawGizmosSelected() => Gizmos.DrawWireCube(transform.position + new Vector3(0, groundHeight), groundSize);

    
    private void Run()
    {
        Vector3 runV = new Vector3(speed * PlayerInputs.instance.run, rb.velocity.y);
        rb.velocity = Vector3.Lerp(rb.velocity, runV, 0.1f);

        if (jumpTrigger && grounded && !jumping)
        {
            rb.AddForce(transform.up * jumpForce * 100);
            jumpTrigger = false;
            jumping = true;
        }
    }
    #endregion


    #region World Editing
    private void Build() => BlockManager.instance.SetTile(GameRef.cursor.position, 2);
    private void Break() => BlockManager.instance.SetTile(GameRef.cursor.position, 0);
    #endregion

    #endregion

    #endregion
}