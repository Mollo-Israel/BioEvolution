using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Sprite[] frames; // Array de sprites para los frames
    public float frameRate = 0.1f; // Duración de cada frame (en segundos)

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        // Obtén el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Asegúrate de que hay frames
        if (frames.Length > 0)
        {
            spriteRenderer.sprite = frames[currentFrame];
        }
    }

    void Update()
    {
        // Actualizar el timer
        timer += Time.deltaTime;

        // Cambiar al siguiente frame si se supera el tiempo
        if (timer >= frameRate)
        {
            timer -= frameRate; // Reinicia el timer
            currentFrame = (currentFrame + 1) % frames.Length; // Avanza al siguiente frame en bucle
            spriteRenderer.sprite = frames[currentFrame]; // Actualiza el sprite
        }
    }
}
