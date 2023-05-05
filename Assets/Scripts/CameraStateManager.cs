using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateManager : MonoBehaviour
{
	[SerializeField]
	private CameraState thisState;
	private Transform playerPos = null;

	public CameraState getState() { return thisState; }

	public void setState(CameraState state)
	{
		thisState = state;
	}

	public void setPlayerPos(Transform p) { playerPos = p; }

	public Transform getPlayerPos() { return playerPos; }
}

public enum CameraState
{
	PATROLLING = 1,
	DISCOVERED_PLAYER = 3,
	CAUGHT_PLAYER = 4,
}
