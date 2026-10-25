# Trabajo Práctico 02 / Programación de Videojuegos I

## Descripción

Este proyecto corresponde al **Trabajo Práctico N.º 02 de Programación de Videojuegos I**.

El objetivo del ejercicio fue desarrollar un **prototipo de videojuego 3D de infiltración y sigilo utilizando Unity**, aplicando diferentes conceptos de programación, inteligencia artificial, detección mediante raycasts e interfaces de usuario.

El jugador comienza fuera de un laboratorio y debe infiltrarse en sus instalaciones, evitando ser detectado por los drones de seguridad. Para avanzar, deberá recorrer el escenario, esquivar enemigos y atravesar un pequeño laberinto hasta llegar a una computadora que debe hackear.

Una vez completado el hackeo, el jugador deberá regresar por el mismo camino y alcanzar la plataforma inicial para completar la misión.

Durante la partida, el jugador puede recibir daño de los drones, recuperar vida automáticamente y recoger monedas distribuidas por el escenario para aumentar su puntaje.

El proyecto permite poner en práctica conceptos como **Raycast, LayerMask, inteligencia artificial con NavMesh, detección de objetivos, colisiones, temporizadores, gestión de vida, puntuación, interfaces de usuario y administración de estados del juego**.

<img width="409" height="248" alt="image" src="https://github.com/user-attachments/assets/275a938f-9b18-42a3-95df-5d29e589e3b8" />


---

## Versión de Unity

Unity 6

---

## Controles

| Tecla | Acción |
| :--- | :--- |
| **W / A / S / D** | Movimiento del personaje |
| **Espacio** | Saltar |
| **Flecha Izquierda / Derecha** | Ajustar la cámara horizontalmente |
| **Flecha Arriba / Abajo** | Ajustar la cámara verticalmente |
| **E** | Interactuar con la terminal e iniciar el hackeo cuando se encuentra dentro del rango |
| **ESC** | Pausar la partida o reanudarla |

---

## Mecánicas implementadas

* **Movimiento y control del jugador:**
  * Movimiento mediante las teclas WASD.
  * Salto mediante la tecla Espacio.
  * Control de cámara mediante las flechas del teclado.
  * Movimiento y temporizadores utilizando `Time.deltaTime` para mantener un comportamiento consistente con la velocidad de ejecución del juego.

<img width="777" height="335" alt="image" src="https://github.com/user-attachments/assets/c1ece2b7-01c6-4db6-9ecf-a12fd6eed4d2" />

* **Diseño del escenario mediante Terrain Tool:**
  * Creación de un terreno 3D utilizando las herramientas de terreno de Unity.
  * Aplicación de texturas para diferenciar las superficies.
  * Construcción de desniveles y zonas elevadas.
  * Distribución de estructuras, paredes, obstáculos y elementos ambientales para representar el exterior y el interior del laboratorio.

<img width="611" height="307" alt="image" src="https://github.com/user-attachments/assets/086c7475-8ab3-486b-bc82-6c59698b4f01" />
<img width="468" height="273" alt="image" src="https://github.com/user-attachments/assets/8a13774f-c6f4-488c-bb10-376b5d339f4a" />


* **Raycast y LayerMask para la detección de objetos interactivos:**
  * Utilización de raycasts para detectar objetos interactivos a distancia.
  * Filtrado de objetos mediante `LayerMask`, permitiendo detectar únicamente los objetos pertenecientes a la capa interactiva.
  * Uso de la tecla E para iniciar la interacción con la terminal de hackeo.
  * Detección adicional por proximidad para mostrar el texto de interacción sin necesidad de mirar directamente al objeto.

<img width="454" height="259" alt="image" src="https://github.com/user-attachments/assets/72cdf904-dfde-4867-a4c6-28014512753b" />


* **Terminal de hackeo con temporizador:**
  * Al presionar E dentro del rango de interacción, comienza una cuenta regresiva de 10 segundos.
  * Durante el proceso se muestra el tiempo restante.
  * Al finalizar, la terminal cambia al color verde y el texto pasa a indicar «Hackeado».
  * El sistema impide iniciar nuevamente el hackeo una vez completado.

<img width="475" height="250" alt="image" src="https://github.com/user-attachments/assets/47b2f47e-0202-48f1-a51b-44405917d6b6" />

<img width="461" height="241" alt="image" src="https://github.com/user-attachments/assets/b335d36a-029a-4922-a4c7-74420284e77f" />


* **Inteligencia artificial de los drones mediante NavMesh:**
  * Utilización de `NavMeshAgent` para permitir que los drones se desplacen por el escenario.
  * Patrullaje entre puntos definidos.
  * Detección del jugador dentro de un rango de visión.
  * Persecución automática del jugador cuando es detectado.
  * Abandono de la persecución cuando el jugador se aleja del rango de detección.
  * Representación visual semitransparente del área de visión para indicar la zona de detección del enemigo.

