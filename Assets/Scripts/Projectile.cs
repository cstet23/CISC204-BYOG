using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Boss boss;
    public float projSpeed = 4.0f;
    public bool projDir; //false is left, true is right
    public float spawnTime;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        try{boss = GameObject.Find("Boss").GetComponent<Boss>();}
        catch{boss = null;}
        spawnTime = Time.time;
        sprite.flipX = !projDir;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 trans = gameObject.transform.position;
        if(trans.x < -30 || trans.x > 200 || Time.time - spawnTime > 3.0f) {
            Destroy(gameObject);
        } else {
            if(projDir) gameObject.transform.position += new Vector3(projSpeed/60.0f, 0.0f, 0.0f);
            else gameObject.transform.position -= new Vector3(projSpeed/60.0f, 0.0f, 0.0f); 
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Player") {
            other.gameObject.transform.position = new Vector3(-4.0f, -1.0f, -1.0f);
            if (boss != null) boss.health = boss.startHealth;
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("boss")) {
            Destroy(gameObject);
        }
    }
}
