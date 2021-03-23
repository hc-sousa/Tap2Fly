using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public gamemanager gameManager;
    public float velocity = 1;
    private Rigidbody2D rb;
    public Button vida;
    public Button ad;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Jump
            if (gameManager.currentGameMode == "inverse") rb.velocity = Vector2.down * velocity;
            else rb.velocity = Vector2.up * velocity;
            if (gameManager.tag == "jogando")
            {
                SoundManagerScript.PlaySound("Flap");
                PlayerPrefs.SetInt("taps", (PlayerPrefs.GetInt("taps", 0) + 1));
            }
            else if (gameManager.tag == "naojogando") { SoundManagerScript.PlaySound("Button"); }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "NotDie")
        {
            // SoundManagerScript.PlaySound("Hit"); SE VIR ANÚNCIO FICA ASSIM
            // gameManager.GameOver();
            PlayerPrefs.SetInt("deaths", (PlayerPrefs.GetInt("deaths", 0) + 1));
            gameManager.GameOver();
            SoundManagerScript.PlaySound("Hit");
            //gameManager.Replay();

        }
    }

}
