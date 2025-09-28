using UnityEngine;

public class Car : MonoBehaviour
{
    private void Start()
    {
        // 자식 3개 가져오기
        int childCount = transform.childCount;

        if (childCount == 0) return;

        // 0~childCount-1 사이에서 랜덤으로 하나 선택
        int randomIndex = Random.Range(0, childCount);

        // 자식 전체 비활성화 후, 선택된 자식만 활성화
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == randomIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerDieSystem>().PlayerDie();
        }
    }
}
