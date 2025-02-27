using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private GameObject _frame;
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
        _getSlot(item)?.DestroySlot();
        //Frame.SetActive(false);
        _hand.sprite = null;           
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
        _selectedSlot = _getSlot(Inventory.instance.UpdateSelectedItem());
        //if (_selectedSlot == null) return;
        _selectedSlot.transform.SetParent(transform.parent);
        //Frame.SetActive(true);
        //Frame.transform.position = _selectedSlot.transform.position;
        _hand.sprite = _selectedSlot.item.Icon;
        print("SHOW");
    }
    

    public void HideCursor(){
        // cursor takip eden icon silinecek
        _selectedSlot?.transform.SetParent(transform);
        _selectedSlot = null;
        _hand.sprite = null;
        print("HIDE");
    }
    
    /*
    private void createSlotList(){
        for (int i = 0; i < transform.childCount; i++)
        {
            Slot slot = transform.GetChild(i).GetComponent<Slot>();
            if (slot != null)
            {
                _slotsList.Add(slot);
            }
        }
      
    }
    private List<Slot> _slotsList = new List<Slot>();
    void Update()
    {
        createSlotList();
        if (_slotsList.Count == 0) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _frame.SetActive(true);

        if (scroll > 0f)
        {
            SelectedIndex = (SelectedIndex + 1) % _slotsList.Count;
        }
        else if (scroll < 0f)
        {
            SelectedIndex = (SelectedIndex - 1 + _slotsList.Count) % _slotsList.Count;
        }

        if (_frame != null && SelectedIndex < _slotsList.Count)
        {
            _frame.transform.position = _slotsList[SelectedIndex].transform.position;
        }
    }
    */

 
    

    /*
        private void _followCursor(){
            _selectedSlot.transform.position = Hand.transform.position;//Input.mousePosition;
            Hand.sprite = _selectedSlot.item.Icon;
            print("FOLLOW");
        }

        private void Update() {
            if(_selectedSlot){
                _followCursor();
            }
        }
    */


}

