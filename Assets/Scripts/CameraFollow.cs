using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // �÷��̾��� Transform
    public Vector3 offset;          // �÷��̾�� ī�޶� ���� �Ÿ�
    public float smoothingSpeed = 5f; // ī�޶� �ε巯�� �̵� �ӵ�

    void LateUpdate()
    {
        // ī�޶��� ��ǥ ��ġ ���
        Vector3 desiredPosition = player.position + offset;

        // �ε巴�� �̵��ϵ��� ����
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothingSpeed);
    }
}
