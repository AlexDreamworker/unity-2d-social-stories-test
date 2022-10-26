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
		
		public void RefreshCardsId() 
		{
			for (int i = 0; i < _cards.Count; i++)
				_cards[i].SetId(i);
		}

		public void KillCard(int id)
		{
			Destroy(transform.GetChild(id).gameObject);
			_cards.Remove(_cards[id]);
		}

		public void ChangeCardsActiveState(bool active)
		{
			foreach (Card card in _cards)
				card.SetActivity(active);
		}

		public Transform GetTransform()
		{
			return transform;
		}

		public Vector2 GetCardAnchoredPos(int id) 
		{
			Vector2 position = transform.GetChild(id).GetComponent<RectTransform>().anchoredPosition;
			return position;
		}

		public Card GetCurrentCard(int id) 
		{
			return _cards[id]; 
		}
	}
}

