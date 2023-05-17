using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject linkedTele;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other) {
        Player controls = other.GetComponent<Player>();
        if(controls.teleMe == true) {
            if(Time.time - controls.lastTele > controls.teleCooldown) {
                controls.lastTele = Time.time;
                controls.teleMe = false;
                controls.transform.position = new Vector3(linkedTele.transform.position.x, linkedTele.transform.position.y, -1.0f);
                if(controls.keyGrabbed) {
                    controls.keyGrabbed = false;
                    controls.keyCount++;
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, linkedTele.transform.position);
    }
}
