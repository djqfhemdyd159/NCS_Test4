using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapCreator : MonoBehaviour
{
    public List<GameObject> tilePrefab;
    public List<GameObject> tiles;
    public GameObject startTilePrefab;
    public Transform playerTransform;

    public float moveDuration = 0.3f;

    void Start()
    {
        CreateTile();
    }

    public void CreateTile()
    {
        for (int i = -10; i <= 10; i++)
        {
            GameObject tile;

            if (i == 0)
            {
                tile = Instantiate(startTilePrefab, new Vector3(0, -0.5f, playerTransform.position.z), Quaternion.identity);
            }
            else
            {
                float zPos = playerTransform.position.z + i * 2;
                tile = Instantiate(tilePrefab[Random.Range(0, tilePrefab.Count)], new Vector3(0, -0.5f, zPos), Quaternion.identity);
            }

            tiles.Add(tile);
        }
    }

    public void DestroyAndCreateTile()
    {
        GameObject lastTile = tiles[tiles.Count - 1];
        Vector3 lastPos = lastTile.transform.position;

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].transform.DOMove(tiles[i].transform.position + new Vector3(0, 0, -2f), moveDuration);
        }

        // 새 타일은 이동이 끝난 뒤 생성되게끔 지연 호출
        DOVirtual.DelayedCall(moveDuration, () =>
        {
            GameObject tile = Instantiate(tilePrefab[Random.Range(0, tilePrefab.Count)], new Vector3(0, -0.5f, lastPos.z), Quaternion.identity);
            tiles.Add(tile);
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
        });
    }

    public void ReverseDestroyAndCreateTile()
    {
        GameObject firstTile = tiles[0];
        Vector3 firstPos = firstTile.transform.position;

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].transform.DOMove(tiles[i].transform.position + new Vector3(0, 0, 2f), moveDuration);
        }

        // 이동 끝난 뒤 새 타일 생성
        DOVirtual.DelayedCall(moveDuration, () =>
        {
            GameObject tile = Instantiate(tilePrefab[Random.Range(0, tilePrefab.Count)], new Vector3(0, -0.5f, firstPos.z), Quaternion.identity);
            tiles.Insert(0, tile);
            Destroy(tiles[tiles.Count - 1]);
            tiles.RemoveAt(tiles.Count - 1);
        });
    }
}
