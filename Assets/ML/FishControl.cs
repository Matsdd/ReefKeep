using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FishControl : MonoBehaviour
{
    // Settings that are changeable for each fish
    public float minMoveSpeed = 1f;
    public float maxMoveSpeed = 4.0f;
    public float turnSpeed = 100f;

    private float currentSpeed = 1f;

    // Map bounds
    private Vector2 minBounds = new(-28f, -15f);
    private Vector2 maxBounds = new(28f, 15f);

    private SpriteRenderer spriteRenderer;

    // Fish preferences
    private List<FishPreferences> preferencesList;
    private List<GameObject> likedObjects = new();
    private List<GameObject> dislikedObjects = new();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        preferencesList = LoadPreferences();

        Debug.Log("Name: " + gameObject.name);

        FishPreferences fishPreferences = GetFishPreferences(gameObject.name);
        if (fishPreferences != null)
        {
            likedObjects = FindObjectsByPartialName(fishPreferences.likes);
            dislikedObjects = FindObjectsByPartialName(fishPreferences.dislikes);
        }

        Vector2 fishSize = spriteRenderer.sprite.rect.size / spriteRenderer.sprite.pixelsPerUnit;
        Vector3 fishScale = transform.localScale;

        // Adjust the map bounds based on the size and scale of this fish sprite
        minBounds = new Vector2(-28f + fishSize.x * fishScale.x / 2f, -15f + fishSize.x * fishScale.x / 2f);
        maxBounds = new Vector2(28f - fishSize.x * fishScale.x / 2f, 15f - fishSize.x * fishScale.x / 2f);
    }

    private List<GameObject> FindObjectsByPartialName(string names)
    {
        List<GameObject> foundObjects = new();

        foreach (var name in names.Split(','))
        {
            GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            GameObject obj = objects.FirstOrDefault(go => go.name.Contains(name.Trim()));
            if (obj != null)
            {
                foundObjects.Add(obj);
            }
        }

        return foundObjects;
    }

    private List<FishPreferences> LoadPreferences()
    {
        string filePath = Path.Combine(Application.dataPath + "/ML/EcoPrefs.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<FishPreferencesList>(json).fishPreferences;
        }
        else
        {
            Debug.LogError("Fish preferences file not found");
            return new List<FishPreferences>();
        }
    }

    private FishPreferences GetFishPreferences(string fishName)
    {
        return preferencesList.FirstOrDefault(fp => fp.name.Equals(fishName, StringComparison.OrdinalIgnoreCase));
    }

    public void Move(float rotationInput, float speedInput)
    {
        float rotation = Mathf.Clamp(rotationInput, -1f, 1f);

        // Change speed depening on positive or negative number
        if (speedInput >= 0f)
        {
            // Smoothly increase speed towards maxMoveSpeed for positive values
            currentSpeed = Mathf.Lerp(currentSpeed, maxMoveSpeed, Mathf.Abs(speedInput) * Time.deltaTime * 2f);
        }
        else
        {
            // Smoothly decrease speed towards minMoveSpeed for negative values
            currentSpeed = Mathf.Lerp(currentSpeed, minMoveSpeed, Mathf.Abs(speedInput) * Time.deltaTime * 2f);
        }

        // Apply smooth rotation
        transform.Rotate(Vector3.forward, rotation * turnSpeed * Time.deltaTime);

        Debug.Log("CurrentSpeed:" + currentSpeed);

        // Move forward
        transform.position += transform.right * currentSpeed * Time.deltaTime;

        // Clamp position within bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z);

        // Adjust sprite orientation to always appear upright
        AdjustSpriteOrientation();
    }


    // Adjust the rotation of the sprite so the fish always looks upright
    private void AdjustSpriteOrientation()
    {
        float zRotation = transform.eulerAngles.z;
        if (zRotation > 90f && zRotation < 270f)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }

    // Check if the fish is swimming against the border of the map
    public bool IsOutOfBounds()
    {
        return transform.position.x >= maxBounds.x || transform.position.x <= minBounds.x ||
               transform.position.y >= maxBounds.y || transform.position.y <= minBounds.y;
    }

    public Vector3 GetLikedObjectPosition()
    {
        Vector3 pos = new(100, 100, 0);

        if (likedObjects.Count > 0)
        {
            pos = likedObjects[0].gameObject.transform.position;
        }
        Debug.Log("Like Pos: " + pos);

        return pos;
    }

    public Vector3 GetDislikedObjectPosition()
    {
        Vector3 pos = new(100, 100, 0);

        if (dislikedObjects.Count > 0)
        {
            pos = dislikedObjects[0].gameObject.transform.position;
        }
        Debug.Log("Dislike Pos: " + pos);

        return pos;
    }
}

[Serializable]
public class FishPreferences
{
    public string name;
    public string likes;
    public string dislikes;
}

[Serializable]
public class FishPreferencesList
{
    public List<FishPreferences> fishPreferences;
}
