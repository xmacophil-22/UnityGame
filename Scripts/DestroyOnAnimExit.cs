using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimExit : StateMachineBehaviour
{
    /////////////////////////////////////////zerstört Objekt, wenn die animation Endet, für Rohstoffpopup
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex){
        Destroy(animator.gameObject, animatorStateInfo.length);
    }
}
