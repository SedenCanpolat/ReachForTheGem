using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockedObject : Interactable
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _item;

    public override bool Interact(Item item)
    {
        Debug.Log("Opened");
        InsideOut insideOut = GetComponent<InsideOut>();
        if(insideOut){
            if(_isEmpty){
                Debug.Log("Empty");
                insideOut.NoItem();
            }
            else{
                Debug.Log("Not Empty");
                insideOut.ItemExist(_item);
            }
        }

        return true;
    }
   
}