<img width="765" height="272" alt="image" src="https://github.com/user-attachments/assets/429295ff-ea2f-4f51-a525-745641bd88df" />


* **Sistema de vida, daño y regeneración:**
  * El jugador comienza con 100 puntos de vida.
  * Los drones pueden reducir la vida del jugador al atacarlo o entrar en contacto con él, según la configuración del sistema de daño.
  * Regeneración automática de 1 punto de vida por segundo mientras el jugador permanece con vida.
  * Actualización de la vida actual y máxima en la interfaz.
  * La barra de vida cambia su relleno según la proporción de vida restante.
  * Al llegar a cero puntos de vida, se activa el estado de derrota.

<img width="357" height="106" alt="image" src="https://github.com/user-attachments/assets/6b4e3bff-2749-4a45-b219-22e786d81d05" />


* **Sistema de puntuación y recolección de monedas:**
  * Distribución de objetos coleccionables por el escenario.
  * Detección de colisiones entre el jugador y las monedas.
  * Eliminación de la moneda al recogerla.
  * Incremento de un punto por cada moneda recogida.
  * Visualización del puntaje en la esquina superior derecha de la pantalla.

<img width="197" height="94" alt="image" src="https://github.com/user-attachments/assets/17e03e2c-ee73-4109-9d83-059fc967fef9" />

<img width="141" height="84" alt="image" src="https://github.com/user-attachments/assets/03ce0e31-f926-4345-8f44-2467c6e41b85" />


* **Plataforma de salida y condición de victoria:**
  * El jugador debe regresar a la plataforma inicial después de hackear la terminal.
  * Si pisa la plataforma sin haber completado el hackeo, se mantiene apagada y muestra el mensaje «Debes hackear la terminal para ganar».
  * Si la terminal fue hackeada, la plataforma cambia al color de encendido y muestra «Has Ganado».
  * La condición de victoria se comunica al sistema central del juego.

<img width="640" height="243" alt="image" src="https://github.com/user-attachments/assets/681e5722-e2f1-4ac8-a455-421ba837fe05" />


* **Sistema de pausa mediante ESC:**
  * Al presionar ESC durante la partida, se congela el tiempo del juego.
  * Se muestra un panel oscuro semitransparente que cubre la pantalla.
  * El menú presenta el título «PAUSA» y el mensaje «Presiona ESC para seguir».
  * Al volver a presionar ESC, se oculta el menú y se reanuda la partida.

<img width="708" height="270" alt="image" src="https://github.com/user-attachments/assets/1f0dc2aa-796a-476e-b312-efb42c488d28" />


* **GameManager y estados del juego:**
  * Centralización de los estados principales de la partida.
  * `Start`: inicialización del juego.
  * `Playing`: estado normal durante la partida.
  * `Paused`: pausa y congelación temporal del juego.
  * `Victory`: victoria al completar el objetivo y regresar a la plataforma.
  * `GameOver`: derrota al perder todos los puntos de vida.
  * Registro de cambios de estado, vida y puntaje en la consola.
  * Reinicio de la escena para comenzar una nueva partida desde el estado inicial.

<img width="640" height="243" alt="image" src="https://github.com/user-attachments/assets/681e5722-e2f1-4ac8-a455-421ba837fe05" />

<img width="708" height="270" alt="image" src="https://github.com/user-attachments/assets/1f0dc2aa-796a-476e-b312-efb42c488d28" />

<img width="619" height="214" alt="image" src="https://github.com/user-attachments/assets/27136a55-0397-4542-8ddf-ff61e97c88c4" />

---

## Conceptos de programación utilizados

Durante el desarrollo del proyecto se utilizaron diferentes herramientas y conceptos de Unity:

