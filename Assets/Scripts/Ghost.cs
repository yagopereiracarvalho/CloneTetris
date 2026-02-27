using UnityEngine;

public class Ghost : MonoBehaviour
{
    PieceMove piece;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        piece = GameObject.FindFirstObjectByType<PieceMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!piece.enabled)
        {
            Destroy(gameObject);
            return;
        }
        if (!GameController.instance.IsPaused)
        {
            UpdateGhostPosition();
        }
    }

    void UpdateGhostPosition()
    {
        transform.position = piece.transform.position;
        transform.rotation = piece.transform.rotation;

        while (ValidationPosition())
        {
            transform.position += Vector3.down;
        }

        transform.position += Vector3.up;
    }

    bool ValidationPosition()
    {
        foreach(Transform child in transform)
        {
            Vector2 posBlock = GameController. instance.Round(child.position);
            if (!GameController.instance.InsideGrid(posBlock))
            {
                return false;
            }
            Transform ocupation = GameController.instance.TransformGridPosition(posBlock);
            if (ocupation != null
                && ocupation.parent != transform
                && ocupation.parent != piece.transform)
            {
                return false;
            }
        }
        return true;
    }
}
