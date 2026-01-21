using NUnit.Framework; // La librería de testing
using UnityEngine;

public class DificultadTests
{
    // --- CLASE SIMULADA (La lógica pura que queremos probar) ---
    // En un proyecto real, esto estaría en un archivo separado, no aquí.
    public static class CalculadoraDificultad
    {
        public static float Calcular(float puntos)
        {
            // La fórmula de tu juego: Base 1 + 0.1 por cada 50 puntos
            return 1f + (Mathf.Floor(puntos / 50f) * 0.1f);
        }
    }

    // --- EL TEST UNITARIO ---
    [Test]
    public void Dificultad_Aumenta_Correctamente_Al_Tener_100_Puntos()
    {
        // 1. ARRANQUE 
        // Definimos los datos de entrada
        float puntosSimulados = 100f;
        float resultadoEsperado = 1.2f; // 1 + (2 * 0.1) = 1.2

        // 2. ACT (Actuar)
        // Ejecutamos la función que queremos probar
        float resultadoReal = CalculadoraDificultad.Calcular(puntosSimulados);

        // 3. ASSERT (Verificar)
        // Preguntamos: ¿Es el resultado real igual al esperado?
        // Usamos un pequeño margen de error (0.001f) porque los floats a veces son imprecisos.
        Assert.AreEqual(resultadoEsperado, resultadoReal, 0.001f);
    }

    [Test]
    public void Dificultad_Se_Mantiene_En_Puntos_Bajos()
    {
        // Si tengo 49 puntos, la dificultad debería seguir siendo 1 (no ha subido de nivel)
        float resultado = CalculadoraDificultad.Calcular(49f);
        Assert.AreEqual(1f, resultado, 0.001f);
    }
}