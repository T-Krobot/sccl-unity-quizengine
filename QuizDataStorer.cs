using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDataStorer : MonoBehaviour 
{
	public QuizData qData;

	public void StoreQuizData(ScenerySet latstScenery)
    {
        Debug.Log(latstScenery.Questions_list.Count);
        for(int i = 0; i < latstScenery.Questions_list.Count; i++)
        {   // storing questin data, can add fields to QuestionData class and then add lines here
            QuestionData q = new QuestionData();
            q.questionText = latstScenery.Questions_list[i].question_text;
            q.imagePath = latstScenery.Questions_list[i].question_image;
            q.audioPath = latstScenery.Questions_list[i].question_audio;
            q.questionNum = i;
            for(int b = 0; b < latstScenery.Questions_list[i].option_parts.Count; b++)
            {   // can add extra data for answers here. need to add fields to Answerdata first
                Answerdata a = new Answerdata();
                a.answerText = latstScenery.Questions_list[i].option_parts[b].text;
                a.imagePath = latstScenery.Questions_list[i].option_parts[b].image;
                a.audioPath = latstScenery.Questions_list[i].option_parts[b].audio;
                a.answerNum = latstScenery.Questions_list[i].option_parts[b].answer_number;
                a.isCorrect = latstScenery.Questions_list[i].option_parts[b].iscorrect;

                q.answers.Add(a);

            }

            qData.questions.Add(q);
        }
    }
}
