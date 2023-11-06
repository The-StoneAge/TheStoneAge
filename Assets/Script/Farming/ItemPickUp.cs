using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public float pickupRadius = 5f;
    public LayerMask itemLayer;
    public Button pickupButton;
    public Text inventoryText;

    private GameObject nearbyItem;

    void Start()
    {
        pickupButton.gameObject.SetActive(false);
        inventoryText.text = "Inventory: 0";
        pickupButton.onClick.AddListener(PickupItem);
    }

    void Update()
    {
        CheckForNearbyItems();
    }

    void CheckForNearbyItems()
    {
        // SphereCast�� ����Ͽ� �ֺ��� �������� �ִ��� Ȯ��
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, pickupRadius, transform.forward, out hit, Mathf.Infinity, itemLayer))
        {
            nearbyItem = hit.collider.gameObject;

            // �������� �±׿� ���� ��ư�� Ȱ��ȭ
            if (nearbyItem.CompareTag("Branch") || nearbyItem.CompareTag("RockCluster") || nearbyItem.CompareTag("Fruit"))
            {
                ActivateButton();
            }
            else
            {
                DeactivateButton();
            }
        }
        else
        {
            // �ֺ��� �������� ������ ��ư ��Ȱ��ȭ
            DeactivateButton();
            nearbyItem = null;
        }
    }


    void ActivateButton()
    {
        pickupButton.gameObject.SetActive(true);
    }

    void DeactivateButton()
    {
        pickupButton.gameObject.SetActive(false);
    }

    // PickUp ��ư�� onclick ����
    void PickupItem()
    {
        // �������� �ֿ��� ���� ����
        if (nearbyItem != null)
        {
            // �������� �±׿� ������ �޽����� ȭ�鿡 ǥ��
            string itemTag = nearbyItem.tag;
            Debug.Log(itemTag + " +1"); // �����δ� �� �κ��� ���ϴ� ���·� ȭ�鿡 ǥ���ϴ� ������� ����

            if (nearbyItem.CompareTag("Branch"))
            {
                inventoryText.text = "�������� +1";
                inventoryText.gameObject.SetActive(true);
                // �κ��丮�� �������� �߰�
            }

            else if (nearbyItem.CompareTag("RockCluster"))
            {
                inventoryText.text = "�� ���� +1";
                inventoryText.gameObject.SetActive(true);
                // �κ��丮�� �� ���� �߰�
            }

            else if (nearbyItem.CompareTag("Fruit"))
            {
                inventoryText.text = "���� +1";
                inventoryText.gameObject.SetActive(true);
                // �κ��丮�� ���� �߰�
            }

            int inventoryCount = int.Parse(inventoryText.text.Substring("Inventory: ".Length));
            inventoryCount++;
            inventoryText.text = "Inventory: " + inventoryCount;

            // ������ ���� �� ��ư ��Ȱ��ȭ
            Destroy(nearbyItem);
            DeactivateButton();
        }
    }
}
