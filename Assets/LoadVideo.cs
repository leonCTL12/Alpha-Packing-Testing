using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows;
using System.IO;

public class LoadVideo : MonoBehaviour
{
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    
    void Start()
    {
        // StartCoroutine(DownloadVideo());
        StreamVideo(); 
    }

    private IEnumerator DownloadVideo()
    {
        UnityWebRequest www =
            UnityWebRequest.Get(
                "https://wof-game-dev.gusto-labs.com/TestingAssetbundle/Comp_4_2.mp4");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Success!!!!");
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/file2.mp4", www.downloadHandler.data);
            PlayVideo(); 
        }
    }

    private async void WriteToFile(UnityWebRequest www)
    {
        await System.IO.File.WriteAllBytesAsync(Application.persistentDataPath + "file2.mp4", www.downloadHandler.data);
    }
    private void PlayVideo()
    {
        bool isOpening = _mediaPlayer.OpenMedia(new MediaPath("file2.mp4", MediaPathType.RelativeToPersistentDataFolder), autoPlay:true);
    }

    private void StreamVideo()
    {
        bool isOpening = _mediaPlayer.OpenMedia(new MediaPath("https://wof-game-dev.gusto-labs.com/TestingAssetbundle/Comp_4_2.mp4", MediaPathType.AbsolutePathOrURL), autoPlay:true);
    }

}
