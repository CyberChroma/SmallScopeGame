using UnityEngine;

public class AddThisToGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridManager.AddToGrid(gameObject);
    }
}
