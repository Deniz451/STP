using System.Collections;
using UnityEngine;

public class TitleSpiderModel : MonoBehaviour
{
    public Material dissolveMAT;
    private Animator anim;
    
    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, Continue);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, Continue);
    }

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Continue() {
        anim.SetTrigger("Continue");
    }

    public void Destroy() {
        StartCoroutine(ApplyDisolveShader());
    }

    private IEnumerator ApplyDisolveShader()
    {
        float dissolveProgress = 0f;
        transform.Find("body").GetComponent<Renderer>().material = dissolveMAT;

        while (dissolveProgress < 1f)
        {
            dissolveProgress += Time.deltaTime / 2;
            dissolveProgress = Mathf.Clamp01(dissolveProgress);
            dissolveMAT.SetFloat("_AlphaCliping", dissolveProgress);

            yield return null;
        }

        Destroy(gameObject);
    }
}
