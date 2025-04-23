using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private GameObject _interactionArea;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _rotateSpeed = 5;
    private void _afterAction()
    {
        throw new System.NotImplementedException();
    }

    void Update() {
        if (CheckPlayerInExplosionArea()) {
            Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, _rotateSpeed * Time.deltaTime);
            gameObject.transform.position = new Vector3(newPos.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
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
