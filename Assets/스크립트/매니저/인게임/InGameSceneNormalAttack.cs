using BehaviorDesigner.Runtime;
using UnityEngine;


public class InGameSceneNormalAttack : MonoBehaviour
{
    [SerializeField]
    CharState playerCharState;
    CharState enemyCharState;
    [SerializeField]
    BehaviorTree behaviorTree;


    SharedGameObject obj;
    GameObject target;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;


    private void Start()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
    }


    public void attackEnemy()
    {
        //obj = (SharedGameObject)behaviorTree.GetVariable("target");
        //target = obj.Value;

        if (inGameSceneCheckTargetAndGetDistance.target == null) return;

        enemyCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();

        int damage = playerCharState.attackPower - enemyCharState.defensePower;
        if (damage <= 0) damage = 1;
        enemyCharState.hpPoint -= damage;
    }
}
