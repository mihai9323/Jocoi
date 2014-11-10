using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    
    public int xSize;
    public int ySize;
    public float density;
    public float sphereSize = 1.0f;
    Node[,] map;

   
   
    public GameObject debug;
    private void Awake()
    {
        
    }
   
    private void BuildMap()
    {
        map  = new Node[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {


                GameObject go = Instantiate(debug, transform.position + new Vector3(x - (float)xSize / 2, transform.position.y, y - (float)ySize / 2) / density, Quaternion.identity) as GameObject;
                //go.transform.parent = transform;
                go.name = "Tile "+transform.name;
                map[x, y] = new Node(transform.position + new Vector3(x - (float)xSize / 2, 0, y - (float)ySize / 2) / density, x, y, go.collider);
            }
        }
    
    }
    public void RemoveMap()
    {
       if(map!=null){
            //map = new Node[xSize, ySize];
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {


                    Destroy(map[x, y].col.gameObject);
                    //map[x, y] = new Node(transform.position + new Vector3(x - (float)xSize / 2, 0, y - (float)ySize / 2) / density, x, y, go.collider);
                }
            }
        }
        map = null;
    }
    public IEnumerator CalculatePath(Vector3 s, Vector3 f, GameData.VOID_FUNCITON_PATH complete)
    {
        yield return new WaitForSeconds(Random.value*0.2f);
        float myId = Time.time;
        
        int c = 0;
        BuildMap();
        c = ySize * xSize;
       
        List<Node> openList, closedList;
        openList = new List<Node>();
        closedList = new List<Node>();
        
        //Node start = map[(int)(transform.position.x - s.x + (float)xSize/2),(int)(transform.position.z - s.z + (float)ySize/2)];
        //Node end = map[(int)(transform.position.x - f.x + (float)xSize/2),(int)(transform.position.z - f.z + (float)ySize/2)];
        Node start = findClosestNode(s);
        Node end = findClosestNode(f);
        start.gCost = 0;
        start.parentNode = null;
        start.hCost = HCost(start, end);
        start.fCost = start.gCost + start.hCost;
        
        openList.Add(start);
        while (openList.Count > 0 && !closedList.Contains(end))
        {
            if (c > 400)
            {
                yield return new WaitForFixedUpdate();
                c -= 400;
            }
            Node currentNode = BestNode(openList.ToArray());
            int startX=-1, startY=-1, endX=2, endY=2;
            if (currentNode.x == 0)
            {
                startX = 0;
            }
            if (currentNode.y == 0)
            {
                startY = 0;
            }
            if (currentNode.x == xSize -1)
            {
                endX = 1;
            }
            if (currentNode.y == ySize - 1)
            {
                endY = 1;
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        RaycastHit hit;
                        
                        float cost = 1 / density;
                        if (x != 0 && y != 0) cost *= 1.42f;
                        Node nNode = map[currentNode.x + x, currentNode.y + y];
                        bool ok = true;
                        if (Physics.SphereCast(currentNode.position, sphereSize, nNode.position - currentNode.position, out hit))
                        {
                            if (hit.collider != nNode.col)
                            {
                                if (!hit.collider.isTrigger)   ok = false;
                            }

                        }
                        if (ok)
                        {
                            Debug.DrawRay(currentNode.position, (nNode.position-currentNode.position).normalized*cost, Color.green, 10.0f);
                        }
                        else
                        {
                            Debug.DrawRay(currentNode.position, (nNode.position - currentNode.position).normalized * cost, Color.gray, 10.0f);
                        }
                        if (ok)
                        {
                            if (openList.Contains(nNode))
                            {
                                float costG = GCost(nNode, cost);
                                if (costG < nNode.gCost)
                                {
                                    nNode.gCost = costG;
                                    nNode.fCost = nNode.gCost + nNode.hCost;
                                    nNode.parentNode = currentNode;
                                }
                            }
                            else if (closedList.Contains(nNode))
                            {
                                // do nothing
                            }
                            else
                            {
                                nNode.parentNode = currentNode;
                                nNode.gCost = GCost(nNode, cost);
                                nNode.hCost = HCost(nNode, end);
                                nNode.fCost = nNode.gCost + nNode.fCost;
                                openList.Add(nNode);
                            }
                        }
                        c += 9;
                    }
                }
            }
        }
        Node iterateNode = end;
        List<Vector3> path = new List<Vector3>();
        while (iterateNode != null)
        {
            path.Add(iterateNode.position);
            iterateNode = iterateNode.parentNode;
            
        }
        Vector3[] returnPath = new Vector3[path.Count+1];
        for (int i = 0; i < path.Count; i++)
        {
            returnPath[i] = path[path.Count - i - 1];
        }
        returnPath[path.Count] = f;
        for (int i = 1; i < path.Count; i++)
        {
                Debug.DrawRay(path[i - 1], (path[i] - path[i - 1]).normalized * Vector3.Distance(path[i], path[i - 1]), Color.red, 10);
        }
        openList = null;
        closedList = null;
        RemoveMap();
        
      
        complete(returnPath);
    }

    private float HCost(Node thisNode, Node finalNode)
    {
        float xDist = Mathf.Abs(thisNode.position.x - finalNode.position.x);
        float yDist = Mathf.Abs(thisNode.position.y - finalNode.position.y);

        if (xDist > yDist)
        {
            return (xDist - yDist) * 1.42f + yDist;
        }
        else return (yDist - xDist) * 1.42f + xDist;
    }
    private float GCost(Node thisNode,float distance)
    {
        return thisNode.parentNode.gCost + distance;
    }
    private Node BestNode(Node[] nodeList)
    {
        float minF = float.MaxValue;
        Node bestNode = nodeList[0]; 
        foreach (Node n in nodeList)
        {
            if (n.fCost < minF)
            {
                bestNode = n;
                minF = n.fCost;
            }
        }
        return bestNode;
    }


    private Node findClosestNode(Vector3 position)
    {
        float minX, minZ, maxX, maxZ;
        minX = map[0, 0].position.x;
        minZ = map[0, 0].position.z;
        maxX = map[xSize - 1, ySize - 1].position.x;
        maxZ = map[xSize - 1, ySize - 1].position.z;

        int targetX, targetZ;

        
        
        
        
        
        
        
        if (position.x < minX) targetX = 0;
        else  if (position.x > maxX) targetX = xSize - 1;
        else
        {
            targetX = (int) ((position.x - map[0, 0].position.x) / ((map[xSize - 1, 0].position.x - map[0, 0].position.x) / xSize));
        }
        if (position.z < minZ) targetZ = 0;
        else if (position.z > maxZ) targetZ = ySize - 1;
        else
        {
            targetZ = (int)((position.z - map[0, 0].position.z) / ((map[0, ySize - 1].position.z - map[0, 0].position.z) / ySize));
        }


        return map[targetX, targetZ];


    }
}
public class Node{
    public Vector3 position;
    public float gCost, fCost, hCost;
    public Node parentNode;
    public int x, y;

    public Collider col;

    public Node(Vector3 position,int x, int y, Collider collider)
    {
        this.position = position;
        gCost = hCost = fCost = 0;
        this.x = x;
        this.y = y;
        col = collider;
       
    }
   
    public Node(float x, float y, float z, int _x, int _y,Collider collider)
    {
        this.position = new Vector3(x, y, z);
        this.x = _x;
        this.y = _y;
        gCost = hCost = fCost = 0;
        col = collider;
    }
}
