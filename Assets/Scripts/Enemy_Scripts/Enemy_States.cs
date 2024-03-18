using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_States : MonoBehaviour
{
    public Animator animator;
    
    
    enum States{ Idle, Moving, Attack, Kill, Death };

    private States state;
    public Big_Knight_Movement movement;
    private bool isComplete, isMoving; //isGrounded;
    //isMoving refrences if input is being held down
    //isGrounded refrences the characters isGrounded check in their movement, probably dont need it

    private void StateUpdate(){
        switch(state){
            case States.Idle:
                IdleUpdate();
                break;
            case States.Moving:
                MovingUpdate();
                break;
            // case States.Attack:
            //     AttackUpdate();
            //     break;
            // case States.Kill:
            //     KillUpdate();
            //     break;
            // case States.Death:
            //     DeathUpdate();
            //     break;
        }
    }

    private void SelectNewState(){
        isComplete = false;

        if(!isMoving){
            state = States.Idle;
            animator.Play("Idle");
        }
        else{
            state = States.Moving;
            animator.Play("Moving");
        }
    }

    private void IdleUpdate(){
        //When to start &
        if(!isMoving){
            state = States.Idle;

        }
        //When to exit
        else{
            isComplete = true;
            //SelectNewState();
        }
    }
    private void MovingUpdate(){
        if(isMoving){
            state = States.Moving;
        }
        else{
            isComplete = true;
        }
    }
    // private void AttackUpdate(){

    // }
    // private void KillUpdate(){

    // }
    // private void DeathUpdate(){

    // }

    private void Update() {
        if (movement.move.x == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        //print(movement.move.x);

        if (isComplete){
            SelectNewState();
        }
        StateUpdate();
    }
}