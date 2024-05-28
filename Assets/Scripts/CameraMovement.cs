using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform Player;

    private void Update()
    {

        //Follow player
        transform.position = new Vector3(Player.position.x, Player.position.y+1, transform.position.z);

    }
}