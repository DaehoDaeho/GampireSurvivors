using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private float fadeSpeed = 3.0f;
    
    private Color color;
        
    // Update is called once per frame
    void Update()
    {
        color.a -= fadeSpeed * Time.deltaTime;
        sr.color = color;

        if(color.a <= 0.0f)
        {
            Destroy(gameObject);
        }    
    }

    public void Init(Sprite currentSprite, Vector3 pos, Quaternion rot, Vector3 scale, bool flipX)
    {
        transform.position = pos;
        transform.rotation = rot;
        transform.localScale = scale;

        sr.sprite = currentSprite;
        color = Color.white;
        color.a = 0.5f;
        sr.color = color;
        sr.flipX = flipX;
    }
}
