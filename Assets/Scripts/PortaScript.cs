using System;
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

    public void OnInteract()
    {
        print("portaA");
        Transform TransP = GetComponentInParent<Transform>();
        TransP = TransP.parent.transform;
        print(TransP.name);
        foreach (Transform child in TransP){
            if(child.name == "portaEsq" | child.name == "portaDir"){
            print("porta");
            child.gameObject.SetActive(false);
            }
        }
    }
    public void OnInteract(object sender, EventArgs e)
    {
        print("portaA");
        Transform TransP = GetComponentInParent<Transform>();
        TransP = TransP.parent.transform;
        print(TransP.name);
        foreach (Transform child in TransP){
            if(child.name == "portaEsq" | child.name == "portaDir"){
            print("porta");
            child.gameObject.SetActive(false);
            }
        }
    }

}
