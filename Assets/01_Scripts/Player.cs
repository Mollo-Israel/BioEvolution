using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float velocidadMovimiento = 5f;
    public int nivelMejorVelocidad = 0;

    // Referencia al Rigidbody2D
    private Rigidbody2D rb;

<<<<<<< Updated upstream
    void Start()
    {
        // Obtener el componente Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
=======
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
    public int puntos = 0;

    // Referencia al panel de la tienda
    public GameObject panelTienda;
    public Text textoCostoMejora;

    // Texto para mostrar los puntos de ADN del jugador
    public Text textoPuntos;

    // Costo de la mejora
    private int costoMejoraVelocidad = 5;
    private int costoMejoraVida = 10;

    void Start()
    {
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        textoComida.text = comidaActual.ToString() + "/" + comidaActual.ToString();
        // Asignar vida al player
        vidaActual = vidaMaxima;

        // Inicializar Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Inicializar valores de vida y comida
        // Actualizar UI inicial
        ActualizarBarras();
        ActualizarTextoPuntuacion();
        ActualizarTextoPuntos(); // Inicializamos los puntos de ADN
>>>>>>> Stashed changes
    }

    void Update()
    {
        // Llamar a la función para mover el jugador
        MoverJugador();
<<<<<<< Updated upstream
=======
        DescontarComida();
    }

    void DescontarComida()
    {
        // Agragar timer para descontar cada cierto tiempo
        if (comidaActual == 0)
        {
            vidaActual--;
        }
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        // El Rigidbody2D manejará las colisiones automáticamente con el BoxCollider2D de los límites
=======
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un organismo
        if (collision.gameObject.CompareTag("Organismo"))
        {
            puntos += 1; // Aumenta los puntos de ADN
            Destroy(collision.gameObject); // Destruye el organismo
            ActualizarTextoPuntuacion(); // Actualiza el texto en pantalla
            ActualizarTextoPuntos();
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
        textoPuntuacion.text = "X: " + puntos.ToString();
    }

    void ActualizarTextoPuntos()
    {
        // Actualiza la visualización de los puntos de ADN
        textoPuntos.text = "Puntos ADN: " + puntos.ToString();
    }

    void ActualizarBarras()
    {
        // Actualiza las barras de vida y comida
        if (barraVida != null)
            barraVida.fillAmount = vidaActual / vidaMaxima;

        if (barraComida != null)
            barraComida.fillAmount = comidaActual / comidaMaxima;
>>>>>>> Stashed changes
    }

    // Método para abrir el panel de la tienda
    public void AbrirTienda()
    {
        panelTienda.SetActive(true);
        textoCostoMejora.text = "Velocidad: " + costoMejoraVelocidad.ToString() + " ADN\nVida: " + costoMejoraVida.ToString() + " ADN";
    }

    // Método para cerrar el panel de la tienda
    public void CerrarTienda()
    {
        panelTienda.SetActive(false);
    }

    // Método para comprar mejora de velocidad
    public void ComprarMejoraVelocidad()
    {
        if (puntos >= costoMejoraVelocidad)
        {
            puntos -= costoMejoraVelocidad;
            nivelMejorVelocidad++;
            velocidadMovimiento += 1f; // Incrementa la velocidad de movimiento
            ActualizarTextoPuntuacion();
            ActualizarTextoPuntos(); // Actualiza los puntos después de la compra
        }
    }

    // Método para comprar mejora de vida
    public void ComprarMejoraVida()
    {
        if (puntos >= costoMejoraVida)
        {
            puntos -= costoMejoraVida;
            vidaMaxima += 5f; // Incrementa la vida máxima
            vidaActual = vidaMaxima; // Restaura la vida actual
            ActualizarTextoPuntuacion();
            ActualizarTextoPuntos(); // Actualiza los puntos después de la compra
        }
    }
}
