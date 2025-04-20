using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Countdowners : MonoBehaviour
{
    public bool CheckPlayerInExplosionArea(GameObject _explosionArea, LayerMask _layerMask)
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

    public void ClockFormatting(float _countdown, TextMeshProUGUI _timerText){
        int minutes = Mathf.FloorToInt(_countdown / 60);
        int seconds = Mathf.FloorToInt(_countdown % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    } 
}
