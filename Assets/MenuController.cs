using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class MenuController : MonoBehaviour
{
    // Esta función la llamará el botón
    public void CargarJuego()
    {
        // IMPORTANTE: Asegúrate de que tu escena de juego se llame "MainScene"
        // Si tiene otro nombre, cámbialo aquí entre las comillas.
        SceneManager.LoadScene("JuegoAstronauta");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo...");
    }
}