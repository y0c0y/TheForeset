using UnityEngine;

public enum Condition
{
    Normal,
    GoTrough,
    Hide,
    SpiderWeb,
    Kick,
}

public class ConditionMode
{
    
    public static Condition presentCondition;
    //플레이어 컨디션 변화
    public static void PlayerCondition(Condition condition)
    {
        presentCondition = condition;

        switch (condition)
        {
            case Condition.Normal:
                Debug.Log("Normal");
                break;
            
            case Condition.GoTrough:
                Debug.Log("GoTrough");
                break;
            
            case Condition.Hide:
                Debug.Log("Hide");
                break;
            
            case Condition.SpiderWeb:
                Debug.Log("SpiderWeb");
                break;
            
            case Condition.Kick:
                Debug.Log("Kick");
                break;
        }
    }
}

