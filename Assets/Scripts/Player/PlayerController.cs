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

    // �ӵ� ���� ���� ����
    private bool isSpeedBoosted = false;   // �ӵ� �ν�Ʈ ����
    private float speedBoostMultiplier = 1f; // �ӵ� �ν�Ʈ ���
    private float speedBoostDuration = 5f;   // �ӵ� �ν�Ʈ ���� �ð�
    private Coroutine speedBoostCoroutine;   // �ӵ� �ν�Ʈ �ڷ�ƾ

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        // Rigidbody ����
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
        dir *= moveSpeed * speedBoostMultiplier;  // �ӵ� �ν�Ʈ�� ����� �̵�

        // Y�� (���� ����) �ӵ��� �߷¿� ���� �ڿ������� �����ǵ���, x, z �ӵ��� ����
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
        // �Է��� ���۵� ��
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

    // �ӵ� �ν�Ʈ ������ ���
    public void UseSpeedItem(SpeedItem speedItem)
    {
        if (!isSpeedBoosted)
        {
            // ������ �ӵ� �ν�Ʈ�� ���� ������ �ʴٸ�, ���� ����
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
        speedBoostMultiplier = 1 + speedItem.speedBoost;  // �ӵ� ���� ���

        yield return new WaitForSeconds(speedItem.duration);  // ������ �ð� ��

        speedBoostMultiplier = 1f;  // �ӵ� ������� �ǵ�����
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
