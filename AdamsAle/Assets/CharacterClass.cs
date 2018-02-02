using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu]
public class CharacterClass : PlayableAsset
{
    public Sprite CharSprite;
    public Animator AnimControl;

    public string CharClass;

	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
		return Playable.Create(graph);
	}
}
