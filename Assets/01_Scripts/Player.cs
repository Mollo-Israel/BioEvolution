using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{// Velocidad de movimiento del jugador
 // Velocidad de movimiento del jugador
    public float velocidadMovimiento = 5f;

    // Referencia al Rigidbody2D
    private Rigidbody2D rb;

    // Referencia al texto de puntuación
    public Text textoPuntuacion;

    // Referencia a las barras de vida y comida
    public Image barraVida;
    public Image barraComida;

    //texto barra de vida
    public Text textoVida;

    //texto comida
    public Text textoComida;

    // Valores de vida y comida
    private float vidaMaxima = 10f;
    private float vidaActual = 3;
    private float comidaMaxima = 10f;
    private float comidaActual = 0;

    // Puntos de ADN
    private int puntos = 0;

    void Start()
    {

        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        textoComida.text = comidaActual.ToString() + "/" + comidaActual.ToString();
        //asignar vida al player
        vidaActual = vidaMaxima;
        //textoVida.text = "Life = " + vidaActual;


        // Inicializar Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Inicializar valores de vida y comida
        
        // Actualizar UI inicial
        ActualizarBarras();
        ActualizarTextoPuntuacion();
    }

    void Update()
    {
        barraComida.fillAmount = comidaActual / comidaMaxima;
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        textoComida.text = comidaActual.ToString() + "/" + comidaActual.ToString();
        // Llamar a la función para mover el jugador
        MoverJugador();
        DescontarComida();
    }
    void DescontarComida()
    {
        //agragar timer para descontar cada cierto tiempo
        if (comidaActual == 0)
        {
            vidaActual--;
        }
    }

    void MoverJugador()
    {
        // Obtener las entradas del usuario (teclas de dirección o 'WASD')
        float movimientoX = Input.GetAxis("Horizontal"); // A/D o Flechas izquierda/derecha
        float movimientoY = Input.GetAxis("Vertical");   // W/S o Flechas arriba/abajo

        // Crear un vector de movimiento
        Vector2 movimiento = new Vector2(movimientoX, movimientoY) * velocidadMovimiento;

        // Aplicar el movimiento al Rigidbody2D
        rb.velocity = movimiento;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un organismo
        if (collision.gameObject.CompareTag("Organismo"))
        {
            puntos += 1; // Aumenta los puntos de ADN
            ActualizarTextoPuntuacion(); // Actualiza el texto en pantalla
            Destroy(collision.gameObject); // Destruye el organismo
        }

        // Si colisiona con comida
        if (collision.gameObject.CompareTag("Food"))
        {
            comidaActual += 1;
            Destroy(collision.gameObject); // Destruye la comida
        }

        
    }

    public void TakeDamage(float dmg)
    {

        vidaActual -= dmg;
        barraVida.fillAmount = vidaActual / vidaMaxima;
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        if (vidaActual <= 0)
        {
            SceneManager.LoadScene("Start_Game");
            //Destroy(gameObject);
        }

    }

    void ActualizarTextoPuntuacion()
    {
        textoPuntuacion.text = "X" + puntos.ToString();
    }

    void ActualizarBarras()
    {
        // Actualiza las barras de vida y comida
        if (barraVida != null)
            barraVida.fillAmount = vidaActual / vidaMaxima;

        if (barraComida != null)
            barraComida.fillAmount = comidaActual / comidaMaxima;
    }
}
