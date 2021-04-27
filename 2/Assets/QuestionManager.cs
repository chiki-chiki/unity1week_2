using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public Text question;
    public Text leftButtonText;
    public Text rightButtonText;
    public Button leftButton;
    public Button rightButton;

    int totalQuestions = 4; //出題される質問の総種類数

    string[] questions;
    string[] leftChoices;
    string[] rightChoices;
    int[] answers;
    
    public int questionNumber;

    public static int correct;    //正解数

    public Text timeText;
    public float remainingTime;    //残り時間

    bool isPlayable = true;

    private void Awake()
    {
        questions = new string[totalQuestions];
        leftChoices = new string[totalQuestions];
        rightChoices = new string[totalQuestions];
        answers = new int[totalQuestions];

        InitQuestions(0, "右", "左", "右", 1);
        InitQuestions(1, "左", "左", "右", 0);
        InitQuestions(2, "右じゃない", "左", "右", 0);
        InitQuestions(3, "左じゃない", "左", "右", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetQuestion(Random.Range(0, totalQuestions));
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        timeText.text = remainingTime.ToString("f1");

        if (Input.GetKeyDown(KeyCode.LeftArrow)&&isPlayable)
        {
            SelectLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)&&isPlayable)
        {
            SelectRight();
        }

        if (remainingTime <= 0)
        {
            StartCoroutine("Finish");
        }
    }

    void InitQuestions(int questionID,string question,string leftChoice,string rightChoice,int answer)
    {
        questions[questionID] = question;
        leftChoices[questionID] = leftChoice;
        rightChoices[questionID] = rightChoice;
        answers[questionID] = answer;
    }
    void SetQuestion(int questionID)
    {
        question.text = questions[questionID];
        leftButtonText.text = leftChoices[questionID];
        rightButtonText.text = rightChoices[questionID];
        questionNumber = questionID;
    }

    public void SelectLeft()
    {
        if (answers[questionNumber] == 0)
        {
            correct++;
            Debug.Log(correct);
            SetQuestion(Random.Range(0, totalQuestions));
        }
        else
        {
            //Debug.Log("GameOver");
            StartCoroutine("Miss");
        }
    }

    public void SelectRight()
    {
        if (answers[questionNumber] == 1)
        {
            correct++;
            Debug.Log(correct);
            SetQuestion(Random.Range(0, totalQuestions));
        }
        else
        {
            //Debug.Log("GameOver");
            StartCoroutine("Miss");
        }
    }

    private IEnumerator Miss()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        question.text = "Miss!";
        isPlayable = false;

        yield return new WaitForSeconds(1.0f);

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        question.text = questions[questionNumber];
        isPlayable = true;
    }

    private IEnumerator Finish()
    {
        timeText.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        question.text = "Finish!";

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Result");
    }
}
