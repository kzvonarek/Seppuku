using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menumusic : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip start;
    public AudioClip loop;
    private float startTimer;
    private float loopTimer;
    private bool startM;
    public shared ss;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        if (!ss.musicBegin)
        {
            DontDestroyOnLoad(transform.gameObject);
            startTimer = 9f;
            loopTimer = 19f;
            startM = true;
            audiosource = GetComponent<AudioSource>();
            audiosource.PlayOneShot(start);
            ss.musicBegin = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(!
            (scene.name=="Main Menu"|| scene.name == "LevelSelect" ||
            scene.name == "Menu Help"
            ||scene.name== "Menu Credits")
          )
        {
            audiosource.Stop();
        }
        if(startM)
        {
            startTimer -= Time.deltaTime;
            if(startTimer<=0)
            {
                startM = false;
                audiosource.PlayOneShot(loop);
            }
        }
        else
        {
            loopTimer -= Time.deltaTime;
            if(loopTimer<=0)
            {
                loopTimer = 19f;
                audiosource.PlayOneShot(loop);
            }
        }
    }
}
