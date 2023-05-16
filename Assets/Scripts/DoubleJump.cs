using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] Camera getCam;
    public SpriteRenderer sprite;
    public Camera cam;

    bool sizeUp = false;

    float startCamSize, endCamSize, newCamSize;
    float smoothTime = 0.3f;
    float velocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        cam = getCam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sizeUp) {
            newCamSize = Mathf.SmoothDamp(cam.orthographicSize, endCamSize, ref velocity, smoothTime);
            cam.orthographicSize = newCamSize;
            if(Mathf.Approximately(cam.orthographicSize, endCamSize)) {
                
                cam.orthographicSize = endCamSize;
                sizeUp = false;
                startCamSize = cam.orthographicSize;
            }
        }
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(sprite.enabled) {
            pickup(other);
        }
    }

    void pickup(Collider2D player) {
        Player controls = player.GetComponent<Player>();
        startCamSize = cam.orthographicSize;
        controls.numJumps++;
        controls.jumpsLeft++;
        endCamSize = startCamSize + 1;
        sizeUp = true;
        sprite.enabled = false;
    }
}
