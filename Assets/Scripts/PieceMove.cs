using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    float fall;
    [SerializeField] float timeCount;
    float cout;
    [SerializeField] float timeCountFlip;
    float countFlip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
        Piecefall();

        if (cout > 0)
        {
            cout -= Time.deltaTime;
        }
        if (countFlip > 0)
        {
            countFlip -= Time.deltaTime;
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
    void Flip()
    {
        if (InputManager.GetMovementInput() && countFlip <= 0)
        {
            countFlip = timeCountFlip;
            transform.Rotate(0, 0, 90);
        }
    }
    void Piecefall()
    {
        if (Time.time - fall >= GameController.instance.Speed)
        {
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;
        }
    }

}
