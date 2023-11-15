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

        // num�� ���� �ִϸ��̼� �ӵ� ����
        float animationSpeed = 1.0f; // �⺻ �ӵ�

        switch (num)
        {
            case 0:
                animationSpeed = 1.0f; // num == 0�� ���� �ӵ� ����
                break;
            case 1:
                animationSpeed = 2.0f; // num == 1�� ���� �ӵ� ����
                break;
            case 2:
                animationSpeed = 0.5f; // num == 2�� ���� �ӵ� ����
                break;
            case 3:
                animationSpeed = 1.5f; // num == 3�� ���� �ӵ� ����
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
