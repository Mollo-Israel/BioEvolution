using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5f;

    // Referencia al Rigidbody2D
    private Rigidbody2D rb;

    // Referencia al texto de puntuación
    public Text textoPuntuacion;

    // Referencia a las barras de vida y comida
    public Image barraVida;
    public Image barraComida;

    // Texto barra de vida
    public Text textoVida;

    // Texto comida
    public Text textoComida;

    // Valores de vida y comida
    private float vidaMaxima = 10f;
    private float vidaActual = 3;
    private float comidaMaxima = 10f;
    private float comidaActual = 0;

    // Puntos de ADN
    private int puntos = 0;

    // Límites del área donde el jugador puede moverse (en el eje X y Y)
    public float limiteIzquierdo = -24f;
    public float limiteDerecho = 24f;
    public float limiteInferior = -20f;
    public float limiteSuperior = 20f;

    void Start()
    {
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString();

        // Asignar vida al player
        vidaActual = vidaMaxima;

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
        // Agragar timer para descontar cada cierto tiempo
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

        // Limitar la posición del jugador dentro de los límites definidos
        Vector2 posicionJugador = rb.position;
        posicionJugador.x = Mathf.Clamp(posicionJugador.x, limiteIzquierdo, limiteDerecho);
        posicionJugador.y = Mathf.Clamp(posicionJugador.y, limiteInferior, limiteSuperior);

        // Actualizar la posición del jugador para que no sobrepase los límites
        rb.position = posicionJugador;
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
            // Destroy(gameObject);
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
