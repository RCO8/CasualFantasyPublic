![image](https://github.com/user-attachments/assets/bcfdee22-250c-463e-be22-359afe4d179f)

로딩화면 없이 필드에서 배틀 씬으로 넘어가니 매우 부자연스러운 부분이 있어 로딩화면을 아래처럼 제작하게 되었다.

```cs
private void FadeScreen(string _scene)
{
    _image.gameObject.SetActive(true);

    _image.DOFade(1, 0.5f).OnComplete(
        () => {
            StartCoroutine("LoadScene", _scene);
        });
}

IEnumerator LoadScene(string _scene)
{
    _percentText.gameObject.SetActive(true);
    _progressSlider.gameObject.SetActive(true);

    AsyncOperation async = SceneManager.LoadSceneAsync(_scene);
    async.allowSceneActivation = false;

    float _time = 0.0f;
    float _per = 0.0f;

    while (!(async.isDone))
    {
        yield return null;
        _time += Time.deltaTime;

        if(_per >= 90.0f)
        {
            _per = Mathf.Lerp(_per, 100.0f, _time);

            if (_per >= 100.0f)
            {
                async.allowSceneActivation = true;
            }
        }
        else
        {
            _per = Mathf.Lerp(_per, async.progress * 100f, _time);
            if (_per >= 90.0f) _time = 0.0f;
        }

        _progressSlider.value = _per * 0.01f;
        _percentText.text = _per.ToString("0") + "%"; //로딩 퍼센트 표기
    }
}
```

-설명-

Dotween을 이용해 화면을 페이드 아웃하고, 페이드 아웃이 시작되면 LoadSceneAsync함수를 이용해 비동기 에셋 로딩을 실행한다.<br>
로딩 바의 자연스러운 움직임을 위해서 Lerp을 활용했다.
