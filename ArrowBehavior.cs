using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public GameObject corpseSamurai;
    public GameObject corpseNinja;

    private bool isItNinja;
    private bool isItSamurai;

    private Rigidbody2D rb;
    public shared ss;
    AudioSource audioSource;
    PlayerMovement playerStatus;
    public float flightSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * flightSpeed;

        audioSource = GetComponent<AudioSource>();
        playerStatus = ss.player.GetComponent<PlayerMovement>();

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerStatus status = collide.gameObject.GetComponent<PlayerStatus>();
            status.nextLife();
            playerStatus = ss.player.GetComponent<PlayerMovement>();
            if (!playerStatus.ninja)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                var corpsePlayerSamuraiVar = Instantiate(corpseSamurai, new Vector2(gameObject.transform.position.x + 0.75f, gameObject.transform.position.y), Quaternion.identity);
                corpsePlayerSamuraiVar.transform.SetParent(gameObject.transform);
                gameObject.tag = "SamCorpsedArrow";
                audioSource.Play();
            }
            
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                var corpsePlayerNinjaVar = Instantiate(corpseSamurai, new Vector2(gameObject.transform.position.x + 0.75f, gameObject.transform.position.y), Quaternion.identity);
                corpsePlayerNinjaVar.transform.SetParent(gameObject.transform);
                gameObject.tag = "SamCorpsedArrow";
                audioSource.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (collision.gameObject.tag == "SamCorpsedArrow" || collision.gameObject.tag == "NinjaCorpsedArrow")
        {
            Destroy(GameObject.FindGameObjectWithTag("Arrow"));
        }
    }
}
