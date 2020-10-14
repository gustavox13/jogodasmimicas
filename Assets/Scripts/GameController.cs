using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] questions = new GameObject[4];

    private int currentQuestion = 0;

    [SerializeField]
    private GameObject pacocanegative;

    private Animator negative;

    [SerializeField]
    private GameObject questions1;

    [SerializeField]
    private GameObject questions2;

    public string correctID;

    [SerializeField]
    private GameObject endScreen;


    public int QuantPlays = 0;

    private void Awake()
    {
        ShuffleQuestions();
        negative = pacocanegative.GetComponent<Animator>();
    }


    private void Start()
    {
        
        StartCoroutine(WaitForFirstRound());
    }


    IEnumerator WaitForFirstRound()
    {
        if (currentQuestion > 0)
        {
            StartCoroutine(EnableQuestions1(0f));
        }

        yield return new WaitForSeconds(2); // espera para iniciar o turno

        Round();
    }


    private void Round()
    {
        
        RunAnim();//EXECUTA ANIMACAO
        if (currentQuestion == 0)
        {
            StartCoroutine(EnableQuestions1(2f));
        }
    }


    public void RunAnim()
    {
        questions[currentQuestion].GetComponent<QuestionClass>().ExeAnimation(); //chama animação da fase atual 
    }


    public void VerifyAnswer(string idSelected)  // VERIFICA RESPOSTA
    {
     if(idSelected == questions[currentQuestion].GetComponent<QuestionClass>().idFrase) //RESPOSTA CERTA
        {
            correctID = idSelected;
  
            DisableQuestions1();
            StartCoroutine(EnableQuestions2(0f)); //ATIVA SEGUNDA ETAPA


        }
        else //RESPOSTA ERRADA
        {
            DisableQuestions1();
            negative.SetTrigger("wrong");
            StartCoroutine(EnableQuestions1(2f));
            QuantPlays++;
        }

    }

    public void VerifySecondAnswer(int idSelected)
    {
        if(questions[currentQuestion].GetComponent<QuestionClass>().numWords == idSelected) //RESPOSTA CERTA
        {
            FinalRound();
        }
        else //RESPOSTA ERRADA
        {
            QuantPlays++;
            DisableQuestions2();
            negative.SetTrigger("wrong");
            StartCoroutine(EnableQuestions2(2f));
        }
    }

    private void FinalRound()
    {
        DisableQuestions2();

        if (currentQuestion >= 3) // GANHOU
        {
            Debug.Log("fim de jogo");
            endScreen.SetActive(true);
        }
        else // PROX ROUND
        {
            currentQuestion++;
            //Round();
            StartCoroutine(WaitForFirstRound());
        }

    }

 
        //DESABILITA QUESTÕES 1
        private void DisableQuestions1()
    {
        questions1.SetActive(false);
    }


    //HABILITA QUESTÕES 1 DEPOIS DE N SEGUNDOS
    IEnumerator EnableQuestions1(float timeToEnable)
    {
        yield return new WaitForSeconds(timeToEnable);
        questions1.SetActive(true);




        float timetoActiveButton;

        if(timeToEnable == 2)
        {
            timetoActiveButton = 2.2f;
        }
        else
        {
            timetoActiveButton = 4.2f;
        }

        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(timetoActiveButton);
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].GetComponent<Button>().interactable = true;
        }



    }


    //DESABILITA QUESTÕES 2
    private void DisableQuestions2()
    {
        questions2.SetActive(false);
    }


    //HABILITA QUESTÕES 2 DEPOIS DE N SEGUNDOS
    IEnumerator EnableQuestions2(float timeToEnable)
    {
        
        yield return new WaitForSeconds(timeToEnable);
        questions2.SetActive(true);
        //yield return new WaitForSeconds(1f);
        questions2.GetComponent<QuestionsTwo>().EnableQuestions2();
    }


    //EMBARALHAR FRASES
    private void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            GameObject obj = questions[i];
            int randomizeArray = Random.Range(0, i);
            questions[i] = questions[randomizeArray];
            questions[randomizeArray] = obj;
        }
    }

}
