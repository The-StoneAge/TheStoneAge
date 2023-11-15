using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisterDialogue : baseDialogue
{
    public GameObject camera;

    private void Update()
    {
        GameObject sisterQuest = GameObject.FindGameObjectWithTag("sisterquest");
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        checkQuest(sisterQuest, sister);
    }
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject sisterQuest = GameObject.FindGameObjectWithTag("sisterquest");
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        
        if (sisterQuest != null && sister != null)
        {
            StartDialogue(sisterQuest, sister);
        }
        else
        {
            Debug.Log("������Ʈ�� ã�� ���� ��쿡 ���� ó��");
        }
    }
    public void isCompletePanel()
    {
        //if (isComplete) UIManager.Instance.ShowQuestCompletionPanel(); return;
       // if(isComplete2) UIManager.Instance.ShowQuestCompletionPanel(); return;
    }
}
