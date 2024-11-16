using UnityEngine;

public class FailMessage : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (GetComponent<CanvasGroup>().alpha > 0)
        {
            transform.Translate(new Vector3(0, -0.002f, 0));
            GetComponent<CanvasGroup>().alpha -= 0.002f;
        }
        else Destroy(gameObject);
    }
}
