using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public Item item;

    public Action<Item> OnSelected;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelected(item);
    }

    public void SetSlot(Item item){
        this.item = item;
        GetComponent<Image>().sprite = item.Icon;
        Debug.Log("Slot icon is created");
    }


    public void DestroySlot(){
        Destroy(gameObject);
    }
}
