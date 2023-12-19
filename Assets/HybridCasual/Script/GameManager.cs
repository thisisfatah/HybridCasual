using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] SpawnSystemEnemy spawnEnemy;
    [SerializeField] GameObject shootUI;
    [SerializeField] List<GameObject> moveUI;
    [SerializeField] GameObject panelUI;

    List<Enemy> enemies = new List<Enemy>();
    public Character character;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnNewEnemy();
    }

    public void SpawnNewEnemy()
    {
        List<Enemy> list = spawnEnemy.SpawnNewEnemy();
        for (int i = 0; i < list.Count; i++)
        {
            enemies.Add(list[i]);
        }
        for (int i = 0;i < enemies.Count; i++)
        {
            enemies[i].MoveStepForward();
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count <= 0)
        {
            enemies.Clear();
        }
    }

    public void ShowUIShoot(bool isShow)
    {
        shootUI.SetActive(isShow);
    }

    public void GameOver()
    {
        shootUI.SetActive(false);
        for (int i = 0; i < moveUI.Count; i++)
        {
            moveUI[i].SetActive(false);
        }
        panelUI.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
