using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 MaxPos;
    public Vector2 MinPos;

    void Start()
    {
      transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        
    }

    void FixedUpdate()
    {
      if(transform.position != target.position){
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        targetPosition.x = Mathf.Clamp(targetPosition.x, MinPos.x, MaxPos.x );
        targetPosition.y = Mathf.Clamp(targetPosition.y, MinPos.y, MaxPos.y );
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
      }  
    }
}
