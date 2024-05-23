using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 2.5f;

    public List<GameObject> obstacles = new List<GameObject>();

    private float timer;

    private void Start()
    {
        timer = spawnRate; // 시작할 때 타이머를 초기화합니다.
    }

    private void Update()
    {
        if (GameManager.Instance.GameStart)
        {
            timer -= Time.deltaTime; // 프레임마다 타이머를 감소시킵니다.

            if (timer <= 0f)
            {
                Spawn(); // 타이머가 0이 되면 Spawn 메서드를 호출합니다.
                timer = spawnRate; // 타이머를 리셋합니다.
            }
        }
    }

    private void Spawn()
    {
        GameObject obstacle = Instantiate(prefab, transform.position, Quaternion.identity);
        obstacles.Add(obstacle);
    }
}
