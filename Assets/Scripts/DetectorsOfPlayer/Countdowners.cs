using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Countdowners : MonoBehaviour, IResetUpdater
{
    [SerializeField] protected float _startCountdown;
    protected float _countdown;
     [SerializeField] protected TextMeshProUGUI _timerText;
    // [SerializeField] protected TMP_Text _timerText;
    [SerializeField] protected GameObject _explosionArea;
    protected bool _countend = false;
    protected bool _counting = false;
    [SerializeField] protected LayerMask _layerMask;
    protected bool _restartHandled = false;


    protected virtual void Update()
    {
        GameManagementHandling();

        if(CheckPlayerInExplosionArea()){
           
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

        
        if(_countend && !CheckPlayerInExplosionArea()){
            AfterAction();
        }

        ClockFormatting();
    }


    protected abstract void AfterAction();

    
    protected virtual void GameManagementHandling(){

        if(GameManagement.instance.isGameOver){
            IRestarted();
        }

        if (GameManagement.instance.isGameRestarted && !_restartHandled)
        {
            IRestarted();
            _restartHandled = true;
        }

        if (!GameManagement.instance.isGameRestarted)
        {
            _restartHandled = false;
        }
    }


    protected bool CheckPlayerInExplosionArea()
    {
        if(_explosionArea.gameObject.activeInHierarchy){
            Collider[] colliders = Physics.OverlapSphere(_explosionArea.transform.position, _explosionArea.transform.localScale.x, _layerMask);
            if (colliders.Length > 0)
            {
                Debug.Log("Player detected in explosion area!");
                return true;
            }
        }
        
            
        Debug.Log("Player out");
        return false;        
    }


    protected void ClockFormatting(){
        int minutes = Mathf.FloorToInt(_countdown / 60);
        int seconds = Mathf.FloorToInt(_countdown % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    protected virtual void Start()
    {
        _timerText.enabled = false;
        _countdown = _startCountdown;
    }

    public virtual void IRestarted()
    {   _timerText.enabled = false;
        _countdown = _startCountdown;
        _countend = false;
        _counting = false;
    }
}    
