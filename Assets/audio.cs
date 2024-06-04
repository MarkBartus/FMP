using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
   

    }

    public void PlaySoundEffect()
    {
        audioManager.PlaySFX(audioManager.slash);
    }

}
