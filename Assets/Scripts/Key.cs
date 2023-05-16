using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(sprite.enabled) {
            pickup(other);
        }
    }

    void pickup(Collider2D player) {
        Player controls = player.GetComponent<Player>();
        controls.keyGrabbed = true;
        sprite.enabled = false;
    }
}
