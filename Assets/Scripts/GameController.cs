using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float speed;
    public static GameController instance;
    public float Speed { get { return speed;} }


    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
