using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : Interactable
{
    [SerializeField] private Item _item;
    public override bool Interact(Item item)
    {
        if(item) return false;
        if(Inventory.instance.AddItem(_item)){
             _destroyItem();
             return true;
        }
        else{
            Debug.Log("Inventory is full");
            return false;
        }
    }

    private void _destroyItem(){
        Destroy(gameObject);
    }
}
