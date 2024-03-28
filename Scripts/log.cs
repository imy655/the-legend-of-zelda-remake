﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy {					

	public Rigidbody2D myRigidbody;
	public Transform target;
	public float chaseRadius;
	public float attackRadius;
	public Transform homePosition;
	public Animator anim;

	// Use this for initialization
	void Start () {
		currentState = EnemyState.idle;
		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		target = GameObject.FindWithTag("Player").transform;
		anim.SetBool("wakeUp", true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckDistance();
	}

	public virtual void CheckDistance()
	{
		if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
		{
			if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
			{
				Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

				changeAnim(temp - transform.position);
				myRigidbody.MovePosition(temp);
				ChangeState(EnemyState.walk);
				anim.SetBool("wakeUp", true);
			}
		}else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
		{
			anim.SetBool("wakeUp", false);
		}
	}

	private void SetAnimFloat(Vector2 setvector)
	{
		anim.SetFloat("moveX", setvector.x);
		anim.SetFloat("moveY", setvector.y);
	}

	public void changeAnim(Vector2 direction) {
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			if (direction.x > 0){
				SetAnimFloat(Vector2.left);
			}else if (direction.x < 0)
			{
				SetAnimFloat(Vector2.up);
			}
		}else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
			if (direction.y > 0) {
				SetAnimFloat(Vector2.right);
			} else if (direction.y < 0)
			{
				SetAnimFloat(Vector2.down);
			}
			

        }
    }

	private void ChangeState(EnemyState newState) {
		if(currentState != newState)
		{
			currentState = newState;
		}
	}
}
