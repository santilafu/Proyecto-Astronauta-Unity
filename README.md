# 🚀 Space Runner - Proyecto DAM

**Space Runner** es un videojuego 2D de tipo *Endless Runner* desarrollado como proyecto práctico para el ciclo de Desarrollo de Aplicaciones Multiplataforma (DAM). Este repositorio documenta la evolución incremental del desarrollo, desde la lógica básica hasta la optimización y validación.

---

## 📋 Índice de Actividades
1.  **Actividad 5:** Lógica de Juego (Game Loop), Audio y Despliegue Móvil.
2.  **Actividad 6:** Quality Assurance (QA) y Unit Testing.
3.  **Actividad 7:** Documentación Técnica y Análisis de Diseño.

---

## 🎮 Actividad 5: Game Loop y Adaptación Móvil

### Descripción
En este módulo se transformó el prototipo visual en un producto jugable ("Game Loop" completo). Se implementaron los estados de juego (Playing/Game Over), la persistencia de datos y la adaptación de controles.

### Implementación Técnica
Para cumplir con los principios **SOLID**, se separó la lógica en dos componentes principales:

#### 1. GameManager (Singleton)
Actúa como el orquestador global del juego.
* **Patrón Singleton:** Permite el acceso estático (`GameManager.Instance`) desde cualquier script.
* **Dificultad Progresiva:** Aumenta la velocidad del juego (`Time.timeScale`) dinámicamente según la puntuación obtenida.
* **Persistencia:** Utiliza `PlayerPrefs` para almacenar y recuperar el *High Score* localmente.

#### 2. AstronautaController (Player)
Script refactorizado para encargarse exclusivamente de físicas e input.
* **Input Unificado:** Se implementó `Input.GetMouseButtonDown(0)` para soportar clic izquierdo en PC y *tap* (toque) en pantallas móviles sin necesidad de código condicional extra.

### Audio e Inmersión
* **Música (BGM):** Audio en bucle gestionado desde la cámara principal.
* **Efectos (SFX):** Feedback auditivo inmediato al realizar acciones de salto.

---

## 🧪 Actividad 6: Unit Testing y QA

### Importancia de los Tests
Se han introducido pruebas unitarias utilizando **Unity Test Framework (NUnit)** para garantizar la robustez del código ante futuros cambios (*Refactoring*).

### Caso de Prueba: Algoritmo de Dificultad
Se aisló la lógica matemática que calcula la velocidad del juego para verificar que la dificultad escala correctamente y evitar errores críticos (como velocidad 0 o negativa).

**Ejemplo del Test (Patrón AAA):**
```csharp
[Test]
public void Dificultad_Aumenta_Correctamente_Al_Tener_100_Puntos()
{
    // 1. ARRANGE: Preparamos un escenario con 100 puntos
    float puntos = 100f;
    float esperado = 1.2f; // La velocidad base es 1 + bonificador

    // 2. ACT: Ejecutamos la lógica de cálculo
    float resultado = 1f + (Mathf.Floor(puntos / 50f) * 0.1f);

    // 3. ASSERT: Verificamos que el resultado es exacto
    Assert.AreEqual(esperado, resultado, 0.001f);
}
````
---

## 📝 Actividad 7: Documentación Técnica y Análisis de Diseño
Esta sección recoge las decisiones de diseño y arquitectura tomadas durante el ciclo de vida del desarrollo.

### 7.1. Análisis de la Fase de Diseño
Para la concepción del proyecto, se evaluaron diferentes paradigmas antes de comenzar la implementación:

* Opción A (Plataformas por Niveles): Se contempló un juego clásico tipo Mario Bros.

  * Descarte: Se descartó debido a la alta carga de trabajo requerida para el diseño manual de niveles (Level Design) y la gestión de recursos gráficos, lo cual podría comprometer los plazos de entrega.

* Opción B (Endless Runner): Opción Elegida.

Justificación de la elección:

1. Enfoque Mobile-First: El género se adapta perfectamente a pantallas verticales y permite jugar con una sola mano (One-Thumb Gameplay), lo cual mejora la experiencia de usuario (UX) en dispositivos móviles.

2. Eficiencia en Desarrollo: Permitió centrar los esfuerzos en la arquitectura de programación y la generación procedimental de dificultad, optimizando el uso de memoria en dispositivos de gama baja al no cargar mapas extensos.

3. Rejugabilidad: El sistema de High Score fomenta la competición y repetición del bucle de juego sin necesidad de crear contenido infinito manualmente.

### 7.2. Análisis del Desarrollo Interno
Durante el desarrollo, se identificó y solucionó un problema de arquitectura crítico conocido como "God Class".

* **El Problema**: Inicialmente, el script del jugador (AstronautaController) controlaba el movimiento, la UI, la música y el reinicio de la escena. Esto provocaba un acoplamiento excesivo: al morir el jugador (y destruirse su objeto), el juego colapsaba al no poder ejecutar la lógica de "Game Over" porque el script responsable dejaba de existir.

* **La Solución**: Se desacopló el código extrayendo la lógica de gestión al GameManager.

  * El Jugador ahora solo notifica eventos ("he chocado") y se desactiva.

  * El GameManager escucha esos eventos y decide qué hacer (pausar tiempo, mostrar menú, guardar puntos).

Esta decisión de diseño hace que el código sea modular, mantenible y escalable, facilitando la expansión futura del proyecto.

## 📱 Instrucciones de Instalación
1. Clonar el repositorio.

2. Abrir con Unity 2022.3 LTS o superior.

3. Para probar en móvil:

  * Ir a Build Settings.

  * Cambiar plataforma a Android.

  * Asegurar que la escena MainScene está añadida.

  * Pulsar Build and Run con el dispositivo conectado.
