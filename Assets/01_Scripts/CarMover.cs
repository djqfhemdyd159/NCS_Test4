using UnityEngine;

public class CarMover : MonoBehaviour
{
    [Header("이동 속성")]
    public float speed = 5f;         // 이동 속도
    public float moveDistance = 40f; // 이동할 최대 거리

    private Vector3 startPos;        

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // 앞으로 이동 (transform.forward 방향)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float traveled = Vector3.Distance(startPos, transform.position);
        if (traveled >= moveDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarBreaker"))
        {
            Destroy(gameObject);
        }
    }
}
