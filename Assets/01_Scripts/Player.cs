using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public TiendaSkills tienda;

    public float velocidadMovimiento = 5f;
    public int nivelMejorVelocidad = 0;

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
    private float vidaMaxima = 100f;
    private float vidaActual = 25f;
    private float comidaMaxima = 10f;
    private float comidaActual = 5f;

    // Puntos de ADN
    public int puntos = 0;

    // Límites del área donde el jugador puede moverse (en el eje X y Y)
    public float limiteIzquierdo = -24f;
    public float limiteDerecho = 24f;
    public float limiteInferior = -20f;
    public float limiteSuperior = 20f;

    public Text textoCostoMejora;
    public Text textoPuntos;
    // Costo de la mejora
    private int costoMejoraVelocidad = 5;
    private int costodefensa = 8;

    // Temporizador para descontar vida
    private float timerComida = 0f;
    private float tiempoRestarVida = 10f; // Tiempo en segundos para restar vida cada vez que la comida está a 0

    public float tiempoParaPerderComida = 5f;  // Por ejemplo, cada 5 segundos perderemos 1 de comida

    private float timerComidaGradual = 0f;  // Temporizador para reducir la comida

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
        textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString();

        // Llamar a la función para mover el jugador
        MoverJugador();

        // Llamar a la función para descontar comida
        DescontarComida();

        // Temporizador para descontar vida si comida está en 0
        if (comidaActual == 0)
        {
            timerComida += Time.deltaTime;

            // Si han pasado 10 segundos, restar vida
            if (timerComida >= tiempoRestarVida)
            {
                RestarVida(1f); // Resta 10 de vida
                timerComida = 0f; // Reinicia el temporizador
            }
        }
    }

    void DescontarComida()
    {
        // Si la comida es mayor que 0, se sigue descontando gradualmente
        if (comidaActual > 0)
        {
            timerComidaGradual += Time.deltaTime;

            // Si han pasado los segundos necesarios para reducir la comida
            if (timerComidaGradual >= tiempoParaPerderComida)
            {
                // Reducir la comida gradualmente
                comidaActual -= 1; // Resta 1 de comida cada vez
                timerComidaGradual = 0f; // Reinicia el temporizador

                // Actualiza el texto y la barra de comida
                textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString();
                barraComida.fillAmount = comidaActual / comidaMaxima;
            }
        }

        // Si la comida es 0, no descontamos más comida
        if (comidaActual <= 0)
        {
            comidaActual = 0;  // Asegura que la comida no baje de 0
            textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString(); // Actualiza el texto de la comida
            barraComida.fillAmount = comidaActual / comidaMaxima; // Actualiza la barra de comida
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

        // Rotación del jugador según la dirección del movimiento
        if (movimientoX != 0 || movimientoY != 0)
        {
            // Calcular el ángulo de rotación basado en la dirección del movimiento
            float angulo = Mathf.Atan2(movimientoY, movimientoX) * Mathf.Rad2Deg;  // Convertir de radianes a grados

            // Aplicar la rotación al jugador (esto hace que el jugador rote hacia la dirección en la que se mueve)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        }

        // Si no hay movimiento, el jugador mantiene la rotación predeterminada (como se hacía antes)
        else
        {
            // Aseguramos que el jugador no gire si no hay movimiento
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un organismo
        if (collision.gameObject.CompareTag("Organismo"))
        {
            puntos += 1; // Aumenta los puntos de ADN
            ActualizarTextoPuntuacion(); // Actualiza el texto en pantalla
            Destroy(collision.gameObject); // Destruye el organismo
            //tienda.ActualizarTextoPuntos();
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

    void RestarVida(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0)
        {
            vidaActual = 0;
        }
        barraVida.fillAmount = vidaActual / vidaMaxima;
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
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
    public void ComprarMejoraVelocidad()
    {
        if (puntos >= costoMejoraVelocidad)
        {
            puntos -= costoMejoraVelocidad;
            nivelMejorVelocidad++;
            velocidadMovimiento += 1f; // Incrementa la velocidad de movimiento
            ActualizarTextoPuntuacion();
            tienda.ActualizarTextoPuntos(); // Actualiza los puntos después de la compra
            // Metodo o if para el limite de nivel de la habilidad (speed)
        }
    }

}
