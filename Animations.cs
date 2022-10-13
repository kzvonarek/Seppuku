using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    PlayerMovement playerMoveStatus;
    PlayerStatus status;
    public shared ss;

    public GameObject PlatformSpikes;
    SpikeBehavior whichChar;

    private bool isItNinja;
    private bool isItSamurai;

    public Animator playerAnimator;
    public RuntimeAnimatorController samuraiAC;
    public RuntimeAnimatorController ninjaAC;
    public RuntimeAnimatorController doorAC;
    public GameObject door;

    enum NinjaStates
    {
        IdleNinja,
        RunNinja,
        JumpNinja,
        ClimbNinja,
        ArrowNinja,
        LadderNinja,
        IdleLadderNinja
    }

    enum SamuraiStates
    {
        IdleSamurai,
        RunSamurai,
        JumpSamurai,
        ClimbSamurai,
        ArrowSamuraia,
        LadderSamurai,
        IdleLadderSamurai
    }

    enum DoorStates
    {
        IdleDoor,
        OpeningDoor
    }

    private NinjaStates currentNinjaState;
    private SamuraiStates currentSamuraiState;
    private DoorStates currentDoorState;
    private float h_velocity;

    private bool isOnGround = false;
    private bool isClimbingLadder = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMoveStatus = this.gameObject.GetComponent<PlayerMovement>();
        status = GetComponent<PlayerStatus>();
        playerAnimator = GetComponent<Animator>();
        currentNinjaState = NinjaStates.IdleNinja;
        whichChar = PlatformSpikes.GetComponent<SpikeBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        h_velocity = playerMoveStatus.h_movement;
        isOnGround = playerMoveStatus.onGround;
        isClimbingLadder = playerMoveStatus.ladderisLocked;
        if (whichChar.isNinja == false)
        {
            isItSamurai = true;
            isItNinja = false;
        }
        else if (whichChar.isNinja == true)
        {
            isItSamurai = false;
            isItNinja = true;
        }

        //Ninja Animations
        if(isItNinja)
        {
            playerAnimator.runtimeAnimatorController = ninjaAC;
            // make ninja use run animation
            if (currentNinjaState == NinjaStates.IdleNinja)
            {
                if (h_velocity != 0) // ninja moving
                {
                    playerAnimator.SetBool("isRun", true);
                    currentNinjaState = NinjaStates.RunNinja;
                }
            }

            else if (currentNinjaState == NinjaStates.RunNinja)
            {
                if (h_velocity == 0) // ninja not moving
                {
                    playerAnimator.SetBool("isRun", false);
                    currentNinjaState = NinjaStates.IdleNinja;
                }
            }

            // make ninja use jump animation after run
            if (currentNinjaState == NinjaStates.RunNinja)
            {
                if (isOnGround == false) // ninja in air
                {
                    playerAnimator.SetBool("isJump", true);
                    currentNinjaState = NinjaStates.JumpNinja;
                }
            }

            // fix glitch with jump and run causing endless run
            if (h_velocity == 0 && currentNinjaState != NinjaStates.JumpNinja && currentNinjaState != NinjaStates.ClimbNinja && currentNinjaState != NinjaStates.IdleLadderNinja)
            {
                playerAnimator.SetBool("isRun", false);
                currentNinjaState = NinjaStates.IdleNinja;
            }

            // make ninja use jump animation after idle
            if (currentNinjaState == NinjaStates.IdleNinja)
            {
                if (isOnGround == false) // ninja in air
                {
                    playerAnimator.SetBool("isJump", true);
                    currentNinjaState = NinjaStates.JumpNinja;
                }
            }

            // ninja stops jumping
            else if (currentNinjaState == NinjaStates.JumpNinja)
            {
                if (isOnGround) // ninja on ground
                {
                    playerAnimator.SetBool("isJump", false);
                    currentNinjaState = NinjaStates.IdleNinja;
                }
            }

            // make ninja go into climb animation
            if (currentNinjaState == NinjaStates.IdleNinja)
            {
                if (isClimbingLadder == true) // ninja climbing
                {
                    playerAnimator.SetBool("isClimbing", true);
                    currentNinjaState = NinjaStates.ClimbNinja;
                }
            }

            else if (currentNinjaState == NinjaStates.ClimbNinja || currentNinjaState == NinjaStates.IdleLadderNinja)
            {
                if (isClimbingLadder == false) // ninja going out of ladder
                {
                    playerAnimator.SetBool("isClimbing", false);
                    playerAnimator.SetBool("ladderIdle", false);
                    currentNinjaState = NinjaStates.IdleNinja;
                }
            }

            if (currentNinjaState == NinjaStates.JumpNinja)
            {
                if (isClimbingLadder == true) // ninja climbing after jump
                {
                    playerAnimator.SetBool("isClimbing", true);
                    currentNinjaState = NinjaStates.ClimbNinja;
                }
            }

            // ladder idle animation when ninja is not climbing but is on ladder
            if (h_velocity == 0 && currentNinjaState == NinjaStates.ClimbNinja)
            {
                playerAnimator.SetBool("ladderIdle", true);
                currentNinjaState = NinjaStates.IdleLadderNinja;
            }

            else if (h_velocity != 0 && currentNinjaState == NinjaStates.IdleLadderNinja)
            {
                playerAnimator.SetBool("ladderIdle", false);
                currentNinjaState = NinjaStates.ClimbNinja;
            }
        }

        //Samurai Animations
        if (isItSamurai)
        {
            playerAnimator.runtimeAnimatorController = samuraiAC;
            // make Samurai use run animation
            if (currentSamuraiState == SamuraiStates.IdleSamurai)
            {
                if (h_velocity != 0) // Samurai moving
                {
                    playerAnimator.SetBool("isRun", true);
                    currentSamuraiState = SamuraiStates.RunSamurai;
                }
            }

            else if (currentSamuraiState == SamuraiStates.RunSamurai)
            {
                if (h_velocity == 0) // Samurai not moving
                {
                    playerAnimator.SetBool("isRun", false);
                    currentSamuraiState = SamuraiStates.IdleSamurai;
                }
            }

            // make samurai use jump animation after run
            if (currentSamuraiState == SamuraiStates.RunSamurai)
            {
                if (isOnGround == false) // samurai in air
                {
                    playerAnimator.SetBool("isJump", true);
                    currentSamuraiState = SamuraiStates.JumpSamurai;
                }
            }

            // fix glitch with jump and run causing endless run
            if (h_velocity == 0 && currentSamuraiState != SamuraiStates.JumpSamurai && currentSamuraiState != SamuraiStates.ClimbSamurai && currentSamuraiState != SamuraiStates.IdleLadderSamurai)
            {
                playerAnimator.SetBool("isRun", false);
                currentSamuraiState = SamuraiStates.IdleSamurai;
            }

            // make samurai use jump animation after idle
            if (currentSamuraiState == SamuraiStates.IdleSamurai)
            {
                if (isOnGround == false) // samurai in air
                {
                    playerAnimator.SetBool("isJump", true);
                    currentSamuraiState = SamuraiStates.JumpSamurai;
                }
            }

            // samurai stops jumping
            else if (currentSamuraiState == SamuraiStates.JumpSamurai)
            {
                if (isOnGround) // samurai on ground
                {
                    playerAnimator.SetBool("isJump", false);
                    currentSamuraiState = SamuraiStates.IdleSamurai;
                }
            }

            // make Samurai go into climb animation
            if (currentSamuraiState == SamuraiStates.IdleSamurai)
            {
                if (isClimbingLadder == true) // samurai climbing
                {
                    playerAnimator.SetBool("isClimbing", true);
                    currentSamuraiState = SamuraiStates.ClimbSamurai;
                }
            }

            else if (currentSamuraiState == SamuraiStates.ClimbSamurai || currentSamuraiState == SamuraiStates.IdleLadderSamurai)
            {
                if (isClimbingLadder == false) // samurai going out of ladder
                {
                    playerAnimator.SetBool("isClimbing", false);
                    playerAnimator.SetBool("ladderIdle", false);
                    currentSamuraiState = SamuraiStates.IdleSamurai;
                }
            }

            if (currentSamuraiState == SamuraiStates.JumpSamurai)
            {
                if (isClimbingLadder == true) // samurai climbing after jump
                {
                    playerAnimator.SetBool("isClimbing", true);
                    currentSamuraiState = SamuraiStates.ClimbSamurai;
                }
            }

            // ladder idle animation when samurai is not climbing but is on ladder
            if (h_velocity == 0 && currentSamuraiState == SamuraiStates.ClimbSamurai)
            {
                playerAnimator.SetBool("ladderIdle", true);
                currentSamuraiState = SamuraiStates.IdleLadderSamurai;
            }

            else if (h_velocity != 0 && currentSamuraiState == SamuraiStates.IdleLadderSamurai)
            {
                playerAnimator.SetBool("ladderIdle", false);
                currentSamuraiState = SamuraiStates.ClimbSamurai;
            }
        }

        // flip the player according to the moving direction
        if (h_velocity > 0)
        {
            // facing right
            GetComponent<SpriteRenderer>().flipX = false;
        }

        else if (h_velocity < 0)
        {
            // facing left
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentDoorState == DoorStates.IdleDoor)
        {
                playerAnimator.SetBool("isDoorHit", true);
                currentDoorState = DoorStates.OpeningDoor;
        }
    }
}