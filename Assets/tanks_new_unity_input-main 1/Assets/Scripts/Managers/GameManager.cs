using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI winner;
    public GameObject winnerBoard;

    public static GameManager instance;

    public Transform spawnPositionManager;

    List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    List<int> killNumber = new List<int>();

    int index = 0;

    //Hace solo 1 vez la instance y solo cuando se destruye
    private void OnDestroy()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Timer.instance.finishCountDown = GameResult;
        //for (int i = 0; i < spawnPositionManager.transform.childCount; i++)
        //{
        //    spawnPoints.Add(spawnPositionManager.transform.GetChild(i));
        //}

        foreach (Transform trans in spawnPositionManager)
        {
            spawnPoints.Add(trans);
        }
        PlayerInputManager.instance.EnableJoining();
        //Une al primer jugador
        PlayerInputManager.instance.JoinPlayer(playerIndex: 0, controlScheme: "Keyboard&Mouse" ,pairWithDevice: Keyboard.current);
        //Une al segundo jugador
        var input = PlayerInputManager.instance.JoinPlayer(playerIndex: 1);
        //Debug.Log(PlayerInput.all.Count);
        //Debug.Log($"input is{input}");
        PlayerInput.all[1].SwitchCurrentControlScheme(controlScheme: "Keyboard2", Keyboard.current);
    }

    public void UpdateKills(int killerIndex)
    {
        killNumber[killerIndex]++;
    }

    public void OnSpawnPlayer(PlayerInput input)
    {
        Debug.Log("call me");
        killNumber.Add(0);

        int randomIndex = Random.Range(0, spawnPoints.Count);

        
        var tankSc = input.GetComponent<Tank>();
            tankSc.SetInitialPos(spawnPoints[randomIndex].position);
        tankSc.setIndex(index);
        var timerText = tankSc.transform.GetComponentInChildren<Canvas>().transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        Debug.Log(timerText.name);
        Timer.instance.text.Add(timerText);

        index++;

        spawnPoints.RemoveAt(randomIndex);
    }

    void GameResult()
    {
        int masMuertes = 0;
        int indexDelGanador = 0;

        for(int i = 0; i < killNumber.Count; i++)
        {
            int x = i;
            if(killNumber[x] > masMuertes)
            {
                masMuertes = killNumber[x];

                indexDelGanador = x;
            }

        }


        if(masMuertes == 0)
        {
            winnerBoard.SetActive(true);
            winner.SetText("No hay ganador");

            Time.timeScale = 0;
        }
        if(masMuertes != 0)
        {
            winnerBoard.SetActive(true);
            winner.SetText($"El ganador es el P{indexDelGanador + 1}");

            Time.timeScale = 0;
        }

        
    }


}
