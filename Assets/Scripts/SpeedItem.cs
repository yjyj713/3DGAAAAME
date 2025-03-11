using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedItem", menuName = "Items/SpeedItem")]
public class SpeedItem : ScriptableObject
{
    public string itemName;     // 아이템 이름
    public string description;  // 아이템 설명
    public float speedBoost;    // 속도 증가량
    public float duration;      // 속도 증가 시간
}
