using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform player;
     public float speed;

    // Variables for determining the state of the enemy
    [SerializeField] private bool isRoaming = false;
    [SerializeField] private bool isChasing = false;
    [SerializeField] private bool isStarting = true;
    [SerializeField] private bool isDieing = false;
    [SerializeField] private bool isResetting = false;


    // Variables for time
    [SerializeField] private float timeToStart = 5f;
    [SerializeField] private float timeToRoam = 20f;
    [SerializeField] private float currentTime = 0.0f;
    
    [SerializeField] private bool down = true;
    [SerializeField] private bool isFast = false;

    // Variables for determining target
    private Transform target;
    private Transform lastTarget;
    private Transform lastPath;
    private GameObject start;

    public Vector3 startPos;
    GameObject GM;

    // Sets initial values
    void Start()
    {
        startPos = transform.position;
        target = getTarget();
        lastTarget = null;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        start = GameObject.FindGameObjectWithTag("StartOne");
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    void Update()
    {

        currentTime += Time.deltaTime;
        // If resetting, count down until start, if not:
        // When you reach your target: 
        // Save your last two destinations, this will be used to prevent enemies going back and forth between two points and loop around instead
        // Then set a new target
        if (isResetting)
        {
            target = transform;
            if (currentTime >= 1f)
            {
                isResetting = false;
                isStarting = true;
            }
        }
        else if (Vector2.Distance(transform.position, target.position) < .002f)
        {
            lastPath = lastTarget;
            lastTarget = target;
            target = getTarget();
        }
        // Moves enemy towards target position
        if (!GM.GetComponent<PausedMenu>().isPaused)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


        // Switching between start, roam, and chase
        if (currentTime >= timeToStart && isStarting)
        {
            SwitchToRoam();
        }
        else if (currentTime >= timeToRoam)
        {
            isRoaming = false;
            isChasing = true;
        }
    }
    // Handles a switch to the roam state
    void SwitchToRoam()
    {
        target = GameObject.FindGameObjectWithTag("StartOne").transform;
        isStarting = false;
        isRoaming = true;
        currentTime = 0;
    }
    // Sets the target for the enemy
    Transform getTarget()
    {
        if (isStarting) return getTargetStartState();
        // Send out a raycast in all directions
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);
        RaycastHit2D[] hitsDown = Physics2D.RaycastAll(transform.position, Vector2.down);
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left);
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);

        // Compile the results into one list
        RaycastHit2D[][] allDirs = { hitsUp, hitsDown, hitsLeft, hitsRight };
        List<Transform> possiblePoints = new List<Transform>();


        // Loop through the results one direction at a time
        foreach (RaycastHit2D[] curDir in allDirs)
        {
            for (int i = 1; i < curDir.Length; i++)
            {
                // If you hit a wall, don't bother checking anymore hits in this direction
                if (curDir[i].transform.tag == "Wall")
                {
                    break;
                }
                // If you hit a node, check to make sure it isn't the node you're currently at
                // and that it isn't the node you were just previously at
                // If both conditions are met, add to the list of possible points and check another direction
                else if (curDir[i].transform.tag == "Node")
                {
                    if (curDir[i].transform != target)
                    {
                        possiblePoints.Add(curDir[i].transform);
                        break;
                    }
                }
            }
        }
        // if more than one option, don't go the same way you just came
        // that is unless you're chasing the player
        if (possiblePoints.Count > 1 && !isChasing)
        {
            for (int j = 0; j < possiblePoints.Count; j++)
            {
                if (possiblePoints[j] == lastPath)
                    possiblePoints.RemoveAt(j);
            }
        }
        return getTargetOtherState(possiblePoints);
    }

    // Targeting is different for start state so it is its own method
    private Transform getTargetStartState()
    {
        Transform node = null;
        if (down)
        {
            node = Physics2D.OverlapBox(transform.position + new Vector3(0, -.75f, 0), new Vector2(0.1f, 0.1f), .5f).transform;
            down = false;
        }
        else
        {
            down = true;
            node = Physics2D.OverlapBox(transform.position + new Vector3(0, .75f, 0), new Vector2(0.1f, 0.1f), .5f).transform;
        }
        return node;
    }
    // Handles targeting for roaming, chasing, and dieing states
    private Transform getTargetOtherState(List<Transform> possiblePoints)
    {
        Transform node = null;
        if (isRoaming)
        {
            // Select a random point to travel to from the list of available points
            int j = Random.Range(0, possiblePoints.Count);
            node = possiblePoints[j];
        }
        else if (isChasing)
        {
            float closest = float.MaxValue;
            foreach (Transform point in possiblePoints)
            {
                if (Vector2.Distance(point.transform.position, player.transform.position) < closest)
                {
                    node = point;
                    closest = Vector2.Distance(point.transform.position, player.transform.position);
                }
            }
        } 
        else if (isDieing)
        {
            if (Vector2.Distance(transform.position, start.transform.position) < .002f)
            {
                isStarting = true;
                isDieing = false;
                GetComponent<BoxCollider2D>().enabled = true;
                speed /= 2f;
                return getTargetStartState();
            }
            float closest = float.MaxValue;
            foreach (Transform point in possiblePoints)
            {
                if (Vector2.Distance(point.transform.position, start.transform.position) < closest)
                {
                    node = point;
                    closest = Vector2.Distance(point.transform.position, start.transform.position);
                }
            }
        }
        return node;
    }
    // Ensures all variables are properly set upon death
    public void Die()
    {
        currentTime = -5; 
        isStarting = false;
        isRoaming = false;
        isChasing = false;
        isDieing = true;
        GetComponent<BoxCollider2D>().enabled = false;
        down = true;
        if (isFast) timeToRoam = 5;
        else timeToRoam = 10;
        speed *= 2f;
    }
    // Ensures all variables are properly set upon reset
    public void Reset()
    {
        isStarting = false;
        isRoaming = false;
        isChasing = false;
        if (isDieing) speed /= 2;
        isDieing = false;
        isResetting = true;
        currentTime = 0;
        transform.position = startPos;
        GetComponent<BoxCollider2D>().enabled = true;
        lastPath = null;
        lastTarget = null;
        down = true;
    }
}
