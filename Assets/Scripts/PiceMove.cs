using Unity.VisualScripting;
using UnityEngine;

public class PiceMove : MonoBehaviour
{
    [SerializeField] float timeCount;
    float cout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (cout > 0)
        {
            cout -= Time.deltaTime;
        }
    }
    void Move()
    {
        float horizontal = InputManager.GetMovementInpt().x;
        if (horizontal != 0 && cout <= 0)
        {
            cout = timeCount;
            transform.position += new Vector3(horizontal, 0, 0);
        }
        if (horizontal == 0)
            cout = 0;
        
    }
}
