using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // �����뿡�� ĳ���Ϳ� ���� ���� ũ��

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // �����뿡 ����� �� �������� ���� ���մϴ�.
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
