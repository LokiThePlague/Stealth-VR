using __Content.Scripts.Network;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace __Content.Scripts
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text joinCodeText;

        private void Start()
        {
            joinCodeText.SetText("Join code: " + RelayManager.Instance.joinCode);

            if (RelayManager.Instance.isHost)
                NetworkManager.Singleton.StartHost();
            else
                NetworkManager.Singleton.StartClient();
        }
    }
}