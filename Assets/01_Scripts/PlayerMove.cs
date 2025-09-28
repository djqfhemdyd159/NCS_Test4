using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower = 1f;      // 점프 높이
    public float duration = 0.3f;     // 점프 애니메이션 시간
    public int jumpCount = 1;         // 점프 횟수
    public int deathCount = 3;

    private bool isJumping = false;   // 점프 중 여부
    private MapCreator mapCreator;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        mapCreator = FindObjectOfType<MapCreator>();
        animator = GetComponentInChildren<Animator>();
    }

    // 화살표 입력
    public void OnArrows(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            TryMove(input);
        }
    }

    // WASD 입력
    public void OnWASD(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            TryMove(input);
        }
    }

    private void TryMove(Vector2 input)
    {
        if (input == Vector2.zero) return;
        if (isJumping) return;

        // 좌우 이동
        if (input.x != 0)
        {
            Vector3 direction = new Vector3(input.x, 0, 0);

            if (CanMove(direction, 1f))
            {
                MoveSide(input.x);
            }
        }

        // 앞으로 이동
        if (input.y > 0)
        {
            Vector3 direction = Vector3.forward;

            if (CanMove(direction, 2f))
            {
                JumpInPlace();
                mapCreator.DestroyAndCreateTile();
            }
        }

        // 뒤로 이동
        else if (input.y < 0)
        {
            Vector3 direction = Vector3.back;

            if (CanMove(direction, 2f))
            {
                JumpInPlace();
                mapCreator.ReverseDestroyAndCreateTile();
                deathCount--;
                if (deathCount <= 0)
                {
                    FindObjectOfType<PlayerDieSystem>().PlayerDie();
                }
            }
        }
    }

    // 좌우 이동
    private void MoveSide(float xInput)
    {
        Vector3 moveOffset = new Vector3(xInput * 1f, 0, 0);
        Vector3 targetPos = transform.position + moveOffset;

        isJumping = true;
        animator.SetBool("IsJumping", true); // 점프 시작 시

        transform.DOJump(targetPos, jumpPower, jumpCount, duration)
            .OnComplete(() =>
            {
                isJumping = false;
                animator.SetBool("IsJumping", false); // 점프 끝나면
            });
    }

    // 제자리 점프
    private void JumpInPlace()
    {
        isJumping = true;
        animator.SetBool("IsJumping", true); // 점프 시작 시

        transform.DOJump(transform.position, jumpPower, jumpCount, duration)
            .OnComplete(() =>
            {
                isJumping = false;
                animator.SetBool("IsJumping", false); // 점프 끝나면
            });
    }

    // 이동 가능 여부 체크
    private bool CanMove(Vector3 direction, float distance)
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Objects"))
            {
                return false;
            }
        }
        return true;
    }
}
