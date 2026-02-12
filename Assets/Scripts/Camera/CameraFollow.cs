using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform targetTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject go = GameObject.Find("Player");
        if (go != null)
        {
            targetTransform = go.transform;
        }
    }

    private void LateUpdate()
    {
        if(targetTransform != null)
        {
            transform.position = targetTransform.position + new Vector3(0.0f, 0.0f, -10.0f);
        }
    }
}
