
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class TreeClick : MonoBehaviour
{
    public GameObject branchPrefab;

    private bool isGravityEnabled = false; // �߷� Ȱ��/��Ȱ�� ���¸� ��Ÿ���� ����
    private GameObject currentBranch; // ���� ������ ���������� �����ϴ� ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    // Ŭ�� �� �߷� ���� ��� �� �������� ����
                    CreateBranch(hit.collider.gameObject);
                }
            }
        }
    }

    void CreateBranch(GameObject tree)
    {
        if (currentBranch != null)
        {
            // �̹� ���������� �����Ǿ� �ִٸ� ����
            Destroy(currentBranch);
        }

        // �������� �������� ����
        currentBranch = Instantiate(branchPrefab, tree.transform.position - new Vector3(0, -9, 3), Quaternion.identity);
        currentBranch.SetActive(true);

        // ȸ������ �����Ͽ� ���������� Ư�� ������ ȸ���ǵ��� ��
        Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
        currentBranch.transform.rotation = rotation;

        //������ ���������� Rigidbody �� �߷� ����
        Rigidbody branchRigidbody = currentBranch.GetComponent<Rigidbody>();
        if (branchRigidbody != null)
        {
            branchRigidbody.useGravity = true;
        }
    }
}