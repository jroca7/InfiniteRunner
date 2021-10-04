using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static int score;
    public InputField nameText;
    public static GameManager inst;

    public string URLdeDatabase = "https://uai-simuladores21-tp1grupo6-default-rtdb.firebaseio.com/";

    [SerializeField] Text scoreText;
    User user = new User();
    public static int playerScore;
    public static string playerName;

    public Button BotonMostrarRanking;
    public Transform contenedorDelTemplateRanking;
    public Transform templateDelRanking;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE:  " + score;

    
    }

    private void Awake()
    {
        inst = this;

        contenedorDelTemplateRanking = transform.Find("ContenedorTemplateFranjaNombreConScore");
        templateDelRanking = transform.Find("TemplateTxtsNombre+Score");
    }

    private void UpdateScore()
    {
        scoreText.text = "Puntos: " + user.userScore;
    }

    private void PostToDataBase()
    {
        User user = new User(); // NO SE SI CORRESPONDE CREARLO RECIEN ACA O AL PRINCIPIO, COMO EN LA LINEA 18

        //List lista10Jugadores = new List<User>(); // Creo lista donde voy a guardar y ordenar 10 jugadores con LINQ
        
        RestClient.Put(URLdeDatabase + playerName + ".json", user);
    }
    public void OnSubmit()
    {
        playerName = nameText.text;
        PostToDataBase();
    }
    private void RetrieveFromDataBase()
    {
        RestClient.Get<User>(URLdeDatabase + nameText.text + ".json").Then(response =>

        {
            user = response;
            UpdateScore();
        });
    }
    public void OnGetScore()
    {
        RetrieveFromDataBase();
    }

    public void MostrarRankingOrdenado()
    {
        OrganizarRanking();
    }

    // Bloque de sorting de los 10 usuarios segun su puntuacion
    public void OrganizarRanking()
    {
        /*var ranking = from jugador in user
                      where user.userScore >= 0
                      orderby user.userScore
                      select user;*/

        

        List<User> lista10Jgdrs = new List<User>();

        float AlturaDelTemplate = 20f;
        for (int i=0; i< lista10Jgdrs.Count; i++) // EL 'Count' ES EL EQUIVALENTE EN LISTAS DE "ARRAY.LENGTH"
        {
            templateDelRanking.gameObject.SetActive(true);

            Transform transformParaInstanciar = Instantiate(templateDelRanking, contenedorDelTemplateRanking);
            RectTransform rectTransformParaInstanciar = transformParaInstanciar.GetComponent<RectTransform>();
            rectTransformParaInstanciar.anchoredPosition = new Vector2(0, -AlturaDelTemplate * i);
            

            transformParaInstanciar.Find("TxtPlaceholderNombreJgdrRanking").GetComponent<Text>().text = user.userName; // INYECTAMOS EL NOMBRE QUE FIGURA EN LA DATABASE, EN EL PLACEHOLDER 'nombre' DEL PANEL DEL RANKING
            transformParaInstanciar.Find("TxtPlaceholderScoreJgdrRanking").GetComponent<Text>().text = user.userScore.ToString(); // HACEMOS QUE EL SCORE SE IMPRIMA EN EL PLACEHOLDER 'score' DEL PANEL DEL RANKING

            lista10Jgdrs.Add(new User());
            var ordenadoMayorMenor = lista10Jgdrs.OrderByDescending(orden => orden.userScore).ToList();

        }
        //lista10Jgdrs = lista10Jgdrs.OrderBy(puntuacionJgdr => puntuacionJgdr.userScore).ToList();

    }


}
