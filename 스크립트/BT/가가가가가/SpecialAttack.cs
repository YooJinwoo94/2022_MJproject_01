using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SpecialAttack : Action
{	
	CharState charState;

	public override void OnStart()
	{
		charState = gameObject.GetComponentInChildren<CharState>();


        switch (charState.charName)
        {
            case "player01":
                InGameScenePlayerCharAttack01 inGameScenePlayerCharAttack01 = gameObject.GetComponentInChildren<InGameScenePlayerCharAttack01>();
                inGameScenePlayerCharAttack01.skillAniStart();
                break;

            case "player02":
                InGameScenePlayerCharAttack02 inGameScenePlayerCharAttack02 = gameObject.GetComponentInChildren<InGameScenePlayerCharAttack02>();
                inGameScenePlayerCharAttack02.skillAniStart();
                break;

            case "player03":
                InGameScenePlayerCharAttack03 inGameScenePlayerCharAttack03 = gameObject.GetComponentInChildren<InGameScenePlayerCharAttack03>();
                inGameScenePlayerCharAttack03.skillAniStart();

                break;

            case "player04":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;

                break;

            case "player05":
             //   charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;

                break;

            case "player06":
               // charState.skillPoint = 0;
               // charState.skillPointBar.Amount = 0;

                break;

            case "player07":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;

                break;

            case "player08":
               // charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;

                break;

            case "player09":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;

                break;

            case "player10":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;

                break;
        }
        switch (charState.charName)
		{
			case "monster01":
			//	charState.skillPoint = 0;
			//	charState.skillPointBar.Amount = 0;
				break;

			case "monster02":
			//	charState.skillPoint = 0;
			//	charState.skillPointBar.Amount = 0;
				break;

			case "monster03":
			//	charState.skillPoint = 0;
			//	charState.skillPointBar.Amount = 0;
				break;

			case "monster04":
			//	charState.skillPoint = 0;
			//	charState.skillPointBar.Amount = 0;
				break;

			case "monster05":
			//	charState.skillPoint = 0;
			//	charState.skillPointBar.Amount = 0;
				break;

			case "monster06":
			//	charState.skillPoint = 0;
			//	charState.skillPointBar.Amount = 0;
				break;

			case "monster07":
			//	charState.skillPoint = 0;
				//charState.skillPointBar.Amount = 0;
				break;

            case "monster08":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;
                break;

            case "monster09":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;
                break;

            case "monster10":
              //  charState.skillPoint = 0;
              //  charState.skillPointBar.Amount = 0;
                break;
        }
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}