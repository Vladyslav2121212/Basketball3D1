using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public SphereCollider scoreZoneCollider; 
    public float scoreZoneIncrease = 0.2f;
    public float bonusDestroyDelay = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            FindObjectOfType<ScoreManager>().AddScore();
        }
        else if (other.CompareTag("Bonus"))
        {
            IncreaseScoreZone();
            Destroy(other.gameObject, bonusDestroyDelay);
        }
    }

    void IncreaseScoreZone()
    {
        if (scoreZoneCollider != null)
        {
            scoreZoneCollider.radius += scoreZoneIncrease; 
        }
    }
}
