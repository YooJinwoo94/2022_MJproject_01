using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCharMove : MonoBehaviour
{
    [SerializeField]
    Transform moveForwardPos;
    [SerializeField]
    Transform charTransform;
   // [SerializeField]
   // Rigidbody2D rid;

    CharState charState;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    private void Start()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.transform.GetComponent<CharState>();
    }

    public void moveToEnemy(int wayX)
    {
        charTransform.position =
            Vector2.MoveTowards(charTransform.position, inGameSceneCheckTargetAndGetDistance.target.transform.position, 1f * Time.deltaTime);
    }

    public void moveForward()
    {
        charTransform.position =
          Vector2.MoveTowards(charTransform.position, moveForwardPos.position, 1f * Time.deltaTime);
    }


    public void moveToBush(GameObject bush)
    {
        charTransform.position =
            Vector2.MoveTowards(charTransform.position, bush.transform.position, 1f * Time.deltaTime);
    }

    public bool checkCharIsInBush(GameObject bush)
    {
        if (charTransform.position== bush.transform.position)
        {
            return true;
        }
        return false;
    }
}
