using UnityEngine;
using UnityEngine.Video;

public class VideoRenderer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoName;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName + ".mp4");
    }
}