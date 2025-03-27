using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _countdown;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _explosionArea;
    private bool _counting = false;
    [SerializeField] private LayerMask _layerMask;

    void Update()
    {
        if(_counting){
            if (_countdown > 0){
                _countdown -= Time.deltaTime;
                if (_countdown < 0) _countdown = 0;
            }
        
        }

        int minutes = Mathf.FloorToInt(_countdown / 60);
        int seconds = Mathf.FloorToInt(_countdown % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        _checkPlayerInExplosionArea();
    }

    private void _checkPlayerInExplosionArea()
    {
        if(!_counting){
            Collider[] colliders = Physics.OverlapSphere(_explosionArea.transform.position, _explosionArea.transform.localScale.x, _layerMask);
            if (colliders.Length > 0)
            {
                _counting = true;
                _timerText.enabled = true;
                Debug.Log("Player detected in explosion area!");
            }    
            
        }
       
    } 

    void Start()
    {
        _timerText.enabled = false;
    }  
}
