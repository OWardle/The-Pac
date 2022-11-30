using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform startNodeParent;
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform fastEnemy;
    [SerializeField] private Transform multiplyingEnemy;

    private Transform[] startNodes = new Transform[6];

    public int level = 1;
    // Handles spawning based on current level
    public void Spawn()
    {
        startNodeParent = GameObject.FindGameObjectWithTag("StartNode").transform;
        for (int i = 0; i < startNodeParent.childCount; i++)
        {
            startNodes[i] = startNodeParent.GetChild(i);
        }
        if (level == 1)
        {
            Instantiate(enemy, new Vector3(startNodes[0].position.x, startNodes[0].position.y, startNodes[0].position.z), Quaternion.identity);
            Instantiate(fastEnemy, new Vector3(startNodes[2].position.x, startNodes[2].position.y, startNodes[2].position.z), Quaternion.identity);
            Instantiate(enemy, new Vector3(startNodes[4].position.x, startNodes[4].position.y, startNodes[4].position.z), Quaternion.identity);
            GetComponent<Reset>().SetValues();
        } else if (level == 2)
        {
            Instantiate(fastEnemy, new Vector3(startNodes[0].position.x, startNodes[0].position.y, startNodes[0].position.z), Quaternion.identity);
            Instantiate(fastEnemy, new Vector3(startNodes[2].position.x, startNodes[2].position.y, startNodes[2].position.z), Quaternion.identity);
            Instantiate(fastEnemy, new Vector3(startNodes[4].position.x, startNodes[4].position.y, startNodes[4].position.z), Quaternion.identity);
            GetComponent<Reset>().SetValues();
        } else if (level >= 3)
        {
            Instantiate(fastEnemy, new Vector3(startNodes[0].position.x, startNodes[0].position.y, startNodes[0].position.z), Quaternion.identity);
            Instantiate(multiplyingEnemy, new Vector3(startNodes[2].position.x, startNodes[2].position.y, startNodes[2].position.z), Quaternion.identity);
            Instantiate(fastEnemy, new Vector3(startNodes[4].position.x, startNodes[4].position.y, startNodes[4].position.z), Quaternion.identity);
            GetComponent<Reset>().SetValues();
        }
    }
}
