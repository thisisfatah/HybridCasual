using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSystemEnemy : MonoBehaviour
{
    [SerializeField] Transform[] enemyPos;
    [SerializeField] GameObject[] enemyObject;

    public List<Enemy> SpawnNewEnemy()
    {
        int randomIndex = Random.Range(1, enemyPos.Length);
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < randomIndex; i++)
        {
            GameObject spawnNewEnemy = Instantiate(enemyObject[Random.Range(0, enemyObject.Length)],
                enemyPos[i].position,
                new Quaternion(0.0f, 180.0f, 0.0f, 0.0f),
                enemyPos[i]);

            Enemy enemy = spawnNewEnemy.GetComponent<Enemy>();
            enemies.Add(enemy);
        }

        return enemies;
    }
}