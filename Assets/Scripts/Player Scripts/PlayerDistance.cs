using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField] private float _distanceRadius;
    

    public bool CreateAreaForPlayer(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _distanceRadius);
        foreach (Collider colliderObj in hitColliders){
            Interactable interactableObj = colliderObj.GetComponent<Interactable>();
            if(interactableObj && interactableObj.IsInteractable()){
                return true;
            }
        }

        return false;

        // SNAKE GAME 
        // Vector3 wantedPos = transform.position + new Vector3(_distanceRadius, 0, 0);
        // GameObject cube = Instantiate(_cube, wantedPos, transform.rotation);

    }

    
    public void ShowInteractText(TextMeshProUGUI text){//, TextMeshProUGUI button){ {button.text}
        text.text = $"Press E button";
        text.gameObject.SetActive(true);
    }

    public void CloseInteractText(TextMeshProUGUI text){
        text.gameObject.SetActive(false);
    }
    

    
}
