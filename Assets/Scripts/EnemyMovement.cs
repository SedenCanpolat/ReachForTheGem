using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    private float _originalSpeed;
    private bool _isChasing = false;
    private Vector3 _startPos;
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    void Start()
    {

        _fieldOfView = GetComponent<FieldOfView>();
        _startRotation = transform.rotation;
        _controller = GetComponent<CharacterController>();
        _originalSpeed = _speed;
        _startPos = transform.position;
    }

    private void _resetEnemyPosition()
    {
        _controller.enabled = false;  
        transform.position = _startPos; 
        _controller.enabled = true;  
    }

    void Update()
    {
        if(GameManagement.instance.isGameOver){
            //_speed = 0;
            
            _resetEnemyPosition();
            return;
        }

        if (GameManagement.instance.isGameRestarted)
        {
            
            //_speed = _originalSpeed;
        }  
               
        if (_fieldOfView.IsInView(out _raycastHit))
        {
            StopAllCoroutines();
            _speed = _originalSpeed;
            //transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            Vector3 enemyPlayerDifference = _player.transform.position - transform.position;
            navMeshAgent.SetDestination(_player.transform.position);
            /*
            Vector3 direction = enemyPlayerDifference.normalized;
            _controller.Move(direction * _speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(direction);
            */
            if(enemyPlayerDifference.magnitude <= 2.63){
                GameManagement.instance.LostGame();
            }
            _isChasing = true;
        }
        
        else{
            if(_isChasing){
                print("RUN");
                //StartCoroutine(_lookAround(7));
                Vector3 turnBackDirection = (_startPos - transform.position).normalized;
                _controller.Move(turnBackDirection * _speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(turnBackDirection);

                if (Vector3.Distance(transform.position, _startPos) < 0.1f)
                {
                    _isChasing = false;
                }
            }
            
            //if(!_isChasing) _patrol();
            else _patrol();
        
        }

        
        
    }

    private void _patrol(){
        Vector3 moveDirection = isRight ? Vector3.right : Vector3.forward;
        Vector3 moveAmount = moveDirection * _speed * Time.deltaTime;
        _controller.Move(moveAmount);
        _movedDistance += moveAmount.magnitude;
        if (_movedDistance >= _walkDistance)
        {
            StartCoroutine(_lookAround(7));
            _movedDistance = 0f;
        }

        if (moveAmount != Vector3.zero)
        {
        transform.rotation = Quaternion.LookRotation(moveAmount.normalized);
        }
        //transform.forward = Vector3.right;
    }

    private IEnumerator _lookAround(float time){
        float actualSpeed = _speed;
        _speed = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < time && !_fieldOfView.IsInView(out _raycastHit)){
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
