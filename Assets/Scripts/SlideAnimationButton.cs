using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SlideAnimationButton : MonoBehaviour {

    public float timeBeforeAnimationStarts = 3.0f;

    //animator reference
    private Animator anim;


    void Start () {
        //get the animator component
        anim = GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        anim.enabled = false;
        Invoke("playAnimation", timeBeforeAnimationStarts);
    }

    public void playAnimation(){

        //enable the animator component
        anim.enabled = true;
        //play the Slidein animation
        anim.Play("ButtonSlideAnimation");
    }
    
    bool AnimatorIsPlaying(){
         return anim.GetCurrentAnimatorStateInfo(0).length >
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
     }

    void Update() {
        if (!AnimatorIsPlaying()){
            GameObject.Destroy(gameObject);
        }
    }

}