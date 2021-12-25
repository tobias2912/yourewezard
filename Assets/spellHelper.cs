using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellHelper : MonoBehaviour
{
    private Vector3 targetDestination;
    [SerializeField]
    private float speed;
    private Transform tempParent;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetDestination, Time.deltaTime * speed);
    }
    //hide the helper
    public void endHelp()
    {
        print("hides mesh");
        GetComponent<MeshRenderer>().enabled = false;
        transform.parent = tempParent;
    }
    public void startHelp()
    {
        tempParent = transform.parent;
        transform.parent = null;
        GetComponent<MeshRenderer>().enabled = true;
    }

    internal void setDestination(Vector3 newDest, bool instant)
    {
        targetDestination = newDest;
        if(instant){
            transform.position = newDest;
        }
    }
}
