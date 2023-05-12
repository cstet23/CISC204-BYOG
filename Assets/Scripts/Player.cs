using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4.5f;

    public float jumpForce = 12.0f;

    public bool grounded = false;

    public float lastJump = 0.0f;

    public int numJumps = 1;
    public int jumpsLeft = 1;

    public int contactCheck;

    private Rigidbody2D body;

    private ContactPoint2D[] contacts;
    
    // private Animator anim;

    private BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        Collider2D[] hits = Physics2D.OverlapAreaAll(corner1, corner2);

        //kinda cringe way to make jumps work properly
        //checks for any hit, then goes through all hits, checks if the hit is "the ground" using normals, and refreshes jumps if so
        //if the hit is a wall, does some jank so player doesn't get an extra jump (for some reason)
        grounded = false;
        Vector2 norm = Vector2.zero;
        if(hit != null) {
            for (int i=0; i<hits.Length; i++) {
                contacts = new ContactPoint2D[2];
                contactCheck = hits[i].GetContacts(contacts);
                if (contactCheck>0) {
                    for (int j=0; j<contacts.Length; j++) {
                        norm = contacts[j].normal;
                        if (norm == Vector2.down) {
                            grounded = true;
                            jumpsLeft = numJumps;
                        } else if (jumpsLeft > numJumps - 1) jumpsLeft = numJumps - 1;
                    }
                }
            }
        } else {
            contactCheck = 0;
            if (jumpsLeft > numJumps - 1) jumpsLeft = numJumps - 1;
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        //jumps don't need to be grounded by necessity, instead they're tracked and can "run out" until you touch the ground again (explained above)
        //jumps also don't just add to force, they fully reset vertical velocity
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(Time.time - lastJump > 0.4f && jumpsLeft > 0) {
                lastJump = Time.time;
                jumpsLeft--;
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        // MovingPlatform platform = null;
        // if(hit != null) {
        //     platform = hit.GetComponent<MovingPlatform>();
        // }
        // if(platform != null) {
        //     transform.parent = platform.transform;
        // } else {
            transform.parent = null;
        // }

        // anim.SetFloat("speed", Mathf.Abs(deltaX));
        // anim.SetBool("grounded", grounded);
        Vector3 pScale = Vector3.one;
        // if (platform != null) {
        //     pScale = platform.transform.localScale;
        // }
        if (!Mathf.Approximately(deltaX, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) * 3 / pScale.x, 1 * 3 / pScale.y, 1);
        }

        // if(transform.position.y < -10) {
        //     Debug.Log("oops you fell, try again");
        //     transform.position = new Vector3(0.0f, 0.0f, 1.0f);
        // }
    }
}