* **Variables, referencias y configuración desde el Inspector:** permiten modificar valores como vida máxima, daño, rango de visión, distancia de interacción, duración del hackeo y colores de las plataformas sin modificar directamente el código.
* **Métodos y estructuras de control:** utilizados para organizar las acciones del jugador, las interacciones, el comportamiento de los drones y las condiciones de victoria y derrota.
* **`Time.deltaTime`:** utilizado para calcular el avance de temporizadores y procesos que dependen del tiempo, como el hackeo y la regeneración de vida.
* **Raycast:** permite lanzar un rayo desde la cámara para detectar objetos interactivos.
* **`LayerMask`:** filtra los objetos que pueden ser detectados por el raycast y las consultas de proximidad.
* **`Physics.OverlapSphere()`:** permite detectar objetos interactivos dentro de un radio alrededor del jugador.
* **`NavMeshAgent`:** permite que los drones se desplacen por las superficies navegables del escenario.
* **Colisiones y Triggers (`OnTriggerEnter`, `OnTriggerStay`, `OnTriggerExit`):** utilizados para detectar la recolección de monedas y la entrada del jugador en la plataforma de salida.
* **`Vector3.Distance()`:** permite calcular distancias entre el jugador, la terminal y otros elementos del escenario.
* **`Mathf.Clamp01()` y `Mathf.Min()`:** utilizados para limitar valores, actualizar proporciones de vida y evitar superar la vida máxima.
* **`Time.timeScale`:** permite congelar el tiempo del juego al pausar la partida o alcanzar un estado terminal.
* **`Debug.Log()`:** utilizado para informar sobre cambios de estado, vida, puntaje, hackeo y resultados de la partida.
* **`GameObject.SetActive()`:** permite mostrar y ocultar los distintos paneles de la interfaz.
* **`SceneManager.LoadScene()`:** utilizado para reiniciar la escena y comenzar nuevamente la partida.
* **Interfaces de usuario con TextMeshPro e Image:** permiten mostrar mensajes de interacción, vida, puntaje y menús de estado.

---

## Objetivo del juego

El objetivo principal es **infiltrarse en el laboratorio, evitar la detección de los drones, hackear la computadora y regresar a la plataforma inicial para completar la misión**.

Para conseguirlo, el jugador deberá recorrer el escenario, atravesar el laberinto, evitar o escapar de los enemigos y administrar su vida durante el recorrido.

Al llegar a la computadora, deberá iniciar el hackeo y esperar los 10 segundos necesarios para completarlo. Una vez que la terminal indique «Hackeado», deberá regresar por el mismo camino y alcanzar la plataforma de salida.

Si el jugador llega a la plataforma sin haber hackeado la terminal, no podrá completar el objetivo. Si pierde todos sus puntos de vida, se activará el menú de derrota. Si logra regresar después de completar el hackeo, se activará el menú de victoria.

---

## Tareas y mecánicas completadas

- [x] Movimiento del jugador.
- [x] Salto del jugador.
- [x] Control de cámara mediante las flechas del teclado.
- [x] Creación del escenario mediante Terrain Tool.
- [x] Aplicación de texturas y construcción de desniveles.
- [x] Detección de objetos mediante Raycast.
- [x] Filtrado de objetos mediante LayerMask.
- [x] Detección por proximidad para mostrar el texto de interacción.
- [x] Interacción con la terminal mediante la tecla E.
- [x] Hackeo de la terminal con temporizador de 10 segundos.
- [x] Cambio de color y texto de la terminal al completar el hackeo.
- [x] Drones con navegación mediante NavMesh.
- [x] Patrullaje y persecución del jugador.
- [x] Rango de visión visible y semitransparente.
- [x] Daño al jugador y regeneración de vida.
- [x] Barra de vida y texto de vida actual/máxima.
- [x] Recolección de monedas y sistema de puntuación.
- [x] Plataforma de salida con condición de victoria.
- [x] Mensajes de interacción, hackeo y victoria.
- [x] Menú de pausa mediante ESC.
- [x] Congelación y reanudación del tiempo.
- [x] GameManager con estados Start, Playing, Paused, Victory y GameOver.
- [x] Menú de derrota por pérdida de vida.
- [x] Menú de victoria al completar la misión.
- [x] Botón para reiniciar la partida.
- [x] Registro de estados, vida y puntaje en la consola.

---

## Autor

**Robert Fuentes Facundo Guillermo**  
DNI: [46597991]
LU: [000808]

---

## Ejemplo de código

Una de las herramientas utilizadas en el proyecto fue `Physics.Raycast()`, que permite detectar objetos interactivos mediante un rayo y filtrar los resultados utilizando un `LayerMask`.

El siguiente ejemplo representa la detección de una terminal desde la cámara del jugador:

```csharp
Ray ray = new Ray(
    playerCamera.transform.position,
    playerCamera.transform.forward
);

if (Physics.Raycast(
    ray,
    out RaycastHit hit,
    interactionDistance,
    interactableLayer))
{
    HackTerminal terminal =
        hit.collider.GetComponentInParent<HackTerminal>();

    if (terminal != null)
    {
        Debug.Log("Terminal detectada: " + hit.collider.name);
    }
}
```

De esta manera, el jugador puede detectar objetos pertenecientes a la capa interactiva sin realizar comprobaciones innecesarias sobre todos los objetos del escenario.

El sistema se complementa con una detección por proximidad para mostrar el mensaje de interacción, mientras que la tecla E permite iniciar el hackeo cuando el jugador se encuentra dentro del rango correspondiente.
