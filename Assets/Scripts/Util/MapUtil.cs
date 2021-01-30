using System.Collections.Generic;
using OpenCvSharp;

public static class MapUtil
{
    /// <summary>
    /// read and parse from a yaml path
    /// </summary>
    /// <param name="yamlPath">yaml path</param>
    /// <returns>Map with key of marker id, and value of corner point set</returns>
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