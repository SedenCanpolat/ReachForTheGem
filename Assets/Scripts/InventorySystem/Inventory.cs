using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    [SerializeField] private InventoryUI _inventoryUI;
    private List<Item> _inventory = new();

    public Item ItemOnHand => _inventoryUI.SelectedItem; // getter
    private int _selectedIndex = 0;
    [SerializeField] private GameObject _frame;

    public void EmptyHand(){
        _inventoryUI.UnSelectItem();
        _frame.SetActive(false);
    }

    public bool AddItem(Item item){
        if(item != null){
            _inventory.Add(item);
            _inventoryUI.OnItemAdded(item);
            return true;
        }
        else{
            Debug.LogError("Item is null");
        }
        return false;
    }

    public void RemoveItem(Item item){
        if(item != null){
            _inventory.Remove(item);
            _inventoryUI.OnItemRemoved(item);
           // _frame.SetActive(false); 
        }
        else{
            Debug.LogError("Item is null");
        }
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            int idx = _inventoryUI.GetSelectIdx();
            if (idx >= 0 && idx < _inventory.Count)
            {
                _inventoryUI.SelectItem(_inventory[idx]);
            }
        }
    }
    */
    


    void Update()
    {
        if (_inventory.Count == 0) return;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _frame.SetActive(true);
        if (scroll > 0f)
        {
            _selectedIndex = (_selectedIndex + 1) % _inventory.Count;
            UpdateSelectedItem();
            _frame.transform.position = _frame.transform.position + new Vector3(90, 0, 0);
            
        }
        else if (scroll < 0f)
        {
            _selectedIndex = (_selectedIndex - 1 + _inventory.Count) % _inventory.Count;
            UpdateSelectedItem();
            _frame.transform.position = _frame.transform.position - new Vector3(90, 0, 0);
            //_framePositioning();
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            _inventoryUI.SelectItem(_inventory[_selectedIndex]);
        }
        

    }

    public Item UpdateSelectedItem()
    {
        //_frame.transform.position = _inventory[_selectedIndex].transform.position;
        Item selectedItem = _inventory[_selectedIndex];
        Debug.Log("Selected Item: " + selectedItem.Name);
        return selectedItem;
    }

    private void _framePositioning(){
       _frame.transform.position = _inventoryUI.GetSelectedSlot().transform.position;
    }

   
}
