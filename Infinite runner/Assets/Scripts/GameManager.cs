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

    public User[] lista10Jgdrs; // Creo lista o array donde voy a guardar y ordenar 10 jugadores con LINQ
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

        /*contenedorDelTemplateRanking.gameObject.SetActive(false);
        templateDelRanking.gameObject.SetActive(false);*/
    }

    private void UpdateScore() // MOSTRAMOS EL NOMBRE Y PUNTAJE DE 'x' JUGADOR PREVIO QUE FUE GUARDADO EN LA DATABASE
    {
        scoreText.text = user.userName + " logró " + user.userScore + " puntos "; 
    }

    private void PostToDataBase()
    {
        User user = new User(); // NO SE SI CORRESPONDE CREARLO RECIEN ACA O AL PRINCIPIO, COMO EN LA LINEA 18

                                // VI QUE SI NO DECLARO EL NEW USER ACA, EN LA DATABASE AL SUBMITTEAR CADA NUEVA PARTIDA JUGADA
                                // EL JUGADOR APARECE SIN NOMBRE Y CON SCORE 0!!!!
        
        RestClient.Put(URLdeDatabase + playerName + ".json", user);
        
    }
    public void OnSubmit()
    {
        playerName = nameText.text;
        PostToDataBase();
    }
    private void RetrieveFromDataBase()
    {
        // ACA ABAJO CREO QUE TAMBIEN DEBERIA IR ".GetArray", PERO CUANDO LO PROBÉ (cambiando también el "user = response" por "lista10Jgdrs = response"),
        // AL APRETAR EL BOTON "GET SCORE" EN EL UNITY ME TIRABA ERROR "Unexpected node type", Y NO ME DEJABA VER LOS SCORES Y NOMBRES GUARDADOS 
        RestClient.Get<User>(URLdeDatabase + nameText.text + ".json").Then(response =>

        {
            user = response; // LEO DE MANERA INDIVIDUAL CADA USUARIO GUARDADO; ESTO PERMITE QUE EL BOTON "GET SCORE" ANDE
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

        

        //List<User> lista10Jgdrs = new List<User>(); // QUERÍA PROBAR ESTO DE LA CONVERSIÓN, PERO ENTIENDO QUE PUEDE SER REDUNDANTE CON RESPECTO AL 'ARRAY'
        //User[] lista10JgdrsEnArray = lista10Jgdrs.ToArray();

        float AlturaDelTemplate = 20f;
        for (int i=0; i< lista10Jgdrs.Length; i++) // EL 'Count' ES EL EQUIVALENTE EN LISTAS DE "ARRAY.LENGTH"
        {
            

            templateDelRanking.gameObject.SetActive(true);

            Transform transformParaInstanciar = Instantiate(templateDelRanking, contenedorDelTemplateRanking);
            RectTransform rectTransformParaInstanciar = transformParaInstanciar.GetComponent<RectTransform>();
            rectTransformParaInstanciar.anchoredPosition = new Vector2(0, -AlturaDelTemplate * i);
            

            transformParaInstanciar.Find("TxtPlaceholderNombreJgdrRanking").GetComponent<Text>().text = user.userName; // INYECTAMOS EL NOMBRE QUE FIGURA EN LA DATABASE, EN EL PLACEHOLDER 'nombre' DEL PANEL DEL RANKING
            transformParaInstanciar.Find("TxtPlaceholderScoreJgdrRanking").GetComponent<Text>().text = user.userScore.ToString(); // HACEMOS QUE EL SCORE SE IMPRIMA EN EL PLACEHOLDER 'score' DEL PANEL DEL RANKING

            

            //lista10Jgdrs.Add(new User());
            RestClient.GetArray<User>(URLdeDatabase); // LEO EL ARRAY DECLARADO ARRIBA DE TODO
            Debug.Log(lista10Jgdrs[i]);
            lista10Jgdrs = lista10Jgdrs.OrderByDescending(user => user.userScore).ToArray();
            

        }
        //lista10Jgdrs = lista10Jgdrs.OrderBy(puntuacionJgdr => puntuacionJgdr.userScore).ToList();

    }


}
