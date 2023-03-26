using System.Collections;
using UnityEngine;
using TMPro;

public class SistemaDeDialogos : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject panelPregunta1;
    [SerializeField] private GameObject panelRespuesta1;
    [SerializeField] private GameObject panelPregunta2;
    [SerializeField] private GameObject panelRespuesta2;
    [SerializeField] private GameObject panelPregunta3;
    [SerializeField] private GameObject panelRespuesta3;
    [SerializeField] private GameObject panelPregunta4;
    [SerializeField] private GameObject panelRespuesta4;
    [SerializeField] private GameObject panelPregunta5;
    [SerializeField] private GameObject panelRespuesta5;
    [SerializeField] private GameObject panelPregunta6;
    [SerializeField] private GameObject panelRespuesta6;
    [SerializeField] private GameObject panelReady;
    [SerializeField] private GameObject libreta;
    [HideInInspector] public int cantidadDePreguntas = 0;


//Elementos de audio
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField] private AudioClip npcVoice;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int cantidadCharParaPlaySonido = 3;
    

    private bool didDialogueStart;
    private int lineIndex;

    private bool primeraVez = true;
    private bool elDialogoTermino = false;

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
                else if(dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
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
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
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
            DialogoTerminado();
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;

            if(charIndex % cantidadCharParaPlaySonido == 0) audioSource.PlayOneShot(npcVoice);

            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    public void DialogoTerminado()
    {
        Debug.Log("Dialogo terminado");
        panelPregunta1.SetActive(true);
        panelPregunta2.SetActive(true);
        panelPregunta3.SetActive(true);
        panelPregunta4.SetActive(true);
        panelPregunta5.SetActive(true);
        panelPregunta6.SetActive(true);
        if(libreta!=null)
        {
        libreta.SetActive(true);
        }
        if(cantidadDePreguntas >= 3)
        {
            panelReady.SetActive(true);
        }
    }
    private void DesactivarPregruntas()
    {
        panelPregunta1.SetActive(false);
        panelPregunta2.SetActive(false);
        panelPregunta3.SetActive(false);
        panelPregunta4.SetActive(false);
        panelPregunta5.SetActive(false);
        panelPregunta6.SetActive(false);
        panelReady.SetActive(false);
        if(libreta!=null)
        {
        libreta.SetActive(false);
        }
    }

//.GetComponent<NumeroDaño>().Inicializar(Damage);
    public void SeleccionPregunta1 ()
    {
        DesactivarPregruntas();
        cantidadDePreguntas++;
        panelRespuesta1.SetActive(true);
    }
    public void SeleccionPregunta2 ()
    {
        DesactivarPregruntas();
        cantidadDePreguntas++;
        panelRespuesta2.SetActive(true);
    }
        public void SeleccionPregunta3 ()
    {
        DesactivarPregruntas();
        cantidadDePreguntas++;
        panelRespuesta3.SetActive(true);
    }
    public void SeleccionPregunta4 ()
    {
        DesactivarPregruntas();
        cantidadDePreguntas++;
        panelRespuesta4.SetActive(true);
    }
    public void SeleccionPregunta5 ()
    {
        DesactivarPregruntas();
        cantidadDePreguntas++;
        panelRespuesta5.SetActive(true);
    }
    public void SeleccionPregunta6 ()
    {
        DesactivarPregruntas();
        cantidadDePreguntas++;
        panelRespuesta6.SetActive(true);
    }
    public void TerminaronLasPreguntas()
    {
        gameObject.SetActive(false);
        if(libreta!=null)
        {
        libreta.SetActive(false);
        }
    }



}
