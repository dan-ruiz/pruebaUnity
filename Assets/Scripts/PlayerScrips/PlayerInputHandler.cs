using System;
using UnityEngine;

// Input handling
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool IsAttackPressed { get; private set; }
    public bool SwitchCandy { get; private set; }

    private void Update()
    {
        // Get raw input for more responsive controls in top-down games
        MovementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized; // Normalize to prevent faster diagonal movement

        //IsAttackPressed = Input.GetButtonDown("Fire1");
        IsAttackPressed = Input.GetKeyDown(KeyCode.Space);
        //SwitchCandy = Input.GetKeyDown(KeyCode.Tab);
    }
}