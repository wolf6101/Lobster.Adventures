# Lobster Adventures Test Assignment

## Design assumptions

1. Left subtree is considered to be Yes subtree. Right subtree is considered to be No subtree.
2. For simplicity sake, system supports only binary decision tree with YES and NO decision.
3. Storing tree in serialized BFS, DFS manner is not an option as we need a way to work with a big trees, efficently querying subtrees or getting right or left children of specific node without tree deserialization.
4. API is imlemented to support full tree retrieval by client in case of small tree. For huge trees retrieval of whole tree by the client would cause high latencies, additinalty due to the nature of domain,  each choice eliminates half of the tree, which makes huge tree retrieval by client is not beneficial. Client can implement hybrid approach by making `HEAD` request of `GET adventure/tree/id`and fetch full subtree if `ContentLength` is small and go for node by node approach when tree is huge.

## Binary Decision Tree representation

<img src="docs/BinaryDecisionTree.jpg" width="80%">

## Domain Class Diagram

<img src="docs/DomainClassDiagram.jpg" width="80%">

1. `Adventure` - class represents decision tree
2. `AdventureNode` - class represents decision tree node
3. `UserJourney` - class represents client's game experience. Client can action many journeys of the same adventure

# Client to API communication sequence diagrams

API supports 2 types of client to API communication:

1. Complete tree retrieval
2. Node by node retrieval

## Complete tree retrieval

Retrieval of complete adventure tree, caching it on the client side, handling user choices on the client, persisting its state to the database. This approach is recommended when tree is not huge and nodes are lightweight.

<img src="docs/SequenceClientSideExecution.jpg" width="80%">

## Node by node retrieval

Retrieving tree adventure nodes by node, rendering on the client, persisting choice in the database.
This approach is recommended when decision tree is huge (either huge number of nodes or huge size of the node itself) and transferring it to the client would not be considered beneficial. Request on each user choice might look like overhead, but after doing some calculations - it starts looking reasonable.

Let’s assume we have a tree of 1 000 000 nodes. Tree of this size has 20 levels (`log2(1000000) = ~20`). In this case user would have to do 20 request to the server. Taking into account that these requests would be stateless with small payload - it makes it a great candidate for reverse proxy caching.

Additionally this can be optimized even further by retrieving node’s subtrees in chunks, say 3 levels, which is a good compromise between number of requests and payload. More on this in Improvements section of this document.

<img src="docs/SequenceServerSideExecution.jpg" width="80%">

## Possible improvements
_____________________________________________________________________________________________________
## Non binary nature of decision tree

In case Decision Tree should have more options or name decision differently than YES or NO, next schema could be used:

<img src="docs/ImprovementNaryTreeSchema1.jpg" width="80%">
<img src="docs/ImprovementNaryTreeSchema2.jpg" width="50%">

Described above design works only for N-ary tree, with the standard Decisions list.

In case decisions names vary from node to node, or tree transforms to a graph - adjacency list can be used to persist its state.

## Partial tree retrieval and cache

In case of tradeoffs between load and bandwitdh, to avoid chatty communication it is possible to optimize by caching nodes subtree on the client side or cache server and access `JourneyController` only when node is not in the cache.

<img src="docs/ImprovementLocalCacheSubtreeRetrieval.jpg" width="80%">

In order to facilitate quick queries of node’s subtree, [Modified Preorder Traversal](https://gist.github.com/tmilos/f2f999b5839e2d42d751) approach can be used.

By applying this approach, `AdventureNode` would be extended with 2 more fields, `rightIndex` and `leftIndex`, which are used for fast `SELECT`s of node’s subtree.

## Solution Architecture

Solution Architecture inspired by [Uncle Bob's Clean Atchitecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html), with merged Use Cases and Interface Adapters layers.

<img src="docs/Layers.jpg" width="80%">

## How to run

1. Pull repository
2. Run `docker compose build` from project root
3. Run `docker compose up`
4. Browse <http://localhost:5095/swagger>

## Nice to do:
1. Improve exception handling and errors communication to the client
2. Refactor Mediator Exception Handlers so they all inherit base handler
3. Cover commands and queries with unit tests
4. Add swagger documentation, summary, error description, parameters, Enums
5. Fix 500 error to BadRequest in POST UserJourneys endpoint, when user inputs string.Empty path
6. Add GET GetAllUserJourney by adventureId, userId