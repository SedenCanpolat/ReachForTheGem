using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LockedObject : Interactable, IResetUpdater
{
    [SerializeField] private bool _isEmpty;
    [SerializeField] private GameObject _itemInside;
    [SerializeField] private Item _key;
    private bool _isHappend = false;
    private MovingObject _movingObject;
    [SerializeField] private bool _isSecond = false;

    [SerializeField] private GameObject _pairedObject;
    [SerializeField] private GameObject _blockObject;
    private bool _isInteracted;
    private InsideOut _insideOut;
    private Vector3 _startPos;
    private GameObject _spawnedItem;
    private Color _startColor;

    void Start()
    {
        _startPos = gameObject.transform.position;
        _startColor = gameObject.GetComponent<MeshRenderer>().material.color;
        _movingObject = GetComponent<MovingObject>();
        _insideOut = GetComponent<InsideOut>();
        if(_blockObject != null){
            _blockObject.SetActive(false);
        }
    }

    public override bool Interact(Item item)
    {
        _isInteracted = false;
        if(item != null && item.ID == _key.ID){
            Debug.Log("Door Opened :)");
            if(_insideOut && !_isHappend){
                if(_isEmpty){
                    Debug.Log("Empty");
                    _insideOut.NoItem();
                    _isHappend = true;
                    _isInteracted = true;
                }
                else{
                    if (_itemInside != null)
                    {
                        Debug.Log("Not Empty");
                        //_insideOut.ItemExist(_itemInside);
                        _spawnedItem = _insideOut.ItemExist(_itemInside);
                        _isHappend = true;
                        _isInteracted = true;
                    }
                    if(_itemInside == null){
                        print("null item");
                    }
                }
            }
            else if(!_isHappend){
                if(_pairedObject != null && _blockObject != null) {
                    _blockObject.SetActive(true);
                    if(_pairedObject.GetComponent<LockedObject>()._isHappend){
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
                    
                _isInteracted = true;
                _isHappend = true;
                
            }
        
        }
        return _isInteracted;
    }

    public void IRestarted()
    { 
        _isInteracted = false;
        _isHappend = false;
        gameObject.transform.position = _startPos;
        if(_isEmpty){
            gameObject.GetComponent<MeshRenderer>().material.color = _startColor;
        }
        if(_itemInside != null){
            Destroy(_spawnedItem);
        }
    }
}