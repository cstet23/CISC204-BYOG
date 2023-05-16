using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        leaveStage(other);
    }

    void leaveStage(Collider2D player) {
        Player controls = player.GetComponent<Player>();
        StartCoroutine(controls.switchScenes("OtherLevel"));
    }
}
