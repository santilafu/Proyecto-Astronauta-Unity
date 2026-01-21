using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- SINGLETON (Para acceder fácilmente desde el Astronauta) ---
    public static GameManager Instance;

    // --- CONFIGURACIÓN DE UI ---
    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoPuntuacion;
    public GameObject panelGameOver;
    public TextMeshProUGUI textoRecord;

    // --- CONFIGURACIÓN DE JUEGO ---
    private float puntos = 0f;
    private bool juegoTerminado = false;
    private float dificultadActual = 1f;

    void Awake()
    {
        // Configuración del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        // ------------------------------------------

        // Resto de tu código (ocultar panel, poner puntos a 0, etc.)
        if (panelGameOver != null) panelGameOver.SetActive(false);
        puntos = 0;
        juegoTerminado = false;
    }
    void Update()
    {
        if (juegoTerminado) return;

        // 1. LÓGICA DE PUNTOS
        puntos += Time.deltaTime * 10;

        // 2. ACTUALIZAR INTERFAZ
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntos: " + puntos.ToString("F0");
        }

        // 3. CONTROL DE DIFICULTAD (Acelerar el juego progresivamente)
        float nuevaDificultad = 1f + (Mathf.Floor(puntos / 50f) * 0.1f);
        if (nuevaDificultad > dificultadActual)
        {
            dificultadActual = nuevaDificultad;
            Time.timeScale = dificultadActual;
        }
    }

    // --- FUNCIÓN PÚBLICA DE GAME OVER ---
    public void GameOver()
    {
        juegoTerminado = true;
        Time.timeScale = 0; // Pausa total del mundo

        // Gestión del Récord
        int recordAnterior = PlayerPrefs.GetInt("RecordMaximo", 0);
        int puntosActuales = (int)puntos;

        if (puntosActuales > recordAnterior)
        {
            PlayerPrefs.SetInt("RecordMaximo", puntosActuales);
            recordAnterior = puntosActuales;
        }

        // Mostrar UI
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            if (textoRecord != null)
            {
                textoRecord.text = "RÉCORD: " + recordAnterior.ToString();
            }
        }
    }

    // --- FUNCIONES PARA LOS BOTONES ---
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}