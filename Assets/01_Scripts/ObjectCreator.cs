using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    [Header("장애물 프리팹")]
    public GameObject obstaclePrefab;

    [Header("설정")]
    public int obstacleCount = 6;     // 생성할 장애물 개수
    public float xRange = 20f;         // x ± 범위
    public Transform player;           // 플레이어 (위치 제외용)

    private void Start()
    {
        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        if (obstaclePrefab == null) return;

        Vector3 platformPos = transform.position;
        Vector3 platformScale = transform.localScale;

        float minX = Mathf.Max(-xRange, -platformScale.x / 2f);
        float maxX = Mathf.Min(xRange, platformScale.x / 2f);

        HashSet<float> usedX = new HashSet<float>();

        int created = 0;
        int safety = 0;

        while (created < obstacleCount && safety < 500)
        {
            safety++;

            float gap = Random.Range(2f, 3f);

            int steps = Mathf.FloorToInt((maxX - minX) / gap);
            int randStep = Random.Range(0, steps + 1);

            float randomX = minX + randStep * gap;

            if (player != null)
            {
                float playerX = player.position.x;
                if (Mathf.Abs(randomX - playerX) < 0.1f) continue;
            }

            // 중복된 위치면 스킵
            bool isDuplicate = false;
            foreach (float used in usedX)
            {
                if (Mathf.Abs(randomX - used) < 0.1f)
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (isDuplicate) continue;

            float zPos = platformPos.z;

            float yPos = platformPos.y + platformScale.y / 2f;

            Vector3 spawnPos = new Vector3(randomX + platformPos.x, yPos, zPos);

            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, transform);

            obstacle.transform.localScale = new Vector3(1f / 200f, 0.5f, 0.25f);

            usedX.Add(randomX);
            created++;
        }
    }
}
