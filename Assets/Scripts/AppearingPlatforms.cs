using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingPlatforms : MonoBehaviour
{
    [SerializeField] int jumpsToAppear;
    [SerializeField] Player player;
    public SpriteRenderer sprite;
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        sprite.enabled = false;
        if(box != null) box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.numJumps >= jumpsToAppear && sprite.enabled == false) {
            sprite.enabled = true;
            if(box != null) box.enabled = true;
        }
    }
}
