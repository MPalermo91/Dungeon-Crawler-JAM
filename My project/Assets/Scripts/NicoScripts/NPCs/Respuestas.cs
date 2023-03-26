using System.Collections;
using UnityEngine;
using TMPro;

public class Respuestas : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLinesCansado;
    [SerializeField] private SistemaDeDialogos _sistemaDeDialogos;

    [SerializeField] private float typingTime = 0.05f;
    [SerializeField] private AudioClip npcVoice, npcVoiceCansado;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int cantidadCharParaPlaySonido = 3;
    

    private bool didDialogueStart;
    private int lineIndex;
    private bool primeraVez = true;
    private bool elDialogoTermino = false;

    //falta hacer referencias a los sprites de las caras y activarlas o desactivalas con un bool que tambien
    //se puede usar para reprodicir los sonidos de las voces.


    void Update()
    {
        if(!elDialogoTermino)
        {
            if(Input.GetButtonDown("Fire1") || primeraVez)
            {
                primeraVez = false;
                if (!didDialogueStart)
                {
                    StartDialogue();
                }
                else if(dialogueText.text == dialogueLines[lineIndex] && _sistemaDeDialogos.cantidadDePreguntas <= 3)
                {
                    NextDialogueLine();
                }
                else if(_sistemaDeDialogos.cantidadDePreguntas > 3 && dialogueText.text == dialogueLinesCansado[lineIndex])
                {
                    NextDialogueLine();
                }
                else 
                {
                    StopAllCoroutines();
                    if(_sistemaDeDialogos.cantidadDePreguntas <= 3)
                    {
                        dialogueText.text = dialogueLines[lineIndex];
                    }
                    else
                    {
                        dialogueText.text = dialogueLinesCansado[lineIndex];
                    }
                }
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        if (_sistemaDeDialogos.cantidadDePreguntas <= 3)
        {  
            StartCoroutine(ShowLine());
        }
        else
        {
            StartCoroutine(ShowLineCansado());
        }
    }

    private void NextDialogueLine()
    {
        if(_sistemaDeDialogos.cantidadDePreguntas <= 3)
        {
            lineIndex++;
            if(lineIndex < dialogueLines.Length)
            {
                StartCoroutine(ShowLine());
            }
            else
            {
                elDialogoTermino = true;
                dialoguePanel.SetActive(false);
                Time.timeScale = 1f;
                _sistemaDeDialogos.DialogoTerminado();

            }
        }
        else if(_sistemaDeDialogos.cantidadDePreguntas > 3)
        {
            lineIndex++;
            if(lineIndex < dialogueLinesCansado.Length)
            {
                StartCoroutine(ShowLineCansado());
            }
            else
            {
                elDialogoTermino = true;
                dialoguePanel.SetActive(false);
                Time.timeScale = 1f;
                _sistemaDeDialogos.DialogoTerminado();
            }
        }
        
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        // if (_sistemaDeDialogos.cantidadDePreguntas <= 3)
        // {  
        // }
        int charIndex = 0;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            
            if(charIndex % cantidadCharParaPlaySonido*2 == 0) audioSource.PlayOneShot(npcVoice);
            charIndex++;

            yield return new WaitForSecondsRealtime(typingTime);
        }
    
    }
    private IEnumerator ShowLineCansado()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;
        foreach(char ch in dialogueLinesCansado[lineIndex])
        {
            dialogueText.text += ch;
            if(charIndex % cantidadCharParaPlaySonido*2 == 0) audioSource.PlayOneShot(npcVoiceCansado);
            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
}
