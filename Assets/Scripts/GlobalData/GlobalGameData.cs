using UnityEngine;

/// <summary>
/// 외부의 어디에서나 접근할 수 있는 정적 클래스.
/// 오브젝트에 연결할 필요 없이 바로 사용할 수 있다.
/// </summary>
public static class GlobalGameData
{
    public static int currentStageIndex = 1;
}
