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
        timer = spawnRate; // ������ �� Ÿ�̸Ӹ� �ʱ�ȭ�մϴ�.
    }

    private void Update()
    {
        if (GameManager.Instance.GameStart)
        {
            timer -= Time.deltaTime; // �����Ӹ��� Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.

            if (timer <= 0f)
            {
                Spawn(); // Ÿ�̸Ӱ� 0�� �Ǹ� Spawn �޼��带 ȣ���մϴ�.
                timer = spawnRate; // Ÿ�̸Ӹ� �����մϴ�.
            }
        }
    }

    private void Spawn()
    {
        GameObject obstacle = Instantiate(prefab, transform.position, Quaternion.identity);
        obstacles.Add(obstacle);
    }
}
