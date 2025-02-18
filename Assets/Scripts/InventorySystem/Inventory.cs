using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private void Awake() {
        instance = this;
    }

    [SerializeField] private InventoryUI _inventoryUI;
    private List<Item> _inventory = new();

    public Item ItemOnHand => _inventoryUI.SelectedItem; // getter

    public void EmptyHand(){
        _inventoryUI.UnSelectItem();
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
        }
        else{
            Debug.LogError("Item is null");
        }
    }
}
