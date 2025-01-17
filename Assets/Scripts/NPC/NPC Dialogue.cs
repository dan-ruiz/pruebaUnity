using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Granny : MonoBehaviour
{
    public GameObject dialoguePanelGranny;
    public TMP_Text dialogueTextGranny;
    public GameObject dialoguePanelPlayer;
    public TMP_Text dialogueTextPlayer;

    public string[] dialogueGranny;  // Array de líneas de diálogo de Granny
    public string[] dialoguePlayer;  // Array de líneas de diálogo del Player
    private int index;

    public GameObject contButton;
    public float wordSpeed = 0.05f;
    public bool playerIsClose;
    public KeyCode BTNShoot = KeyCode.X;

    private bool isTyping = false;
    private bool isPlayerTalking = false; // Controla quién está hablando
    private bool conversationComplete = false; // Para asegurarse de que la conversación se ha completado

    // Referencia al UI Image que muestra el avatar de Player
    public Image playerAvatarImage;
    // Array para los avatares de Player
    public Sprite[] playerAvatars;

    void Start()
    {
        ResetDialogue(); // Asegura que la conversación siempre empiece con Granny
    }

    void Update()
    {
        // Si el jugador está cerca, presiona la tecla y la conversación no está completa
        if (Input.GetKeyDown(BTNShoot) && playerIsClose && !conversationComplete)
        {
            // Solo si no se está escribiendo
            if (!isTyping)
            {
                // Llamar al método que maneja el diálogo
                NextLine();
            }
        }
    }

    // Resetea el estado del diálogo para siempre comenzar con Granny
    public void ResetDialogue()
    {
        index = 0; // Reinicia el índice a 0 para que Granny siempre comience con la primera línea
        isPlayerTalking = false; // Asegura que Granny empiece a hablar primero
        conversationComplete = false; // Asegura que la conversación no esté completada
        dialoguePanelGranny.SetActive(false); // Oculta ambos paneles al reiniciar
        dialoguePanelPlayer.SetActive(false);
        contButton.SetActive(false); // Ocultamos el botón de continuar
        dialogueTextGranny.text = ""; // Limpiar los textos
        dialogueTextPlayer.text = "";
    }

    // Reinicia el texto y el panel de diálogo cuando se cierra el diálogo
    public void zeroText()
    {
        dialogueTextGranny.text = "";
        dialogueTextPlayer.text = "";
        index = 0; // Reseteamos el índice para que la conversación comience desde el principio
        dialoguePanelGranny.SetActive(false);
        dialoguePanelPlayer.SetActive(false);
        contButton.SetActive(false); // Cerrar el botón de continuar
        isPlayerTalking = false; // Asegura que la próxima vez Granny empezará a hablar
    }

    // Coroutine para escribir el texto letra por letra
    IEnumerator Typing(string text, TMP_Text textField)
    {
        isTyping = true;
        textField.text = ""; // Limpiar el texto anterior
        foreach (char letter in text.ToCharArray())
        {
            textField.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false;

        // Aquí activamos el botón de continuar una vez que el texto está completamente escrito
        contButton.SetActive(true);  // Mostrar el botón de continuar
    }

    // Avanza la conversación
    public void NextLine()
    {
        contButton.SetActive(false); // Ocultamos el botón de continuar inmediatamente al avanzar

        if (isPlayerTalking) // Si es el turno de Player
        {
            index++; // Aumentar el índice

            // Cambiar el avatar de Player según el índice de diálogo (solo para las líneas de Player)
            if (index % 2 == 0)  // Si Player está hablando (índice par)
            {
                int playerIndex = (index / 2) - 1;  // Calculamos el índice en el array de Player (dividir entre 2 y restar 1)
                if (playerIndex < playerAvatars.Length)  // Verifica si el índice es válido en el array de Player
                {
                    playerAvatarImage.sprite = playerAvatars[playerIndex];  // Cambiar al avatar correspondiente
                }
            }

            // Si aún hay más diálogo del Player
            if (index < dialoguePlayer.Length)
            {
                StartCoroutine(Typing(dialoguePlayer[index], dialogueTextPlayer));
                dialoguePanelGranny.SetActive(false); // Ocultar el panel de Granny
                dialoguePanelPlayer.SetActive(true); // Mostrar el panel del Player
            }
            else
            {
                // Si ya no hay más diálogo del Player, terminamos la conversación
                conversationComplete = true; // Marcar que la conversación terminó
                zeroText(); // Cerrar el diálogo
            }
        }
        else // Si es el turno de Granny
        {
            index++; // Aumentar el índice

            // Si aún hay más diálogo de Granny
            if (index < dialogueGranny.Length)
            {
                StartCoroutine(Typing(dialogueGranny[index], dialogueTextGranny));
                dialoguePanelGranny.SetActive(true); // Mostrar el panel de Granny
                dialoguePanelPlayer.SetActive(false); // Ocultar el panel del Player
            }
            else
            {
                // Si ya no hay más diálogo de Granny, terminamos la conversación
                conversationComplete = true; // Marcar que la conversación terminó
                zeroText(); // Cerrar el diálogo
            }
        }

        // Alternar entre los personajes para el siguiente turno
        isPlayerTalking = !isPlayerTalking; // Alterna el turno de conversación
    }

    // Detecta cuando el jugador entra en la zona de interacción con Granny
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true; // El jugador está cerca
            if (!conversationComplete)
            {
                ResetDialogue(); // Reseteamos el diálogo solo si no está completo
            }
        }
    }

    // Detecta cuando el jugador sale de la zona de interacción con Granny
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false; // El jugador se aleja
            conversationComplete = false; // La conversación no está completa
            zeroText(); // Cerrar el diálogo
        }
    }
}
