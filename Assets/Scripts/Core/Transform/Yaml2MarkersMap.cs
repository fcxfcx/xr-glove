using System.Collections.Generic;
using OpenCvSharp;

public static class Yaml2MarkersMap
{
    public static Dictionary<int, Point3f[]> ReadAndParse(string yamlPath)
    {
        var retDic = new Dictionary<int, Point3f[]>();
        var fs = new FileStorage(yamlPath, FileStorage.Mode.FormatYaml);
        var markersNode = fs["aruco_bc_markers"];
        foreach (var markerNode in markersNode)
        {
            var points = new Point3f[4];
            var id = markerNode["id"].ReadInt();
            for (var i = 0; i < 4; i++)
            {
                var tempPoint = markerNode["corners"][i].ReadPoint3d();
                points[i] = new Point3f((float)tempPoint.X, 
                    (float)tempPoint.Y, (float)tempPoint.Z);
            }
            retDic.Add(id, points);
        }

        return retDic;
    }
}