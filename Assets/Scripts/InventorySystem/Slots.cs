using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private InventoryUI inventoryUI;

    public void CreateSlot(Item item)
    {
        GameObject newSlot = Instantiate(slotPrefab, transform); 
        newSlot.GetComponent<Slot>().SetSlot(item);
        newSlot.GetComponent<Slot>().OnSelected += inventoryUI.SelectItem;
        Debug.Log("Slot place is created");
    }

    public void RemoveSlot(Item item){
        _getSlot(item)?.DestroySlot();           
    }

    private Slot _getSlot(Item item){
        for(int i=0; i<transform.childCount; i++){
            Slot slotObj = transform.GetChild(i).GetComponent<Slot>();
            if(slotObj.item.ID == item.ID){
                return slotObj;
            }
        }
        return null;
    }

    private Slot _selectedSlot;
    public void ShowCursor(Item item){
        // cursor takip eden icon belirecek
        _selectedSlot = _getSlot(item);
        //if (_selectedSlot == null) return;
        _selectedSlot.transform.SetParent(transform.parent);
    }

    public void HideCursor(){
        // cursor takip eden icon silinecek
        _selectedSlot?.transform.SetParent(transform);
        _selectedSlot = null;
    }

    private void _followCursor(){
        _selectedSlot.transform.position = Input.mousePosition;
    }

    private void Update() {
        if(_selectedSlot){
            _followCursor();
        }
    }
}

