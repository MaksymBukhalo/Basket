using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerHit : MonoBehaviour
{

	public Text Score;

	[SerializeField] private LifeAndScoreController lifeAndScoreTrigger;
	[SerializeField] private SpriteRenderer topRing;
	[SerializeField] private SpriteRenderer bottomRing;
	[SerializeField] private GameObject triggerPosition;
	[SerializeField] private GameObject grid;
	[SerializeField] private RingStatus ringStatus;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Ball")
		{
			ringStatus.HitBall = true;
			AddScore();
			grid.GetComponent<Animation>().Play();
			grid.GetComponent<AudioSource>().Play();
			collision.transform.position = triggerPosition.transform.position;
			collision.GetComponent<Ball>().DeactiveRigidbody();
			collision.GetComponent<Ball>().ActiveRigidbody();
			collision.transform.SetParent(triggerPosition.transform);
			ActiveRing();
			collision.GetComponent<Ball>().RotateBall = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Ball")
		{
			ringStatus.HitBall = false;
			collision.GetComponent<Ball>().RotateBall = true;
		}
	}

	private void ActiveRing()
	{
		topRing.color = Color.gray;
		bottomRing.color = Color.gray;
	}

	public void DeactiveRing()
	{
		topRing.color = new Color(225f, 0f, 0f);
		bottomRing.color = new Color(225f, 0f, 0f);
	}

	private void AddScore()
	{
		if (ringStatus.Obstacles[0].activeSelf == true || ringStatus.Obstacles[1].activeSelf == true)
		{
			//add score
			if (lifeAndScoreTrigger.numberOfLives == 3)
			{
				lifeAndScoreTrigger.numberOfScore += 100;
			}
			else if (lifeAndScoreTrigger.numberOfLives == 2)
			{
				lifeAndScoreTrigger.numberOfScore += 50;
			}
			else if (lifeAndScoreTrigger.numberOfLives == 1)
			{
				lifeAndScoreTrigger.numberOfScore += 25;
			}
			Score.text = "Score: " + lifeAndScoreTrigger.numberOfScore;

			//restart life after hit
			lifeAndScoreTrigger.numberOfLives = 3;
			lifeAndScoreTrigger.Life.text = "Life: " + lifeAndScoreTrigger.numberOfLives;
		}
	}
}
