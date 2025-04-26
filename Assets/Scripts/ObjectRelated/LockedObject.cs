using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockedObject : Interactable
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _itemInside;
    [SerializeField] private Item _key;
    public bool IsHappend = false;
    private MovingObject _movingObject;
    [SerializeField] private bool _isSecond = false;

    [SerializeField] private GameObject _pairedObject;
    [SerializeField] private GameObject _blockObject;

    void Start()
    {
        _movingObject = GetComponent<MovingObject>();
        if(_blockObject != null){
            _blockObject.SetActive(false);
        }
    }

    public override bool Interact(Item item)
    {
        bool isInteracted = false;
        if(item != null && item.ID == _key.ID){
            Debug.Log("Door Opened :)");
            InsideOut insideOut = GetComponent<InsideOut>();
            if(insideOut && !IsHappend){
                if(_isEmpty){
                    Debug.Log("Empty");
                    insideOut.NoItem();
                    IsHappend = true;
                    isInteracted = true;
                }
                else{
                    if (_itemInside != null)
                    {
                        Debug.Log("Not Empty");
                        insideOut.ItemExist(_itemInside);
                        IsHappend = true;
                        isInteracted = true;
                    }
                    if(_itemInside == null){
                        print("AAAA");
                    }
                }
            }
            else if(!IsHappend){
                if(_pairedObject != null && _blockObject != null) {
                    _blockObject.SetActive(true);
                    if(_pairedObject.GetComponent<LockedObject>().IsHappend){
                        _blockObject.SetActive(false);
                    }
                }
                    if(_isSecond){
                        StartCoroutine(_movingObject.MoveAnimation(MovingObject.MoveType.Right));
                    }
                    else if(gameObject.transform.rotation.y != 0){
                        StartCoroutine(_movingObject.MoveAnimation(MovingObject.MoveType.Rotated));
                    }
                    else{
                        StartCoroutine(_movingObject.MoveAnimation(MovingObject.MoveType.Left));
                    }
                    
                isInteracted = true;
                IsHappend = true;
                
            }
        
        }
        return isInteracted;
    }   
}