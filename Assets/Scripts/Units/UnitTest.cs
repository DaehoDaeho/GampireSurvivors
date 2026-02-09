using UnityEngine;

public class UnitTest : MonoBehaviour
{
    public DerivedUnit derivedUnit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        derivedUnit.TakeDamage(10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
