using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Text correctText;
    // Start is called before the first frame update
    void Start()
    {
        correctText.text = "正解数："+QuestionManager.correct.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
