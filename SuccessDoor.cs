using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessDoor : MonoBehaviour
{
    public shared ss;
    public GameObject player;
    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine("awaitDoor");
    }
    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }
    private IEnumerator awaitDoor()
    {
        for (int i = 0; i < 150; i++)
        {
            if (i == 149)
            {
                Debug.Log("ok");
                ss.nextLevel();
            }
            yield return null;
        }

    }
}