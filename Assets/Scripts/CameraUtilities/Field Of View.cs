using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    //[SerializeField] private float _angle;

    [SerializeField] private LayerMask _layerMask;
    //[SerializeField] private GameObject _fovSphere;

    private void OnDrawGizmos()
    {
        // Draw the detection area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward / 2, _radius);

        // Detect objects in range
        Collider[] collidersOnEyes = Physics.OverlapSphere(transform.position + transform.forward / 2, _radius, _layerMask);
        foreach (Collider collider in collidersOnEyes)
        {
            Vector3 targetDirection = (collider.transform.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection, out hit, distanceToTarget, _layerMask))
            {
                Gizmos.color = Color.red; // If it hits an object
                Gizmos.DrawLine(transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.1f);
            }
            else
            {
                Gizmos.color = Color.green; // If no obstacle, draw full vision line
                Gizmos.DrawLine(transform.position, collider.transform.position);
            }
        }
    }
  
  
    public bool IsInView(out RaycastHit hit){
        
        hit = new RaycastHit();
        Collider[] collidersOnEyes = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        System.Array.Sort(collidersOnEyes, (a, b) =>
        Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));

        foreach (Collider collider in collidersOnEyes)
        {
            Vector3 targetDirection = (collider.transform.position - transform.position).normalized;
            //float seeAngle = Vector3.Angle(transform.forward, targetDirection);
            //if (seeAngle <= _angle)
            {
                print("AREA");
                
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position); //collider.ClosestPoint(transform.position));
                //RaycastHit hit;
                if (Physics.Raycast(transform.position, targetDirection, out hit, distanceToTarget, _layerMask))
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

