using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 점프대에서 캐릭터에 가할 힘의 크기

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // 점프대에 닿았을 때 순간적인 힘을 가합니다.
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
