
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class PlantClick : MonoBehaviour
{
    public GameObject fruitPrefab;

    private bool isGravityEnabled = false; // �߷� Ȱ��/��Ȱ�� ���¸� ��Ÿ���� ����
    private GameObject currentFruit; // ���� ������ ���Ÿ� �����ϴ� ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Plant"))
                {
                    // Ŭ�� �� �߷� ���� ��� �� ���� ����
                    CreateFruit(hit.collider.gameObject);
                }
            }
        }
    }

    void CreateFruit(GameObject plant)
    {
        if (currentFruit != null)
        {
            // �̹� �� ������ �����Ǿ� �ִٸ� ����
            Destroy(currentFruit);
        }

        // ���� �������� ����
        currentFruit = Instantiate(fruitPrefab, plant.transform.position - new Vector3(0, -2, 1), Quaternion.identity);
        currentFruit.SetActive(true);

        // ȸ������ �����Ͽ� �� ������ Ư�� ������ ȸ���ǵ��� ��
        Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
        currentFruit.transform.rotation = rotation;

        //������ �� ������ Rigidbody �� �߷� ����
        Rigidbody branchRigidbody = currentFruit.GetComponent<Rigidbody>();
        if (branchRigidbody != null)
        {
            branchRigidbody.useGravity = true;
        }
    }
}