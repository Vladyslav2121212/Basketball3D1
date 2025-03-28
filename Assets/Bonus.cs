using UnityEngine;

public class Bonus : MonoBehaviour
{
    public float scoreZoneIncrease = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            IncreaseScoreZone();
            Destroy(gameObject, 0.2f); 
        }
    }

    void IncreaseScoreZone()
    {
        GameObject scoreZone = GameObject.Find("ScoreZone");
        if (scoreZone != null)
        {
            Vector3 newScale = scoreZone.transform.localScale;
            newScale.x += scoreZoneIncrease;
            newScale.z += scoreZoneIncrease;
            scoreZone.transform.localScale = newScale;
        }
    }
}
