using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public Transform startPosition;
    private GameObject currentBall;
    private Rigidbody rb;
    private bool isThrown = false;

    public float throwForce = 10f;
    public float upwardForce = 5f;
    public float respawnHeight = -5f;

    public Trajectory trajectory;

    private int currentBallIndex = 0;

    void Start()
    {
        currentBallIndex = PlayerPrefs.GetInt("SelectedBall", 0);
        SpawnBall();
    }

    void Update()
    {
        if (!isThrown && Input.GetMouseButtonDown(0))
        {
            ThrowBall();
        }

        if (!isThrown)
        {
            trajectory.MoveTrajectory();
            trajectory.DrawTrajectory(startPosition.position);
        }

        if (isThrown && currentBall.transform.position.y < respawnHeight)
        {
            ResetBall();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeBall(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeBall(1);
        }
    }

    void ThrowBall()
    {
        isThrown = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        Vector3 throwDirection = trajectory.GetThrowDirection();
        rb.velocity = throwDirection * throwForce + Vector3.up * upwardForce;

        if (trajectory != null)
        {
            trajectory.HideTrajectory();
        }
    }

    void ResetBall()
    {
        Destroy(currentBall);
        SpawnBall();
    }

    void SpawnBall()
    {
        currentBall = Instantiate(ballPrefabs[currentBallIndex], startPosition.position, Quaternion.identity);
        rb = currentBall.GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
        isThrown = false;

        trajectory.ShowTrajectory();
        trajectory.DrawTrajectory(startPosition.position);
    }

    public void ChangeBall(int direction)
    {
        currentBallIndex += direction;

        if (currentBallIndex < 0)
        {
            currentBallIndex = ballPrefabs.Length - 1;
        }
        else if (currentBallIndex >= ballPrefabs.Length)
        {
            currentBallIndex = 0;
        }

        PlayerPrefs.SetInt("SelectedBall", currentBallIndex);
        PlayerPrefs.Save();

        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        SpawnBall();
    }
}








