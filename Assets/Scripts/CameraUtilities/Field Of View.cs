using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   // [SerializeField] private float _radius;
    public float _radius; ///
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


}

