using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour 
{

	public CanvasGroup aPanel;										// if your buttons are childed to a panel with canvas group to disable during question cooldown
	public Text questionDisplay;									// text to display question text
	public QuizDataStorer quizDataStorer;							// reference to the script that stores quiz data in classes
	public GameObject[] answerObject;								// the objects that will be used for answers. buttons, asteroids, whatever
	public Image questionImage;										// if the question has an image attached then can output it here if you want

	private int roundNum = 0;										// i wonder what this is
	private int maxRounds;											// you can probably figure this out

	void Start () 
	{
		UpdateQuestions();											// show the first question
		SetMaxRounds();												// call to set max rounds
	}

	void ResetRound()
	{
		roundNum = 0;												// reset round number if you need to
	}

	void SetMaxRounds()
	{
		maxRounds = quizDataStorer.qData.questions.Count - 1;		// syncing up rounds
	}
	

	void UpdateQuestions()											// pulls question and related answers, images and audio at [roundNum]
	{
		if(roundNum <= maxRounds)
		{
			//aPanel.interactable = true;								// if you're using a pause between questions and disabling buttons
			questionDisplay.text = quizDataStorer.qData.questions[roundNum].questionText;									// set question text
			//questionImage.sprite = Resources.Load<Sprite>(quizDataStorer.qData.questions[roundNum].imagePath);				// set question image
			
			
			for(int i = 0; i < answerObject.Length; i++)
			{
				
				answerObject[i].GetComponentInChildren<Text>().text = quizDataStorer.qData.questions[roundNum].answers[i].answerText;		// set answer text
				answerObject[i].GetComponent<aButtonScript>().isCorrect = quizDataStorer.qData.questions[roundNum].answers[i].isCorrect;	// set the isCorrect bool
			}

		}
		else
		{
			Debug.Log("out of questions");			// round over
		}
		
	}

	public void ButtonClicked(bool isCorrect)
	{
		if(isCorrect)
		{
			//aPanel.interactable = false; 		// disable buttons if you want a pause
			Debug.Log("yes");
			//StartCoroutine(QuestionCooldown());		// do you want a pause between questions?
			UpdateQuestions();						// if answer is correct then we show next question
		}
		else
		{
			Debug.Log("no");						// if incorrect do nothing. can just go to next question anyway but deduct/not award score or smth
		}
	}

	IEnumerator QuestionCooldown()
	{
		roundNum++;
		yield return new WaitForSeconds(2f);
		UpdateQuestions();
	} 
}
