using UnityEngine;
using TMPro;
using System.Collections;
public class BossIntroManager : MonoBehaviour
{
    public static BossIntroManager instance;

    [SerializeField]
    private TMP_Text warningText;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float moveDuration = 1.0f;  // 카메라를 이동시킬 시간.

    [SerializeField]
    private float stayDuration = 2.0f;  // 보스를 보여주는 시간.

    void Awake()
    {
        instance = this;
        warningText.gameObject.SetActive(false);
    }

    public void PlayBossIntro(Vector3 bossSpawnPos)
    {
        // 코루틴 함수 호출.
        StartCoroutine(IntroRoutine(bossSpawnPos));
    }

    IEnumerator IntroRoutine(Vector3 bossSpawnPos)
    {
        CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
        if(cameraFollow != null)
        {
            // 컴포넌트 비활성화 처리.
            cameraFollow.enabled = false;
        }

        Time.timeScale = 0.0f;
        Vector3 originCamPos = mainCamera.transform.position;
        Vector3 targetCamPos = new Vector3(bossSpawnPos.x, bossSpawnPos.y, originCamPos.z);

        // 호출한 코루틴 함수의 실행이 끝날때까지 대기.
        yield return StartCoroutine(MoveCamera(originCamPos, targetCamPos, moveDuration));

        warningText.gameObject.SetActive(true);
        float elapsed = 0.0f;
        while(elapsed < stayDuration)
        {
            // Time.unscaledDeltaTime : Time.timeScale에 영향을 받지 않는 deltaTime의 일종.
            elapsed += Time.unscaledDeltaTime;

            // 투명도 점멸 (0.0 ~ 1.0 사이)
            float alpha = Mathf.PingPong(elapsed * 4.0f, 1.0f);
            warningText.color = new Color(1.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }

        warningText.gameObject.SetActive(false);

        // 카메라 위치 복귀.
        yield return StartCoroutine(MoveCamera(targetCamPos, originCamPos, moveDuration));

        Time.timeScale = 1.0f;

        if (cameraFollow != null)
        {
            // 컴포넌트 활성화 처리.
            cameraFollow.enabled = true;
        }
    }

    IEnumerator MoveCamera(Vector3 start, Vector3 end, float time)
    {
        float t = 0.0f;
        while (t < moveDuration)
        {
            t += Time.unscaledDeltaTime / time;

            // 가속/감속이 포함된 보간 효과.
            float smooth = t * t * (3.0f - 2.0f * t);

            // Vector3.Lerp : 선형 보간 함수.
            // 세번째 인자는 시간이 아니라 진행비율을 의미.
            mainCamera.transform.position = Vector3.Lerp(start, end, smooth);
            yield return null;
        }

        mainCamera.transform.position = end;
    }
}
