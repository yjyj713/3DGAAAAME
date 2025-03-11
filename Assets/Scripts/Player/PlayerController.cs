using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rigidbody;

    // 속도 증가 관련 변수
    private bool isSpeedBoosted = false;   // 속도 부스트 여부
    private float speedBoostMultiplier = 1f; // 속도 부스트 배수
    private float speedBoostDuration = 5f;   // 속도 부스트 지속 시간
    private Coroutine speedBoostCoroutine;   // 속도 부스트 코루틴

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        // Rigidbody 설정
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed * speedBoostMultiplier;  // 속도 부스트를 고려한 이동

        // Y축 (수직 방향) 속도는 중력에 의해 자연스럽게 결정되도록, x, z 속도만 수정
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력이 시작될 때
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    // 속도 부스트 아이템 사용
    public void UseSpeedItem(SpeedItem speedItem)
    {
        if (!isSpeedBoosted)
        {
            // 기존에 속도 부스트가 적용 중이지 않다면, 새로 시작
            if (speedBoostCoroutine != null)
            {
                StopCoroutine(speedBoostCoroutine);
            }

            speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(speedItem));
        }
    }

    private IEnumerator SpeedBoostCoroutine(SpeedItem speedItem)
    {
        isSpeedBoosted = true;
        speedBoostMultiplier = 1 + speedItem.speedBoost;  // 속도 증가 배수

        yield return new WaitForSeconds(speedItem.duration);  // 지정된 시간 후

        speedBoostMultiplier = 1f;  // 속도 원래대로 되돌리기
        isSpeedBoosted = false;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray (transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray (transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray (transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray (transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
