using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float _speed;
    public enum MoveType {Left, Right, Rotated}

    public IEnumerator MoveAnimation(MoveType moveType){
        
        Vector3 wantedAmount = Vector3.zero;

        switch(moveType){
        case MoveType.Left:
            wantedAmount = new Vector3(transform.localScale.x + 0.5f, 0, transform.localScale.z);
            break;
        case MoveType.Right:
            wantedAmount = new Vector3(transform.localScale.x + 0.5f, 0, - transform.localScale.z);
            break;
        case MoveType.Rotated:
            wantedAmount = new Vector3(transform.localScale.z, 0, - (transform.localScale.x + 0.5f));
            break;
        }

        Vector3 wantedPos = transform.position - wantedAmount;

        while(Vector3.Distance(transform.position, wantedPos) >= 0.01f){
            transform.position = Vector3.MoveTowards(transform.position, wantedPos, _speed * Time.deltaTime);
            yield return null;
        }  
    }
}
