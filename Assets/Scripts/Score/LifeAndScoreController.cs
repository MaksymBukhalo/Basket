using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeAndScoreController : MonoBehaviour
{
	public Text Life;
	public int numberOfLives;
	public int numberOfScore;
	public GameManager startPositionBall;
	public GameObject Ring1;
	public GameObject Ring2;
	public ButtonController ButtonController;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Ball")
		{
			if (numberOfLives > 1)
			{
				numberOfLives -= 1;
				Life.text = "Life: " + numberOfLives;
				Ring1.transform.rotation = Quaternion.Euler(Vector3.zero);
				Ring2.transform.rotation = Quaternion.Euler(Vector3.zero);
				collision.transform.position = startPositionBall.PositionBall;
			}
			else
			{
				ButtonController.ResultGame();
			}
		}
	}
}
