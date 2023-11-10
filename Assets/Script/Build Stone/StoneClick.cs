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
    
    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 4f);
    }

    void Update()
    {
        // 0: �ָԵ���
        if (UIManager.Instance.itemNo == 0)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount - 5)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }
        // 1: �� ����
        if (UIManager.Instance.itemNo == 1)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount - 2)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }
        // 2: â
        if (UIManager.Instance.itemNo == 2)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount +2)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }
        // 3: �� ȭ��
        if (UIManager.Instance.itemNo == 3)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount + 5)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating ���߱�
                CancelInvoke("InstantiateButton");
            }
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone(int i)
    {
        // �̿ϼ� �� �̹��� ��Ȱ��ȭ
        foreach (GameObject item in UIManager.Instance.incompleteStone[UIManager.Instance.itemNo])
        {
            item.SetActive(false);
        }

        // �ϼ��� �� �̹��� Ȱ��ȭ
        UIManager.Instance.completeStone[UIManager.Instance.itemNo].SetActive(true);

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
