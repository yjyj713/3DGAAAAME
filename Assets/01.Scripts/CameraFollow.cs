using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // 플레이어의 Transform
    public Vector3 offset;          // 플레이어와 카메라 간의 거리
    public float smoothingSpeed = 5f; // 카메라 부드러운 이동 속도

    void LateUpdate()
    {
        // 카메라의 목표 위치 계산
        Vector3 desiredPosition = player.position + offset;

        // 부드럽게 이동하도록 보간
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothingSpeed);
    }
}
