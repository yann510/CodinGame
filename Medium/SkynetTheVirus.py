import sys
import math


# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.


# n: the total number of nodes in the level, including the gateways
# l: the number of links
# e: the number of exit gateways



def single_source_shortest_path(g, source, cutoff=None):
    """Compute shortest path between source
    and all other nodes reachable from source.
    Parameters
    ----------
    G : NetworkX graph
    source : node label
       Starting node for path
    cutoff : integer, optional
        Depth to stop the search. Only paths of length <= cutoff are returned.
    Returns
    -------
    lengths : dictionary
        Dictionary, keyed by target, of shortest paths.
    Examples
    --------
    >>> G=nx.path_graph(5)
    >>> path=nx.single_source_shortest_path(G,0)
    >>> path[4]
    [0, 1, 2, 3, 4]
    Notes
    -----
    The shortest path is not necessarily unique. So there can be multiple
    paths between the source and each target node, all of which have the
    same 'shortest' length. For each target node, this function returns
    only one of those paths.
    See Also
    --------
    shortest_path
    """
    level = 0  # the current level
    nextlevel = {source: 1}  # list of nodes to check at next level
    paths = {source: [source]}  # paths dictionary  (paths to key from source)
    if cutoff == 0:
        return paths
    while nextlevel:
        thislevel = nextlevel
        nextlevel = {}
        for v in thislevel:
            for w in g[v]:
                if w not in paths:
                    paths[w] = paths[v] + [w]
                    nextlevel[w] = 1
        level += 1
        if cutoff is not None and cutoff <= level:
            break
    return paths


n, l, e = [int(i) for i in input().split()]
for i in range(l):
    # n1: N1 and N2 defines a link between these nodes
    n1, n2 = [int(j) for j in input().split()]
for i in range(e):
    ei = int(input())  # the index of a gateway node

# game loop
while 1:
    si = int(input())  # The index of the node on which the Skynet agent is positioned this turn

    # Write an action using print
    # To debug: print("Debug messages...", file=sys.stderr)

    # Example: 0 1 are the indices of the nodes you wish to sever the link between
    print("0 1")
