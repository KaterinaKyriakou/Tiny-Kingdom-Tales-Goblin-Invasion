using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 MaxPos;
    public Vector2 MinPos;

    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        
    }

    // Update is called once per frame
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
