using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoneClick : MonoBehaviour
{
    public GameObject[] incompleteStone; // �̿ϼ� �� �̹���
    public GameObject completeStone;   // �ϼ��� �� �̹���

    public Slider clickCountSlider;    // Ŭ�� Ƚ���� ǥ���� �����̴�
    public Text successText;           // ���� �ؽ�Ʈ
    public Text failText;              // ���� �ؽ�Ʈ
    public Text completeText;          // �ϼ� �ؽ�Ʈ

    public int clickCount = 0;
    public int maxCount;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 3f);
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
        Buttonobject.SetActive(false);
        clickCount += 1;

        // �����̴� ������Ʈ
        UpdateSlider();

        // ���� �ؽ�Ʈ�� ��ġ�� ��ư�� ��ġ�� ����
        Vector2 buttonPosition = Buttonobject.GetComponent<RectTransform>().anchoredPosition;
        successText.gameObject.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

        // ���� �ؽ�Ʈ Ȱ��ȭ
        successText.gameObject.SetActive(true);

        // ���� �ؽ�Ʈ ��Ȱ��ȭ
        StartCoroutine(DelayedSuccessTextDeactivation());
    }

    // Fail ��ư�� onClick���� ����
    public void Fail()
    {
        Buttonobject.SetActive(false);

        // ���� �ؽ�Ʈ�� ��ġ�� ��ư�� ��ġ�� ����
        Vector2 buttonPosition = Buttonobject.GetComponent<RectTransform>().anchoredPosition;
        failText.gameObject.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

        // ���� �ؽ�Ʈ Ȱ��ȭ
        failText.gameObject.SetActive(true);

        // ���� �ؽ�Ʈ ��Ȱ��ȭ
        StartCoroutine(DelayedFailTextDeactivation());
    }

    // �����̴� ������Ʈ
    void UpdateSlider()
    {
        float normalizedValue = (float)clickCount / maxCount;
        clickCountSlider.value = normalizedValue;
    }

    // �ð� ���� �� ���� �ؽ�Ʈ ��Ȱ��ȭ
    IEnumerator DelayedSuccessTextDeactivation()
    {
        yield return new WaitForSeconds(1.0f); // ���ϴ� �ð��� �� ������ ����
        successText.gameObject.SetActive(false);
    }
    // �ð� ���� �� ���� �ؽ�Ʈ ��Ȱ��ȭ
    IEnumerator DelayedFailTextDeactivation()
    {
        yield return new WaitForSeconds(1.0f); // ���ϴ� �ð��� �� ������ ����
        failText.gameObject.SetActive(false);
    }
}
