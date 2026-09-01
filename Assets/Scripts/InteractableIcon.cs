using UnityEngine;
using UnityEngine.Serialization;

public class InteractableIcon : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset = new(0, 2f, 0);

    public void SetTarget(Transform target)
    {
        Target = target;
    }
    
    void LateUpdate()
    {
        if (Target == null)
        {
            gameObject.SetActive(false);
        }
        
        var screenPos = Camera.main.WorldToScreenPoint(Target.position + offset);
        
        if (screenPos.z > 0)
        {
            if (!gameObject.activeSelf) 
                gameObject.SetActive(true);

            transform.position = screenPos;
        }
        else
        {
            if (gameObject.activeSelf) 
                gameObject.SetActive(false);
        }
    }
}
