using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockedObject : Interactable
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _item;
    private bool isHappend = false;

    private InsideOut _insideOut;
    

    public override bool Interact(Item item)
    {
        Debug.Log("Opened");
        if(_insideOut && !isHappend){
            if(_isEmpty){
                Debug.Log("Empty");
                _insideOut.NoItem();
                isHappend = true;
            }
            else{
                Debug.Log("Not Empty");
                _insideOut.ItemExist(_item);
                isHappend = true;
            }
        }

        return false;
    }

    void Start()
    {
        _insideOut = GetComponent<InsideOut>();
    }

    
}
