using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public GameObject arrow;
    public AudioClip click;
    public AudioClip shoot;
    AudioSource audioSource;

    private float coolDownTime = 3.0f;
    private float currTime = 0.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        var ar = Instantiate(arrow, new Vector2(gameObject.transform.position.x + 1f, gameObject.transform.position.y + 0.45f), Quaternion.identity);
        Destroy(ar, 5);
    }

    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > coolDownTime)
        {
            launchArrow();
            currTime = currTime - coolDownTime;
        }
    }

    private void launchArrow()
    {
        audioSource.PlayOneShot(click,0.2f);
        StartCoroutine("waitAudio");
    }
    private IEnumerator waitAudio()
    {
        for (int i = 0; i < 80; i++)
        {
            if (i == 79)
            {
                audioSource.PlayOneShot(shoot,0.2f);
                var ar = Instantiate(arrow, new Vector2(gameObject.transform.position.x + 1f, gameObject.transform.position.y + 0.45f), Quaternion.identity);
                Destroy(ar, 7);
            }
            yield return null;
        }

    }
}