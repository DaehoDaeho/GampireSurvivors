using UnityEngine;

/// <summary>
/// 모든 UI 클래스가 상속할 부모 클래스.
/// 추상 클래스로 만들어 해당 클래스의 객체는 생성할 수 없게 한다.
/// </summary>
public abstract class UIBase : MonoBehaviour
{
    protected virtual void Start()
    {
        CloseUI();
    }

    public virtual void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public bool IsOpened()
    {
        return gameObject.activeSelf;
    }
}
