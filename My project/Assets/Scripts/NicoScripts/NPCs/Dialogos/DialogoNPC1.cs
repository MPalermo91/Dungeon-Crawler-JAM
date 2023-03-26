namespace RedBlueGames.Tools.TextTyper
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using RedBlueGames.Tools.TextTyper;
    using UnityEngine.UI;
    using TMPro;

    public class DialogoNPC1 : MonoBehaviour
    {


        /*--TABLA-DE-REFERENCIAS-DE-COMENTARIOS-//

            LNK = ~LNK

        //--TABLA-DE-REFERENCIAS-DE-COMENTARIOS-*/

        /// ESTE SCRIPT NO ESTÁ REWORKEADO PARA PENDULUM, cualquier duda preguntar a LNK. ~LNK

        #pragma warning disable 0649 // Ignore "Field is never assigned to" warning, as these are assigned in inspector
        
        [Header("Referencias de audio")]
        [SerializeField] private AudioClip printSoundEffect;



        [Header("Ref de UI | botones de acciones")]
        [SerializeField] private Button printNextButton;
        [SerializeField] private GameObject libreta;

        [Header("Ref de UI | Botones de preguntas")]

        [SerializeField] private Button pregunta1boton;
        [SerializeField] private Button pregunta2boton;
        [SerializeField] private Button pregunta3boton;
        [SerializeField] private Button pregunta4boton;
        [SerializeField] private Button pregunta5boton;
        [SerializeField] private Button pregunta6boton;
        [SerializeField] private GameObject botonBastaDePreguntas;

        [Header("Ref de preguntas (en caso de que haya)")]
        //Esta parte se puede simplificar pero me dio paja hacerlo. LNK~
        [SerializeField] private GameObject pregunta1;
        [SerializeField] private GameObject pregunta2;
        [SerializeField] private GameObject pregunta3;
        [SerializeField] private GameObject pregunta4;
        [SerializeField] private GameObject pregunta5;
        [SerializeField] private GameObject pregunta6;

        //activar esto en caso de que se necesite desactivar el panel de dialogos cada vez que se abra la libreta
        //[SerializeField] private GameObject panelDeDialogos;



        //lineas de dialogo introductorios. LNK~
        private Queue<string> dialogueLines = new Queue<string>();

        //lineas de dialogos respuestas. LNK~
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
        [Tooltip("TextTyper script")]
        private TextTyper testTextTyper;



        //este count define la cantidad de preguntas que se hicieron. LNK~
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


            // Aqui se crean las lineas de dialogos, cada una con su respectiva respuesta. LNK~

            dialogueLines.Enqueue("Miau, buenas tardes, encontré esta caseta y decía que es una clínica, ¿correcto?");
            dialogueLines.Enqueue("Estoy buscando algo para los mareos <i>miau</i>");

            dialogueLinesRes1.Enqueue("Hace un par de días.");

            dialogueLinesRes2.Enqueue("A veces me duele todo el cuerpo.");

            dialogueLinesRes3.Enqueue("Nada de nada... bueno capaz recorrer todo el reino helado...");
            dialogueLinesRes3.Enqueue("Buscando un grimorio de hielo se considere actividad");

            dialogueLinesRes4.Enqueue("<i>Gato demonio</i>");

            dialogueLinesRes5.Enqueue("<i>Miau</i>\nSi y escalofrios");

            dialogueLinesRes6.Enqueue("Nop. Pero me dijeron que irradio más calor de lo normal");
            dialogueLinesRes6.Enqueue("Será por el maná?");


            dialogueLinesRes1Cansado.Enqueue("No sé, estoy cansado.");

            dialogueLinesRes2Cansado.Enqueue("¿Vas a curarme, cierto?");

            dialogueLinesRes3Cansado.Enqueue("¿Solo me preguntaras cosas?");

            dialogueLinesRes4Cansado.Enqueue("<i>Miau</i>");

            dialogueLinesRes5Cansado.Enqueue("Si y mucho");
            dialogueLinesRes5Cansado.Enqueue("Se terminaron las preguntas?");

            dialogueLinesRes6Cansado.Enqueue("No sé cómo se hace");
            

           
           
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

            // Activar en caso de que se necesite activar un boton de basta de preguntas. LNK~
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

            // Activar en caso de que se necesite la funcoin libreta. LNK~
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
            // Esta sentencia es para que no se reproduzca un sonido cuando hay silencio o cambio de linea. LNK~
            if (printedCharacter == " " || printedCharacter == "\n")
            {
                return;
            }

            // Aqui se llama al AudioSource para realizar sonidos con efecto de habla. LNK~
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


        // Aqui van las respuestas (no descartar cambiarlo por un switch). LNK~
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
