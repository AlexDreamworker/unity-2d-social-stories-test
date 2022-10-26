using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SocialStories
{
	[RequireComponent(typeof(Image))]
	public class Card : MonoBehaviour, IPointerDownHandler
	{
		[SerializeField] [Range(0, 3)] private int _number;
		[SerializeField] private TMP_Text _text;
		[SerializeField] private Image _icon;

		private Image _image;
		private Color _textDefaultColor;
		private RectTransform _rectTransform;
		private int _id;
		private bool _isActive = true;

		public int Number => _number;

		void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
			_image = GetComponent<Image>();
			_textDefaultColor = _text.color;
		}

		public void SetCardId(int id)
		{
			_id = id;
		}

		public void CardActive(bool active)
		{
			_isActive = active;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!_isActive) return;
			Game.Instance.StartAddingCard(_number, _rectTransform.anchoredPosition, _id);
		}

		public void Invisibility(bool visible)
		{
			if (!visible) 
			{
				_image.color = _icon.color = new Color(1f, 1f, 1f, 1f);
				_text.color = _textDefaultColor;
			}
			else 	
				_image.color = _icon.color = _text.color = new Color(1f, 1f, 1f, 0f);
		}
	}
}

