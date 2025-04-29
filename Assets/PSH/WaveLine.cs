using System.Collections.Generic;
using UnityEngine;

public class WaveLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] points0;
    public Vector3[] points1;
    public List<Vector3[]> paths = new List<Vector3[]>();

    [Range(0, 5)][SerializeField] private int yPos = 0;
    private void Start()
    {
        // �� �׸� ����Ʈ ���� �̰� �ڵ� �ϳ��� ��ĥ���� ��
        // ���������� ���̺�����Ʈ 2���� 1�������� = 01, 2�������� = 23 ...

        points0 = new Vector3[]
        {
            new Vector3(0, yPos, -20),
            new Vector3(0, yPos, -40),
            new Vector3(20, yPos, -40),
            new Vector3(20, yPos, -20),
            new Vector3(20, yPos, 20),
            new Vector3(40, yPos, 20),
            new Vector3(40, yPos, -40)
        };
        points1 = new Vector3[]
        {
             new Vector3(0, yPos, 20),
             new Vector3(0, yPos, 40),
             new Vector3(40, yPos, 40),
             new Vector3(40, yPos, -40)
        };

        paths.Add(points0);
        paths.Add(points1);
    }
    public void DrawPath(int stagenum, int n)//n�� 1�Ǵ� 2
    {
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.red;

        lineRenderer.positionCount = paths[stagenum + n - 2].Length;
        lineRenderer.SetPositions(paths[stagenum + n - 2]);

    }
    public void HidePath()
    {
        lineRenderer.enabled = false;
    }
}
