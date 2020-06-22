using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public bool RotateBall = false;
	private Rigidbody2D ballBody;
	public Vector3 ballPose
	{
		get
		{
			return transform.position;
		}
	}

	private void Awake()
	{
		ballBody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (RotateBall)
		{
			transform.Rotate(0f, 0f, 5f);
		}
	}

	public void Push(Vector2 force)
	{
		ballBody.AddForce(force,ForceMode2D.Impulse);
	}

	public void ActiveRigidbody()
	{
		ballBody.isKinematic = false;
	}

	public void DeactiveRigidbody()
	{
		ballBody.velocity = Vector3.zero;
		ballBody.angularVelocity = 1f;
		ballBody.isKinematic = true;
	}

}
