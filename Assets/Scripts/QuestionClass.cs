using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionClass : MonoBehaviour
{
    [SerializeField]
    private GameObject pacoca;

    [SerializeField]
    private string nameAnim;

    
    private AudioSource audioEffect;
    
    public int numWords;

    private Animator animPacoca;

    [SerializeField]
    private GameObject controller;

    public string idFrase;
    
    

    private void Start()
    {
        GetAllComponents();
    }

    private void GetAllComponents()
    {
        audioEffect = this.gameObject.GetComponent<AudioSource>();
        animPacoca = pacoca.GetComponent<Animator>();
    }

    public void ExeAnimation()
    {

        animPacoca.SetTrigger(nameAnim);
        StartCoroutine(ExeAudio());
        
    }


    IEnumerator ExeAudio()
    {
        yield return new WaitForSeconds(0.5f);
        audioEffect.Play();

    }


    public void SelectAnswer()
    {
        controller.GetComponent<GameController>().VerifyAnswer(idFrase);
        
    }

}
