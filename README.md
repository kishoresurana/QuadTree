# QuadTree
This project is simply a C# implementation where, there aer no more than 10 items in a single Quad tree.

## Explanation of Quad tree (from reference):
Quadtrees are a very straightforward spatial indexing technique. In a Quadtree, each node represents a bounding box covering some part of the space being indexed, with the root node covering the entire area. Each node is either a leaf node - in which case it contains one or more indexed points, and no children, or it is an internal node, in which case it has exactly four children, one for each quadrant obtained by dividing the area covered in half along both axes - hence the name.

Inserting data into a quadtree is simple: Starting at the root, determine which quadrant your point occupies. Recurse to that node and repeat, until you find a leaf node. Then, add your point to that node's list of points. If the list exceeds some pre-determined maximum number of elements, split the node, and move the points into the correct subnodes.

To query a quadtree, starting at the root, examine each child node, and check if it intersects the area being queried for. If it does, recurse into that child node. Whenever you encounter a leaf node, examine each entry to see if it intersects with the query area, and return it if it does.

Note that a quadtree is very regular - it is, in fact, a trie, since the values of the tree nodes do not depend on the data being inserted. A consequence of this is that we can uniquely number our nodes in a straightforward manner: Simply number each quadrant in binary (00 for the top left, 10 for the top right, and so forth), and the number for a node is the concatenation of the quadrant numbers for each of its ancestors, starting at the root. Using this system, the bottom right node in the sample image would be numbered 11 01.

**References**:
* http://blog.notdot.net/2009/11/Damn-Cool-Algorithms-Spatial-indexing-with-Quadtrees-and-Hilbert-Curves
* https://www.geeksforgeeks.org/quad-tree/

## Code architectecture
The key classes and methods for this implementation are:
* Quad class (quad_tree.cs)
  - Insert - method to insert items to a quad tree
  - Search - method to search if an item exists in quad tree
* Program class (Program.cs)
  - Main - driver method to test quad tree

# Output from the console app
    x:36 y:37 found at 01
    x:50 y:33 found at 01
    x:77 y:6 found at 11
    x:0 y:16 found at 01
    x:37 y:74 found at 00
    x:5 y:43 found at 01
    x:3 y:55 found at 00
    x:2 y:91 found at 00
    x:33 y:51 found at 00
    x:40 y:52 found at 00
    x:89 y:72 found at 10
    x:6 y:40 found at 01
    x:63 y:36 found at 11
    x:61 y:17 found at 11
    x:95 y:38 found at 11
    x:11 y:64 found at 00
    x:82 y:9 found at 11
    x:18 y:59 found at 00
    x:66 y:48 found at 11
    x:94 y:51 found at 10
    x:65 y:94 found at 10
