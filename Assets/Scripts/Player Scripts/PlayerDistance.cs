using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField] private float _distanceRadius;
    [SerializeField] private Interaction _interaction;
    [SerializeField] private TextMeshProUGUI _text;
    

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

    
    public void ShowInteractText(){
        _text.text = $"Press E button";
        _text.gameObject.SetActive(true);
    }

    public void CloseInteractText(){
        _text.gameObject.SetActive(false);
    }

    void Update()
    {
          if(_interaction.CheckCanInteract()){ 
            ShowInteractText();
          }

          else{
            CloseInteractText();
          }

    }

    void Start()
    {
        _interaction.OnInteracted += CloseInteractText;
        _interaction.CheckCanInteract += CreateAreaForPlayer;
    }



}
