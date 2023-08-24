using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowScript : MonoBehaviour
{
    private bool awake = false;
    private Queue<Vector2> history;
    private Stack<Vector2> toEgg;
    private Queue<Vector2> eggPath;
    public PlayerMovement player;
    public uint delay = 120;
    public uint margin = 60;
    private int topop = 0;
    /*
    following = []
    trackegg = false
    toegghistory = []

    fromegg pops to toegg

    on TP: {
        trackegg = false
        following = toegghistory .. following
    }
    on lay egg: {
        trackegg = true
    }

    // Keep track of what part of history doesn't need to be done anymore

    while trackegg: {
        history is appended to both indephistory and the back of toegghistory
    }
    */
    void Start()
    {
        history = new Queue<Vector2>();
        toEgg = new Stack<Vector2>();
        eggPath = new Queue<Vector2>();
    }

    void FixedUpdate()
    {
        if (!awake && history.Count > delay) {
            awake = true;
        }

        Vector2 playerPosition = (Vector2)player.transform.position;
        history.Enqueue(playerPosition);
        if (player.lastegg) {
            toEgg.Push(playerPosition);
            eggPath.Enqueue((Vector2)player.lastegg.transform.position);
        }

        if (awake) {
            if (history.Count > delay + margin) {
                history.Dequeue();
                if (player.lastegg) {
                    topop += 1;
                }
            }
            transform.position = (Vector3)history.Dequeue();
        }
    }

    public void OnThrowEgg() {
        topop = 0;
        toEgg.Clear();
        eggPath.Clear();
    }

    public void OnHatchEgg() {

        Stack<Vector2> tempstack = new Stack<Vector2>();
        Stack<Vector2> tempstack2 = new Stack<Vector2>();
        for (int i = history.Count; i > 0; i--) {
            tempstack.Push(history.Dequeue());
        }

        int toeggcount = toEgg.Count;
        int tempstackcount = tempstack.Count;
        for (int i = Mathf.Min(tempstack.Count + topop, toEgg.Count); i > 0; i--) {
            toEgg.Pop();
        }
        for (int i = Mathf.Min(tempstack.Count, toeggcount - topop); i > 0; i--) {
            tempstack.Pop();
        }
        while (tempstack.Count > 0) {
            tempstack2.Push(tempstack.Pop());
        }
        while (tempstack2.Count > 0) {
            history.Enqueue(tempstack2.Pop());
        }

        while (toEgg.Count > 0) {
            history.Enqueue(toEgg.Pop());
        }

        while (eggPath.Count > 0) {
            history.Enqueue(eggPath.Dequeue());
        }
        topop = 0;
        toEgg.Clear();
        eggPath.Clear();
    }
}
