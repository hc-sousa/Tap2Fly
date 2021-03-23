using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    public gamemanager gameManager;
    public float speed, multiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.score >= 100 && gameManager.currentGameMode == "power") { transform.position += Vector3.left * speed * multiplier * Time.deltaTime; }
        else { transform.position += Vector3.left * speed * Time.deltaTime; }
    }

}
