using UnityEngine;


[RequireComponent(typeof(Character))]

public class ControlController : MonoBehaviour
{
    private Character player;
        

    private void Start()
    {
        player = GetComponent<Character>();
    }

    private void Update()
    {
        if (player == null) return;

        ControlKeyboard();
    }


    private void ControlKeyboard()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector2 movementVector = new Vector2(horizontalMovement, verticalMovement);

        if (movementVector.magnitude > 1)
        {
            movementVector = movementVector.normalized;
        }

        player.MovementControl = movementVector;
    }
}
