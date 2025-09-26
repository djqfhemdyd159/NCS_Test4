using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    public MapCreator mapCreator;        // 기존 MapCreator 스크립트
    public Transform playerTransform; // player의 Transform
    private float scrollSpeed = 0.5f;       // 타일 이동 속도

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
                // 타일을 뒤로 스크롤
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
