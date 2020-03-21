using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameDevTV.Saving;
using VZT.Core;

public class PointToUse : MonoBehaviour, ISaveable
{
    [SerializeField] Core core = null;
    [SerializeField] InputField fix = null;
    [SerializeField] Point pointPrefab = null;
    [SerializeField] Transform pointsParent = null;

    [System.Serializable]
    class PointData
    {
        public string pointName = string.Empty;
        public float pointValue = 0f;
    }

    public void UseSavedPoint(Text pointValueText)
    {
        fix.text = pointValueText.text;
        core.Recalculate();
        FindObjectOfType<SavedPointsOpener>().CloseSavePointsCanvas();
    }

    public void AddNewPoint()
    {
        Point newPoint = Instantiate(pointPrefab, pointsParent);
        newPoint.EditPoint();
    }

    public object CaptureState()
    {
        Point[] points = GetComponentsInChildren<Point>();

        List<PointData> pointsDir = new List<PointData>();
        foreach (Point point in points)
        {
            PointData pointData = new PointData();
            pointData.pointName = point.GetName();
            pointData.pointValue = point.GetValue();

            pointsDir.Add(pointData);
        }

        return pointsDir;
    }

    public void RestoreState(object state)
    {
        Point[] oldPoints = GetComponentsInChildren<Point>();
        foreach(Point point in oldPoints)
        {
            Destroy(point.gameObject);
        }

        List<PointData> points = (List<PointData>)state;
        foreach (PointData point in points)
        {
            Point loadedPoint = Instantiate(pointPrefab, pointsParent);

            loadedPoint.SetName(point.pointName);
            loadedPoint.SetValue(point.pointValue);
        }
    }
}
