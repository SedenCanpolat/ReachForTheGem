using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool Interact(Item item);

    public virtual bool IsInteractable(){
        return true;
    }

}
