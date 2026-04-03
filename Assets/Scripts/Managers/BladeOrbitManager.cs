using UnityEngine;
using System.Collections.Generic;

public class BladeOrbitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bladePrefab;

    [SerializeField]
    private int bladeCount = 3;

    [SerializeField]
    private float radius = 2.0f;

    [SerializeField]
    private float rotationSpeed = 150.0f;

    private List<GameObject> blades = new List<GameObject>();
    private float currentAngle = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnBlades();
    }

    private void Update()
    {
        if(blades.Count == 0)
        {
            return;
        }

        currentAngle += rotationSpeed * Time.deltaTime;
        if(currentAngle >= 360.0f)
        {
            currentAngle -= 360.0f;
        }

        for(int i=0; i<blades.Count; ++i)
        {
            // 각 칼날의 고유 간격 계산.
            float angleOffset = (360.0f / bladeCount) * i;

            // 초기 위치 설정.
            UpdateBladePosition(blades[i], currentAngle + angleOffset);
        }
    }

    public void SpawnBlades()
    {
        foreach(GameObject blade in blades)
        {
            Destroy(blade);
        }

        blades.Clear();

        for(int i=0; i<bladeCount; ++i)
        {
            // 각 칼날의 고유 간격 계산.
            float angleOffset = (360.0f / bladeCount) * i;

            GameObject go = Instantiate(bladePrefab, transform);
            blades.Add(go);

            // 초기 위치 설정.
            UpdateBladePosition(go, angleOffset);
        }
    }

    void UpdateBladePosition(GameObject blade, float angle)
    {
        // 각도를 라디안으로 변경.
        float radian = angle * Mathf.Deg2Rad;

        // 원형 좌표 계산.
        float x = Mathf.Cos(radian) * radius;
        float y = Mathf.Sin(radian) * radius;

        blade.transform.localPosition = new Vector3(x, y, 0.0f);

        // 칼날이 회전 방향을 바라보게 하는 코드.
        blade.transform.right = blade.transform.localPosition;
    }

    public void AddBladeCount(int count)
    {
        bladeCount += count;
        SpawnBlades();
    }
}
