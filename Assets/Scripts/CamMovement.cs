using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamMovement : MonoBehaviour
{
    static bool spawned = false;
    public Transform target;

    public float smoothTime = 0.1f;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (spawned) Destroy(gameObject);
        else spawned = true;
    }
    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "LevelSelect") {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        } else transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }
}
