namespace RedBlueGames.Tools.TextTyper
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using RedBlueGames.Tools.TextTyper;
    using UnityEngine.UI;
    using TMPro;

    /// <summary>
    /// Class that tests TextTyper and shows how to interface with it.
    /// </summary>
    public class DialogoNPC6 : MonoBehaviour
    {
#pragma warning disable 0649 // Ignore "Field is never assigned to" warning, as these are assigned in inspector
        [SerializeField]
        private AudioClip printSoundEffect;

        [Header("UI References")]

        [SerializeField]
        private Button printNextButton;


        [Header("Referencia de botones de preguntas")]
        [SerializeField] private Button pregunta1boton;
        [SerializeField] private Button pregunta2boton;
        [SerializeField] private Button pregunta3boton;
        [SerializeField] private Button pregunta4boton;
        [SerializeField] private Button pregunta5boton;
        [SerializeField] private Button pregunta6boton;
        [SerializeField] private GameObject botonBastaDePreguntas;
        [SerializeField] private GameObject libreta;

        //Esta parte se puede simplificar pero me dio paja hacerlo. LNK~
        [SerializeField] private GameObject pregunta1;
        [SerializeField] private GameObject pregunta2;
        [SerializeField] private GameObject pregunta3;
        [SerializeField] private GameObject pregunta4;
        [SerializeField] private GameObject pregunta5;
        [SerializeField] private GameObject pregunta6;



        //lineas de dialogo introductorios. LNK~
        private Queue<string> dialogueLines = new Queue<string>();

        //lineas dedialogos respuestas. LNK~
        private Queue<string> dialogueLinesRes1 = new Queue<string>();
        private Queue<string> dialogueLinesRes2 = new Queue<string>();
        private Queue<string> dialogueLinesRes3 = new Queue<string>();
        private Queue<string> dialogueLinesRes4 = new Queue<string>();
        private Queue<string> dialogueLinesRes5 = new Queue<string>();
        private Queue<string> dialogueLinesRes6 = new Queue<string>();

        //lineas de dialogo cansados. LNK~
        private Queue<string> dialogueLinesRes1Cansado = new Queue<string>();
        private Queue<string> dialogueLinesRes2Cansado = new Queue<string>();
        private Queue<string> dialogueLinesRes3Cansado = new Queue<string>();
        private Queue<string> dialogueLinesRes4Cansado = new Queue<string>();
        private Queue<string> dialogueLinesRes5Cansado = new Queue<string>();
        private Queue<string> dialogueLinesRes6Cansado = new Queue<string>();
        

        [SerializeField]
        [Tooltip("The text typer element to test typing with")]
        private TextTyper testTextTyper;


        //este count define la cantidad de preguntas que se hicieron . LNK~
        private int count = 1;




#pragma warning restore 0649
        public void Start()
        {
            this.testTextTyper.PrintCompleted.AddListener(this.HandlePrintCompleted);
            this.testTextTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

            this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);


            //aqui se añade y configura el boton de las respuestas. LNK~
            this.pregunta1boton.onClick.AddListener(this.Respuesta1);
            this.pregunta2boton.onClick.AddListener(this.Respuesta2);
            this.pregunta3boton.onClick.AddListener(this.Respuesta3);
            this.pregunta4boton.onClick.AddListener(this.Respuesta4);
            this.pregunta5boton.onClick.AddListener(this.Respuesta5);
            this.pregunta6boton.onClick.AddListener(this.Respuesta6);
            
            // Lista de efectos. LNK~
            // aplicar delay: <delay=0.5>NPC</delay>
            // aplicar italica: <i>bub</i>
            // aplicar negrita: <b>use</b>
            // cambiar tamaño de letra: <size=40>text</size>
            // aplicar color: <color=#ff0000ff>color</color>
            // efecto delay: <delay=0.5>PALABRA</delay>
            // efecto shake de rotacion ligera: <anim=lightrot>Light Rotation</anim>
            // efecto shake de posicion ligera: <anim=lightpos>Light Position</anim>
            // efecto shake completo: <anim=fullshake>Full Shake</anim>
            // efecto de curva: <animation=slowsine>Slow Sine</animation>
            // efecto de rebote: <animation=bounce>Bounce Bounce</animation>
            // efecto de flip (muy loco): <animation=crazyflip>Crazy Flip</animation>
            // para aplicar punto y aparte, utilizar: \n


            dialogueLines.Enqueue("¿Es aquí la clínica?, ¿Usted es el doctor?");
            dialogueLines.Enqueue("Me generó curiosidad este edificio, ¿Cuándo lo construyeron?");
            dialogueLines.Enqueue("¿Este libro es un auténtico tomo de pociones legendarias?");
            dialogueLines.Enqueue("Me intriga saber como funcionan las pociones");
            dialogueLines.Enqueue("Mi familia me dice que tengo un problema, ¿Usted cree que lo tengo?, Me interesa saber que opina…");
            //dialogueLines.Enqueue("You can <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");
            //dialogueLines.Enqueue("Sample Shake Animations: <anim=lightrot>Light Rotation</anim>, <anim=lightpos>Light Position</anim>, <anim=fullshake>Full Shake</anim>\nSample Curve Animations: <animation=slowsine>Slow Sine</animation>, <animation=bounce>Bounce Bounce</animation>, <animation=crazyflip>Crazy Flip</animation>");


            dialogueLinesRes1.Enqueue("Si, es que me interesa mucho las cosas");
            dialogueLinesRes1.Enqueue("Podría decir que soy Curioso");
            
            dialogueLinesRes2.Enqueue("No la conozco. ¿Será real?");

            dialogueLinesRes3.Enqueue("A aprender sobre la vida, pero para vivir soy Investigador Privado");

            dialogueLinesRes4.Enqueue("Solo cuando no se las respuestas a mis preguntas");

            dialogueLinesRes5.Enqueue("Incontable cantidad de veces, Mi trabajo me lleva a situaciones complejas");
            dialogueLinesRes5.Enqueue("Ser curioso no me permite ser precavido");
            dialogueLinesRes5.Enqueue("Supongo, estar cerca de la muerte constantemente es parte de la vida. ¿No?");

            dialogueLinesRes6.Enqueue("Gato Curioso");

            dialogueLinesRes1Cansado.Enqueue("¿Lo cansan mis preguntas?");
            dialogueLinesRes2Cansado.Enqueue("Me atemoriza saber las respuetas.");
            dialogueLinesRes3Cansado.Enqueue("Soy Investigador privado porque hago muchas preguntas");
            dialogueLinesRes4Cansado.Enqueue("¿Este, es un caldero para hacer pociones?");
            dialogueLinesRes5Cansado.Enqueue("Incontable cantidad de veces, Mi trabajo me lleva a situaciones complejas");
            dialogueLinesRes5Cansado.Enqueue("Ser curioso no me permite ser precavido");
            dialogueLinesRes5Cansado.Enqueue("Supongo, estar cerca de la muerte constantemente es parte de la vida. ¿No?");
            dialogueLinesRes6Cansado.Enqueue("¿Cuantas especies conoce?");
            

           
           
            ShowScript();
        }

        public void Update()
        {

            //UnityEngine.Time.timeScale = this.pauseGameToggle.isOn ? 0.0f : 1.0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {

                var tag = RichTextTag.ParseNext("blah<color=red>boo</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("<color=blue>blue</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No tag in here");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No <color=blueblue</color tag here either");
                LogTag(tag);
                tag = RichTextTag.ParseNext("This tag is a closing tag </bold>");
                LogTag(tag);
            }


            if(count > 3)
            {
                botonBastaDePreguntas.SetActive(true);
            }
        }

        private void HandlePrintNextClicked()
        {
            if (this.testTextTyper.IsSkippable() && this.testTextTyper.IsTyping)
            {
                this.testTextTyper.Skip();
            }
            else
            {
                ShowScript();
            }
        }

        private void HandlePrintNoSkipClicked()
        {
            ShowScript();
        }

        private void ShowScript()
        {
            if (dialogueLines.Count <= 0)
            {

                libreta.SetActive(true);

                return;
            }
            libreta.SetActive(false);
                this.testTextTyper.TypeText(dialogueLines.Dequeue());
        }

        private void LogTag(RichTextTag tag)
        {
            if (tag != null)
            {
                Debug.Log("Tag: " + tag.ToString());
            }
        }

        private void HandleCharacterPrinted(string printedCharacter)
        {
            // Do not play a sound for whitespace
            if (printedCharacter == " " || printedCharacter == "\n")
            {
                return;
            }

            var audioSource = this.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = this.gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = this.printSoundEffect;
            audioSource.Play();
        }

        private void HandlePrintCompleted()
        {
            Debug.Log("TypeText Complete");
        }



        private void Respuesta1 ()
        {
            if(count <= 3)
            {
                dialogueLines = dialogueLinesRes1;
                ShowScript();
            }
            else
            {
                dialogueLines = dialogueLinesRes1Cansado;
                ShowScript();
            }
            pregunta1.SetActive(false);
            count++;
            Debug.Log(count);
        }


                private void Respuesta2 ()
        {
            if(count <= 3)
            {
                dialogueLines = dialogueLinesRes2;
                ShowScript();
            }
            else
            {
                dialogueLines = dialogueLinesRes2Cansado;
                ShowScript();
            }
            pregunta2.SetActive(false);
            count++; 
            Debug.Log(count);
        }


                private void Respuesta3 ()
        {
            if(count <= 3)
            {
                dialogueLines = dialogueLinesRes3;
                ShowScript();
            }
            else
            {
                dialogueLines = dialogueLinesRes3Cansado;
                ShowScript();
            }
            pregunta3.SetActive(false);
            count++;
            Debug.Log(count);
        }


                private void Respuesta4 ()
        {
            if(count <= 3)
            {
                dialogueLines = dialogueLinesRes4;
                ShowScript();
            }
            else
            {
                dialogueLines = dialogueLinesRes4Cansado;
                ShowScript();
            }
            pregunta4.SetActive(false);
            count++;
            Debug.Log(count);
        }


                private void Respuesta5 ()
        {
            if(count <= 3)
            {
                dialogueLines = dialogueLinesRes5;
                ShowScript();
            }
            else
            {
                dialogueLines = dialogueLinesRes5Cansado;
                ShowScript();
            }
            pregunta5.SetActive(false);
            count++;
            Debug.Log(count);
        }


                private void Respuesta6 ()
        {
            if(count <= 3)
            {
                dialogueLines = dialogueLinesRes6;
                ShowScript();
            }
            else
            {
                dialogueLines = dialogueLinesRes6Cansado;
                ShowScript();
            }
            pregunta6.SetActive(false);
            count++;
            Debug.Log(count);
        }



    }

}
