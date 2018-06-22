using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キーの効果音
/// 製作者：冨田 大悟
/// </summary>
public class KeyInputSE : MonoBehaviour {

     public static KeyInputSE keyInoutSE;

    [SerializeField]
    private AudioClip DecideSE;
    [SerializeField]
    private AudioClip CancelSE;
    [SerializeField]
    private AudioClip MoveSE;

    [SerializeField]
    private AudioClip PauseSE;
    public enum Type
    {
        Decide,
        Cancel,
        Move,
        Pause
    }

	// Use this for initialization
	void Start () {
        keyInoutSE = this;

    }
	
    public AudioClip GetClip(Type type)
    {
        if (type == Type.Decide)
        {
            return DecideSE;
        }
        else if (type == Type.Cancel)
        {
            return CancelSE;
        }
        else if (type == Type.Move)
        {
            return MoveSE;
        }
        else if (type == Type.Pause)
        {
            return PauseSE;
        }
        return null;
    }
}
