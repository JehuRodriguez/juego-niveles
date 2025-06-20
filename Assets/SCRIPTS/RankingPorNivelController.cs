using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RankingPorNivelController : MonoBehaviour
{
    public TMP_InputField nivelInputField;
    public TextMeshProUGUI resultadoText;

    private string url = "http://localhost/juego_niveles/ranking_por_nivel.php?nombre_nivel=";

    public void ObtenerRanking()
    {
        string nivel = nivelInputField.text.Trim();
        StartCoroutine(ObtenerRankingCoroutine(nivel));
    }

    IEnumerator ObtenerRankingCoroutine(string nivel)
    {
        string finalUrl = url + UnityWebRequest.EscapeURL(nivel);
        UnityWebRequest www = UnityWebRequest.Get(finalUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"usuarios\":" + www.downloadHandler.text + "}";
            UserList lista = JsonUtility.FromJson<UserList>(json);

            resultadoText.text = "Ranking de " + nivel + ":\n";
            foreach (UserModel user in lista.usuarios)
            {
                resultadoText.text += $"{user.nombre_usuario}: {user.puntaje}\n";
            }
        }
        else
        {
            resultadoText.text = "Error al conectar con el servidor.";
            Debug.Log(www.error);
        }
    }

}
