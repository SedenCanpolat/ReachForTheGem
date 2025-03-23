using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockedObject : Interactable
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _item;
    private bool isHappend = false;

    public override bool Interact(Item item)
    {
        Debug.Log("Opened");
        InsideOut insideOut = GetComponent<InsideOut>();
        if(insideOut && !isHappend){
            if(_isEmpty){
                Debug.Log("Empty");
                insideOut.NoItem();
                isHappend = true;
            }
            else{
                Debug.Log("Not Empty");
                insideOut.ItemExist(_item);
                isHappend = true;
            }
        }

        return false;
    }
   
}
