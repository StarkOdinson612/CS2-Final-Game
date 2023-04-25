using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
	private EnemyState thisState;

	public EnemyState getState() { return thisState; }

	public void setState(EnemyState state)
	{
		thisState = state;
	}
}

public enum EnemyState
{
	PATROLLING = 1,
	STOPPED = 2,
	DISCOVERED_PLAYER = 3,
	CAUGHT_PLAYER = 4,
	STUNNED = 5
}
