using UnityEngine;

public class SetStartingPosition : MonoBehaviour
{
    public VectorValue startingPosition;

    void Start()
    {
        transform.position = startingPosition.initialValue;
    }
}
