using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] private float rad = 14f;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            float alpha = Random.Range(0f, 360f);
            Vector2 temp = new Vector2(Mathf.Cos(alpha), Mathf.Sin(alpha));
            temp = temp * rad + (Vector2)_player.transform.position;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, temp, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
