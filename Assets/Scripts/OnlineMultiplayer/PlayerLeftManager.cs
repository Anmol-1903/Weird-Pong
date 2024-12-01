using UnityEngine;
using Photon.Pun;

public class PlayerLeftManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject aiPrefab; // AI prefab to spawn
    [SerializeField] GameObject aiPrefab2; // AI prefab to spawn
    string lastPositionKey, scoreKey;

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);

        lastPositionKey = $"LastPlayerPosition_{otherPlayer.ActorNumber}";
        scoreKey = $"LastPlayerScore_{otherPlayer.ActorNumber}";

        // Retrieve the last position of the player who left
        Vector3 lastPlayerPos = Vector3.zero;
        int score = 0;

        if (otherPlayer.CustomProperties.ContainsKey(lastPositionKey))
            lastPlayerPos = (Vector3)otherPlayer.CustomProperties[lastPositionKey];

        if (otherPlayer.CustomProperties.ContainsKey(scoreKey))
            score = (int)otherPlayer.CustomProperties[scoreKey];


        GameObject ai;

        if (lastPlayerPos.x < 0)
        {
            ai = Instantiate(aiPrefab2, lastPlayerPos, Quaternion.identity);
            ai.GetComponent<Player>().SetScore(score);
        }
        else
        {
            ai = Instantiate(aiPrefab, lastPlayerPos, Quaternion.identity);
            ai.GetComponent<Player>().SetScore(score);
        }

        // Flip AI's direction if it was on the left side
        if (lastPlayerPos.x < 0)
        {
            ai.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
