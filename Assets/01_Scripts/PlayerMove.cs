using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerMove : MonoBehaviour
{
    Vector3 moveDirection;
    public float jumpPower = 1f; // 점프 높이
    public float duration = 0.3f; // 이동 애니메이션 시간
    public int jumpCount = 1; // 점프 횟수 (1이면 딱 한번 뜀)
    public int deathCount = 0;

    private bool isJumping = false; // 점프 중인지 여부를 저장할 변수

    MapCreator mapCreator;

    private void Start()
    {
        mapCreator = FindObjectOfType<MapCreator>();
    }


    public void OnArrows(InputAction.CallbackContext context)
    {
        if (context.performed) // 누르는 순간만 반응
        {
            if (context.performed) // 누르는 순간만 반응
            {
                Vector2 input = context.ReadValue<Vector2>();
                // 위쪽 화살표 || W 키를 눌렀을 때
                if (input.y > 0 && !isJumping)
                {
                    mapCreator.DestroyAndCreateTile();
                }
                if (input.y < 0 && !isJumping)
                {
                    mapCreator.ReverseDestroyAndCreateTile();
                }
                MovePlayer(input);
            }
        }
    }

    public void OnWASD(InputAction.CallbackContext context)
    {
        if (context.performed) // 누르는 순간만 반응
        {
            if (context.performed) // 누르는 순간만 반응
            {
                Vector2 input = context.ReadValue<Vector2>();
                if (input.y > 0 && !isJumping)
                {
                    mapCreator.DestroyAndCreateTile();
                }
                if (input.y < 0 && !isJumping)
                {
                    mapCreator.ReverseDestroyAndCreateTile();
                }
                if (input.y < 0 && !isJumping)
                {
                    deathCount++;
                }
                MovePlayer(input);
            }
        }
    }

    private void MovePlayer(Vector2 input)
    {
        if (input == Vector2.zero) return;
        if (isJumping) return;

        Vector3 moveOffset = new Vector3(input.x * 1f, 0, 0);
        Vector3 targetPos = transform.position + moveOffset;

        isJumping = true;

        transform.DOJump(targetPos, jumpPower, jumpCount, duration).OnComplete(() => isJumping = false);
    }
}
