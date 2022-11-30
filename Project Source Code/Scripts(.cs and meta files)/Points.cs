using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] GameObject GM;
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    // When colliding with a point this adds the points to the Game Manager, updates points remaining, and destroys the object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            GM.GetComponent<Score>().score += 100;
            GM.GetComponent<WonLevel>().pointsRemaining--;
            Destroy(collision.gameObject);
        }
    }
}
