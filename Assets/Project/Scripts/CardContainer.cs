using System.Collections.Generic;
using UnityEngine;

namespace SocialStories
{
	public class CardContainer : MonoBehaviour
	{
		private List<Card> _cards = new List<Card>();

		private void Awake()
		{
			for (int i = 0; i < transform.childCount; i++)
				_cards.Add(transform.GetChild(i).GetComponent<Card>());
		}
		
		public void RefreshId() 
		{
			for (int i = 0; i < _cards.Count; i++)
				_cards[i].SetCardId(i);
		}

		public void KillCard(int id)
		{
			Destroy(transform.GetChild(id).gameObject);
			_cards.Remove(_cards[id]);
		}

		public void ChangeCardActiveState(bool active)
		{
			foreach (var card in _cards)
				card.CardActive(active);
		}

		public Transform Transform()
		{
			return transform;
		}

		public Vector2 CardAnchoredPos(int id) 
		{
			Vector2 position = transform.GetChild(id).GetComponent<RectTransform>().anchoredPosition;
			return position;
		}

		public Card CurrentCard(int id) 
		{
			return _cards[id]; 
		}
	}
}

