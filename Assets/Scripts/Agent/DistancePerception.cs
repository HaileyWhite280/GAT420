using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders =  Physics.OverlapSphere(transform.position, distance);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;

            if (tagName == "" || collider.CompareTag(tagName))
            {
               //Compute Angle
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                               
                float cos = Vector3.Dot(transform.forward, direction);

                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

                //check if angle is less or equal to max angle
                if(angle <= this.angle)
                {
                    result.Add(collider.gameObject);
                }
            }
        }

        return result.ToArray();
    }
}
