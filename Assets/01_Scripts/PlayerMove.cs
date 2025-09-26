using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerMove : MonoBehaviour
{
    Vector3 moveDirection;
    public float jumpPower = 1f; // ���� ����
    public float duration = 0.3f; // �̵� �ִϸ��̼� �ð�
    public int jumpCount = 1; // ���� Ƚ�� (1�̸� �� �ѹ� ��)
    public int deathCount = 0;

    private bool isJumping = false; // ���� ������ ���θ� ������ ����

    MapCreator mapCreator;

    private void Start()
    {
        mapCreator = FindObjectOfType<MapCreator>();
    }


    public void OnArrows(InputAction.CallbackContext context)
    {
        if (context.performed) // ������ ������ ����
        {
            if (context.performed) // ������ ������ ����
            {
                Vector2 input = context.ReadValue<Vector2>();
                // ���� ȭ��ǥ || W Ű�� ������ ��
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
        if (context.performed) // ������ ������ ����
        {
            if (context.performed) // ������ ������ ����
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
