using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    GameObject ghost;
    public GameObject[] enemies;
    public List<GameObject> cloneEnemies = null;
    GameObject player;
    // Ensures the time is moving properly
    private void Start()
    {
        Time.timeScale = 1f;
    }
    // Sets initial values
    public void SetValues()
    {
        ghost = GameObject.Find("GhostEnemy");
        ghost = GameObject.Find("GhostEnemy");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        cloneEnemies = new List<GameObject>();
        cloneEnemies.Add(GameObject.FindGameObjectWithTag("Clone"));
    }
    // Sets initial values for clone enemies
    public void SetCloneEnemies(GameObject clone)
    {
        cloneEnemies.Add(clone);
    }
    // Ensures values are properly set whenever a restart is needed
    public void ResetEverything()
    {
        ghost.transform.position = ghost.GetComponent<GhostEnemy>().startPos;
        foreach(GameObject e in enemies)
        {
            e.GetComponent<EnemyNavigation>().Reset();
        }
        for (int i = cloneEnemies.Count - 1; i > 0; i--)
        {
            GameObject tmp = cloneEnemies[i];
            cloneEnemies.RemoveAt(i);
            Destroy(tmp);
        }
        if(cloneEnemies[0] != null)
            cloneEnemies[0].GetComponent<EnemyNavigation>().Reset();
        player.GetComponent<Player>().Reset();
    }
}
