using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    public MapCreator mapCreator;        // ���� MapCreator ��ũ��Ʈ
    public Transform playerTransform; // player�� Transform
    private float scrollSpeed = 0.5f;       // Ÿ�� �̵� �ӵ�

    void Update()
    {
        ScrollTiles();
        ScrollPlayer();
    }

    private void ScrollTiles()
    {
        for (int i = 0; i < mapCreator.tiles.Count; i++)
        {
            if (mapCreator.tiles[i] != null)
            {
                // Ÿ���� �ڷ� ��ũ��
                mapCreator.tiles[i].transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    private void ScrollPlayer()
    {
        if (playerTransform != null)
        {
            playerTransform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);
        }
    }
}
