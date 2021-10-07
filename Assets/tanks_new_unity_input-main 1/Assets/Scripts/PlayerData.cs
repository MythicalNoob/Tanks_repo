using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerData" ,menuName = "Scriptables/PlayerData" ,order = 1)]
public class PlayerData : ScriptableObject
{
    public List<PlayerInfo> playerData;
}

[System.Serializable]
public struct PlayerInfo
{
    public string scheme;
    public InputDevice device;

    public PlayerInfo(string scheme, InputDevice device)
    {
        this.scheme = scheme;
        this.device = device;
    }
    
}
