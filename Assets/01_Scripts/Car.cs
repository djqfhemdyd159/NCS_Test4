using UnityEngine;

public class Car : MonoBehaviour
{
    private void Start()
    {
        int childCount = transform.childCount;

        if (childCount == 0) return;

        int randomIndex = Random.Range(0, childCount);

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
