using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public SpeedItem speedItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().UseSpeedItem(speedItem);
            Destroy(gameObject);  // �������� ��� �� ����
        }
    }
}
