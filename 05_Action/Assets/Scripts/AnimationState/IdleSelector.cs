using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSelector : StateMachineBehaviour
{
    const int Not_Select = -1;
    public int testSelect = Not_Select;

    readonly int IdleSelect_Hash = Animator.StringToHash("IdleSelect");

    int prevSelect = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(IdleSelect_Hash, RandomSelect());
    }
    
    int RandomSelect()
    {
        int select = 0;

        // 이전 선택이 0번일 경우에만 일정확률로 1~4를 선택
        // testSelect가 Not_Select가 아닌 경우 무조건 설정된 값으로 변경(0~4만 가능)



        return select;
    }
}
