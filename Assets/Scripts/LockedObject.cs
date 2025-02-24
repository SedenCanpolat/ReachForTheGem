using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockedObject : Interactable
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _itemInside;
    [SerializeField] private Item _key;
    private bool isHappend = false;
    private bool isMoved = false;
    private MovingObject _movingObject;
    [SerializeField] private bool isSecond = false;

    void Start()
    {
        _movingObject = GetComponent<MovingObject>();
    }

    public override bool Interact(Item item)
    {
        if(item != null && item.ID == _key.ID){
            Debug.Log("Door Opened :)");
            InsideOut insideOut = GetComponent<InsideOut>();
            if(insideOut && !isHappend){
                if(_isEmpty){
                    Debug.Log("Empty");
                    insideOut.NoItem();
                    isHappend = true;
                }
                else{
                    if (_itemInside != null)
                    {
                        Debug.Log("Not Empty");
                        insideOut.ItemExist(_itemInside);
                        isHappend = true;
                    }
                }
            }
            else if(!isHappend){
                if(!isMoved){
                    if(isSecond){
                        StartCoroutine(_movingObject.MoveAnimation(MovingObject.MoveType.Right));
                    }
                    else if(gameObject.transform.rotation.y != 0){
                        StartCoroutine(_movingObject.MoveAnimation(MovingObject.MoveType.Rotated));
                    }
                    else{
                        StartCoroutine(_movingObject.MoveAnimation(MovingObject.MoveType.Left));
                    }
                    isMoved = true;
                }
                isHappend = true;
            }
            return true;
        }
        return false;
    }   
}