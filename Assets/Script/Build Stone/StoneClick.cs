using UnityEngine;
using UnityEngine.UI;

public class StoneClick : MonoBehaviour
{
    public GameObject incompleteStone; // �̿ϼ� �� �̹���
    public GameObject completeStone;   // �ϼ��� �� �̹���

    private int clickCount = 0;

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ����ĳ��Ʈ�� �̿��Ͽ� ���� Ŭ���ߴ��� Ȯ��
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Stone"))
            {
                // ���� Ŭ���ϸ� clickCount ����
                clickCount++;

                // Ŭ�� Ƚ���� 10���� �����ϸ� �ϼ��� �� �̹��� Ȱ��ȭ
                if (clickCount >= 10)
                {
                    ActivateCompleteStone();
                }
            }
        }
    }

    void ActivateCompleteStone()
    {
        // �̿ϼ� �� �̹��� ��Ȱ��ȭ
        incompleteStone.SetActive(false);

        // �ϼ��� �� �̹��� Ȱ��ȭ
        completeStone.SetActive(true);
    }
}
