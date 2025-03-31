using UnityEngine;

public class PingPongAnimation : MonoBehaviour
{
    public Animator animator;
    public string animationStateName; // Name of the animation state in the Animator
    public float animationDuration = 1f; // Duration of the animation

    private bool playForward = true;
    private float playbackTime = 0f;

    private void Update()
    {
        // Update playback time
        if (playForward)
            playbackTime += Time.deltaTime / animationDuration;
        else
            playbackTime -= Time.deltaTime / animationDuration;

        // Clamp playback time between 0 and 1
        playbackTime = Mathf.Clamp01(playbackTime);

        // Play the animation at the specified playback time
        animator.Play(animationStateName, 0, playbackTime);

        // Reverse direction when reaching the end points
        if (playbackTime <= 0f || playbackTime >= 1f)
        {
            playForward = !playForward;
        }
    }
}
