using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject clone;
    [SerializeField] float timeToSpawn;
    private float currentTime = 0;
    GameObject GM;
    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    // Handles spawn times of enemies, slowing them temporarily to make sure they don't overlap in the game
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToSpawn)
        {
            Spawn();
            currentTime = 0;
        }
        if (currentTime <= 1f)
        {
            GetComponent<EnemyNavigation>().speed = Random.Range(0.2f, 1.2f);
        }
        else
        {
            GetComponent<EnemyNavigation>().speed = 1.875f;
        }
    }
    // Spawns the enemies
    private void Spawn()
    {
        GameObject cloneObj = Instantiate(clone, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        GM.GetComponent<Reset>().SetCloneEnemies(cloneObj);
    }
}
