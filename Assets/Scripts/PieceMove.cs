using System.Diagnostics;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    #region Variaves 
    float fall;
    [SerializeField] float timeCount;
    float cout;
    //[SerializeField] float timeCountFlip;
    float countFlip;
    #endregion
    //[SerializeField] float timeCountDown;
    float countDown;

    [SerializeField] bool canRotate;
    [SerializeField] bool Rotate360; 
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

        cout = UpdateTimer(cout);
        countDown = UpdateTimer(countDown);
        countFlip = UpdateTimer(countFlip);

        //    // if (cout > 0)
        //     {
        //         cout -= Time.deltaTime;
        //     }
        //     if (countFlip > 0)
        //     {
        //         countFlip -= Time.deltaTime;
        //     }

        //     if (countDown > 0)
        //     {
        //         countDown -= Time.deltaTime;
        //     }

    }
    void Move()
    {
        float horizontal = InputManager.GetMovementInpt().x;
        float vertical = InputManager.GetMovementInpt().y;
        // if (horizontal != 0 && cout <= 0)
        // {
        //     cout = timeCount;
        //     transform.position += new Vector3(horizontal, 0, 0);

        //     if (Valideteposition())
        //     {

        //     }
        //     else
        //     {
        //         if (horizontal == 1)
        //         {
        //             transform.position += new Vector3(-1, 0, 0);
        //         }
        //         if(horizontal == -1)
        //         {
        //             transform.position += new Vector3(1, 0, 0);
        //         }


        //     }
        // }
        if (horizontal == 1 && cout <= 0)
        {
            transform.position += new Vector3(1, 0, 0);
            cout = timeCount;

            if (Valideteposition())
            {
                GameController.instance.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
               
            }
        }

          
        if (horizontal == -1 && cout <= 0)
        {
            transform.position += new Vector3(-1, 0, 0);
            cout = timeCount;

            if (Valideteposition())
            {
                 GameController.instance.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        if (horizontal == 0)
            cout = 0;

        if (vertical == -1 && countDown <= 0)
        {
            transform.position += new Vector3(0, -1, 0);
            countDown = timeCount;

             if (Valideteposition())
            {
                 GameController.instance.UpdateGrid(this);
            }
            else
            {
                  transform.position += new Vector3(0, 1, 0);
                
            }
        }

    }
    void Flip()
    {
        if (InputManager.GetMovementInput() && countFlip <= 0)
        {
            countFlip = timeCount;
            // transform.Rotate(0, 0, 90);

            // if (Valideteposition())
            // {
            //     GameController.instance.UpdateGrid(this);
            // }
            // else
            // {
            //     transform.Rotate(0, 0, -90);
            // }
            CheckedRotate();
        }
    }
    void Piecefall()
    {
        if (Time.time - fall >= GameController.instance.Speed)
        {
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;


             if (Valideteposition())
            {
                 GameController.instance.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                enabled = false;
                
            }
        }
    }
    float UpdateTimer(float timer)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        return timer;
    }

    bool Valideteposition()
    {
        foreach (Transform child in transform)
        {
            Vector2 blockPos = GameController.instance.Round(child.position);

            if (!GameController.instance.InsideGrid(blockPos))
            {
                return false;
            }

            if (GameController.instance.TransformGridPosition(blockPos) != null &&
            GameController.instance.TransformGridPosition(blockPos).parent != transform)
            {
                return false;
            }
        }
        return true;

    }
        void CheckedRotate()
    {
        if (canRotate)
        {
            if (!Rotate360)
            {
                if (transform.rotation.z < 0)
                {
                    transform.Rotate(0, 0, 90);
                    if (Valideteposition())
                    {
                        GameController.instance.UpdateGrid(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                    if (Valideteposition())
                    {
                        GameController.instance.UpdateGrid(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
            }
            else
            {
                transform.Rotate(0, 0, -90);
                if (Valideteposition())
                {
                    GameController.instance.UpdateGrid(this);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }
    }

}
