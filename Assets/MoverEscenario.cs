using UnityEngine;

public class MoverEscenario : MonoBehaviour
{
    public float velocidad = 7f;

    // DISTANCIA: ¿Cuánto mide tu suelo de largo? (Lo ajustamos en Unity)
    public float anchoDelObjeto = 19f;

    // COORDENADA: ¿Cuándo consideramos que se ha salido de la pantalla?
    public float coordenadaParaReiniciar = -19f;

    void Update()
    {
        // 1. Moverse a la izquierda
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        // 2. Comprobar si nos hemos salido por la izquierda
        if (transform.position.x < coordenadaParaReiniciar)
        {
            // 3. ¡TELETRANSPORTE! Lo movemos a la derecha
            // Le sumamos el doble del ancho para ponerlo a la cola
            Vector3 nuevaPosicion = transform.position;
            nuevaPosicion.x += anchoDelObjeto * 2;
            transform.position = nuevaPosicion;
        }
    }
}