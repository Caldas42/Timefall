using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    public Animator animLogo;
    public string proximaCena = "TitleScreen";

    void Start()
    {
        StartCoroutine(RodarSequencia());
    }

    IEnumerator RodarSequencia()
    {

        yield return new WaitForSeconds(4f);

    yield return new WaitUntil(() => animLogo.GetCurrentAnimatorStateInfo(0).IsName("introforja"));
    yield return new WaitUntil(() => animLogo.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        SceneManager.LoadScene(proximaCena);
    }
}

