using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void startGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(1);
    }
}
