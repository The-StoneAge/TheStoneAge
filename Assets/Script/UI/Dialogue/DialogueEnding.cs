using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueEnding : MonoBehaviour
{ 
    public List<DialogueLine> firstdialogueLines;
    public List<DialogueLine> seconddialogueLines;
    public GameObject grandfathercamera;
    public GameObject momcamera;
    public GameObject dadcamera;
    public GameObject sistercamera;
    public float rotationSpeed = 60.0f;
    public Animator momanimator;
    public Animator dadanimator;
    public Animator grandfhateranimator;
    public Animator sisteranimator;

    private void Start()
    {
        StartCoroutine(ShowCharacters());
    }
    private IEnumerator ShowCharacters()
    {
        // ��ȭ ����
        DialogueManager.Instance.StartDialogue(firstdialogueLines);
        yield return new WaitForSeconds(2.0f); // 2�� ���

        // Ȱ��ȭ�� ������� ĳ���� �� ī�޶� Ȱ��ȭ�ϰ� �ִϸ��̼� ���
        UIManager.Instance.ToggleDialoguePanel();
        grandfathercamera.SetActive(true);
        grandfhateranimator.SetBool("isPray", true);
        yield return new WaitForSeconds(2.0f); 

        sistercamera.SetActive(true);
        sisteranimator.SetBool("isPray", true);
        grandfathercamera.SetActive(false);
        yield return new WaitForSeconds(2.0f); 

        dadcamera.SetActive(true);
        dadanimator.SetBool("isPray", true);
        sistercamera.SetActive(false);
        yield return new WaitForSeconds(2.0f); 

        momcamera.SetActive(true);
        momanimator.SetBool("isPray", true);
        dadcamera.SetActive(false);
        yield return new WaitForSeconds(2.0f); 

        // ��ȭ ����
        momcamera.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        grandfathercamera.SetActive(true);
        grandfhateranimator.SetBool("isPray", false);
        DialogueManager.Instance.StartDialogue(seconddialogueLines);

    }
}


