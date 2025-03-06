using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

     public Func<bool> CheckCanInteract;
     public Action OnInteracted;

     public Func<bool> CanView;

     void Update(){
          
          var seenObject = _raycast();

          bool canInteract = true;
          if(CheckCanInteract != null){
               canInteract = CheckCanInteract(); 
          }
   
        
          if(Input.GetKeyDown(KeyCode.E)){//GetMouseButtonDown(0)){
               if(seenObject && seenObject.IsInteractable() && canInteract){                         
                    Debug.Log(seenObject.name);
                    seenObject.Interact(null);
                    OnInteracted?.Invoke();
               }  
          }

          if(Input.GetKeyDown(KeyCode.F)){//GetMouseButtonUp(0)){
               if(seenObject && seenObject.IsInteractable() && Inventory.instance.ItemOnHand && canInteract){
                    if(seenObject.Interact(Inventory.instance.ItemOnHand)){
                         var item = Inventory.instance.ItemOnHand;
                         print("item " + item);
                         Inventory.instance.EmptyHand();
                         Inventory.instance.RemoveItem(item);
                    }
               }
               Inventory.instance.EmptyHand();    
          }
        
          /*

               var seenObject = _raycast();
               
               if(_playerDistance.CreateAreaForPlayer()){
                    _playerDistance.ShowInteractText(_text);
               }

               else{
                    _playerDistance.CloseInteractText(_text);
               }

               if(Input.GetKeyDown(KeyCode.E) && _playerDistance.CreateAreaForPlayer()){
                    if(seenObject && seenObject.IsInteractable()){                         
                         Debug.Log(seenObject.name);
                         seenObject.Interact(null);
                         _playerDistance.CloseInteractText(_text);
                    }  
               }

               if(Input.GetKeyDown(KeyCode.E) && _playerDistance.CreateAreaForPlayer()){
                    if(seenObject && seenObject.IsInteractable() && Inventory.instance.ItemOnHand){
                         if(seenObject.Interact(Inventory.instance.ItemOnHand)){
                              var item = Inventory.instance.ItemOnHand;
                              print("item " + item);
                              Inventory.instance.EmptyHand();
                              Inventory.instance.RemoveItem(item);
                         }
                    }
                    Inventory.instance.EmptyHand();    
               }

          */ 

          
     }

     [SerializeField] private FieldOfView FOV;

    void Start()
    {
     
    }


     private Interactable _raycast(){
          Interactable seen = null;
          RaycastHit raycastHit;
         
          
          if (FOV.IsInView(out raycastHit))
          {
              Debug.Log("I see: " + raycastHit.transform.name);
          

          //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          //if(Physics.Raycast(ray, out raycastHit)){
              raycastHit.collider.TryGetComponent(out seen);
          }
          return seen;
     }
    
}
