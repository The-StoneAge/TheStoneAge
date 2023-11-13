using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

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

    public GameObject Buttonobject;

    private void Awake()
    {
        if (num == 0)
        {
            for(int i = 0;i<handAxeItems.Length;i++)
            {
                Debug.Log(handAxeItems[i].name);
                handAxeItems[i].SetActive(true);
            }
            UIManager.Instance.maxCount -= 5;
            InvokeRepeating("InstantiateButton", 1f, 4f);
        }
        if (num == 1)
        {
            foreach (GameObject item in stoneAxeItems)
            {
                Debug.Log(item.name);
                item.SetActive(true);
            }
            UIManager.Instance.maxCount -= 2;
            InvokeRepeating("InstantiateButton", 1f, 3f);
        }
        if (num == 2)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 2;
            InvokeRepeating("InstantiateButton", 1f, 2f);
        }
        if (num == 3)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 5;
            InvokeRepeating("InstantiateButton", 1f, 1f);
        }
    }

    void Update()
    {
        if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
        {
            ActivateCompleteStone(num);
            CancelInvoke("InstantiateButton");
        }
        /*
        // 0: �ָԵ���
        if (num == 0)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }

        // 1: �� ����
        if (num == 1)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }

        // 2: â
        if (num == 2)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }

        // 3: �� ȭ��
        if (num == 3)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }
        */
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

        // �ϼ��� �� �̹��� Ȱ��ȭ
        completeItems[num].SetActive(true);

        // �ϼ� �ؽ�Ʈ
        completeText.gameObject.SetActive(true);
    }

    // Success ��ư�� onClick���� ����
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        UIManager.Instance.clickCount += 1;

        // �����̴� ������Ʈ
        UpdateSlider();

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
