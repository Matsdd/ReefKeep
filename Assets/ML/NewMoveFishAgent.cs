using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class NewMoveFishAgent : Agent
{
    [SerializeField] private FishControl fishControl;

    // Rewards and Penalties
    private readonly float borderPenalty = -0.4f;
    private readonly float proximityMultiplier = 0.1f;
    private readonly float horizontalMultiplier = 0.8f;

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
        transform.position = initialPositionFish + new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 10f), 0);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        // Show the total reward from the previous run
        Debug.Log("Episode ended. Total reward: " + cumulativeReward);
        cumulativeReward = 0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);

        likedObjectPosition = fishControl.GetLikedObjectPosition();
        dislikedObjectPosition = fishControl.GetDislikedObjectPosition();

        sensor.AddObservation(likedObjectPosition);
        sensor.AddObservation(dislikedObjectPosition);

        // Show what the fish is capable off
        sensor.AddObservation(fishControl.maxMoveSpeed);
        sensor.AddObservation(fishControl.minMoveSpeed);
        sensor.AddObservation(fishControl.turnSpeed);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // The inputs from the AI are now discrete actions
        int rotationAction = actions.DiscreteActions[0];
        int speedAction = actions.DiscreteActions[1];
        Debug.Log("rtyuiop: " + rotationAction);

        float rotationInput = 0f;
        float speedInput = 0f;

        // Define your actions based on discrete values
        switch (rotationAction)
        {
            case 0: rotationInput = -1f; break; // Turn left
            case 1: rotationInput = 0f; break;  // No turn
            case 2: rotationInput = 1f; break;  // Turn right
        }

        switch (speedAction)
        {
            case 0: speedInput = -1f; break; // Slow down
            case 1: speedInput = 0f; break;  // No change
            case 2: speedInput = 1f; break;  // Speed up
        }

        Debug.Log("Rotate: " + rotationInput + ", Speed: " + speedInput);

        // Move the fish using the FishControl script
        fishControl.Move(rotationInput, speedInput);

        CalcReward();
    }

    // Calculate all the rewards & penalties
    private void CalcReward()
    {
        // Calculate the distance to the like
        float distanceToLike = Vector3.Distance(fishControl.transform.position, likedObjectPosition);
        float likeReward = DistanceReward(distanceToLike, 20f);
        Debug.Log(gameObject.name + " Distance Reward: " + likeReward);
        AddReward(likeReward);
        cumulativeReward += likeReward;

        // Calculate the distance to the dislike
        float distanceToDislike = Vector3.Distance(fishControl.transform.position, dislikedObjectPosition);
        float dislikePunishment = -DistanceReward(distanceToDislike, 20f);
        Debug.Log(gameObject.name + " Distance Punishment: " + dislikePunishment);
        AddReward(dislikePunishment);
        cumulativeReward += dislikePunishment;

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

    float DistanceReward(float distance, float scaleFactor)
    {
        return Mathf.Exp(-distance / scaleFactor) * proximityMultiplier;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        // Get input from the player
        float rotationInput = Input.GetAxisRaw("Horizontal");
        float speedInput = Input.GetAxisRaw("Vertical");

        // Convert continuous input to discrete actions
        discreteActionsOut[0] = rotationInput < 0 ? 0 : (rotationInput > 0 ? 2 : 1); // Left, None, Right
        discreteActionsOut[1] = speedInput < 0 ? 0 : (speedInput > 0 ? 2 : 1); // Slow, None, Fast
    }
}
