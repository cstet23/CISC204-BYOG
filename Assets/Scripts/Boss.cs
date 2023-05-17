using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 30;
    public float vertSpeed = 0.5f;
    public float horzSpeed = 1.5f;
    //flase is accross, true is up
    public bool moveDir = false;
    public int startingPos = 140;
    //false is left true is right
    public bool faceDir = false;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveDir){
            Vector3 movement = new Vector3(horzSpeed/60, 0, 0);
            if(faceDir){
                gameObject.transform.position += movement;
            }else{
                gameObject.transform.position -= movement;
            }
                
            
        }
        if (gameObject.transform.position.x <= 80 || gameObject.transform.position.x >= 140) {
            faceDir = !faceDir;
            sprite.flipX = faceDir;
            }
        
    }
    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.name == "Player") {
            other.gameObject.transform.position = new Vector3(60.0f, -1.0f, -1.0f);
        }
    }
}
