using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StoneClick : MonoBehaviour
{
    public GameObject[] incompleteStone; // �̿ϼ� �� �̹���
    public GameObject completeStone;   // �ϼ��� �� �̹���

    public Slider clickCountSlider;    // Ŭ�� Ƚ���� ǥ���� �����̴�
    public TMP_Text completeText;          // �ϼ� �ؽ�Ʈ

    public int clickCount = 0;
    public int maxCount;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 4f);
    }

    void Update()
    {
        // Ŭ�� Ƚ���� 10���� �����ϸ� �ϼ��� �� �̹��� Ȱ��ȭ
        if (clickCount >= maxCount)
        {
            ActivateCompleteStone();
            // InvokeRepeating ���߱�
            CancelInvoke("InstantiateButton");
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone()
    {
        // �̿ϼ� �� �̹��� ��Ȱ��ȭ
        foreach (GameObject item in incompleteStone)
        {
            item.SetActive(false);
        }

        // �ϼ��� �� �̹��� Ȱ��ȭ
        completeStone.SetActive(true);

        // �ϼ� �ؽ�Ʈ
        completeText.gameObject.SetActive(true);
    }

    // Success ��ư�� onClick���� ����
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        clickCount += 1;

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
        float normalizedValue = (float)clickCount / maxCount;
        clickCountSlider.value = normalizedValue;
    }
}
