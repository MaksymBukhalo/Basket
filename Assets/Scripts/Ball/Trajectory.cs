using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] private List<GameObject>  wallLimit;
	[SerializeField] private int dotsNumber;
	[SerializeField] private GameObject dotsParent;
	[SerializeField] private GameObject dotsPrefab;
	[SerializeField] private float dotSpasing;

	private Transform[] dotsList;
	private Vector3 position;
	private float timeStamp;

	private void Start()
	{
		Hide();
		PrepareDots();
	}

	public void Show()
	{
		dotsParent.SetActive(true);
	}

	public void Hide()
	{
		dotsParent.SetActive(false);
	}

	public void PrepareDots()
	{
		dotsList = new Transform[dotsNumber];

		for (int i = 0; i < dotsNumber; i++)
		{
			dotsList[i] = Instantiate(dotsPrefab, null).transform;
			dotsList[i].parent = dotsParent.transform;
		}
	}

	public void UpdateDots(Vector3 ballPosition,Vector2 forceApplied)
	{
		timeStamp = dotSpasing;
		for (int i = 0; i < dotsNumber; i++)
		{
			position.x = (ballPosition.x + forceApplied.x * timeStamp);
			position.y = (ballPosition.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp* timeStamp)/2+10f;
			position.z = ballPosition.z;
			dotsList[i].position = position;
			int LeftOrRightWalls = 0;
			if (position.x > 0)
			{
				LeftOrRightWalls = 1;
			}
			if (Math.Abs(dotsList[i].position.x) > Math.Abs(wallLimit[LeftOrRightWalls].transform.position.x))
			{
				position.x = wallLimit[LeftOrRightWalls].transform.position.x - (position.x - wallLimit[LeftOrRightWalls].transform.position.x);
				dotsList[i].position = position;
			}
			else
			{
				dotsList[i].position = position;
			}
			dotsList[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			timeStamp += dotSpasing;
		}
	}
}
