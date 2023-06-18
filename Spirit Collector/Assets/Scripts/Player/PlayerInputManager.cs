using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    //Input Actions
    private InputAction shootAction;
    private InputAction mouseAction;
    private InputAction movementAction;
    private InputAction changeAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        shootAction = playerInput.actions["Shoot"];
        mouseAction = playerInput.actions["MousePosition"];
        movementAction = playerInput.actions["Movement"];
        changeAction = playerInput.actions["Change"];
    }

    public bool IsShootPressed()
    {
        return shootAction.triggered;
    }

    public Vector2 GetMousePosition()
    {
        return mouseAction.ReadValue<Vector2>();
    }

    public Vector2 GetMovement()
    {
        return movementAction.ReadValue<Vector2>();
    }

    public bool IsChangePressed()
    {
        return changeAction.triggered;
    }
}
