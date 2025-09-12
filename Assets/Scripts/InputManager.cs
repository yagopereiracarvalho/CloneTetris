using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class InputManager : MonoBehaviour
{
    Vector2 movementInput;
    public static InputManager instance;
    void Awake()
    {
        instance = this;
    }
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    public static Vector2 GetMovementInpt()
    {
        return instance.movementInput;
    }

}
