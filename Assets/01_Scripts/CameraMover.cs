using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 0.5f; 

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
    }
}