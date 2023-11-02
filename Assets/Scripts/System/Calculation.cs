using System;
using UnityEngine;

public static class Calculation
{
    public static int SecondsToFrames(int seconds)
    {
        return seconds * 60;
    }

    public static float SecondsToFrames(float seconds)
    {
        return seconds * 60f;
    }

    public static float PercentXOfY(float value, float max)
    {
        return value / max;
    }

    public static bool Chance(int percent)
    {
        int roll = UnityEngine.Random.Range(0, 100);
        return roll <= percent;
    }

    public static Vector3 RandomPointOnCircle(Vector3 basePosition, float radius)
    {
        // Generate a random angle within the range [0, 360) degrees
        float randomAngle = UnityEngine.Random.Range(0f, 360f);

        // Convert the random angle from degrees to radians
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        // Calculate the X and Z coordinates of the point on the circle
        float x = basePosition.x + radius * Mathf.Cos(angleInRadians);
        float z = basePosition.z + radius * Mathf.Sin(angleInRadians);

        // The Y coordinate remains the same to keep the point on the same height level in 3D space.

        // Create and return the Vector3 representing the random point on the circle
        return new Vector3(x, basePosition.y, z);
    }
}