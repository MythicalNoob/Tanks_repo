using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyManager : MonoBehaviour
{
    public InputAction join1;
    public InputAction join2;

    //Hago una lista para ver los controles conectados
    [SerializeField]
    List<int> joinControllers = new List<int>();

    public Transform joinPosManager;

    // Start is called before the first frame update
    void Start()
    {
        //Habilito las acciones
        join1.Enable();
        join2.Enable();
        //Agregando la funcionalidad a las acciones
        join1.performed += (call) => JoinPlayer(call, 0);
        join2.performed += (call) => JoinPlayer(call, 1);

       // PlayerInfo info = new PlayerInfo();

    }

    void JoinPlayer(InputAction.CallbackContext callback, int index)
    {
        if (joinControllers.Contains(index)) return;

        var input = PlayerInputManager.instance.JoinPlayer();

        string scheme = "Keyboard&Mouse";
        if (index == 1) scheme = "Keyboard2";

        PlayerInput.all[input.playerIndex].SwitchCurrentControlScheme(controlScheme: scheme, Keyboard.current);

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
        Debug.Log("segunda vuelta");
        input.transform.SetPositionAndRotation(correctPos.position, correctPos.rotation);
        Debug.Log("apareci");
    }
}
