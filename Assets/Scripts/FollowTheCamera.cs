using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheCamera : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private  float _rotationSpeed = 5f;
    [SerializeField] private  float _followSpeed;
    private float _cameraDepth = 11f;
    private float _cameraHeight = 8f;

    private Vector3 target;

    void Update()
    {
        Vector3 playerF =_player.transform.forward; // already normalized
        Vector3 playerU = _player.transform.up;
        target =  _player.transform.position - (playerF * _cameraDepth) + playerU * _cameraHeight;
        //transform.rotation = Quaternion.Slerp(transform.rotation, _player.transform.rotation, _rotationSpeed * Time.deltaTime); 
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * _followSpeed);
        transform.LookAt(_player.transform.position + Vector3.up * 6);
    }
}
