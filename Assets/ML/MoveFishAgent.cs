using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MoveFishAgent : Agent
{
    [SerializeField] private FishControl fishControl;

    // Rewards and Penalties
    private readonly float borderPenalty = -0.1f;
    private readonly float proximityReward = 0.1f;
    private readonly float horizontalMultiplier = 0.1f;

    private float cumulativeReward = 0f;
    private Vector3 initialPositionFish;
    private Vector3 likedObjectPosition;
    private Vector3 dislikedObjectPosition;

    public override void Initialize()
    {
        Time.timeScale = 1;
        initialPositionFish = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        // Reset positions with random values
        // transform.position = initialPositionFish + new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 10f), 0);
        // transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        // Show the total reward from the previous run
        Debug.Log("Episode ended. Total reward: " + cumulativeReward);
        cumulativeReward = 0f;
    }

    // Give the AI info about the environment
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);

        likedObjectPosition = fishControl.GetLikedObjectPosition();
        dislikedObjectPosition = fishControl.GetDislikedObjectPosition();

        sensor.AddObservation(likedObjectPosition);
        sensor.AddObservation(dislikedObjectPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // The inputs from the AI
        float rotationInput = actions.ContinuousActions[0];
        float speedInput = actions.ContinuousActions[1];
        // Debug.Log("AI Rotation:" + rotationInput);
        // Debug.Log("AI Speed:" + speedInput);

        // Move the fish using the FishControl script
        fishControl.Move(rotationInput, speedInput);

        CalcReward();
    }

    // Calculate all the rewards & penalties
    private void CalcReward()
    {
        // Calculate the distance to the like
        float distanceToLike = Vector3.Distance(fishControl.transform.position, likedObjectPosition);
        // Add a small reward based on the distance to the like
        Debug.Log("Distance Reward:" + proximityReward / distanceToLike);
        AddReward(proximityReward / distanceToLike);
        cumulativeReward += proximityReward / distanceToLike;

        // Calculate the distance to the dislike
        float distanceToDislike = Vector3.Distance(fishControl.transform.position, likedObjectPosition);
        // Add a small punishment based on the distance to the dislike
        Debug.Log("Distance Punishment:" + proximityReward / -distanceToDislike);
        AddReward(proximityReward / -distanceToDislike);
        cumulativeReward += proximityReward / -distanceToDislike;

        // Add reward for maintaining a horizontal orientation
        float rotation = Mathf.Abs(Mathf.Cos(fishControl.transform.eulerAngles.z * Mathf.Deg2Rad)); // 1:horizontal 0:vertical
        float horizontalReward = (rotation - 0.5f) * horizontalMultiplier;
        Debug.Log("Rotation Reward: " + horizontalReward);
        AddReward(horizontalReward);
        cumulativeReward += horizontalReward;


        // Check if the fish is out of bounds and apply penalty
        if (fishControl.IsOutOfBounds())
        {
            Debug.Log("Border Penalty:" + borderPenalty);
            AddReward(borderPenalty);
            cumulativeReward += borderPenalty;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        // Get input from the player
        float rotationInput = Input.GetAxisRaw("Horizontal");
        float speedInput = Input.GetAxisRaw("Vertical");

        // Clamp the input values
        float clampedRotation = Mathf.Clamp(-rotationInput, -1f, 1f);
        float clampedSpeed = Mathf.Clamp(speedInput, -1f, 1f);

        // Assign the actions
        continuousActions[0] = clampedRotation; // Rotation control
        continuousActions[1] = clampedSpeed; // Speed control
    }
}
