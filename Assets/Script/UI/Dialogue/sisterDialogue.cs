using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisterDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    npcController npccontroller;
    Ray ray;
    private void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �±װ� "player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.IsDialoguePromptActive();
            if (!isPanelActive)
            {
                dialoguePanel.gameObject.SetActive(true);
            }
            Debug.Log("hi");
        }
    }
    void StartDialogue()
    {
        GameObject sisterquest = GameObject.FindGameObjectWithTag("sisterquest");
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        QuestManager questManager = sisterquest.GetComponent<QuestManager>();
        npccontroller = sister.GetComponent<npcController>();

        // ���� ��ȭ ���� ���� (�̹� �ۼ��� �κ��� �״�� ���)
        Debug.Log($"isquesting in momDialogue {questManager.getisQuesting()}");
        if (DialogueManager.Instance != null)
        {
            Debug.Log($"isquesting {questManager.getisQuesting()} && isComplete{questManager.getisCompleting()}");
            if (questManager.getisCompleting() == false && questManager.getisQuesting() == false)
            {
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.dialogueLines);
            }
            else if (questManager.getisQuesting() == true && questManager.getisCompleting() == true)
            {
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.completedialogueLines);
            }
            else if (questManager.getisQuesting() == true && questManager.getisCompleting() == false)
            {
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.noncompletedialogueLines);
            }
            //Debug.Log("mom1");
        }
    }
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        StartDialogue();
    }

    public void RejectButton()
    {
        dialoguePanel.gameObject.SetActive(false);
    }

}
