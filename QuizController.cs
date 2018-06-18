using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour 
{
	public Parser_Quiz_Usual pqUsual;								// reference to parser
	public Text questionDisplay;									// text to display question text
	public QuizDataStorer quizDataStorer;							// reference to the script that stores quiz data in classes
	public GameObject[] answerObject;								// the objects that will be used for answers. buttons, asteroids, whatever
	public Image questionImage;										// if the question has an image attached then can output it here if you want

	private int questionNum = 0;									// current question in round.
	private int maxQuestions;										// set max number of questions in the current round.
	private int roundNumber;										// the round (section) to grab info from. if the app has multiple quizs that use the same parser this will be used for getting level 1 questions or level 2 etc.
	

	private List<ScenerySet> listOfScenerySets;
	List<QuizData> listofQdatas;


	[Header("Quiz Options")]
	public bool showQuestionImage = true;
	public bool showQuestionText = true;
	public bool showAnswerImage = true;
	public bool showAnswerText = true;

	//private int tempInt = 0; // for testing purposes


	void Start () 
	{
		listOfScenerySets = pqUsual.Start();
		//Debug.Log(listOfScenerySets.Count);
		quizDataStorer.StoreQuizData(listOfScenerySets);
		//UpdateQuestions();											// show the first question. IF YOU GET NULL REFERENCE OR OUT OF RANGE this might be because this is trying to display a question before the quizDataStorer has finished, either make this script execute after with code execution order in unity or idk maybe use a startbutton instead of calling from start
		//SetMaxRounds();												// call to set max rounds
		}

	void ResetRound()
	{
		questionNum = 0;												// reset round number if you need to
	}

	void SetMaxRounds()
	{
		maxQuestions = quizDataStorer.qData.sectionData[roundNumber].questions.Count - 1;		// syncing up rounds
	}
	

	void UpdateQuestions()											// pulls question and related answers, images and audio at [roundNum]
	{
		if(questionNum <= maxQuestions)
		{
			if (showQuestionText)
				questionDisplay.text = quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].questionText;									// set question text
			if (showQuestionImage)
			questionImage.sprite = Resources.Load<Sprite>(quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].imagePath);				// set question image
			
			
			for(int i = 0; i < answerObject.Length; i++)		// as many answer objects as there are, set text and stuff
			{
				if (showAnswerText)
					answerObject[i].GetComponentInChildren<Text>().text = quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].answers[i].answerText;		// set answer text. looks for text in children
				answerObject[i].GetComponent<AnswerObject>().isCorrect = quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].answers[i].isCorrect;	// set the isCorrect bool
				if (showAnswerImage)
					answerObject[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].answers[i].imagePath);
			}

		}
		else
		{
			Debug.Log("out of questions");			// round over
		}

		questionImage.gameObject.GetComponent<Level1Question>().Animate();
		
	}

	public void ReceiveAnswer(bool isCorrect)
	{
		if(isCorrect)
		{
			Debug.Log("yes");
			//StartCoroutine(QuestionCooldown());		// do you want a pause between questions?
			questionNum++;
			UpdateQuestions();						// if answer is correct then we show next question
		}
		else
		{
			Debug.Log("no");						// if incorrect do nothing. can just go to next question anyway but deduct/not award score or smth
		}
	}



	// get rid of roundNum++ in ReceiveAnswer if you use this
	IEnumerator QuestionCooldown()
	{
		questionNum++;
		yield return new WaitForSeconds(2f);
		UpdateQuestions();
	} 

	


}