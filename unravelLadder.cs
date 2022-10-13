using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unravelLadder : MonoBehaviour
{
    private bool isPressed = false;
    public GameObject correspondLadder;
    //SpriteRenderer.color = new Color(1f,1f,1f,1f)
    private SpriteRenderer fade;
    // Start is called before the first frame update
    void Start()
    {
        //fade.color.
        fade = correspondLadder.GetComponent<SpriteRenderer>();
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);
        correspondLadder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPressed == false)
        {
            correspondLadder.SetActive(true);
            StartCoroutine("fadeIn");
            isPressed = true; 
        }
    }
    private IEnumerator fadeIn()
    {
        for(int i=0;i<200;i++)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, i*0.01f/2);
            yield return null;
        }

    }
}
