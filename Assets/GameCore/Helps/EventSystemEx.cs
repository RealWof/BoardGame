using UnityEngine.EventSystems;

public static class EventSystemEx
{
    public static bool IsPointerOverGameObject()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                {
                    return true;
                }
            }
                
        }
#endif
        return false;
    }
}