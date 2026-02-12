using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        moveSpeed = 5.0f;
        rigidBody = GetComponent<Rigidbody2D>();
        Debug.Log("PlayerController Awake 호출!!");
    }

    private void OnEnable()
    {
        Debug.Log("PlayerController OnEnable 호출!!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("PlayerController Start 호출!!");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Debug.Log("Update : ESC 입력!!!");
        }
    }

    private void FixedUpdate()
    {
        if(rigidBody != null)
        {
            rigidBody.linearVelocity = new Vector2(moveSpeed, 0.0f);
        }
    }
}
