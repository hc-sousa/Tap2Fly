using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip flySound, deathSound, scoreSound, buttonSound;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        flySound = Resources.Load<AudioClip>("Flap");
        deathSound = Resources.Load<AudioClip>("Hit");
        scoreSound = Resources.Load<AudioClip>("Score");
        buttonSound = Resources.Load<AudioClip>("Button");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Flap":
                audioSrc.PlayOneShot(flySound); 
                break;
            case "Hit":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "Score":
                audioSrc.PlayOneShot(scoreSound);
                break;
            case "Button":
                audioSrc.PlayOneShot(buttonSound);
                break;
        }
    }

    void Mute()
        {
           if (PlayerPrefs.GetFloat("volume") == 0.0) { PlayerPrefs.SetFloat("volume", 1.0F);}
           else if (PlayerPrefs.GetFloat("volume") == 1.0) { PlayerPrefs.SetFloat("volume", 0.0F); }

    }
}
