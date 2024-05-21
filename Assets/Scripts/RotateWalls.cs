using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateWalls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    void entrouDentro(){
         List<Transform> allChildren = new List<Transform>();
        foreach (Transform child in transform){
            print("My name is " + child.name + " and my position is " + child.position.x + "/" + child.position.y + "/" + child.position.z);
            child.Translate(UnityEngine.Vector3.forward * -4);
            print("My new name is " + child.name + " and my position is " + child.position.x + "/" + child.position.y + "/" + child.position.z);
            child.Rotate(0, 180, 0);
        }
    }
}

