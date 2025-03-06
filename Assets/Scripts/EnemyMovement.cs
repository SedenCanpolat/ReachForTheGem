using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxSeeAngle;
    private FieldOfView _fieldOfView;
    private RaycastHit _raycastHit;
    private Quaternion _startRotation;
    private float _directionIndicator = 1f; 
    private float _rotation;
    
    void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _startRotation = transform.rotation;
    }

    void Update()
    {
        _rotation += _directionIndicator * _rotationSpeed * Time.deltaTime;
        
        if (_rotation >= _maxSeeAngle || _rotation <= -_maxSeeAngle)
        {
            _directionIndicator *= -1;
        }

        transform.rotation = _startRotation * Quaternion.Euler(0, _rotation, 0);
        
        if (_fieldOfView.IsInView(out _raycastHit))
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
    }
}
