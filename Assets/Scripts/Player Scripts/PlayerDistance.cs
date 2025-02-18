using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField] private float _distanceRadius;
    

    public bool CreateAreaForPlayer(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _distanceRadius);
        foreach (Collider colliderObj in hitColliders){
            Interactable interactableObj = colliderObj.GetComponent<Interactable>();
            if(interactableObj && interactableObj.IsInteractable()){
                Debug.Log(colliderObj.name);
                return true;
            }
        }

        return false;

        // SNAKE GAME 
        // Vector3 wantedPos = transform.position + new Vector3(_distanceRadius, 0, 0);
        // GameObject cube = Instantiate(_cube, wantedPos, transform.rotation);

    }

    

    
}
