using System.Security.Cryptography;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField]
    private Collider2D areaCollider;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Area") == false)
        {
            return;
        }

        // 플레이어의 위치와 타일 위치의 차이 계산.
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        // Mathf.Abs : 전달한 인자를 절대값으로 반환해주는 함수.
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // 플레이어의 진행 방향 확인.
        Vector2 playerDir = GameManager.Instance.player.inputVec;

        // 삼항연산자.
        float dirX = playerDir.x > 0.0f ? 1.0f : -1.0f;
        float dirY = playerDir.y > 0.0f ? 1.0f : -1.0f;

        // 거리에 따른 재배치 로직.
        // x축 거리가 y축 거리보다 멀면 x축 방향으로 이동.
        if(diffX > diffY)
        {
            // Transform.Translate : 오브젝트를 이동시켜주는 함수.
            // 타일의 크기가 20 * 20이라고 가정할 때, 방향에 따라 40만큼 이동 (3 * 3 구조 기준)
            transform.Translate(Vector3.right * dirX * 40.0f);
        }
        else if(diffX < diffY)  // Y축 거리가 X축 거리보다 멀다면 Y축 방향으로 이동.
        {
            transform.Translate(Vector3.up * dirY * 40.0f);
        }
    }
}
