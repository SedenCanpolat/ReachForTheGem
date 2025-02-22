using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockedObject : Interactable
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _itemInside;
    [SerializeField] private Item _key;
    private bool isHappend = false;
    public override bool Interact(Item item)
    {
        if(item != null && item.ID == _key.ID){
            Debug.Log("Door Opened :)");
            InsideOut insideOut = GetComponent<InsideOut>();
            if(insideOut && !isHappend){
                if(_isEmpty){
                    Debug.Log("Empty");
                    insideOut.NoItem();
                    isHappend = true;
                }
                else{
                    if (_itemInside != null)
                    {
                        Debug.Log("Not Empty");
                        insideOut.ItemExist(_itemInside);
                        isHappend = true;
                    }
                }
            }
            return true;
        }
        return false;
    }   
}