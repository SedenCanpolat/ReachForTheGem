using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockedObject : Interactable, IResetUpdater
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _item;
    private bool isHappend = false;

    private InsideOut _insideOut;
    private GameObject _spawnedItem;
    private Color _startColor;
    

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
                //_insideOut.ItemExist(_item);
                _spawnedItem = _insideOut.ItemExist(_item);
                isHappend = true;
            }
        }

        return false;
    }

    public void IRestarted()
    {
        isHappend = false;
        if(_isEmpty){
            gameObject.GetComponent<MeshRenderer>().material.color = _startColor;
        }
        else{
            Destroy(_spawnedItem);
        }
    }

    void Start()
    {
        _startColor = gameObject.GetComponent<MeshRenderer>().material.color;
        _insideOut = GetComponent<InsideOut>();
    }

    
}
