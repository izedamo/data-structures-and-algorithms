using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.DataStructures.Trees
{
    public class BTNode
    {
        public int Data { get; set; }

        public BTNode Right { get; set; }

        public BTNode Left { get; set; }

        public bool IsLeafNode() => Right == null && Left == null;
    }

    //Does not support duplicate nodes.
    public class BinarySearchTree
    {
        public BTNode Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        //Create a new node with passed value in tree acc. to BST criteria.
        public void Insert(int value)
        {
            var newNode = new BTNode
            {
                Data = value
            };

            //Set new node as root node if tree is empty.
            if (Root == null)
                Root = newNode;
            else
            {
                var node = Root;
                BTNode parent = null;

                //Find the new node's parent node.
                while (node != null)
                {
                    parent = node;
                    if (node.Data > newNode.Data)
                        node = node.Left;
                    else
                        node = node.Right;
                }

                //Insert at right or left.
                if (parent.Data > newNode.Data)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }
        }

        //Avoids a few extra comparisons. Better logic.
        public void InsertSlightlyFaster(int value)
        {
            var newNode = new BTNode
            {
                Data = value
            };

            //Set new node as root node if tree is empty.
            if (Root == null)
                Root = newNode;
            else
            {
                var currentNode = Root;
                while (true)
                {
                    //New node needs to be inserted to right of current node.
                    if (currentNode.Data < newNode.Data)
                    {
                        //If current node has no right then set new node as right of current node and exit.
                        if (currentNode.Right == null)
                        {
                            currentNode.Right = newNode;
                            return;
                        }
                        //Else set current node as right of current node and continue traversal.
                        currentNode = currentNode.Right;
                    }
                    //New node needs to be inserted left of current node.
                    else
                    {
                        if (currentNode.Left == null)
                        {
                            currentNode.Left = newNode;
                            return;
                        }
                        currentNode = currentNode.Left;
                    }
                }
            }
        }

        //Find if a node in tree contains the passed value.
        public bool Contains(int value)
        {
            if (Root == null)
                return false;

            var currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.Data == value)
                    return true;

                if (currentNode.Data > value)
                    currentNode = currentNode.Left;
                else currentNode = currentNode.Right;
            }

            return false;
        }

        //Get the node which has the passed value.
        public BTNode GetNodeWithValue(int value)
        {
            if (Root == null)
                return default;

            var currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.Data == value)
                    return currentNode;

                if (currentNode.Data > value)
                    currentNode = currentNode.Left;
                else currentNode = currentNode.Right;
            }

            return default;
        }

        //Remove the node whose value equals the passed value and maintain the BST structure.
        public void Remove(int value)
        {
            if (Root == null)
                return;

            var currentNode = Root;
            BTNode parentNode = null;

            //Find the node to remove.
            while (currentNode != null)
            {
                //Found the node to remove.
                if(currentNode.Data == value)
                {
                    break;
                }

                parentNode = currentNode;

                if (currentNode.Data > value)
                    currentNode = currentNode.Left;
                else currentNode = currentNode.Right;
            }

            //Node does not exist in tree.
            if (currentNode == null)
                return;

            if (currentNode.IsLeafNode())
            {
                if (currentNode == Root)
                    Root = null;
                else
                {
                    if (parentNode.Right == currentNode)
                        parentNode.Right = null;
                    else parentNode.Left = null;
                }
            }

            else if (currentNode.Right == null)
            {
                if (currentNode == Root)
                    Root = currentNode.Left;
                else
                {
                    if (parentNode.Right == currentNode)
                        parentNode.Right = currentNode.Left;
                    else parentNode.Left = currentNode.Left;
                }
            }

            else if (currentNode.Left == null)
            {
                if (currentNode == Root)
                    Root = currentNode.Right;
                else
                {
                    if (parentNode.Right == currentNode)
                        parentNode.Right = currentNode.Right;
                    else parentNode.Left = currentNode.Right;
                }
            }

            //Node to be deleted has both children.
            else
            {
                //Find successor and its parent.
                var successor = currentNode.Right.Left;
                var successorParent = currentNode.Right;

                while (successor.Left != null)
                {
                    successorParent = successor;
                    successor = successor.Left;
                }

                //If node to be removed is root then set successor as root.
                if (currentNode == Root)
                    Root = successor;

                // Else point parent to successor.
                else
                {
                    if (parentNode.Right == currentNode)
                        parentNode.Right = successor;
                    else parentNode.Left = successor;
                }

                //Remove successor from successor's parent. Successor's parent will either have null or successor's right subtree as its new left child.
                successorParent.Left = successor.Right;

                //Set successor's new right and left child.
                successor.Right = currentNode.Right;
                successor.Left = currentNode.Left;
            }
        }

        //Do a BFS traversal of the tree. Non-Recursive.
        public List<int> BreadthFirstSearch()
        {
            if (Root == null)
                return null;

            //will contain tree nodes in BFS order.
            var nums = new List<int>();

            //we will process elements in FIFO order and add the left node before the right.
            var nodesToProcess = new Queue<BTNode>();

            nodesToProcess.Enqueue(Root);

            //while we have some items to process.
            while(nodesToProcess.Count > 0)
            {
                var currentNode = nodesToProcess.Dequeue();

                //first add the current node to nums list.
                nums.Add(currentNode.Data);

                //first add left node of current to queue because BFS is left -> right and queue is FIFO.
                if (currentNode.Left != null)
                    nodesToProcess.Enqueue(currentNode.Left);

                //add right node to queue.
                if (currentNode.Right != null)
                    nodesToProcess.Enqueue(currentNode.Right);
            }

            return nums;
        }

        //Do a BFS traversal. Recursive in nature.
        public List<int> BFSRecursive()
        {
            //validations.
            if (Root == null)
                return null;

            var numsInBFSOrder = new List<int>();

            //setting initial state for recursion.
            var nodesToProcess = new Queue<BTNode>();
            nodesToProcess.Enqueue(Root);

            static List<int> BFSRecursive(Queue<BTNode> queue, List<int> nums)
            {
                //base case.
                if (queue.Count == 0)
                    return nums;

                //getting closer to base case by dequeuing.
                var currentNode = queue.Dequeue();
                nums.Add(currentNode.Data);

                //Recursive case. Adding more items to queue until we have nothing to add.
                if (currentNode.Left != null)
                    queue.Enqueue(currentNode.Left);
                if(currentNode.Right != null)
                {
                    queue.Enqueue(currentNode.Right);
                }

                return BFSRecursive(queue, nums);
            }

            return BFSRecursive(nodesToProcess, numsInBFSOrder);
        }

        //Do a DFS traversal of the tree. In-Order traversal. Recursive.
        public List<int> DFSInOrder()
        {
            //validations
            if (Root == null)
                return null;

            //setup initial recursion state.
            var numsInOrder = new List<int>();

            //define recursive function.
            static void DFSInOrder(BTNode currentNode, List<int> nums)
            {
                //base case.
                if (currentNode.IsLeafNode()) {
                    nums.Add(currentNode.Data);
                    return;
                }

                //Recursive case.

                //first go down to the left most leaf and add it to list.
                if (currentNode.Left != null)
                    DFSInOrder(currentNode.Left, nums);

                //then add its parent to list.
                nums.Add(currentNode.Data);

                //then add the parent's right subtree to list.
                if(currentNode.Right != null)
                {
                    DFSInOrder(currentNode.Right, nums);
                }

            }

            DFSInOrder(Root, numsInOrder);

            return numsInOrder;
        }

        public List<int> DFSPreOrder()
        {
            //validations
            if (Root == null)
                return null;

            //setup initial recursion state.
            var numsInOrder = new List<int>();

            //define recursive function.
            static void DFSPreOrder(BTNode currentNode, List<int> nums)
            {
                //base case.
                if (currentNode.IsLeafNode())
                {
                    nums.Add(currentNode.Data);
                    return;
                }

                //Recursive case.

                //first add parent to list.
                nums.Add(currentNode.Data);

                //then add its left subtree to list.
                if (currentNode.Left != null)
                    DFSPreOrder(currentNode.Left, nums);

                //then add the right subtree to list.
                if (currentNode.Right != null)
                {
                    DFSPreOrder(currentNode.Right, nums);
                }

            }

            DFSPreOrder(Root, numsInOrder);

            return numsInOrder;
        }

        public List<int> DFSPostOrder()
        {
            //validations
            if (Root == null)
                return null;

            //setup initial recursion state.
            var numsInOrder = new List<int>();

            //define recursive function.
            static void DFSPostOrder(BTNode currentNode, List<int> nums)
            {
                //base case.
                if (currentNode.IsLeafNode())
                {
                    nums.Add(currentNode.Data);
                    return;
                }

                //Recursive case.

                //first add left subtree to list.
                if (currentNode.Left != null)
                    DFSPostOrder(currentNode.Left, nums);

                //then add the right subtree to list.
                if (currentNode.Right != null)
                {
                    DFSPostOrder(currentNode.Right, nums);
                }

                //then add parent to list.
                nums.Add(currentNode.Data);
            }

            DFSPostOrder(Root, numsInOrder);

            return numsInOrder;
        }
    }
}
