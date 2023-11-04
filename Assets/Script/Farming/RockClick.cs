
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class RockClick : MonoBehaviour
{
    public GameObject rockPrefab;

    private bool isGravityEnabled = false; // �߷� Ȱ��/��Ȱ�� ���¸� ��Ÿ���� ����
    private GameObject currentRock; // ���� ������ �� ������ �����ϴ� ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Stone"))
                {
                    // Ŭ�� �� �߷� ���� ��� �� �� ���� ����
                    CreateRock(hit.collider.gameObject);
                }
            }
        }
    }

    void CreateRock(GameObject tree)
    {
        if (currentRock != null)
        {
            // �̹� �� ������ �����Ǿ� �ִٸ� ����
            Destroy(currentRock);
        }

        // �� ���� �������� ����
        currentRock = Instantiate(rockPrefab, tree.transform.position - new Vector3(2, -3, 0), Quaternion.identity);
        currentRock.SetActive(true);

        // ȸ������ �����Ͽ� �� ������ Ư�� ������ ȸ���ǵ��� ��
        Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
        currentRock.transform.rotation = rotation;

        //������ �� ������ Rigidbody �� �߷� ����
        Rigidbody branchRigidbody = currentRock.GetComponent<Rigidbody>();
        if (branchRigidbody != null)
        {
            branchRigidbody.useGravity = true;
        }
    }
}