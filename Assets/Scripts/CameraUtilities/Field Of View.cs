using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _angle;

    [SerializeField] private LayerMask _layerMask;
  
  
    public bool IsInView(out RaycastHit hit){
        
        hit = new RaycastHit();
        Vector3 wantedAreaAmount = new Vector3(0, 0, transform.localScale.z);
        Collider[] collidersOnEyes = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        foreach (Collider collider in collidersOnEyes)
        {
            Vector3 targetDirection = (collider.transform.position - transform.position).normalized;
            float seeAngle = Vector3.Angle(transform.forward, targetDirection);
            if (seeAngle <= _angle)
            {
                print("AREA");
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position); //collider.ClosestPoint(transform.position));
                //RaycastHit hit;
                if (Physics.Raycast(transform.position, targetDirection, out hit, distanceToTarget))
                {
                    Debug.Log("First object hit: " + hit.transform.name);
        
                    int hitLayer = hit.transform.gameObject.layer;
                    // Check if the hit layer is included in the _layerMask
                    if ((_layerMask.value & (1 << hitLayer)) != 0)
                    {
                        print("SEE YOU!");
                        return true;
                    }
                    else
                    {
                        print("Different Layer");
                        return false;
                    }
                }
                else
                {
                    // If the ray didn't hit anything, the path is clear
                    print("SEE YOU!");
                    return true;
                }
            }
        }
        return false;
    }


    /*

       void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius); // Draw vision radius

        Vector3 leftLimit = Quaternion.Euler(0, -_angle, 0) * transform.forward * _radius;
        Vector3 rightLimit = Quaternion.Euler(0, _angle, 0) * transform.forward * _radius;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftLimit); // Left vision angle
        Gizmos.DrawLine(transform.position, transform.position + rightLimit); // Right vision angle

        Gizmos.color = Color.green;
        Collider[] collidersOnEyes = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        for (int i = 0; i < collidersOnEyes.Length; i++) 
        {
            Vector3 targetDirection = (collidersOnEyes[i].transform.position - transform.position).normalized;
            float seeAngle = Vector3.Angle(transform.forward, targetDirection);

            if (seeAngle <= _angle) 
            {
                Gizmos.DrawLine(transform.position, collidersOnEyes[i].transform.position); // Line to detected objects
            }
        }
    }
    */

    /*
    private void OnDrawGizmos(){
        Gizmos.color = Color. yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Vector3 fovLine1 = Quaternion.AngleAxis(_angle, transform.up) * transform.forward * _radius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-_angle, transform. up) * transform. forward * _radius;
        Gizmos.color = Color. blue;
        Gizmos.DrawRay(transform. position, fovLine1);
        Gizmos.DrawRay(transform. position, fovLine2);
        Gizmos.color = Color. red;
        Collider[] collidersOnEyes = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        for(int i=0; i<collidersOnEyes.Length; i++){
        Gizmos.DrawRay(transform.position, (collidersOnEyes[i].transform.position - transform. position). normalized * _radius);
        }
        Gizmos.color = Color. black;
        Gizmos.DrawRay(transform. position, transform. forward * _radius);
    }
    */

}

