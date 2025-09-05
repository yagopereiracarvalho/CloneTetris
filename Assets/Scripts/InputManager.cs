using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Vector2 movementInput;
    bool flipInput;
    float countFlip;
    float flipTime;
    public static InputManager instance;
    private void Awake() 
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        flipTime = 0.01f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (countFlip <= 0)
        {
            flipInput = false;
        }
        if( countFlip > 0)
        {
            countFlip-= Time.deltaTime;
        }
    }
    public void OnMove (InputValue value)
    {
        movementInput = value. Get<Vector2>();
    }
        
    }