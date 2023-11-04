using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    public Canvas uiCanvasPrefab; // UI ĵ���� ������
    public Canvas uiCanvas;
    public string itemName; // �浹�� ������Ʈ�� �̸�

    void Start()
    {
        // ���� �������� ���ٸ� ���ο� �ν��Ͻ��� �����Ͽ� �Ҵ�
        if (uiCanvasPrefab != null && uiCanvas == null)
        {
            uiCanvas = Instantiate(uiCanvasPrefab);
            uiCanvas.enabled = false;
        }
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ����ĳ��Ʈ�� �̿��Ͽ� �������� Ŭ���ߴ��� Ȯ��
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Item"))
            {
                // �������� Ŭ���ϸ� �˸�â UI ĵ���� Ȱ��ȭ
                uiCanvas.enabled = true;
                // �浹�� ������Ʈ�� �̸�
                itemName = hit.collider.gameObject.name;
            }
        }
    }

    public void DestroyItem(GameObject item)
    {
        Destroy(item.gameObject);
    }
}
