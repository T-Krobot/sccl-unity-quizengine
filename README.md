# QuizEngine
I've edited the Parser_Quiz_usual to pass in latestscenery to QuizDataStorer so it won't directly work with something with multiple stages.

QuizController and DataStorer both need to be on an object in scene. Link data storer to the parser (parser also on an object of course) in the inspector.

Objects being used for answers (e.g buttons or asteroids you're going to shoot) need to have the AnswerObject on them and then be put in into the AnswerObject array on QuizController in inspector.

If everything is just running from start e.g you're testing and are instantly calling UpdateQuestion from start, you might get errors like out of range because the data storer hasn't finished. either add a delay like a button that needs to be clicked to start quiz or using unity script execution order and make data storer happen before quiz controller.

ask Daniel on slack if you need help.
