using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //ref
    private VoxelManager VM;
    private Rigidbody2D rb;

    //running
    public float speed;
    private int runInput;

    //jumping
    public float jumpForce;
    private float jumpCooldown;
    private bool jumpInput;
    private bool jumping;

    //grounded
    public LayerMask Ground;
    public Vector2 groundSize;
    private Vector2 groundOffset;
    public float groundHeight;
    private bool grounded;


    private void Start()
    {
        VM = VoxelManager.instance;
        rb = GetComponent<Rigidbody2D>();

        groundOffset = new Vector2(0, groundHeight);
    }

    private void Update()
    {
        Inputs();
        GroundDetection();
        EditWorld();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        runInput = (int)Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded && !jumping)
        {
            jumpInput = true;
        }
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
    
    
    private void Move()
    {
        Vector3 runV = new Vector3(speed * runInput, rb.velocity.y);
        rb.velocity = Vector3.Lerp(rb.velocity, runV, 0.1f);

        if (jumpInput && grounded && !jumping)
        {
            rb.AddForce(transform.up * jumpForce * 100);
            jumpInput = false;
            jumping = true;
        }
    }


    private void EditWorld()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            Vector2 mousePos = GameRef.cursorPos;
            Vector2[] pos = new Vector2[1];
            pos[0] = mousePos;

            if(Input.GetButtonDown("Fire1"))
                VM.ClearTile(pos);

            if (Input.GetButtonDown("Fire2"))
                VM.SetTile(pos, 1);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 1, 0.5f);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, groundHeight), groundSize);
    }
}
