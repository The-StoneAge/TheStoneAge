using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEntryPoint : MonoBehaviour
{
    [SerializeField]
    SceneTransitionManager.Location locationToSwitch;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            if (SceneTransitionManager.Instance != null)
            {           
                Debug.Log("�浹");
                SceneTransitionManager.Instance.SwitchLocation(locationToSwitch);
            }

        }

    }
}
