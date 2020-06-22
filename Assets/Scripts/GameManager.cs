using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

	public Vector3 PositionBall;

	[SerializeField] private Canvas canvas;
	[SerializeField] private List<GameObject> rings;
	[SerializeField] private Ball ball;
	[SerializeField] private float pushForce;
	[SerializeField] private Trajectory trajectory;
	[SerializeField] private float maxLenghtTrajectory;
	[SerializeField] private float minLenghtTrajectory;
	[SerializeField] private Text life;
	[SerializeField] private LifeAndScoreController lifeAndsCoreController;


	private bool isDragging = false;
	private float timePause;
	private Vector2 startPoint;
	private Vector2 endPoint;
	private Vector2 direction;
	private Vector2 force;
	private float distance;
	private float minBottomRingLine;

	private void Start()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		ball.DeactiveRigidbody();
		minBottomRingLine = rings[0].transform.position.y;
	}

	private void Update()
	{
		for (int i = 0; i < rings.Count; i++)
		{
			if (rings[i].GetComponent<RingStatus>().HitBall == true)
			{
				if (Input.GetMouseButtonDown(0))
				{
					isDragging = true;
					OnDragStart();
				}

				if (Input.GetMouseButtonUp(0))
				{
					isDragging = false;
					OnDragEnd();
				}

				if (isDragging)
				{
					OnDrag();
				}
				ringManager();
			}
		}
	}

	private void OnDragStart()
	{
		for (int i = 0; i < rings.Count; i++)
		{
			if (rings[i].GetComponent<RingStatus>().HitBall == true)
			{
				PositionBall = ball.transform.position;
				startPoint = Input.mousePosition;
				ball.DeactiveRigidbody();
			}
		}
	}

	private void OnDrag()
	{
		endPoint = Input.mousePosition;
		RingRotation(startPoint, endPoint);
		distance = Vector2.Distance(startPoint, endPoint) / 20;
		direction = (startPoint - endPoint).normalized;
		if (Math.Abs(distance) > minLenghtTrajectory)
		{
			trajectory.Show();
			if (Math.Abs(distance) < maxLenghtTrajectory)
			{
				TransformGrid(distance);
				force = direction * distance * pushForce;
			}
			else
			{
				TransformGrid(maxLenghtTrajectory);
				force = direction * maxLenghtTrajectory * pushForce;
			}
			Debug.DrawLine(startPoint, endPoint);
			trajectory.UpdateDots(ball.ballPose, force / 2);
		}
		else
		{
			trajectory.Hide();
		}
	}

	private void OnDragEnd()
	{
		if (Math.Abs(distance) > minLenghtTrajectory)
		{
			ball.GetComponent<AudioSource>().Play();
			ball.ActiveRigidbody();
			ball.Push(force * 2);
			trajectory.Hide();
			rings[0].GetComponent<RingStatus>().Grid.transform.localScale = new Vector3(1f, 1f, 1f);
			rings[1].GetComponent<RingStatus>().Grid.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	private void TransformGrid(float distance)
	{
		float transformGrid = distance / maxLenghtTrajectory * 0.5f;
		if (rings[0].GetComponent<RingStatus>().HitBall == true)
		{
			rings[0].GetComponent<RingStatus>().Grid.transform.localScale = new Vector3(1f, 1f + transformGrid, 1f);
		}
		else if (rings[1].GetComponent<RingStatus>().HitBall == true)
		{
			rings[1].GetComponent<RingStatus>().Grid.transform.localScale = new Vector3(1f, 1f + transformGrid, 1f);
		}

	}

	private void RingRotation(Vector2 startTrajectory, Vector2 endTrajectory)
	{
		for (int i = 0; i < rings.Count; i++)
		{
			if (rings[i].GetComponent<RingStatus>().HitBall == true)
			{

				float rotationZ = (endTrajectory.x - startTrajectory.x) / 2;
				float differenceY = (endTrajectory.y - startTrajectory.y);
				if (differenceY >= 0)
				{
					rings[i].transform.rotation = Quaternion.Euler(0f, 0f, (rotationZ + 180) * -1);
				}
				else if (differenceY < 0)
				{
					rings[i].transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
				}
			}
		}
	}

	private void ObstaclesManeger()
	{
		//activate obstacles
		if (rings[0].GetComponent<RingStatus>().HitBall == true)
		{
			int random = Random.Range(0, 2);
			rings[1].GetComponent<RingStatus>().Obstacles[random].SetActive(true);
			if (random == 0)
			{
				rings[1].GetComponent<RingStatus>().Obstacles[random].GetComponent<Animation>().Play("ObstacleLineAnimation");
			}
		}
		else if (rings[1].GetComponent<RingStatus>().HitBall == true)
		{
			int random = Random.Range(0, 2);
			rings[0].GetComponent<RingStatus>().Obstacles[random].SetActive(true);
			if (random == 0)
			{
				rings[0].GetComponent<RingStatus>().Obstacles[random].GetComponent<Animation>().Play("ObstacleLineAnimation");
			}
		}

		//check speed Obstacles
		NewSpeedObstacles();
	}

	private void NewSpeedObstacles()
	{
		if (lifeAndsCoreController.numberOfScore <= 1000)
		{
			rings[0].GetComponent<RingStatus>().Obstacles[0].GetComponent<Animation>()["ObstacleLineAnimation"].speed = 1f;
			rings[0].GetComponent<RingStatus>().Obstacles[1].GetComponent<RotationPendulum>().SpeedRotation = 1f;

			rings[1].GetComponent<RingStatus>().Obstacles[0].GetComponent<Animation>()["ObstacleLineAnimation"].speed = 1f;
			rings[1].GetComponent<RingStatus>().Obstacles[1].GetComponent<RotationPendulum>().SpeedRotation = 1f;
		}
		else if (lifeAndsCoreController.numberOfScore > 1000 && lifeAndsCoreController.numberOfScore <= 5000)
		{
			rings[0].GetComponent<RingStatus>().Obstacles[0].GetComponent<Animation>()["ObstacleLineAnimation"].speed = 1.25f;
			rings[0].GetComponent<RingStatus>().Obstacles[1].GetComponent<RotationPendulum>().SpeedRotation = 1.25f;

			rings[1].GetComponent<RingStatus>().Obstacles[0].GetComponent<Animation>()["ObstacleLineAnimation"].speed = 1.25f;
			rings[1].GetComponent<RingStatus>().Obstacles[1].GetComponent<RotationPendulum>().SpeedRotation = 1.25f;
		}
		else if(lifeAndsCoreController.numberOfScore > 5000)
		{
			rings[0].GetComponent<RingStatus>().Obstacles[0].GetComponent<Animation>()["ObstacleLineAnimation"].speed = 1.5f;
			rings[0].GetComponent<RingStatus>().Obstacles[1].GetComponent<RotationPendulum>().SpeedRotation = 1.5f;

			rings[1].GetComponent<RingStatus>().Obstacles[0].GetComponent<Animation>()["ObstacleLineAnimation"].speed = 1.5f;
			rings[1].GetComponent<RingStatus>().Obstacles[1].GetComponent<RotationPendulum>().SpeedRotation = 1.5f;
		}
	}

	private void ringManager()
	{
		if (rings[0].GetComponent<RingStatus>().HitBall == true && rings[0].transform.position.y > rings[1].transform.position.y)
		{
			rings[1].GetComponent<Animation>().Play("BasketBallRingDisappearance");
			float time = Time.time;
			TimePause(time);
			if ((time - timePause) > 0.45f)
			{
				rings[1].GetComponent<Animation>().Play("TriggerDisactivated");
				MoveRingStartLine(rings[0]);
				if (rings[0].transform.position.y < minBottomRingLine + 5f)
				{
					rings[0].transform.position = new Vector3(rings[0].transform.position.x, minBottomRingLine, rings[0].transform.position.z);
					NewPositionRing(rings[1]);
					rings[1].GetComponent<RingStatus>().TriggerCollor.DeactiveRing();
					ObstaclesManeger();
				}
			}
		}
		else if (rings[1].GetComponent<RingStatus>().HitBall == true && rings[1].transform.position.y > rings[0].transform.position.y)
		{
			rings[0].GetComponent<Animation>().Play("BasketBallRingDisappearance");
			float time = Time.time;
			TimePause(time);
			if ((time - timePause) > 0.45f)
			{
				rings[0].GetComponent<Animation>().Play("TriggerDisactivated");
				MoveRingStartLine(rings[1]);
				if (rings[1].transform.position.y < minBottomRingLine + 5f)
				{
					rings[1].transform.position = new Vector3(rings[1].transform.position.x, minBottomRingLine, rings[1].transform.position.z);
					NewPositionRing(rings[0]);
					rings[0].GetComponent<RingStatus>().TriggerCollor.DeactiveRing();
					ObstaclesManeger();
				}
			}
		}
	}
	private void MoveRingStartLine(GameObject ring)
	{
		//deactivate obstacles
		ring.GetComponent<RingStatus>().Obstacles[0].SetActive(false);
		ring.GetComponent<RingStatus>().Obstacles[1].SetActive(false);
		//move the ball ring on the starting line
		Vector3 startPosition = ring.transform.position;
		Vector3 endPosition = new Vector3(ring.transform.position.x, minBottomRingLine, ring.transform.position.z);
		ring.transform.position = Vector3.Lerp(startPosition, endPosition, 0.2f);
	}

	private void TimePause(float times)
	{
		if ((times - timePause) > 1f)
		{
			timePause = times;
		}
	}

	private void NewPositionRing(GameObject ring)
	{
		float xValue = (canvas.GetComponent<RectTransform>().sizeDelta.x / 2 - 75) * canvas.transform.localScale.x;
		float yValue = (canvas.GetComponent<RectTransform>().sizeDelta.y / 4) * canvas.transform.localScale.y;
		float randomPositionX = UnityEngine.Random.Range(-xValue, xValue);
		float randomPositionY = UnityEngine.Random.Range(-yValue + 100f, yValue);
		ring.transform.position = new Vector3(randomPositionX, randomPositionY, ring.transform.position.z);
		ring.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		ring.transform.localScale = new Vector3(1f, 1f, 1f);
		
	}
}
