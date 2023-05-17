using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaZoom : MonoBehaviour
{
    public Camera cam;

    bool sizeUp = false;

    float startCamSize, endCamSize, newCamSize;
    float smoothTime = 0.6f;
    float velocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sizeUp) {
            newCamSize = Mathf.SmoothDamp(cam.orthographicSize, endCamSize, ref velocity, smoothTime);
            cam.orthographicSize = newCamSize;
            if(endCamSize - cam.orthographicSize < 0.001f) {
                cam.orthographicSize = endCamSize;
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        startCamSize = cam.orthographicSize;
        endCamSize = startCamSize + 5;
        sizeUp = true;
    }
}
