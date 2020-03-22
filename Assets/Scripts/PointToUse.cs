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
        FindObjectOfType<SavingSystem>().Save("save");
    }

    public object CaptureState()
    {
        Point[] points = GetComponentsInChildren<Point>();

        List<PointData> pointsDir = new List<PointData>();
        foreach (Point point in points)
        {
            PointData pointData = new PointData();
            pointData.pointName = point.pointName;
            pointData.pointValue = point.pointValue;

            pointsDir.Add(pointData);
        }

        return pointsDir;
    }

    public void RestoreState(object state)
    {
        DeleateCurrentPoints();

        List<PointData> points = (List<PointData>)state;
        foreach (PointData point in points)
        {
            Point loadedPoint = Instantiate(pointPrefab, pointsParent);

            loadedPoint.pointName = point.pointName;
            loadedPoint.pointValue = point.pointValue;
        }
    }

    private void DeleateCurrentPoints()
    {
        Point[] oldPoints = GetComponentsInChildren<Point>();
        foreach (Point point in oldPoints)
        {
            Destroy(point.gameObject);
        }
    }
}
