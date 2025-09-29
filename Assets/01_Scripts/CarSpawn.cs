using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("자동차 프리팹")]
    public GameObject carPrefab;

    [Header("설정")]
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

        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, transform.position.z);

        GameObject car = Instantiate(carPrefab, spawnPos, carPrefab.transform.rotation);

        car.transform.localScale = carPrefab.transform.localScale;

        car.transform.SetParent(transform, true); // true = worldPositionStays
    }

}
