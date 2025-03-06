using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    private FieldOfView _fieldOfView;
    private RaycastHit _raycastHit;
    
    void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        
    }

    void Update()
    {

        if(_fieldOfView.IsInView(out _raycastHit)){
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
    }
}
