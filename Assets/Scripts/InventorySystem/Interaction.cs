using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
     [SerializeField] private PlayerDistance playerDistance;
     [SerializeField] private GameObject player;

     void Update(){
               var seenObject = _raycast();

               if(Input.GetKeyDown(KeyCode.E) && playerDistance.CreateAreaForPlayer()){
                    if(seenObject && seenObject.IsInteractable()){
                         Debug.Log(seenObject.name);
                         seenObject.Interact(null);
                    }  
               }

               if(Input.GetKeyDown(KeyCode.E) && playerDistance.CreateAreaForPlayer()){
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
          
     }

     private Interactable _raycast(){
          Interactable seen = null;
          RaycastHit raycastHit;
          var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          if(Physics.Raycast(ray,out raycastHit)){
               raycastHit.collider.TryGetComponent(out seen);
          }
          return seen;
     }
    
}
