using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Explosion : MonoBehaviour
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

        if(_checkPlayerInExplosionArea()){
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

        if(_countend & !_checkPlayerInExplosionArea()){
            Debug.Log("Override");
            _countdown = _startCountdown;
            _countend = false;
            _counting = false;
        }
       
            
        

        int minutes = Mathf.FloorToInt(_countdown / 60);
        int seconds = Mathf.FloorToInt(_countdown % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        
    }

    private bool _checkPlayerInExplosionArea()
    {
        Collider[] colliders = Physics.OverlapSphere(_explosionArea.transform.position, _explosionArea.transform.localScale.x, _layerMask);
        if (colliders.Length > 0)
        {
            Debug.Log("Player detected in explosion area!");
            return true;
        }
            
        Debug.Log("Player out");
        return false;
            
        
    } 

    void Start()
    {
        _timerText.enabled = false;
        _countdown = _startCountdown;
    }  
}
