using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxSeeAngle;
    [SerializeField] private float _walkDistance;
    [SerializeField] private bool isRight = false;
    private FieldOfView _fieldOfView;
    private RaycastHit _raycastHit;
    private Quaternion _startRotation;
    private float _directionIndicator = 1f; 
    private float _rotation;
    private CharacterController _controller;
    private float _movedDistance = 0f;
    
    void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _startRotation = transform.rotation;
        _controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        
        _patrol();
        
        if (_fieldOfView.IsInView(out _raycastHit))
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
    }

    private void _patrol(){
        Vector3 moveDirection = isRight ? Vector3.right : Vector3.forward;
        Vector3 moveAmount = moveDirection * _speed * Time.deltaTime;
        _controller.Move(moveAmount);
        _movedDistance += moveAmount.magnitude;
        if (_movedDistance >= _walkDistance)
        {
            StartCoroutine(_lookAround(10));
            _movedDistance = 0f;
        }
        
        transform.rotation = Quaternion.LookRotation(moveAmount.normalized);
    }

    private IEnumerator _lookAround(float time){
        float actualSpeed = _speed;
        _speed *= 0;
        float elapsedTime = 0f;
        while(elapsedTime < time){
            _rotation += _directionIndicator * _rotationSpeed * Time.deltaTime;
        
            if (_rotation >= _maxSeeAngle || _rotation <= -_maxSeeAngle)
            {
                _directionIndicator *= -1;
            }

            transform.rotation = _startRotation * Quaternion.Euler(0, _rotation, 0);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        _speed = actualSpeed;
        _speed *= -1;
        
    }
}
