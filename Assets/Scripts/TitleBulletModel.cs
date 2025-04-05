using UnityEngine;

public class TitleBulletModel : MonoBehaviour
{
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
        Destroy(gameObject);
    }
}
