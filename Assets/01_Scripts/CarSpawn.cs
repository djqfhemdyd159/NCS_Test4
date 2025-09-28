using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("자동차 프리팹")]
    public GameObject carPrefab;

    [Header("설정")]
    public float moveSpeed = 5f;             // 자동차 이동 속도
    public float moveDistance = 40f;         // x축 이동 거리 기준 삭제
    public float spawnY = 1f;                // 생성 높이
    public float spawnIntervalMin = 3f;      // 최소 생성 간격
    public float spawnIntervalMax = 6f;      // 최대 생성 간격

    private void Start()
    {
        StartCoroutine(SpawnCars());
    }

    private IEnumerator SpawnCars()
    {
        while (true)
        {
            SpawnCar();

            // 랜덤 생성 간격
            float interval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnCar()
    {
        if (carPrefab == null) return;

        // 스폰 위치 (월드 기준)
        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, transform.position.z);

        // 먼저 부모 없이 생성 (월드 기준)
        GameObject car = Instantiate(carPrefab, spawnPos, carPrefab.transform.rotation);

        // 월드 스케일 그대로 유지
        car.transform.localScale = carPrefab.transform.localScale;

        // 부모에 붙이기 (localScale 조정 없이)
        car.transform.SetParent(transform, true); // true = worldPositionStays
    }

}
