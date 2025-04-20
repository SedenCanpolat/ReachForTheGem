using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SecurityCamera : Countdowners
{
    [SerializeField] private float _startCountdown;
    private float _countdown;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _explosionArea;
    private bool _countend = false;
    private bool _counting = false;
    [SerializeField] private LayerMask _layerMask;
    private bool _restartHandled = false;

    void Update()
    {
        if(GameManagement.instance.isGameOver){
            _timerText.enabled = false;
            _countend = false;
            _counting = false;
        }

        if (GameManagement.instance.isGameRestarted && !_restartHandled)
        {
            _countdown = _startCountdown;
            _restartHandled = true;
        }

        if (!GameManagement.instance.isGameRestarted)
        {
            _restartHandled = false;
        }

        if(CheckPlayerInExplosionArea(_explosionArea, _layerMask)){
            _timerText.enabled = true;
            _counting = true;
            if(_countend){
                Debug.Log("GAME OVER");
                GameManagement.instance.LostGame();
            }
        
        }
        if(_counting){
            if (_countdown > 0){
                _countdown -= Time.deltaTime;
                if (_countdown < 0){
                    _countdown = 0;
                    _countend = true;
                    
                } 
            }
        }

        if(_countend & !CheckPlayerInExplosionArea(_explosionArea, _layerMask)){
            Debug.Log("Override");
            _countdown = _startCountdown;
            _countend = false;
            _counting = false;
        } 
        
        
    }


    void Start()
    {
        _timerText.enabled = false;
        _countdown = _startCountdown;
    } 
}
