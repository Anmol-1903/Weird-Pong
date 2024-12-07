using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WallRotator : MonoBehaviourPun
{
    [SerializeField] private float[] angles;

    float selectedAngle1, selectedAngle2;
    int scene, same;

    [SerializeField] Transform[] transforms;


    private void Start()
    {
        InvokeRepeating(nameof(RandomAngle), 5,Random.Range(2, 6));
        scene = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        if(scene == 3)
            if(!photonView.IsMine)
                return;


        if (same == 1)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                if (i == 0 || i == 2)
                {
                    if (i == 0)
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, selectedAngle1), Time.deltaTime * 5);
                    else
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, selectedAngle2), Time.deltaTime * 5);
                }
                else
                {
                    if (i == 1)
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, -selectedAngle1), Time.deltaTime * 5);
                    else
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, -selectedAngle2), Time.deltaTime * 5);
                }
            }
        }
        else
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                if (i == 0 || i == 3)
                {
                    if (i == 0)
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, selectedAngle1), Time.deltaTime * 5);
                    else
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, selectedAngle2), Time.deltaTime * 5);
                }
                else
                {
                    if (i == 1)
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, -selectedAngle1), Time.deltaTime * 5);
                    else
                        transforms[i].localRotation = Quaternion.Lerp(transforms[i].localRotation, Quaternion.Euler(0, 0, -selectedAngle2), Time.deltaTime * 5);
                }
            }
        }
    }

    void RandomAngle()
    {
        selectedAngle1 = angles[Random.Range(0, angles.Length)];
        selectedAngle2 = angles[Random.Range(0, angles.Length)];
        same = Random.Range(0, 2);
    }
}