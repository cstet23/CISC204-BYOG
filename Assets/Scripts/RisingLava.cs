using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{
    [SerializeField] Key lavaTrigger;
    public bool lavaRising = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(lavaTrigger.sprite.enabled == false) lavaRising = true;
        if(lavaRising) {
            float yScale = 0.0f;
            if (gameObject.transform.localScale.y + 0.004f <= 21) yScale = 0.004f;
            gameObject.transform.localScale += new Vector3(0.0f, yScale, 0.0f);
            gameObject.transform.position += new Vector3(0.0f, 0.5f*yScale, 0.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player") {
            other.transform.position = new Vector3(10.0f, 10.0f, -1.0f);
            gameObject.transform.localScale = new Vector3(61.0f, 6.0f, 1.0f);
            gameObject.transform.position = new Vector3(20.0f, 33.0f, 0.0f);
            lavaTrigger.sprite.enabled = true;
            Player controls = other.GetComponent<Player>();
            controls.keyGrabbed = false;
            lavaRising = false;
        }
    }
}
