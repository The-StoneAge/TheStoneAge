using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ItemData item;

    private void OnMouseDown()
    {
        bool isPanelActive = UIManager.Instance.IsInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
            bool addedToExistingStack = false;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Stackable(item))
                {
                    // ���� �������� �̹� ������ ������ �߰�
                    items[i].AddQuantity();
                    addedToExistingStack = true;
                    break;
                }
            }

            // ���� �������� ��� �� ���Կ� �߰��ϴ� ���
            if (!addedToExistingStack)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemData == null)
                    {
                        items[i].itemData = item;
                        item = null;
                        break;
                    }
                }
            }

            // �κ��丮�� �ٽ� �������ϰ� ���� ���� ������Ʈ�� �����մϴ�.
            UIManager.Instance.RenderInventory();
            Destroy(gameObject);
        }
    }
}
