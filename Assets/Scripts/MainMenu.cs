using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;

	public void ActiveMenu()
	{
		Menu.SetActive(true);
		Time.timeScale = 1f;
	}
	public void DeactiveMenu()
	{
		Menu.SetActive(false);
		Time.timeScale = 1f;
	}

}
