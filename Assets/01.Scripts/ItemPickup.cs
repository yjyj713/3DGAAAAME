using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public SpeedItem speedItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().UseSpeedItem(speedItem);
            Destroy(gameObject);  // 아이템을 사용 후 삭제
        }
    }
}
