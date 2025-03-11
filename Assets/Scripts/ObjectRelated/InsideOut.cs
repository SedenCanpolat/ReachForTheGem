using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOut : MonoBehaviour
{
    public void ItemExist(GameObject item){
        Vector3 spawnPosition = transform.position + Vector3.up * 3;
        GameObject spawnedItem = Instantiate(item, spawnPosition, Quaternion.identity, transform);
        spawnedItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);         
    }

    public void NoItem(){
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}

