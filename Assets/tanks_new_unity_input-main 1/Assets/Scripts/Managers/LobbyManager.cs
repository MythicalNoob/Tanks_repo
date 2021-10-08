using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public InputAction join1;
    public InputAction join2;

    //Hago una lista para ver los controles conectados
    [SerializeField]
    List<int> joinControllers = new List<int>();

    public Transform joinPosManager;

    public PlayerData scriptablePlayerDataObject;
    // Start is called before the first frame update
    void Start()
    {
        //Habilito las acciones
        join1.Enable();
        join2.Enable();
        //Agregando la funcionalidad a las acciones
        join1.performed += (call) => JoinPlayer(call, 0);
        join2.performed += (call) => JoinPlayer(call, 1);

        scriptablePlayerDataObject.playerData = new List<PlayerInfo>();

    }

    void JoinPlayer(InputAction.CallbackContext callback, int index)
    {
        if (joinControllers.Contains(index)) return;
        Debug.Log("antes de join");
        var input = PlayerInputManager.instance.JoinPlayer();
        Debug.Log("despues de join");

        string scheme = "Keyboard&Mouse";
        if (index == 1) scheme = "Keyboard2";

        PlayerInput.all[input.playerIndex].SwitchCurrentControlScheme(controlScheme: scheme, Keyboard.current);

        var playerInfo = new PlayerInfo(scheme, Keyboard.current);

        int playerIndex = scriptablePlayerDataObject.playerData.Count - 1;

        scriptablePlayerDataObject.playerData[playerIndex] = playerInfo;

        joinControllers.Add(index);
        //join1.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJoinPlayer(PlayerInput input)
    {
        Debug.Log("me uni");
        Transform correctPos = joinPosManager.GetChild(input.playerIndex);
        
        input.transform.SetPositionAndRotation(correctPos.position, correctPos.rotation);

        scriptablePlayerDataObject.playerData.Add(new PlayerInfo(input.currentControlScheme, input.devices[0]));
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(2);
    }
}
