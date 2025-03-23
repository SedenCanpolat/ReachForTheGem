using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavEnemyMovement : MonoBehaviour
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
    private float _originalSpeed;
    private bool _isChasing = false;
    private Vector3 _startPos;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private enum _enemyState {Patrol, Chase, Return, LookAround};
    private _enemyState _currentState;
    
    void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _startRotation = transform.rotation;
        _controller = GetComponent<CharacterController>();
        _originalSpeed = _speed;
        _startPos = transform.position;
        _currentState = _enemyState.Patrol;
    }

    void Update()
    {
        if(GameManagement.instance.isGameOver){            
            _resetEnemyPosition();
            return;
        }

        switch(_currentState){
            case _enemyState.Patrol:
                _patrol();
                break;
            case _enemyState.Chase:
                _chase();
                break;
            case _enemyState.Return:
                _returnBack();
                break; 
            case _enemyState.LookAround:
                _lookAround(7);  
                break;    
        }
        
    }

    private void _resetEnemyPosition()
    {
        _controller.enabled = false;  
        transform.position = _startPos; 
        _controller.enabled = true;
        _currentState = _enemyState.Patrol;  
    }


    private void _chase(){
        Vector3 enemyPlayerDifference = _player.transform.position - transform.position;
        float distance = enemyPlayerDifference.magnitude;

        if (distance > 2.63f && distance < 8f)
        {
            navMeshAgent.SetDestination(_player.transform.position);
        }
        else if (distance <= 2.63f)
        {
            GameManagement.instance.LostGame();
        }

        if (!_fieldOfView.IsInView(out _raycastHit))
        {
            _currentState = _enemyState.Return;
        }
        
    }

    private void _returnBack(){
        navMeshAgent.SetDestination(_startPos);

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            _currentState = _enemyState.Patrol;
        } 
        
        _checkForPlayerVisibility();   
    }

    private void _checkForPlayerVisibility()
    {
        if (_fieldOfView.IsInView(out _raycastHit))
        {
            StopAllCoroutines();
            navMeshAgent.speed = _originalSpeed;
            _currentState = _enemyState.Chase;
        }
    }

    
    private void _patrol(){
        Vector3 moveDirection = isRight ? Vector3.right : Vector3.forward;
        Vector3 targetPosition = _startPos + moveDirection * _walkDistance * _currentDirection;
        navMeshAgent.SetDestination(targetPosition);

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            _currentDirection *= -1;
            _currentState = _enemyState.LookAround;
        }
        else{
            _checkForPlayerVisibility();
        }
       
        
      
    }

    private float elapsedTime;
    private int _currentDirection = 1;
    private void _lookAround(float time){
        
        if(!navMeshAgent.isStopped){
            navMeshAgent.isStopped = true; 
            elapsedTime = 0f;
        }
        
        if(elapsedTime < time && !_fieldOfView.IsInView(out _raycastHit)){
            _rotation += _directionIndicator * _rotationSpeed * Time.deltaTime;
        
            if (_rotation >= _maxSeeAngle || _rotation <= -_maxSeeAngle)
            {
                _directionIndicator *= -1;
            }

            transform.rotation = _startRotation * Quaternion.Euler(0, _rotation, 0);
            elapsedTime += Time.deltaTime;

        }
        else{
            elapsedTime = 0f;
            navMeshAgent.isStopped = false;
            _speed *= -1;
            
            _currentState = _enemyState.Patrol;
        }
        
        _checkForPlayerVisibility();
        
    }
}
