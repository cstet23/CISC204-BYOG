using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SpriteRenderer sprRenderer;
    public Sprite[] sprites;
    public int spriteIndex = 0;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            spriteIndex = (spriteIndex + 2) % 3;
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            spriteIndex = (spriteIndex + 1) % 3;
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            if (spriteIndex == 0) {
                SceneManager.LoadScene("MultiJumpLevel");
                player.transform.position = new Vector3(-4.0f, -1.0f, -1.0f);
            } else if (spriteIndex == 1) {
                SceneManager.LoadScene("LevelSelect"); //NOT REAL ATM
            } else if (spriteIndex == 2) {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
                Application.Quit();
            }
        }
        sprRenderer.sprite = sprites[spriteIndex];
    }
}
