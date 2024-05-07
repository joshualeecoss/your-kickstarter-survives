using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInput playerInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Debug.Log("EXTRA " + this + " DELETED");
            // Destroy(gameObject);
        }

        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.gameplay.Enable();
    }

    private void OnDisable()
    {
        playerInput.gameplay.Disable();
    }

    public static Vector2 GetMoveDirection()
    {

        return instance.playerInput.gameplay.move.ReadValue<Vector2>();
    }

    public static bool PauseKeyPressed()
    {
        return instance.playerInput.gameplay.pause.WasPressedThisFrame();
    }
}
