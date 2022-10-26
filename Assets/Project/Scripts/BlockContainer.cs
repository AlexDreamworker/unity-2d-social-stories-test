using UnityEngine;

namespace SocialStories
{
	public class BlockContainer : MonoBehaviour
	{
		public Transform GetTransform()
		{
			return transform;
		}

		public Vector2 GetCardAnchoredPos()
		{
			Vector2 position = Vector2.zero;

			foreach(Transform child in transform)
			{
				if(child.gameObject.activeSelf)
					continue;
				else
				{
					position = child.GetComponent<RectTransform>().anchoredPosition;
					break;
				}
			}
			return position;
		}

		public int GetCurrentIndex() 
		{
			int index = 0;

			for (int i = 0; i < transform.childCount; i++) 
			{
				if (transform.GetChild(i).gameObject.activeSelf)
					continue;
				else 
				{
					index = i;
					break;
				}
			}
			return index;
		}

		public void SetActiveCard(int index)
		{
			transform.GetChild(index).gameObject.SetActive(true);
		}

		public bool CheckIfGameEnd() 
		{
			foreach(Transform child in transform)
			{
				if(child.gameObject.activeSelf)
					continue;
				else
					return false;
			}
			return true;
		}
	}
}

