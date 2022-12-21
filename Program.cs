// ReSharper disable InconsistentNaming

namespace c__project
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            startProgram newQuiz = new startProgram();
            newQuiz.Run();
        }
    }

    class startProgram
    {
        public void Run()
        {
            QuizCreation c = new QuizCreation();

            Console.WriteLine("Would you like to create a quiz or take a quiz? Answer with \"taking\" or \"creating\".");
            var creatingOrTaking = c.TryReadLine();

            if (creatingOrTaking == "creating")
            {
                QuizCreation q = new QuizCreation();
                q.CreateQuiz();
            }

            else
            {
                TakeQuiz t = new TakeQuiz();
                t.StartQuiz();
            }
        }
    }
    class TakeQuiz
    {
        string fileName = string.Empty;

        public void StartQuiz()
        {
            QuizCreation q = new QuizCreation();

            Console.WriteLine("Make sure that all 3 .qzz files for the quiz are in the program's directory.");
            BeforeFileFinding:
            Console.WriteLine(
                "What is the name of the file? Do NOT include \"Questions\", \"QuestionsAnswersA\", or \"QuestionsAnswersB\". Do not include a file extension.");
            fileName = q.TryReadLine();
            if ((!File.Exists($"{fileName}Questions.qzz") || !File.Exists($"{fileName}QuestionsAnswersA.qzz") || 
                 !File.Exists($"{fileName}QuestionsAnswersB.qzz")))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Files was not found. Are you sure they are in the program directory?");
                Console.ForegroundColor = ConsoleColor.White;
                goto BeforeFileFinding;
            }
            
            string[] questionDescriptionArray = File.ReadAllLines($"{fileName}Questions.qzz");
            string[] questionAnswerArray = File.ReadAllLines($"{fileName}QuestionsAnswersA.qzz");
            string[] questionCorrectAnswerArray = File.ReadAllLines($"{fileName}QuestionsAnswersB.qzz");
            int correctQuestionsAmount = 0;
            int questionAmount = 0;
            int i = 0;

            foreach (string unused in questionDescriptionArray)
            {
                i++;
                questionAmount = i;
            }

            i = 0;

            string[] questionAnsweredArray = new string[questionAmount];

            while (i < questionAmount)
            {

                Console.WriteLine($"Question {i + 1}:");
                Console.WriteLine(questionDescriptionArray[i]);
                Console.WriteLine(questionAnswerArray[i]);
                questionAnsweredArray[i] = q.TryReadLine();
                i++;
            }

            i = 0;
            Console.Clear();
            Console.WriteLine("You have finished the quiz! Here are your results!");

            while (i < questionAmount)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Question {i + 1}:");
                Console.WriteLine(questionDescriptionArray[i]);
                Console.WriteLine(questionAnswerArray[i]);

                if (questionAnsweredArray[i] == questionCorrectAnswerArray[i])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Your answer: {questionAnsweredArray[i]}");
                    Console.WriteLine("That was the correct answer!");
                    Console.WriteLine(" ");
                    correctQuestionsAmount++;
                    i++;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Your answer: {questionAnsweredArray[i]}");
                    Console.WriteLine("That was the wrong answer.");
                    Console.WriteLine(" ");
                    i++;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"You got {correctQuestionsAmount} out of {questionAmount} questions correct.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }


    class QuizCreation
    {
        string questionInput = string.Empty;
        int questionAmount;
        string userYnAnswer = string.Empty;


        public void CreateQuiz()
        {
            UserInputBeginning:
            Console.WriteLine("Please enter the amount of questions in the quiz.");
            UserInputNull:
            questionInput = TryReadLine();
            switch (questionInput)
            {
                case null:
                    Console.WriteLine("Please enter a valid amount!");
                    goto UserInputNull;
                default:
                    questionAmount = int.Parse(questionInput);
                    break;
            }

            Console.WriteLine($"You want {questionInput} questions. Are you sure? Y/N");
            userYnAnswer = TryReadLine();

            int i = 0;
            string[] questionDescriptionArray = new string[questionAmount];

            if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
            {
                while (i < questionAmount)
                {
                    Console.WriteLine($"Please enter the question for question number {i + 1}.");

                    questionDescriptionArray[i] = TryReadLine();

                    if (questionDescriptionArray[i] == "")
                    {
                        Console.WriteLine("questionDescriptionArray array is empty.");
                        return;
                    }

                    i++;
                }
            }

            else
            {
                goto UserInputBeginning;
            }

            string[] questionAnswerArray = new string[questionAmount];

            i = 0;

            while (i < questionAmount)
            {
                Console.WriteLine(
                    $"Please enter the answer options, for question number {i + 1}. Use a format such as \"A) Answer A\" or \"1. Answer 1.\"");

                questionAnswerArray[i] = TryReadLine();

                if (questionAnswerArray[i] == "")
                {
                    Console.WriteLine("questionAnswerArray array is empty.");
                    return;
                }

                i++;
            }

            i = 0;

            string[] questionCorrectAnswerArray = new string[questionAmount];

            while (i < questionAmount)
            {
                Console.WriteLine(
                    $"Now, enter which answer is correct for question number {i + 1}. Enter it ONLY with the correct letter or number.");

                questionCorrectAnswerArray[i] = TryReadLine();

                if (questionCorrectAnswerArray[i] == "")
                {
                    Console.WriteLine("questionAnswerArray array is empty.");
                    return;
                }

                i++;
            }

            fileNaming:
            Console.WriteLine(
                "You need to save the quiz to a file. What would you like the file to be named? Format it WITHOUT a file extension.");

            var fileName = TryReadLine();
            userYnAnswer = string.Empty;

            Console.WriteLine($"You want to name the file {fileName}.qzz. Is this correct? Y/N");
            userYnAnswer = TryReadLine();

            if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
            {
                if ((File.Exists($"{fileName}Questions.qzz") || File.Exists($"{fileName}QuestionsAnswersA.qzz") ||
                     File.Exists($"{fileName}QuestionsAnswersB.qzz")))
                {
                    userYnAnswer = String.Empty;

                    Console.WriteLine("These files already exists! Do you want to overwrite the file? Y/N");
                    userYnAnswer = TryReadLine();

                    if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
                    {
                        Console.WriteLine("Saving file...");
                        File.WriteAllLines($"{fileName}Questions.qzz",
                            Array.ConvertAll(questionDescriptionArray, x => x.ToString()));
                        File.WriteAllLines($"{fileName}QuestionAnswersA.qzz",
                            Array.ConvertAll(questionAnswerArray, y => y.ToString()));
                        File.WriteAllLines($"{fileName}QuestionAnswersB.qzz",
                            Array.ConvertAll(questionCorrectAnswerArray, z => z.ToString()));
                    }

                    else
                    {
                        goto fileNaming;
                    }
                }
                else
                {
                    Console.WriteLine("Saving file...");
                    File.WriteAllLines($"{fileName}Questions.qzz",
                        Array.ConvertAll(questionDescriptionArray, x => x.ToString()));
                    File.WriteAllLines($"{fileName}QuestionsAnswersA.qzz",
                        Array.ConvertAll(questionAnswerArray, y => y.ToString()));
                    File.WriteAllLines($"{fileName}QuestionsAnswersB.qzz",
                        Array.ConvertAll(questionCorrectAnswerArray, z => z.ToString()));
                }
            }

            Console.WriteLine("You've finished making your quiz! Would you like to return to the main page? Y/N");
            userYnAnswer = String.Empty;
            userYnAnswer = TryReadLine();
            if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
            {
                startProgram newQuiz = new startProgram();
                newQuiz.Run();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public string TryReadLine()
        {
            string? line = Console.ReadLine();
            if (line != null) {return line;}
            else {line = "-1"; return line;}
        }
    }
}