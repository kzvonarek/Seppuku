using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "shared", menuName = "ScriptableObjects/shared", order = 1)]
public class shared : ScriptableObject
{

    public Vector3 initialPlayerPos;
    public int currLevel;
    public GameObject player;
    public Animator samuraiAnimation;
    public Animator ninjaAnimation;
    public bool musicBegin;
    public void respawnLocation()
    {
        initialPlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void nextLevel()
    {
        if(currLevel==0)
        {
            levelOne();
        }
        else if(currLevel==1)
        {
            levelTwo();
        }
        else if(currLevel==2)
        {

            levelZero();
        }
        else if(currLevel==3)
        {
            levelZero();
        }
    }
    public void levelZero()
    {
        SceneManager.LoadScene("Level 0");
        currLevel = 0;
    }
    public void levelOne()
    {
        SceneManager.LoadScene("Level 1");
        currLevel = 1;
    }
    public void levelTwo()
    {
        SceneManager.LoadScene("Level 2");
        currLevel = 2;
    }
    public void tutorialLevel()
    {
        SceneManager.LoadScene("Level Tutorial");
        currLevel = 3;
    }

}
