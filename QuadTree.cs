using System;
using System.Collections.Generic;
public class Point{
    public double x;
    public double y;
    public Point (double _x, double _y){
        x = _x;
        y = _y;
    }
}

public class Node{
    public Point pos;
    public int data;
    public Node(Point _pos, int _data){
        pos = _pos;
        data = _data;
    }
}

public class Quad{
    // Hold details of the boundary of this node
    Point topLeft;
    Point botRight;

    // Contains set of nodes. Do not exceed MAX_NODE_CNT nodes
    List<Node> nodes;
    private readonly int MAX_NODE_CNT;

    // Children of this tree
    Quad topLeftTree;
    Quad topRightTree;
    Quad botLeftTree;
    Quad botRightTree;

    public Quad(Point _topLeft, Point _botRight, int _max_node_cnt){
        topLeft = _topLeft;
        botRight = _botRight;
        nodes = new List<Node>();   
        MAX_NODE_CNT = _max_node_cnt;
    }

    // Insert a node into the quad tree
    public void Insert(Node node, bool split = false){
        if (node == null)
            return;
        
        if (!inBoundary(node.pos))
            return;
        
        // if any of the child trees are already initialized
        // recurse to the correct quadtree
        if (topLeftTree != null || topRightTree != null || botLeftTree != null || botRightTree != null || split == true){
            if ((topLeft.x + botRight.x)/2 >= node.pos.x){
                // indicates botLeftTree
                if ((topLeft.y + botRight.y)/2 >= node.pos.y){
                    if (botLeftTree == null){
                        botLeftTree = new Quad(new Point(topLeft.x, (topLeft.y + botRight.y)/2)
                                        , new Point((topLeft.x + botRight.x)/2, botRight.y)
                                        , MAX_NODE_CNT);
                    }
                    botLeftTree.Insert(node);
                }
                // indicates topLeftTree
                else {
                    if (topLeftTree == null){
                        topLeftTree = new Quad(new Point(topLeft.x, topLeft.y)
                                        , new Point((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2)
                                        , MAX_NODE_CNT); 
                    }
                    topLeftTree.Insert(node); 
                }
            }
            else{
                // Indicates botRightTree
                if ((topLeft.y + botRight.y) / 2 >= node.pos.y) 
                {
                    if (botRightTree == null){
                        botRightTree = new Quad(new Point((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2)
                                        , new Point(botRight.x, botRight.y)
                                        , MAX_NODE_CNT); 
                    }
                    botRightTree.Insert(node);
                }        
                // Indicates topRightTree
                else
                { 
                    if (topRightTree == null) {
                        topRightTree = new Quad(new Point((topLeft.x + botRight.x) / 2, topLeft.y)
                                        , new Point(botRight.x, (topLeft.y + botRight.y) / 2)
                                        , MAX_NODE_CNT); 
                    }
                    topRightTree.Insert(node); 
                } 
            }
        }
        else if (nodes.Count == MAX_NODE_CNT){
            foreach (var chnode in nodes){
                Insert(chnode, true);
            }
            Insert(node, true);
            nodes.Clear();
        }
        else {
            // if this quad contains less than MAX_NODE_CNT nodes, insert this node
            nodes.Add(node);
            return;
        }
    }

    public bool Search(Point p, ref List<string> addr){
        if (inBoundary(p)){
            // if any of the child trees are already initialized
            // recurse to the child quadtree
            if (topLeftTree != null || topRightTree != null || botLeftTree != null || botRightTree != null){
                if ((topLeft.x + botRight.x)/2 >= p.x){
                    // indicates botLeftTree
                    if ((topLeft.y + botRight.y)/2 >= p.y){
                        addr.Add("01");
                        return botLeftTree.Search(p, ref addr);
                    }
                    // indicates topLeftTree
                    else {
                        addr.Add("00");
                        return topLeftTree.Search(p, ref addr);
                    }
                }
                else{
                    // Indicates botRightTree
                    if ((topLeft.y + botRight.y) / 2 >= p.y) 
                    {
                        addr.Add("11");
                        return botRightTree.Search(p, ref addr);
                    }        
                    // Indicates topRightTree
                    else
                    { 
                        addr.Add("10");
                        return topRightTree.Search(p, ref addr);
                    } 
                }
            }
            else{
                foreach (var chnode in nodes){
                    if (chnode.pos.x == p.x && chnode.pos.y == p.y)
                        return true;
                }
            }
        }
        return false;
    }

    private bool inBoundary(Point p){
        return ((topLeft.x <= p.x && botRight.x >= p.x)
            && (topLeft.y >= p.y && botRight.y <= p.y));
    }
}
