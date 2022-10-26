using System.Threading.Tasks;
using UnityEngine;

namespace SocialStories
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private BlockContainer _blockContainer;
		[SerializeField] private CardContainer _cardsContainer;
		[SerializeField] private GameObject _winScreen;
		[SerializeField] private GameObject _tableGroup;
		[SerializeField] private GameObject[] _cardPrefabs;

		private GameObject _clone;
		private RectTransform _cloneTransform;
		private int _currentCardId;

		public static Game Instance { get; private set; }

		void Awake() => Instance = this;

		void Update()
		{
			_cardsContainer.RefreshCardsId();
		}		

		public void StartAddingCard(int number, Vector2 position, int id) 
		{
			_currentCardId = id;

			_cardsContainer.ChangeCardsActiveState(false);

			SpawnClone(number, position, id);
			CheckSelection();
		}	

		void SpawnClone(int number, Vector2 position, int id)
		{
			_clone = Instantiate(
				_cardPrefabs[number], 
				position, 
				Quaternion.identity,
				_cardsContainer.GetTransform()
				);
			
			_cloneTransform = _clone.GetComponent<RectTransform>();
			_cloneTransform.anchorMin = _cloneTransform.anchorMax = new Vector2(0, 1);
			_cloneTransform.pivot = new Vector2(0.5f, 0.5f);
			_cloneTransform.anchoredPosition = position;

			_clone.transform.SetParent(_blockContainer.GetTransform());
			_clone.GetComponent<Card>().SetActivity(false);			
		}

		void CheckSelection()
		{
			int index = _blockContainer.GetCurrentIndex();
			
			if (index == _clone.GetComponent<Card>().Number)
				LerpAnimAsyncSuccess(_cloneTransform.anchoredPosition, _blockContainer.GetCardAnchoredPos(), 0.4f);
			else
				LerpAnimAsyncFail(_cloneTransform.anchoredPosition, _blockContainer.GetCardAnchoredPos(), 0.4f);
			
			_cardsContainer.GetCurrentCard(_currentCardId).SetInvisibility(true);
		}	
		
		async void LerpAnimAsyncSuccess(Vector2 startPos, Vector2 targetPos, float timer)
		{
			float elapsedTime = 0;
			
			while (elapsedTime < timer)
			{
				_cloneTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, (elapsedTime / timer));
				elapsedTime += Time.deltaTime;
				await Task.Delay(1);
			}				

			int index = _blockContainer.GetCurrentIndex();
			_cardsContainer.KillCard(_currentCardId);
			_blockContainer.SetActiveCard(index);

			EndOfTurn();
		}	
		
		async void LerpAnimAsyncFail(Vector2 startPos, Vector2 targetPos, float timer)
		{
			float elapsedTime = 0;
			
			while (elapsedTime < timer)
			{
				_cloneTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, (elapsedTime / timer));
				elapsedTime += Time.deltaTime;
				await Task.Delay(1);
			}

			await Task.Delay(300);
			_clone.transform.SetParent(_cardsContainer.GetTransform());
			
			startPos = _cloneTransform.anchoredPosition;
			targetPos = _cardsContainer.GetCardAnchoredPos(_currentCardId);
			elapsedTime = 0;
			
			while (elapsedTime < timer)
			{
				_cloneTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, (elapsedTime / timer));
				elapsedTime += Time.deltaTime;
				await Task.Delay(1);
			}

			_cardsContainer.GetCurrentCard(_currentCardId).SetInvisibility(false);

			EndOfTurn();
		}				

		void EndOfTurn()
		{
			Destroy(_clone.gameObject);
			_cardsContainer.ChangeCardsActiveState(true);

			if (!_blockContainer.CheckIfGameEnd()) return;

			_tableGroup.SetActive(false);
			_winScreen.SetActive(true);
		}
	}
}

