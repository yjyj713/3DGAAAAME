using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI infoText;            // UI 텍스트 객체
    public float maxRaycastDistance = 120f; // Raycast 최대 거리 설정
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //레이캐스트와 충돌하는 물체의 정보를 담을 변수

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit)) 
            //out hit : hit 안에 들어있는 정보를 꺼내오겠다.라는 뜻
        {
            if(hit.transform.CompareTag("Campfire"))
            {
                infoText.text = "This is a campfire. I think it's going to hurt.";

            }
            else if (hit.transform.CompareTag("JumpPad"))
            {
                infoText.text = "It's a jumping platform. Let's go up.";
            }
            else if (hit.transform.CompareTag("SpeedItem"))
            {
                infoText.text = "This is a speed item. The speed of movement increases for a while..";
            }
            else
            {
                infoText.text = "";
            }
        }
        else
        {
            infoText.text = "";
        }
    }
}
