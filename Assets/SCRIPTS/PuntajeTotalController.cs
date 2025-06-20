using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class PuntajeTotalController : MonoBehaviour
{
    public TextMeshProUGUI resultadoText;

    private string url = "http://localhost/juego_niveles/puntaje_total.php";

    void Start()
    {
        StartCoroutine(ObtenerPuntajesTotales());
    }

    IEnumerator ObtenerPuntajesTotales()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"usuarios\":" + www.downloadHandler.text + "}";
            UserList lista = JsonUtility.FromJson<UserList>(json);

            resultadoText.text = " Puntaje Total por Jugador:\n";
            foreach (UserModel user in lista.usuarios)
            {
                resultadoText.text += $"{user.nombre_usuario}: {user.puntaje} pts\n";
            }
        }
        else
        {
            resultadoText.text = "Error al obtener puntajes.";
            Debug.Log(www.error);
        }
    }

    public void MostrarTotales()
    {
        StartCoroutine(ObtenerPuntajesTotales());
    }
}
