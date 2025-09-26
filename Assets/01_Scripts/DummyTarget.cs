using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public Transform player;
    public float forwardSpeed = 0.1f;

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + Vector3.forward * forwardSpeed * Time.time;
        }
    }
}