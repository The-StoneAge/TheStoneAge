using UnityEngine;
using UnityEngine.UI;

public class StoneClick : MonoBehaviour
{
    public GameObject incompleteStone; // �̿ϼ� �� �̹���
    public GameObject completeStone;   // �ϼ��� �� �̹���

    public int clickCount = 0;
    public int maxCount;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f,3f);
    }

    void Update()
    {
        // Ŭ�� Ƚ���� 10���� �����ϸ� �ϼ��� �� �̹��� Ȱ��ȭ
        if (clickCount >= 10)
        {
            ActivateCompleteStone();
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600,600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone()
    {
        // �̿ϼ� �� �̹��� ��Ȱ��ȭ
        incompleteStone.SetActive(false);

        // �ϼ��� �� �̹��� Ȱ��ȭ
        completeStone.SetActive(true);
    }
    public void Success()
    {
        Buttonobject.SetActive(false);
        clickCount += 1;
    }

    public void Fail()
    {
        Buttonobject.SetActive(false);
    }
}
