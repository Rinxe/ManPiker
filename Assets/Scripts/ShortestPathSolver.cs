
using System;
using System.Collections.Generic;
using System.Linq;

public class ShortestPathFinder
{
    private static readonly int[] dr = { 1, -1, 0, 0 };
    private static readonly int[] dc = { 0, 0, 1, -1 };
    private static readonly string[] directions = { "Down", "Up", "Left", "Right" };

    public static (List<string> path, int steps) FindShortestPath(bool[][] matrix, int m1, int n1, int m2, int n2)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int[][] distance = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            distance[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                distance[i][j] = int.MaxValue;
            }
        }

        distance[m1][n1] = 0;

        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((m1, n1));

        while (queue.Count > 0)
        {
            (int r, int c) = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int newR = r + dr[i];
                int newC = c + dc[i];

                if (newR >= 0 && newR < rows && newC >= 0 && newC < cols && matrix[newR][newC])
                {
                    int newDistance = distance[r][c] + 1;

                    if (newDistance < distance[newR][newC])
                    {
                        distance[newR][newC] = newDistance;
                        queue.Enqueue((newR, newC));
                    }
                }
            }
        }

        if (distance[m2][n2] == int.MaxValue)
        {
            return (new List<string>(), -1);
        }

        List<string> path = ReconstructPath(distance, m1, n1, m2, n2);

        return (path, distance[m2][n2]);
    }

    private static List<string> ReconstructPath(int[][] distance, int m1, int n1, int m2, int n2)
    {
        List<string> path = new List<string>();
        int r = m2;
        int c = n2;

        while (r != m1 || c != n1)
        {
            for (int i = 0; i < 4; i++)
            {
                int newR = r + dr[i];
                int newC = c + dc[i];

                if (newR >= 0 && newR < distance.Length && newC >= 0 && newC < distance[0].Length)
                {
                    if (distance[newR][newC] == distance[r][c] - 1)
                    {
                        path.Insert(0, directions[i]);
                        r = newR;
                        c = newC;
                        break;
                    }
                }
            }
        }

        return path;
    }

   
}