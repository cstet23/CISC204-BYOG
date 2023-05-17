using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    static bool spawned = false;
    public float speed = 6.0f;

    public float jumpForce = 15.0f;

    public bool grounded = false;
    public bool reJump = false;

    public float lastJump = 0.0f;
    public float lastTele = 0.0f;
    public float lastBlink = 0.0f;
    public float lastProj = 0.0f;
    public float jumpCooldown = 0.3f;
    public float blinkCooldown = 1.7f;
    public float teleCooldown = 1.5f;
    public float projCooldown = 0.5f;

    public int numJumps = 1;
    public int jumpsLeft = 1;

    public bool teleMe = false;

    public int keyCount = 0;
    public bool keyGrabbed = false;

    public bool blinkEnabled = false;

    public bool lighterGet = false;

    public int bossKilled = 0;

    public int contactCheck;

    private Rigidbody2D body;

    private ContactPoint2D[] contacts;
    
    private Animator anim;

    private BoxCollider2D box;
    // Start is called before the first frame update

    void Awake() {
        DontDestroyOnLoad(gameObject);
        if(spawned) Destroy(gameObject);
        else spawned = true;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                            reJump = false;
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
        if (Input.GetKeyDown(KeyCode.W)) {
            if(Time.time - lastJump > jumpCooldown && jumpsLeft > 0) {
                lastJump = Time.time;
                jumpsLeft--;
                reJump = true;
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            teleMe = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && blinkEnabled) {
            if(Time.time - lastBlink > blinkCooldown) {
                lastBlink = Time.time;
                transform.position += new Vector3(Mathf.Sign(deltaX) * 8.0f, 0.0f, 0.0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && lighterGet) {
            //shoot a projectile
            if(Time.time - lastProj > projCooldown) {
                lastProj = Time.time;
                GameObject proj = Instantiate(projectile) as GameObject;
                
                Projectile projInstance = proj.GetComponent<Projectile>();
                if(Mathf.Sign(deltaX) == -1) {
                    projInstance.projDir = false;
                    proj.transform.position = gameObject.transform.position - new Vector3(1.5f, 0.0f, 0.0f);
                }
                else if(Mathf.Sign(deltaX) == 1) {
                    projInstance.projDir = true;
                    proj.transform.position = gameObject.transform.position + new Vector3(1.5f, 0.0f, 0.0f);
                }
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

        anim.SetFloat("speed", Mathf.Abs(deltaX));
        anim.SetBool("grounded", grounded);
        anim.SetBool("reJump", reJump);
        reJump = false;
        Vector3 pScale = Vector3.one;
        // if (platform != null) {
        //     pScale = platform.transform.localScale;
        // }
        if (!Mathf.Approximately(deltaX, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) * 3 / pScale.x, 1 * 3 / pScale.y, 1);
        }

        if(transform.position.y < -20) {
            transform.position = new Vector3(-4.0f, -1.0f, -1.0f);
        }
    }

    public IEnumerator switchScenes(string sceneName) {
        Debug.Log("Scene getting switched");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene switched");
        if(sceneName == "MainMenu" || sceneName == "EndCredits") gameObject.transform.position = new Vector3(-18.6f, 0.8f, -1.0f);
        else gameObject.transform.position = new Vector3(-4.0f, -1.0f, -1.0f);
        //SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
    }
}
