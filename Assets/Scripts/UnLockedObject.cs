using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockedObject : Interactable
{
    public override bool Interact(Item item)
    {
        Debug.Log("Opened");
        return true;
    }
   
}
