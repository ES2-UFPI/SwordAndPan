using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class PortaScript : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        Transform TransP = GetComponentInParent<Transform>();
        foreach (Transform child in TransP){
            print("porta");
            child.gameObject.SetActive(false);
        }
    }
}
