using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 31;
    public int startHealth = 31;
    public int bounceCount = 0;
    public int bounceLimit = 3;
    public float vertSpeed = 1.5f;
    public float horzSpeed = 0.0f;
    
    public bool moveDir = false; //false is across, true is up
    public bool faceDir = false; //false is left, true is right
    public bool vertDir = false; //false is down, true is up
    public SpriteRenderer sprite;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player.bossKilled == 1) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDir == false){
            Vector3 movement = new Vector3(horzSpeed/60, 0, 0);
            if (faceDir) gameObject.transform.position += movement;
            else gameObject.transform.position -= movement;
        } else if (moveDir == true) {
            Vector3 movement = new Vector3(0, vertSpeed/60, 0);
            if (vertDir) gameObject.transform.position += movement;
            else gameObject.transform.position -= movement;
        }
        if ((gameObject.transform.position.x <= 80 && !moveDir) || (gameObject.transform.position.x >= 140 && !moveDir)) {
            faceDir = !faceDir;
            moveDir = !moveDir;
            sprite.flipX = faceDir;
        }
        else if ((gameObject.transform.position.y <= 16 && !vertDir)|| (gameObject.transform.position.y >= 32 && vertDir)) {
            vertDir = !vertDir;
            bounceCount++;
        }
        if(bounceCount >= bounceLimit) {
            bounceCount = 0;
            bounceLimit = Random.Range(1, 5);
            moveDir = !moveDir;
        }

        if(health == 30) {
            horzSpeed = 3.0f;
            vertSpeed = 1.5f;
        } else if(health < 20 && health > 10) {
            horzSpeed = 3.5f;
            vertSpeed = 2.0f;
        } else if (health < 10 && health > 0) {
            horzSpeed = 4.0f;
            vertSpeed = 2.5f;
        } else if (health <= 0) {
            player.bossKilled = 1;
            Destroy(gameObject);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("projectile")) {
            health--;
        }
        else if(other.gameObject.name == "Player") {
            other.gameObject.transform.position = new Vector3(60.0f, -1.0f, -1.0f);
            health = startHealth - 1;
        }
    }
    // void OnTriggerEnter2D (Collider2D other) {
    //     if(other.gameObject.name == "Player") {
    //         other.gameObject.transform.position = new Vector3(60.0f, -1.0f, -1.0f);
    //     } else if (other.gameObject.name == "Projectile (Clone)") {
    //         health--;
    //     }
    // }
}
