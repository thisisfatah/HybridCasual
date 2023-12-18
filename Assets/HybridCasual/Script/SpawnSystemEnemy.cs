using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemEnemy : MonoBehaviour
{
    [SerializeField] Transform[] enemyPos;
    [SerializeField] GameObject[] enemyObject;

    public void SpawnNewEnemy()
    {
        for (int i = 0; i < enemyPos.Length; i++)
        {

        }
        GameObject spawnNewEnemy = Instantiate(enemyObject[Random.Range(0, enemyObject.Length - 1)], enemyPos[Random.Range(0, enemyPos.Length - 1)], true);
    }
}
