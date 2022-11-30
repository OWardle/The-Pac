using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{


    public float speed;
    public float distToTravel;

    Vector2 potentialPos;
    public Vector2 target;

    bool movingUp = false;
    bool movingDown = false;
    bool movingLeft = false;
    bool movingRight = false;

    public float wallDetectorSize;
    public bool hitWall = false;
    public LayerMask Walls;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip moveSound;

    // Sets initial values
    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        // always move to your target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // if player input, move
        if (movingDown || movingUp || movingLeft || movingRight)
        {
            StartCoroutine("Move");
        }

        // setting moving direction
        if (Input.GetKeyDown(KeyCode.W))
        {
            movingUp = true;
            movingDown = false;
            movingRight = false;
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0, 0, -90);
            playMovementSound();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movingDown = true;
            movingUp = false;
            movingRight = false;
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            playMovementSound();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movingLeft = true;
            movingDown = false;
            movingUp = false;
            movingRight = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            playMovementSound();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movingRight = true;
            movingDown = false;
            movingUp = false;
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            playMovementSound();
        }
    }
    // Plays the movement sound effect if it enabled in preferences and avoid overlapping sounds
    void playMovementSound()
    {
        if(PlayerPrefs.GetInt("SoundEffects", 1) == 1 && source.isPlaying == false)
        {
            source.Play();
            source.loop = true;
        }
    }
    // this IEnumerator repeats every second to set character target
    IEnumerator Move()
    {
        // If your at your target
        if (Vector2.Distance(transform.position, target) < 0.002f)
        {
            // setting a new potential target position
            if (movingUp)
                potentialPos = new Vector2(transform.position.x, transform.position.y + distToTravel);
            else if (movingDown)
                potentialPos = new Vector2(transform.position.x, transform.position.y - distToTravel);
            else if (movingRight)
                potentialPos = new Vector2(transform.position.x + distToTravel, transform.position.y);
            else if (movingLeft)
                potentialPos = new Vector2(transform.position.x - distToTravel, transform.position.y);

            // detecting wall collision
            if (!Physics2D.OverlapBox(potentialPos, new Vector2(wallDetectorSize, wallDetectorSize), .5f, Walls))
            {
                target = potentialPos;
                hitWall = false;
            }
            else
            {
                target = transform.position;
                movingRight = false;
                movingUp = false;
                movingDown = false;
                movingLeft = false;
                source.Stop();
                yield break;
            }
        }
        yield return new WaitForSeconds(1);
    }
    // Just a visualizer for the editor so you can see the WallDetectorSize in action
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(potentialPos, new Vector3(wallDetectorSize, wallDetectorSize, wallDetectorSize));
    }
}
