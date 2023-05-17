using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingPlatforms : MonoBehaviour
{
    [SerializeField] int pickupsToAppear;
    [SerializeField] string pickupCheck;
    [SerializeField] Player player;
    public SpriteRenderer sprite;
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        if(player == null) player = GetComponentInParent<Player>();
        if(player == null) player = GameObject.Find("Player").GetComponent<Player>();
        sprite.enabled = false;
        if(box != null) box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pickupCheck == "jumps") {
            if(player.numJumps >= pickupsToAppear && sprite.enabled == false) {
                sprite.enabled = true;
                if(box != null) box.enabled = true;
            }
        } else if (pickupCheck == "keys") {
            if(player.keyCount >= pickupsToAppear && sprite.enabled == false) {
                sprite.enabled = true;
                if(box != null) box.enabled = true;
            }
        } else if (pickupCheck == "bossKilled") {
            if(player.bossKilled >= pickupsToAppear && sprite.enabled == false) {
                sprite.enabled = true;
                if(box != null) box.enabled = true;
            }
        }
    }
}
