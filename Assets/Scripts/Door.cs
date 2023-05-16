using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorOpen = false;
    public SpriteRenderer sprite;
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(doorOpen) {
            sprite.enabled = false;
            box.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Player controls = other.GetComponent<Player>();
        if(controls.keyCount >= 3) {
            doorOpen = true;
        }
    }
}
