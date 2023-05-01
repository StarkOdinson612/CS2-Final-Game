using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
	[SerializeField]
	private EnemyState thisState;
	private Transform playerPos = null;

	public EnemyState getState() { return thisState; }

	public void setState(EnemyState state)
	{
		thisState = state;
	}

	public void setPlayerPos(Transform p) { playerPos = p; }

	public Transform getPlayerPos() { return playerPos; }
}

public enum EnemyState
{
	PATROLLING = 1,
	STOPPED = 2,
	DISCOVERED_PLAYER = 3,
	CAUGHT_PLAYER = 4,
	STUNNED = 5
}
