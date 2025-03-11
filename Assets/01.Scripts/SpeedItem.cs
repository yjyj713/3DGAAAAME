using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedItem", menuName = "Items/SpeedItem")]
public class SpeedItem : ScriptableObject
{
    public string itemName;     // ������ �̸�
    public string description;  // ������ ����
    public float speedBoost;    // �ӵ� ������
    public float duration;      // �ӵ� ���� �ð�
}
