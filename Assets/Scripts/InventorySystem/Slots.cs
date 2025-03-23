using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private SpriteRenderer _hand;

    public int SelectedIndex = 0;


    public void CreateSlot(Item item)
    {
        GameObject newSlot = Instantiate(slotPrefab, transform); 
        newSlot.GetComponent<Slot>().SetSlot(item);
        newSlot.GetComponent<Slot>().OnSelected += inventoryUI.SelectItem;
        Debug.Log("Slot place is created");
    }

    public void RemoveSlot(Item item){
        _getSlotByItem(item)?.DestroySlot();
        _hand.sprite = null;           
    }

    public Slot _getSlotByItem(Item item){
        for(int i=0; i<transform.childCount; i++){
            Slot slotObj = transform.GetChild(i).GetComponent<Slot>();
            if(slotObj.item.ID == item.ID){
                return slotObj;
            }
        }
        return null;
    }

    public Slot _getSlotByIndex(int idx){
        return transform.GetChild(idx).GetComponent<Slot>();
    }


    private Slot _selectedSlot;
    public void ShowCursor(int idx){
        // cursor takip eden icon belirecek
        _selectedSlot = _getSlotByIndex(idx);
        _hand.sprite = _selectedSlot.item.Icon;
        print("SHOW");
    }
    

    public void HideCursor(){
        // cursor takip eden icon silinecek
        _selectedSlot = null;
        _hand.sprite = null;
        print("HIDE");
    }


}

