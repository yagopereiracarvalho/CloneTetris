using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class InputManager : MonoBehaviour
{
    Vector2 movementInput;

    bool flipInput;
    float countFlip;
    float flipTime;
    public static InputManager instance;
    void Awake()
    {
        instance = this;
        flipTime =0.01f;
    }

    void Update()
    {
        if (countFlip <= 0)
        {
            flipInput = false;
        }
        if (countFlip > 0)
        {
            countFlip -= Time.deltaTime;
        }    
    }
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    public static Vector2 GetMovementInpt()
    {
        return instance.movementInput;
    }
    public void OnFlip(InputValue Value)
    {
        countFlip = flipTime;
        flipInput = Value.isPressed;
    }
    public static bool GetMovementInput()
    {
        return instance.flipInput;
    }

}
