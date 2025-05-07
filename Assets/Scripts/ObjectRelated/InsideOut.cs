using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOut : MonoBehaviour
{
    public GameObject ItemExist(GameObject item){
        Vector3 spawnPosition;
        if(transform.rotation.y != 0){
            spawnPosition = transform.position + Vector3.back * 1.5f;     
        }
        else{
            spawnPosition = transform.position + Vector3.right * 1.5f;
        }
        Quaternion spawnedQuaternion = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, 90);
        GameObject spawnedItem = Instantiate(item, spawnPosition, spawnedQuaternion, transform);
        spawnedItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return spawnedItem;
    }

    public void NoItem(){
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }


}

