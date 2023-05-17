using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaZoom : MonoBehaviour
{
    public Camera cam;
    public Boss boss;

    bool sizeUp = false;

    float startCamSize, endCamSize, newCamSize;
    float smoothTime = 0.6f;
    float velocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sizeUp) {
            newCamSize = Mathf.SmoothDamp(cam.orthographicSize, endCamSize, ref velocity, smoothTime);
            cam.orthographicSize = newCamSize;
            if(endCamSize - cam.orthographicSize < 0.001f) {
                cam.orthographicSize = endCamSize;
                boss.horzSpeed = 3.0f;
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player") {
            startCamSize = cam.orthographicSize;
            endCamSize = 15;
            sizeUp = true;
        }
    }
}
