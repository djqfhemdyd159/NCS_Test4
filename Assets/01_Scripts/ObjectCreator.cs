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

        // x 범위 계산 (발판 안쪽으로 제한)
        float minX = Mathf.Max(-xRange, -platformScale.x / 2f);
        float maxX = Mathf.Min(xRange, platformScale.x / 2f);

        // 중복 방지용 HashSet
        HashSet<float> usedX = new HashSet<float>();

        int created = 0;
        int safety = 0; // 무한 루프 방지

        while (created < obstacleCount && safety < 500)
        {
            safety++;

            // x 좌표: 2~3 사이 랜덤 간격
            float gap = Random.Range(2f, 3f);

            // 가능한 steps 범위
            int steps = Mathf.FloorToInt((maxX - minX) / gap);
            int randStep = Random.Range(0, steps + 1);

            // 실제 X 좌표
            float randomX = minX + randStep * gap;

            // 플레이어와 겹치면 스킵
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

            // z 좌표: 발판 중심
            float zPos = platformPos.z;

            // y 좌표: 발판 위
            float yPos = platformPos.y + platformScale.y / 2f; // 장애물 높이 기준

            Vector3 spawnPos = new Vector3(randomX + platformPos.x, yPos, zPos);

            // 장애물 생성 (발판 자식)
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, transform);

            // 장애물 스케일 적용
            obstacle.transform.localScale = new Vector3(1f / 200f, 0.5f, 0.25f);

            // 사용된 좌표 기록
            usedX.Add(randomX);
            created++;
        }
    }
}
