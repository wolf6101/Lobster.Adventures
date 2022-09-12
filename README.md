Lobster Adventure Test Assignment
==============================================================
## Binary Decision Tree representation
![BinaryDecisionTree](docs/BinaryDecisionTree.png)

## Domain Class Diagram
![DomainClassDiagram](docs/DomainClassDiagram.png)

## Client to API communication sequence diagram
API supports 2 types of client to API communication:

1. Retrieval of complete adventure tree, caching it on the client side, handling user choices on the client, persisting its state to the database. This approach is recommended when tree is not huge and nodes are lightweight.
![SequenceClientSideExecution](docs/SequenceClientSideExecution.png)

2. Retrieving tree adventure nodes by node, rendering on the client, persisting choice in the database. 
    This approach is recommended when decision tree is huge (either huge number of nodes or huge size of the node itself) and transferring it to the client would not be considered beneficial. Request on each user choice might look like overhead, but after doing some calculations - it starts looking reasonable. 

    Let’s assume we have a tree of 1 000 000 nodes. Tree of this size has 20 levels (log2(1000000) = ~20). In this case user would have to do 20 request to the server. Taking into account that this request would be stateless with small payload, which make it a great candidate for reverse proxy caching.

    Additionally this can be optimized even further by retrieving node’s subtrees in chunks, say 3 levels, which is a good compromise between number of requests and payload. More on this in Improvements section of this document.
    ![SequenceServerSideExecution](docs/SequenceServerSideExecution.png)
