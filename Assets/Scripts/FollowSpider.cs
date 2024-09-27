using UnityEngine;

public class FollowSpider : MonoBehaviour
{

    [SerializeField] private Transform spider;

    private void Update()
    {
        transform.position = new(spider.position.x, transform.position.y, spider.position.z + 10);
    }
}
