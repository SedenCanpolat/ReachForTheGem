using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : Interactable
{
    public override bool Interact(Item item)
    {
        GameManagement.instance.LostGame(1);        
        return false;
    }
}
