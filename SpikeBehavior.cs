using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject corpseSamurai;
    public GameObject corpseNinja;

    public shared ss;

    PlayerStatus status;
    PlayerMovement playerMoveStatus; // calls from player movement
    public bool isNinja = false;
    public bool isSamurai = false;

    AudioSource samuraiDeath;
    AudioSource ninjaDeath;


    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        playerMoveStatus = player.GetComponent<PlayerMovement>();

        samuraiDeath = GetComponent<AudioSource>();
        ninjaDeath = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerMoveStatus.ninja == false)
        {
            isSamurai = true;
            isNinja = false;
            //Debug.Log("Ninja false");
        }
        else if (playerMoveStatus.ninja == true)
        {
            isSamurai = false;
            isNinja = true;
            //Debug.Log("Ninja true");
        }
    }

    // removes player life, respawns player at spawn point, creates corpse of player on collision with spike
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatus status = collision.gameObject.GetComponent<PlayerStatus>();

            if (isSamurai)
            {
                Instantiate(corpseSamurai, new Vector2(collision.transform.position.x, gameObject.transform.position.y + 0.80f), Quaternion.identity);
                samuraiDeath.Play();
                //Debug.Log("samurai");
            }

            else if (isNinja)
            {
                Instantiate(corpseNinja, new Vector2(collision.transform.position.x, gameObject.transform.position.y + 0.23f), Quaternion.identity);
                ninjaDeath.Play();
                //Debug.Log("ninja");
            }

            status.nextLife();
        }
    }
}