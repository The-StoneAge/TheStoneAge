﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class momDialogue : MonoBehaviour
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
        // 충돌한 객체의 태그가 "player"인지 확인
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.IsDialoguePromptActive();
            if (!isPanelActive)
            {
                dialoguePanel.gameObject.SetActive(true);
            }
        }
    }
    void StartMomDialogue()
    {
        GameObject momObject = GameObject.FindGameObjectWithTag("momquest");
        GameObject momObject2 = GameObject.FindGameObjectWithTag("mom");
        QuestManager questManager = momObject.GetComponent<QuestManager>();
        npccontroller = momObject2.GetComponent<npcController>();

        // 이하 대화 시작 로직 (이미 작성한 부분을 그대로 사용)
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
        StartMomDialogue();
    }

    public void RejectButton()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
   
}
