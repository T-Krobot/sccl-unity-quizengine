using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aButtonScript : MonoBehaviour 
{
	public QuizController qController;

	[HideInInspector]
	public bool isCorrect;
	
	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void OnClick()
	{
		qController.ButtonClicked(isCorrect);
	}
}
