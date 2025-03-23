using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Slots slots;
    
    public void OnItemAdded(Item item){
        Debug.Log(item.Name + " Added To Inventory");
        slots.CreateSlot(item);
    }

    public void OnItemRemoved(Item item){
        slots.RemoveSlot(item);
    }

    public Item SelectedItem {get; private set;}

    public void SelectItem(Item item, int idx){
        if(Inventory.instance.ItemOnHand != null){
            UnSelectItem();
            print("HANDS FULL");
        }
        SelectedItem = item;
        slots.ShowCursor(idx);
    }

    public void UnSelectItem(){
        SelectedItem = null;
        slots.HideCursor();
    }

    public Slot GetSelectedSlot(int idx){
        return slots._getSlotByIndex(idx);
    }


}
