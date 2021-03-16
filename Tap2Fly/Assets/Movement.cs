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
            rb.velocity = Vector2.up * velocity;
            if (gameManager.tag == "jogando")
            {
                SoundManagerScript.PlaySound("Flap");
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
            gameManager.GameOver();
            SoundManagerScript.PlaySound("Hit");
            //gameManager.Replay();

        }
    }

}
