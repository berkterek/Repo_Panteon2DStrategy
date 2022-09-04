using System.Collections;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Panteon2DStrategy.Controllers
{
    public class InfiniteScrollController : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] ScrollRect _scrollRect;
        [SerializeField] Transform _transform;
        [SerializeField] float _outOfBoundsThreshold = 40f;
        [SerializeField] float _childWidth = 125f;
        [SerializeField] float _childHeight = 125f;
        [SerializeField] float _itemSpacing = 30f;

        Vector2 _lastDragPosition;
        bool _positiveDrag;
        int _childCount = 0;
        float _height = 0f;

        void Awake()
        {
            this.GetReference(ref _scrollRect);
            this.GetReference(ref _transform);
        }

        void OnValidate()
        {
            this.GetReference(ref _scrollRect);
            this.GetReference(ref _transform);
        }

        IEnumerator Start()
        {
            _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
            _childCount = _scrollRect.content.childCount;
            _height = Screen.height;
            _scrollRect.content.localPosition = Vector2.up * 1500f;

            yield return new WaitForSeconds(1f);
            int counter = 0;
            while (counter < 100)
            {
                _positiveDrag = true;
                HandleHorizontalScroll(DirectionCacheHelper.Vector2Zero);
                counter++;
                _scrollRect.content.transform.Translate(Time.deltaTime * 3f * DirectionCacheHelper.Up);
                yield return null;
            }
        }

        void OnEnable()
        {
            _scrollRect.onValueChanged.AddListener(HandleHorizontalScroll);
        }

        void OnDisable()
        {
            _scrollRect.onValueChanged.RemoveListener(HandleHorizontalScroll);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastDragPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _positiveDrag = eventData.position.y > _lastDragPosition.y;
            _lastDragPosition = eventData.position;
        }

        bool ReachedThreshold(Transform item)
        {
            float posYThreshold = _transform.position.y + _height * 0.5f + _outOfBoundsThreshold;
            float negYThreshold = _transform.position.y - _height * 0.5f - _outOfBoundsThreshold;
            return _positiveDrag
                ? item.position.y - _childWidth * 0.5f > posYThreshold
                : item.position.y + _childWidth * 0.5f < negYThreshold;
        }

        private void HandleHorizontalScroll(Vector2 value)
        {
            int currentItemIndex = _positiveDrag ? _childCount - 1 : 0;
            var currentItem = _scrollRect.content.GetChild(currentItemIndex);

            if (!ReachedThreshold(currentItem))
            {
                return;
            }

            int endItemIndex = _positiveDrag ? 0 : _childCount - 1;
            Transform endItem = _scrollRect.content.GetChild(endItemIndex);
            Vector2 newPosition = endItem.position;

            if (_positiveDrag)
            {
                newPosition.y = endItem.position.y - _childHeight * 1.5f + _itemSpacing;
            }
            else
            {
                newPosition.y = endItem.position.y + _childHeight * 1.5f - _itemSpacing;
            }

            currentItem.position = newPosition;
            currentItem.SetSiblingIndex(endItemIndex);
        }
    }
}