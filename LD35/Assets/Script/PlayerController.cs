using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 3f;
    public float speed = 50f;
    public float jumpForce = 150f;
    public GameObject groundCheck;

    private bool grounded = true;
    private Rigidbody2D rb;
    private Animator anim;

    private int speedHash = Animator.StringToHash("Speed");
    private int groundedHash = Animator.StringToHash("Grounded");

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float m = Input.GetAxis("Horizontal");
        grounded = CheckOnGround();

        anim.SetFloat(speedHash, Mathf.Abs(rb.velocity.x));
        anim.SetBool(groundedHash, grounded);

        if (m <= -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (m > 0.1f)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if (Input.GetButtonDown("Jump") && grounded)
            rb.AddForce(Vector2.up * jumpForce);
    }

    void FixedUpdate()
    {
        float m = Input.GetAxis("Horizontal");

        rb.AddForce((Vector2.right * speed ) * m);

        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);

        if (rb.velocity.x < -maxSpeed)
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
    }

    bool CheckOnGround()
    {
        bool result = false;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, 0.01f);
        if (hit.collider)
        {
            result = true;
        }
        return result;
    }
}
