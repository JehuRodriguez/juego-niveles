using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class PuntajePorJugadorController: MonoBehaviour
{
    public TMP_InputField jugadorInputField;
    public TextMeshProUGUI resultadoText;

    private string url = "http://localhost/juego_niveles/obtener_puntaje_usuario.php?nombre_usuario=";

    public void ObtenerPuntajes()
    {
        string jugador = jugadorInputField.text.Trim();
        StartCoroutine(ObtenerPuntajesCoroutine(jugador));
    }

    IEnumerator ObtenerPuntajesCoroutine(string jugador)
    {
        string finalUrl = url + UnityWebRequest.EscapeURL(jugador);
        UnityWebRequest www = UnityWebRequest.Get(finalUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"usuarios\":" + www.downloadHandler.text + "}";
            UserList lista = JsonUtility.FromJson<UserList>(json);

            resultadoText.text = "Puntajes de " + jugador + ":\n";
            foreach (UserModel user in lista.usuarios)
            {
                resultadoText.text += $"Nivel: {user.nombre_usuario} - Puntaje: {user.puntaje}\n";
            }
        }
        else
        {
            resultadoText.text = "Error al conectar con el servidor.";
            Debug.Log(www.error);
        }
    }
}
