using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    GameObject GM;

    public Vector3 startPos;

    // Sets initial values
    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    // Moves enemy towards target position
    void Update()
    {
        if (!GM.GetComponent<PausedMenu>().isPaused)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}