using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SecurityCamera : Countdowners
{
    [SerializeField] private bool _movable;
    [SerializeField] private float _rotateAmount = 5;
    private float _targetAngle = 45;
    protected override void Update()
    {
        base.Update();
        if(_movable){
           float movingY = Mathf.LerpAngle(transform.eulerAngles.y, _targetAngle, _rotateAmount * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0, movingY, 0);
           if(Mathf.Abs(Mathf.DeltaAngle(movingY, _targetAngle)) < 0.5f){
                _targetAngle = (_targetAngle == 45) ? 135 : 45;
            }
        }
    }

    protected override void AfterAction()
    {
        Debug.Log("Override");
        _countdown = _startCountdown;
        _countend = false;
        _counting = false;
        _timerText.enabled = false;
    }

}
