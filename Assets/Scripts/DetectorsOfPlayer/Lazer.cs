using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private GameObject _interactionArea;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _rotateSpeed = 5;
    private float _targetAmount;
    private Vector3 _startPos;
    Vector3 _target; 
    private bool _goingLeft = true;
    private bool _playerdetection;
    private void _afterAction()
    {
        throw new System.NotImplementedException();
    }

    void Update() {
        
        if(!_playerdetection){
            if(Vector3.Distance(gameObject.transform.position, _target) <= 0.01f){
            _goingLeft = !_goingLeft;
            _target = _goingLeft ? _startPos - Vector3.right * _targetAmount : _startPos;

            /*     
            if(_goingLeft){
                _target = _startPos;
                _goingLeft = false;
            }
            else {
                _target = _startPos - Vector3.right * _targetAmount;
                _goingLeft = true;
            }
            */            
            }

            Vector3 mov = Vector3.MoveTowards(gameObject.transform.position, _target, _rotateSpeed * Time.deltaTime);
            gameObject.transform.position = new Vector3(mov.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        
        
        if (CheckPlayerInExplosionArea()) {
            _playerdetection = true;
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, _rotateSpeed * Time.deltaTime);
            gameObject.transform.position = new Vector3(newPos.x, gameObject.transform.position.y, gameObject.transform.position.z);
            if(Mathf.Abs(gameObject.transform.position.x - _player.transform.position.x) <= 1.5f){
                print("Touch");
                StartCoroutine(_countdown(2));
            }
        }
        if(!CheckPlayerInExplosionArea()){
            _playerdetection = false;
            StopAllCoroutines();
        }
    }

    IEnumerator _countdown(float second){
        yield return new WaitForSeconds(second);
        GameManagement.instance.LostGame();
        Debug.Log("Countdown finished!");  
    }

    void Start()
    {
        _targetAmount = _interactionArea.transform.localScale.y * 3.5f;
        _startPos = gameObject.transform.position;
        _target = _startPos - Vector3.right * _targetAmount;
    }

    protected bool CheckPlayerInExplosionArea()
    {
       
        Collider[] colliders = Physics.OverlapSphere(_interactionArea.transform.position, _interactionArea.transform.localScale.y * 2, _layerMask);
        if (colliders.Length > 0)
        {
            Debug.Log("Player detected in interaction area!");
            return true;
        }
        
        Debug.Log("Player out");
        return false;        
    }


}
