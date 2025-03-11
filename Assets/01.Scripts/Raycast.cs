using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI infoText;            // UI �ؽ�Ʈ ��ü
    public float maxRaycastDistance = 120f; // Raycast �ִ� �Ÿ� ����
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //����ĳ��Ʈ�� �浹�ϴ� ��ü�� ������ ���� ����

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit)) 
            //out hit : hit �ȿ� ����ִ� ������ �������ڴ�.��� ��
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
