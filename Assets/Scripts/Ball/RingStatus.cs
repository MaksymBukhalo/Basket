using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingStatus : MonoBehaviour
{
    public bool HitBall;
	public TriggerHit TriggerCollor;
	public List<GameObject> Obstacles;
	public GameObject Grid;

	private void Start()
	{
		HitBall = false;
	}
}
