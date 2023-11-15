using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class StoneClick : MonoBehaviour
{
    public Slider clickCountSlider;    // Ŭ�� Ƚ���� ǥ���� �����̴�
    public TMP_Text completeText;      // �ϼ� �ؽ�Ʈ
    public GameObject[] handAxeItems;
    public GameObject[] stoneAxeItems;
    public GameObject[] spearItems;
    public GameObject[] arrowItems;

    public GameObject[] completeItems;
    public static int num;
    public EquipmentData[] itemData;
    public GameObject Buttonobject;

    public ParticleSystem StoneStun;
    private bool hasActivatedCompleteStone = false;

    private void Start()
    {
        if (num == 0)
        {
            foreach (GameObject item in handAxeItems)
                item.SetActive(true);
            UIManager.Instance.maxCount -= 5;
            InvokeRepeating("InstantiateButton", 1f, 4f);
        }
        if (num == 1)
        {
            foreach (GameObject item in stoneAxeItems)
                item.SetActive(true);
            UIManager.Instance.maxCount -= 2;
            InvokeRepeating("InstantiateButton", 0.8f, 3f);
        }
        if (num == 2)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 2;
            InvokeRepeating("InstantiateButton", 0.7f, 2f);
        }
        if (num == 3)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 5;
            InvokeRepeating("InstantiateButton", 0.5f, 1f);
        }
    }

    void Update()
    {
        if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount && !hasActivatedCompleteStone)
        {
            ActivateCompleteStone(num);
            CancelInvoke("InstantiateButton");
            hasActivatedCompleteStone = true;
            StartCoroutine(SceneToCave());
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone(int num)
    {
        // �̿ϼ� �� �̹��� ��Ȱ��ȭ
        if (num == 0)
        {
            foreach(GameObject item in handAxeItems)
                item.SetActive(false);
        }
        if (num == 1)
        {
            foreach(GameObject item in stoneAxeItems)
                item.SetActive(false);
        }
        if (num == 2)
        {
            foreach(GameObject item in spearItems)
                item.SetActive(false);
        }
        if (num == 3)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(false);
        }

        // �ϼ� �ؽ�Ʈ
        completeText.gameObject.SetActive(true);

        CameraManager.Instance.MoveCamera(new Vector3(120, 10, 5));

        StoneStun.Play();

        // �ϼ��� �� �̹��� Ȱ��ȭ
        completeItems[num].SetActive(true);
        getItem();
        CameraManager.Instance.MoveCamera(new Vector3(120, 3, 5));
    }

    public void getItem()
    {
        ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        for (int i = 0; i < items.Length; i++)
        {
            if (completeItems[num].name.Contains("HandAxe"))
            {
                if (items[i].itemData == null)
                {
                    Debug.Log("�ָԵ���");
                    newItem(itemData[0]);
                    break;
                }
            }
            if (completeItems[num].name.Contains("StoneAxe"))
            {
                if (items[i].itemData == null)
                {
                    newItem(itemData[1]);
                    break;
                }
            }
            if (completeItems[num].name.Contains("Arrow"))
            {
                if (items[i].itemData == null)
                {
                    newItem(itemData[2]);
                    break;
                }
            }
            if (completeItems[num].name.Contains("Spear"))
            {
                if (items[i].itemData == null)
                {
                    newItem(itemData[3]);
                    break;
                }
            }


        }
    }
    public void newItem(EquipmentData item)
    {
         ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);


        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemData == null)
            {
                Debug.Log("NewItem");
                items[i].itemData = item;
                items[i].AddQuantity();
                item = null;
                break;
            }
            
        }
        UIManager.Instance.RenderInventory();
    }
    private IEnumerator SceneToCave()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Cave");
    }
    // Success ��ư�� onClick���� ����
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        UIManager.Instance.clickCount += 1;

        // �����̴� ������Ʈ
        UpdateSlider();

        // num�� ���� �ִϸ��̼� �ӵ� ����
        float animationSpeed = 1.0f; // �⺻ �ӵ�

        switch (num)
        {
            case 0:
                animationSpeed = 1.0f; // num == 0�� ���� �ӵ� ����
                break;
            case 1:
                animationSpeed = 1.5f; // num == 1�� ���� �ӵ� ����
                break;
            case 2:
                animationSpeed = 2.0f; // num == 2�� ���� �ӵ� ����
                break;
            case 3:
                animationSpeed = 2.5f; // num == 3�� ���� �ӵ� ����
                break;
            default:
                break;
        }

        Buttonobject.GetComponent<Animator>().speed = animationSpeed;
    }

    // Fail ��ư�� onClick���� ����
    public void Fail()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isFail");
    }

    // �����̴� ������Ʈ
    void UpdateSlider()
    {
        float normalizedValue = (float)UIManager.Instance.clickCount / UIManager.Instance.maxCount;
        clickCountSlider.value = normalizedValue;
    }
}
