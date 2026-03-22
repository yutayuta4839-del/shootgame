using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.InputSystem;
public class loadscnene : MonoBehaviour
{
    public Image fadeImage; // インスペクターでFadeImageをアタッチ
    public float fadeTime = 1.0f; // フェードにかかる秒数
    public void OnClickButton(string selectscene)
    {
        StartCoroutine(FadeAndLoad(selectscene));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator FadeAndLoad(string sceneName)
    {
        // 1. 画像を最前面に持ってくる（念のため）
        fadeImage.gameObject.SetActive(true);

        float elapsed = 0;
        Color color = fadeImage.color;

        // 2. 徐々に不透明（黒）にする
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeTime);
            fadeImage.color = color;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void ONClicktitleback(string title)
    {
        SceneManager.LoadScene(title);
    }

    public void ONClickgameplay(string gameplay)
    {
        SceneManager.LoadScene(gameplay);
    }

    public void ONClickboss2(string boss2scene)
    {
        SceneManager.LoadScene(boss2scene);
    }
}
