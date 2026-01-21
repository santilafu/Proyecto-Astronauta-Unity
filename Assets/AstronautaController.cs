using UnityEngine;

public class AstronautaController : MonoBehaviour
{
    // --- CONFIGURACIÓN ---
    [Header("Movimiento")]
    public float fuerzaSalto = 8f;

    [Header("Audio")]
    public AudioClip sonidoSalto;   // Arrastra aquí tu archivo de audio de salto
    private AudioSource audioSource;

    // --- ESTADOS INTERNOS ---
    private Rigidbody2D rb;
    private bool enSuelo = true;
    private bool estaVivo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Añadimos o cogemos el componente de Audio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Si el GameManager dice que el juego acabó, no hacemos nada
        if (!estaVivo) return;

        // --- DETECCIÓN DE ENTRADA (MÓVIL Y PC) ---
        // Input.GetMouseButtonDown(0) detecta tanto el clic izquierdo como el toque en pantalla táctil.
        // Es la forma más sencilla de hacerlo compatible con Android/iOS sin sistemas complejos.
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && enSuelo)
        {
            Saltar();
        }
    }

    void Saltar()
    {
        // Reseteamos velocidad vertical para que el salto sea siempre igual
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        enSuelo = false;

        // Reproducir sonido si existe
        if (sonidoSalto != null) audioSource.PlayOneShot(sonidoSalto);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            estaVivo = false;
            // Avisamos al Director (GameManager) de que hemos muerto
            GameManager.Instance.GameOver();

            // Opcional: Destruir el astronauta o desactivarlo
            // gameObject.SetActive(false); 
        }
    }
}