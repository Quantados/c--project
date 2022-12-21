// ReSharper disable InconsistentNaming

namespace Quizzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            string creatingOrTaking = String.Empty;

            Console.WriteLine(
                "Would you like to create a quiz or take a quiz? Answer with \"taking\" or \"creating\".");
            creatingOrTaking = Console.ReadLine();

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
            Console.WriteLine("Make sure that all 3 .qzz files for the quiz are in the program's directory.");
            Console.WriteLine(
                "What is the name of the file? Do NOT inlude \"Questions\", \"QuestionsAnswersA\", or \"QuestionsAnswersB\". Do not include a file extention.");
            fileName = Console.ReadLine();
            string?[] questionDescriptionArray = File.ReadAllLines($"{fileName}Questions.qzz");
            string?[] questionAnswerArray = File.ReadAllLines($"{fileName}QuestionsAnswersA.qzz");
            string?[] questionCorrectAnswerArray = File.ReadAllLines($"{fileName}QuestionsAnswersB.qzz");
            int correctQuestionsAmount = 0;
            int questionAmount = 0;
            int i = 0;

            foreach (string questionDescription in questionDescriptionArray)
            {
                i++;
                questionAmount = i;
            }

            i = 0;

            string?[] questionAnsweredArray = new string[questionAmount];

            while (i < questionAmount)
            {

                Console.WriteLine($"Question {i + 1}:");
                Console.WriteLine(questionDescriptionArray[i]);
                Console.WriteLine(questionAnswerArray[i]);
                questionAnsweredArray[i] = Console.ReadLine();
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
        string? questionInput = string.Empty;
        int questionAmount = 0;
        string? userYnAnswer = string.Empty;
        List<int> questionList = new List<int>();


        public void CreateQuiz()
        {
            UserInputBeginning:
            Console.WriteLine("Please enter the amount of questions in the quiz.");
            UserInputNull:
            questionInput = Console.ReadLine();
            switch (questionInput)
            {
                case null:
                    Console.WriteLine("Please enter a vaild amount!");
                    goto UserInputNull;
                default:
                    questionAmount = int.Parse(questionInput);
                    break;
            }

            Console.WriteLine($"You want {questionInput} questions. Are you sure? Y/N");
            userYnAnswer = Console.ReadLine();

            int j = 1;
            int i = 0;
            string?[] questionDescriptionArray = new string[questionAmount];

            if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
            {
                while (j < questionAmount)
                {
                    questionList.Add(j);

                    j++;
                }

                while (i < questionAmount)
                {
                    Console.WriteLine($"Please enter the question for question number {i + 1}.");

                    questionDescriptionArray[i] = Console.ReadLine();

                    if (questionDescriptionArray[i] == null || questionDescriptionArray[i] == "")
                    {
                        Console.WriteLine("questionDescriptionArray array is null.");
                        return;
                    }

                    i++;
                }
            }

            else
            {
                goto UserInputBeginning;
            }

            string?[] questionAnswerArray = new string[questionAmount];

            i = 0;

            while (i < questionAmount)
            {
                Console.WriteLine(
                    $"Please enter the answer options, for question number {i + 1}. Use a format such as \"A) Answer A\" or \"1. Answer 1.\"");

                questionAnswerArray[i] = Console.ReadLine();

                if (questionAnswerArray[i] == null || questionAnswerArray[i] == "")
                {
                    Console.WriteLine("questionAnswerArray array is null.");
                    return;
                }

                i++;
            }

            i = 0;

            string?[] questionCorrectAnswerArray = new string[questionAmount];

            while (i < questionAmount)
            {
                Console.WriteLine(
                    $"Now, enter which answer is correct for question number {i + 1}. Enter it ONLY with the correct letter or number.");

                questionCorrectAnswerArray[i] = Console.ReadLine();

                if (questionCorrectAnswerArray[i] == null || questionCorrectAnswerArray[i] == "")
                {
                    Console.WriteLine("questionAnswerArray array is null.");
                    return;
                }

                i++;
            }

            fileNaming:
            Console.WriteLine(
                "You need to save the quiz to a file. What would you like the file to be named? Format it WITHOUT a file extention.");

            string fileName = string.Empty;

            fileName = Console.ReadLine();
            userYnAnswer = string.Empty;

            Console.WriteLine($"You want to name the file {fileName}.qzz. Is this correct? Y/N");
            userYnAnswer = Console.ReadLine();

            if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
            {
                if ((File.Exists($"{fileName}Questions.qzz") || File.Exists($"{fileName}QuestionsAnswersA.qzz") ||
                     File.Exists($"{fileName}QuestionsAnswersB.qzz")))
                {
                    userYnAnswer = String.Empty;

                    Console.WriteLine("These files already exists! Do you want to overwrite the file? Y/N");
                    userYnAnswer = Console.ReadLine();

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

            Console.WriteLine("You've finished making your quiz! Would you like to return to the main page?");
            userYnAnswer = String.Empty;
            userYnAnswer = Console.ReadLine();
            if (userYnAnswer.ToUpper() == "Y" || userYnAnswer.ToUpper() == "YES")
            {
                Program p = new Program();
                //p.Run();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}