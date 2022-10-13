using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStatus : MonoBehaviour
{
    public int currlife;
    public shared ss;

    AudioSource audioSource;
    public AudioClip Death1;
    public AudioClip Death2;
    public AudioClip Death3;
    public AudioClip Death4;
    public AudioClip Death5;
    public AudioClip finalDeath1;
    public AudioClip finalDeath2;
    public AudioClip finalDeath3;
    
    private AudioClip[] clips=new AudioClip[8];
    public enum States
    {
        samurai,
        ninja
    };
    public List<States>[] levelCharacters = new List<States>[4];
    public States currentStatus;
    public int samremaning;
    public int ninremaning;
    public GameObject ninText;
    public GameObject samText;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clips[0] = Death1;
        clips[1] = Death2;
        clips[2] = Death3;
        clips[3] = Death4;
        clips[4] = Death5;
        clips[5] = finalDeath1;
        clips[6] = finalDeath2;
        clips[7] = finalDeath3;
        currlife = 0;
        levelCharacters[0] = new List<States> { States.samurai, States.samurai,States.samurai, States.samurai,States.samurai,States.samurai,States.samurai,States.samurai };
        levelCharacters[1] = new List<States> { States.samurai, States.samurai, States.samurai};
        levelCharacters[2] = new List<States> { States.ninja, States.samurai, States.samurai, States.ninja};
        levelCharacters[3] = new List<States>
        { States.samurai, States.samurai, States.samurai, States.ninja, States.samurai
        ,States.samurai, States.samurai, States.samurai, States.samurai, States.samurai,States.samurai,States.samurai,States.samurai };
        samremaning = 0;
        ninremaning = 0;
        for(int i=0;i<levelCharacters[ss.currLevel].Count;i++)
        {
            if(levelCharacters[ss.currLevel][i]== States.ninja)
            {
                ninremaning++;
            }
            else
            {
                samremaning++;
            }
        }
        currentStatus = levelCharacters[ss.currLevel][currlife];
        
        ss.initialPlayerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ninText.GetComponent<TMPro.TextMeshProUGUI>().text = ": " + ninremaning;
        samText.GetComponent<TMPro.TextMeshProUGUI>().text = ": " + samremaning;

    }//test


    public void nextLife()
    {
        if(currentStatus==States.ninja)
        {
            ninremaning--;
        }
        else
        {
            samremaning--;
        }
        if (currlife == levelCharacters[ss.currLevel].Count - 1)
        {
            transform.position = new Vector3(-61.5999985f, 29.2999992f, 0);
            audioSource.PlayOneShot(clips[Random.Range(5,7)]);
            StartCoroutine("waitAudio");
        }
        else
        {
            audioSource.PlayOneShot(clips[Random.Range(0,4)]);
            transform.position = ss.initialPlayerPos;

            currlife++;
            currentStatus = levelCharacters[ss.currLevel][currlife];
        }
    }
    private IEnumerator waitAudio()
    {
        for (int i = 0; i < 100; i++)
        {
            if(i==99)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            yield return null;
        }

    }

}
