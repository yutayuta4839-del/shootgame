using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
        void Start()
        {
            // WebGLビルドでのみ実行される
          #if !UNITY_EDITOR && UNITY_WEBGL
        // ブラウザの右クリックメニュー（コンテキストメニュー）を無効化するJavaScriptを実行
        Application.ExternalEval("window.oncontextmenu = function(e) { e.preventDefault(); return false; }");
        #endif
        }
    }
}

    // Update is called once per frame
  

