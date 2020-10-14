using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsTwo : MonoBehaviour
{

    [SerializeField]
    private GameObject result5e9;
    [SerializeField]
    private GameObject result4e6;

    [SerializeField]
    private GameObject[] questions2 = new GameObject[4];

    [SerializeField]
    private GameObject gamecontroller;

    

    private void Start()
    {




    }

    public void EnableQuestions2()
    {
        enablePhrase();
        enableAnswers();
    }


    private void enablePhrase()
    {
        

        for (int i = 0; i < questions2.Length; i++)
        {
            if (questions2[i].tag == gamecontroller.GetComponent<GameController>().correctID)
            {
                questions2[i].SetActive(true);
            }
            else
            {
                questions2[i].SetActive(false);
            }
        }
    }

    private void enableAnswers()
    {
        if(gamecontroller.GetComponent<GameController>().correctID == "latir" || 
           gamecontroller.GetComponent<GameController>().correctID == "carinho")
        {
            result4e6.SetActive(true);
            result5e9.SetActive(false);
        }
        else
        {
            result4e6.SetActive(false);
            result5e9.SetActive(true);
        }
    }

}
