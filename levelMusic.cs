using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMusic : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audiosource;
    public shared ss;
    public AudioClip start;
    public AudioClip loop1;
    public AudioClip loop2;
    public AudioClip loop3;
    private float startTimer;
    private float[]loopTimers=new float[3];
    private AudioClip[] clips = new AudioClip[3];
    private bool startM;
    private float loopTime;
    void Start()
    {
        startTimer = 7f;
        loopTimers[0] = 27f;
        loopTimers[1] = 31f;
        loopTimers[2] = 17f;
        clips[0] = loop1;
        clips[1] = loop2;
        clips[2] = loop3;
        startM = true;
        audiosource = GetComponent<AudioSource>();
        audiosource.PlayOneShot(start);
        loopTime = loopTimers[ss.currLevel];
    }

    // Update is called once per frame
    void Update()
    {
        if (startM)
        {
            startTimer -= Time.deltaTime;
            if (startTimer <= 0)
            {
                startM = false;
                audiosource.PlayOneShot(clips[ss.currLevel],1.35f);
            }
        }
        else
        {
            loopTime -= Time.deltaTime;
            if (loopTime <= 0)
            {
                loopTime = loopTimers[ss.currLevel];
                audiosource.PlayOneShot(clips[ss.currLevel],1.35f);
            }
        }
    }
}
