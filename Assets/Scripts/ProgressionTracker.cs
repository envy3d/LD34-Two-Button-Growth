using UnityEngine;
using System.Collections.Generic;

public class ProgressionTracker : MonoBehaviour
{
    public BezierSpline trackSpline;
    public int numberOfLaps = 3;
    public VehicleProgressionInfo[] progressInfo;

    private List<float> totalProgressRank = new List<float>();

    void Start()
    {
        if (progressInfo != null && trackSpline != null)
        {
            foreach (VehicleProgressionInfo pt in progressInfo)
            {
                pt.transform = pt.vc.transform;
                pt.currentLap = 0;
                pt.rank = 1;
                pt.UpdateProgress(0);
            }
        }
	}
	
	void Update()
    {
        if (progressInfo != null && trackSpline != null)
        {
            totalProgressRank.Clear();
            foreach (VehicleProgressionInfo pt in progressInfo)
            {
                float prevProgress = pt.progress;
                float progress = GetSplineDistanceToNearestPoint(pt.transform.position, pt.progress, 10, 0.01f, 3);
                if (prevProgress < 0.1 && progress > 0.3)
                {
                    progress = 0;
                }
                else if (prevProgress > 0.9 && progress < 0.1)
                {
                    pt.currentLap += 1;
                }
                pt.UpdateProgress(progress);

                totalProgressRank.Add(pt.totalProgress);
            }

            totalProgressRank.Sort();
            totalProgressRank.Reverse();

            foreach (VehicleProgressionInfo pt in progressInfo)
            {
                for (int i = 0; i < totalProgressRank.Count; i++)
                {
                    print(totalProgressRank[i]);
                    if (pt.totalProgress == totalProgressRank[i])
                    {
                        pt.rank = i + 1;
                        break;
                    }
                }
                if (pt.currentLap == 3)
                {
                    // End game

                    foreach (VehicleProgressionInfo pj in progressInfo)
                    {
                        pj.vc.EndCar();
                    }
                }
            }
        }
    }

    public float GetSplineDistanceToNearestPoint(Vector3 point, float t, int numberOfSamples,
                                                 float sampleSpacing, int recursionDepth)
    {
        if (recursionDepth == 0)
        {
            return t;
        }

        float startSample = t - (numberOfSamples / 2.0f) * sampleSpacing;
        startSample = startSample < 0 ? 1 + startSample : startSample;

        float smallestSampleDistance = float.MaxValue;
        float bestSample = 0;

        float nextSample;
        for (int i = 0; i < numberOfSamples; i++)
        {
            nextSample = startSample + (i * sampleSpacing);
            nextSample = nextSample > 1 ? nextSample - 1 : nextSample;
            float distance = (trackSpline.GetPoint(nextSample) - point).sqrMagnitude;
            if (distance < smallestSampleDistance)
            {
                smallestSampleDistance = distance;
                bestSample = nextSample;
            }
        }
        return GetSplineDistanceToNearestPoint(point,
                                               bestSample,
                                               numberOfSamples,
                                               sampleSpacing / numberOfSamples,
                                               recursionDepth - 1);
    }
}
