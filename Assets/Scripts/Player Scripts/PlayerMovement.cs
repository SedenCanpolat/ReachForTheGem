using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed = 5;
    [SerializeField] private  float _rotationSpeed;
    private float _originalSpeed;
    
    
    private void Start() {
        _controller = GetComponent<CharacterController>();
        _originalSpeed = _speed;
    }

    void Update()
    {
        if(GameManagement.instance.isGameOver){
            _speed = 0;
        } 
        if(GameManagement.instance.isGameRestarted){
            _speed = _originalSpeed; 
        }

        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;
        _controller.Move(move * Time.deltaTime * _speed);

        if (move.magnitude >= 0.1f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); 
        }
    }
}
