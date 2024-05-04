using UnityEngine;
using Photon.Pun;

public class PlayerLeftManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject aiPrefab;
    Vector3 lastPlayerPos; // Variable to store the last position of the player who left

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        // Check if the player who left is not the local player
        if (!otherPlayer.IsLocal)
        {
            // Retrieve the last known position of the player who left
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("LastPlayerPosition", out object pos))
            {
                lastPlayerPos = (Vector3)pos;
            }

            // Instantiate the AI player at the last player's position
            Quaternion spawnRotation = Quaternion.identity; // Set the spawn rotation of the AI player
            GameObject ai = PhotonNetwork.Instantiate(aiPrefab.name, lastPlayerPos, spawnRotation);

            // Optionally, flip the AI's scale if the last player was on the left side
            if (lastPlayerPos.x < 0)
            {
                ai.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
