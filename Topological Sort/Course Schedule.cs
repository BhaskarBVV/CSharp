// There are a total of numCourses courses you have to take, labeled from 0 to numCourses - 1. 
// You are given an array prerequisites where prerequisites[i] = [ai, bi] indicates that you must take course bi first if you want to take course ai.

// For example, the pair [0, 1], indicates that to take course 0 you have to first take course 1.
// Return the ordering of courses you should take to finish all courses. 
// If there are many valid answers, return any of them. If it is impossible to finish all courses, return an empty array.

public class Solution
{
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var adj = Enumerable.Range(0, numCourses)
            .Select(x => new List<int>())
            .ToList();

        var inDegree = Enumerable.Range(0, numCourses).Select(x => 0).ToList();

        foreach (var node in prerequisites)
        {
            adj[node[1]].Add(node[0]);
            inDegree[node[0]]++;
        }

        var q = new Queue<int>();

        for (var i = 0; i < numCourses; i++)
            if (inDegree[i] == 0)
                q.Enqueue(i);

        var topologicalSort = new List<int>();

        while (q.Count != 0)
        {
            var temp = q.Dequeue();
            topologicalSort.Add(temp);
            foreach (var node in adj[temp])
            {
                inDegree[node] -= 1;
                if (inDegree[node] == 0)
                    q.Enqueue(node);
            }
        }

        return topologicalSort.Any(x => x > 0) ? Array.Empty<int>() : topologicalSort.ToArray();
    }
}
