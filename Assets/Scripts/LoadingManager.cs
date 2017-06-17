using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Sprite[] loadingImages;

    public Image TitleImage;
    public Image loadingBar;
    public Text percent;
    public AsyncOperation loading;

    void Start()
    {
        TitleImage.sprite = loadingImages[Random.Range(0, loadingImages.Length)];
        loadingBar.fillAmount = 0;
        DontDestroyOnLoad(gameObject);
        StartCoroutine("Loading");
    }

    IEnumerator Loading()
    {
        loading = SceneManager.LoadSceneAsync(DataManager.Instance.nextSceneName);

        while (!loading.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }

    void Update()
    {
        loadingBar.fillAmount = loading.progress;
        int temp = (int)(loading.progress * 100.0f);
        percent.text = temp.ToString() + "%";
    }
}
