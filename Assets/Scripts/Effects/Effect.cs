using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    private PoolID poolID;

    private void Start()
    {
        Play();
        Invoke("ReturnObject", 3.0f);
    }

    void Play()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
            particle.Play();
        }
    }

    void ReturnObject()
    {
        PoolManager.instance.ReturnObject(poolID, gameObject);
    }
}
