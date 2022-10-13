using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // sound variables
    AudioSource audioSource;
    public GameObject jumpOff;
    public GameObject jumpButt;
    private float randomNum;
    public AudioClip jumpOne;
    public AudioClip jumpTwo;
    public AudioClip jumpThree;
    public AudioClip jumpFour;
    public AudioClip jumpFive;
    public AudioClip jumpSix;
    public AudioClip jumpSeven;
    public AudioClip SamuraiWalk;

    //movement vars


    public float playerSpeed;
    public float jumpSpeed;
    public float climbSpeed;


    public float h_movement; //also account for ladder y velocity

    Rigidbody2D rb = new Rigidbody2D();

    //jumping conditions

    public bool onGround = false;
    public float castLength;

    public LayerMask groundLayer;

    //special ability
    PlayerStatus status;
    public bool ninja = true;
    private bool canDouble = false;

    //ladder climbing

    private bool ladderInRange;
    public bool ladderisLocked;
    private GameObject ladder;
    private float origGrav;

    public AudioClip ninwalk1;
    public AudioClip ninwalk2;
    public AudioClip ninwalk3;
    public AudioClip ninwalk4;
    private AudioClip[] ninwalks=new AudioClip[4];
    public AudioClip samwalk1;
    public AudioClip samwalk2;
    public AudioClip samwalk3;
    public AudioClip samwalk4;
    private AudioClip[] samwalks=new AudioClip[4];

    private float walkTimer=0.5f;
    void Start()
    {
        ninwalks[0] = ninwalk1;
        ninwalks[1] = ninwalk2;
        ninwalks[2] = ninwalk3;
        ninwalks[3] = ninwalk4;
        samwalks[0] = samwalk1;
        samwalks[1] = samwalk2;
        samwalks[2] = samwalk3;
        samwalks[3] = samwalk4;
        status = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        origGrav = rb.gravityScale;
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        //play menu, loop menu
        walkTimer -= Time.deltaTime;
        if(walkTimer<=0)
        {
            walkTimer = 0.5f;
            if (h_movement!=0&&onGround)
            {
                Debug.Log("footstep");
                if(ninja)
                {

                    audioSource.PlayOneShot(ninwalks[Random.Range(0, 3)],3.5f);
                }
                else
                {

                    audioSource.PlayOneShot(samwalks[Random.Range(0, 3)],3.5f);
                }
            }
            
        }

        onGround =
            (
            Physics2D.Raycast(transform.position, Vector2.down, castLength, groundLayer)
            ||
            Physics2D.Raycast(new Vector2(transform.position.x-0.3f,transform.position.y), Vector2.down, castLength, groundLayer)
            ||
            Physics2D.Raycast(new Vector2(transform.position.x + 0.3f, transform.position.y), Vector2.down, castLength, groundLayer)
            );
        Debug.DrawRay(transform.position, Vector2.down * castLength, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - 0.3f, transform.position.y), Vector2.down * castLength, Color.blue);
        rb.velocity = new Vector2(h_movement * playerSpeed, rb.velocity.y);
        if(status.currentStatus==PlayerStatus.States.ninja)
        {
            jumpOff.GetComponent<Image>().enabled = false;
            jumpButt.GetComponent<Image>().enabled = true;
            ninja = true;
        }
        else
        {
            jumpOff.GetComponent<Image>().enabled = true;
            jumpButt.GetComponent<Image>().enabled = false;
            ninja = false;
        }



        if (ladderisLocked)
        {
            transform.position = new Vector2(ladder.transform.position.x,transform.position.y);
            rb.velocity = new Vector2(0, h_movement * climbSpeed);
        }
        else
        {
            rb.velocity = new Vector2(h_movement * playerSpeed, rb.velocity.y);
        }

    }
    public void onPressRight()
    {
        h_movement += 1;
    }
    public void onReleaseRight()
    {
        h_movement -= 1;
    }
    public void onPressLeft()
    {

        h_movement -= 1;
    }
    public void onReleaseLeft()
    {
        h_movement += 1;
    }
    public void onJump() //also used to lock onto ladder
    {
        if (ladderInRange)
        {
            if (!ladderisLocked)
            {
                rb.gravityScale = 0;
                ladderisLocked = true;
            }
            else
            {
                rb.gravityScale = origGrav;
                ladderisLocked = false;
            }
        }
        else
        {
            if (onGround)
            {
                Debug.Log("jump");
                if (ninja)
                {
                    canDouble = true;

                    rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(0, jumpSpeed*0.75f), ForceMode2D.Impulse);
                }

                //play random jump sound
                randomNum = Random.Range(1, 7);

                if (randomNum == 1)
                {
                    audioSource.PlayOneShot(jumpOne, 1f);
                }

                else if (randomNum == 2)
                {
                    audioSource.PlayOneShot(jumpTwo, 1f);
                }

                else if (randomNum == 3)
                {
                    audioSource.PlayOneShot(jumpThree, 1f);
                }

                else if (randomNum == 4)
                {
                    audioSource.PlayOneShot(jumpFour, 1f);
                }

                else if (randomNum == 5)
                {
                    audioSource.PlayOneShot(jumpFive, 1f);
                }

                else if (randomNum == 6)
                {
                    audioSource.PlayOneShot(jumpSix, 1f);
                }

                else if (randomNum == 7)
                {
                    audioSource.PlayOneShot(jumpSeven, 1f);
                }
            }
        }
    }
    public void onAbility()
    {
        if (ninja)
        {
            if (!onGround && canDouble)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                canDouble = false;
                rb.AddForce(new Vector2(0, jumpSpeed - 0.5f), ForceMode2D.Impulse);
                audioSource.PlayOneShot(jumpSeven, 1);

                //play random jump sound
                randomNum = Random.Range(1, 7);

                if (randomNum == 1)
                {
                    audioSource.PlayOneShot(jumpOne, 1f);
                }

                else if (randomNum == 2)
                {
                    audioSource.PlayOneShot(jumpTwo, 1f);
                }

                else if (randomNum == 3)
                {
                    audioSource.PlayOneShot(jumpThree, 1f);
                }

                else if (randomNum == 4)
                {
                    audioSource.PlayOneShot(jumpFour, 1f);
                }

                else if (randomNum == 5)
                {
                    audioSource.PlayOneShot(jumpFive, 1f);
                }

                else if (randomNum == 6)
                {
                    audioSource.PlayOneShot(jumpSix, 1f);
                }

                else if (randomNum == 7)
                {
                    audioSource.PlayOneShot(jumpSeven, 1f);
                }
            }
        }
        else
        {

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            ladder = collision.gameObject;
            ladderInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            rb.gravityScale = origGrav;
            ladderInRange = false;
            ladderisLocked = false;
        }
    }
}
