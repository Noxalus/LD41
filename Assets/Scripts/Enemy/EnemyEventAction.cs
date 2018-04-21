using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public struct EnemyAction
{
    public Enemy1 enemy;

    public EnemyAction(Enemy1 enemy)
    {
        this.enemy = enemy;
    }
}

[System.Serializable]
public class EnemyActionEvent : UnityEvent<EnemyAction> { }