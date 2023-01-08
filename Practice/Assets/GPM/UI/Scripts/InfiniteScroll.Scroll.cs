namespace Gpm.Ui
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;

    public partial class InfiniteScroll : IMoveScroll
    {
        public enum MoveToType
        {
            MOVE_TO_TOP = 0,
            MOVE_TO_CENTER,
            MOVE_TO_BOTTOM
        }

        private const int NEEDITEM_MORE_LINE = 1;
        private const int NEEDITEM_EXTRA_ADD = 2;

        protected ScrollRect scrollRect = null;
        protected RectTransform viewport = null;
        protected bool isVertical = false;

        protected float sizeInterpolationValue = 0.0001f; // 0.01%

        protected float contentsPostion = 0;

        public Vector2 GetScrollPostion()
        {
            return content.anchoredPosition;
        }

        public void SetScrollPostion(Vector2 postion)
        {
            content.anchoredPosition = postion;
        }

        public bool IsMoveToFirstData()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            float viewportSize = GetViewportSize();
            float contentSize = GetItemTotalSize();
            float position = GetContentPosition();

            return IsMoveToFirstData(position, viewportSize, contentSize);
        }

        private bool IsMoveToFirstData(float position, float viewportSize, float contentSize)
        {
            bool isShow = false;

            if (viewportSize > contentSize)
            {
                isShow = true;
            }
            else
            {
                float interpolation = contentSize * sizeInterpolationValue;
                if (-position > -interpolation)
                {
                    isShow = true;
                }
            }

            return isShow;
        }

        public bool IsMoveToLastData()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            float viewportSize = GetViewportSize();
            float contentSize = GetItemTotalSize();
            float position = GetContentPosition();

            return IsMoveToLastData(position, viewportSize, contentSize);
        }

        private bool IsMoveToLastData(float position, float viewportSize, float contentSize)
        {
            bool isShow = false;

            if (viewportSize > contentSize)
            {
                isShow = true;
            }
            else
            {
                float interpolation = contentSize * sizeInterpolationValue;

                if (-(viewportSize + position - contentSize) <= interpolation)
                {
                    isShow = true;
                }
            }

            return isShow;
        }

        public void MoveTo(InfiniteScrollData data, MoveToType moveToType, float time = 0)
        {
            MoveTo(GetDataIndex(data), moveToType, time);
        }

        public void MoveTo(int dataIndex, MoveToType moveToType, float time = 0)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            if (IsValidDataIndex(dataIndex) == true)
            {
                if (time > 0)
                {
                    Control.MoveTo(this, dataIndex, moveToType, time);
                }
                else
                {
                    Vector2 prevPosition = content.anchoredPosition;
                    float move = 0.0f;

                    if (isVertical == true)
                    {
                        move = GetMovePosition(dataIndex, viewport.rect.height, content.rect.height, moveToType);
                        content.anchoredPosition = new Vector2(prevPosition.x, move);
                    }
                    else
                    {
                        move = GetMovePosition(dataIndex, viewport.rect.width, content.rect.width, moveToType);
                        content.anchoredPosition = new Vector2(-move, prevPosition.y);
                    }
                }
            }
        }

        public Vector2 GetMovePosition(int dataIndex, MoveToType moveToType)
        {
            Vector2 position = content.anchoredPosition;

            float move = GetMovePosition(dataIndex, isVertical, moveToType);
            if (isVertical == true)
            {
                position.y = move;
            }
            else
            {
                position.x = -move;
            }

            return position;
        }

        internal float GetMovePosition(int dataIndex, bool isVertical, MoveToType moveToType)
        {
            if (isVertical == true)
            {
                return GetMovePosition(dataIndex, viewport.rect.height, content.rect.height, moveToType);
            }
            else
            {
                return GetMovePosition(dataIndex, viewport.rect.width, content.rect.width, moveToType);
            }
        }

        internal float GetMovePosition(int dataIndex, float viewportSize, float contentSize, MoveToType moveToType)
        {
            float move = 0.0f;
            float moveItemSize = GetItemSize(dataIndex);
            float passingItemSize = GetItemSizeSumToIndex(dataIndex);

            move = passingItemSize + padding;

            switch (moveToType)
            {
                case MoveToType.MOVE_TO_CENTER:
                    {
                        move -= viewportSize * 0.5f - moveItemSize * 0.5f;
                        break;
                    }
                case MoveToType.MOVE_TO_BOTTOM:
                    {
                        move -= viewportSize - moveItemSize;
                        break;
                    }
            }

            move = Mathf.Clamp(move, 0.0f, contentSize - viewportSize);
            move = Math.Max(0.0f, move);

            return move;
        }

        public void MoveToFirstData()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            if (isVertical == true)
            {
                scrollRect.normalizedPosition = Vector2.one;
            }
            else
            {
                scrollRect.normalizedPosition = Vector2.zero;
            }
        }

        public void MoveToLastData()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            if (isVertical == true)
            {
                scrollRect.normalizedPosition = Vector2.zero;
            }
            else
            {
                scrollRect.normalizedPosition = Vector2.one;
            }
        }

        public void AddScrollValueChangedLisnter(UnityAction<Vector2> listener)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            scrollRect.onValueChanged.AddListener(listener);
        }



        private void OnValueChanged(Vector2 value)
        {
            bool prevIsStartLine = isStartLine;
            isStartLine = IsMoveToFirstData();
            if (prevIsStartLine != isStartLine)
            {
                onStartLine.Invoke(isStartLine);

                changeValue = true;
            }

            bool prevIsEndLine = isEndLine;
            isEndLine = IsMoveToLastData();
            if (prevIsEndLine != isEndLine)
            {
                onEndLine.Invoke(isEndLine);

                changeValue = true;
            }

            UpdateShowItem();
        }

        internal int GetNeedItemNumber()
        {
            int needItemNumber = 0;

            float itemSize = 0.0f;
            if (dynamicItemSize == true)
            {
                itemSize = minItemSize;
            }
            else
            {
                itemSize = defaultItemSize;
            }

            if (itemSize > 0)
            {
                if (isVertical == true)
                {
                    needItemNumber = (int)(viewport.rect.height / itemSize);
                }
                else
                {
                    needItemNumber = (int)(viewport.rect.width / itemSize);
                }

                needItemNumber += NEEDITEM_MORE_LINE;

                if (layout.IsGrid() == true)
                {
                    needItemNumber = needItemNumber * layout.values.Count;
                }

                needItemNumber += NEEDITEM_EXTRA_ADD;
            }

            return needItemNumber;
        }


        public static class Control
        {
            public static void MoveTo(InfiniteScroll scroll, int dataIndex, MoveToType moveToType, float time = 0)
            {
                ScrollMoveTo moveto = scroll.gameObject.GetComponent<ScrollMoveTo>();
                if (moveto == null)
                {
                    moveto = scroll.gameObject.AddComponent<ScrollMoveTo>();
                }

                moveto.dataIndex = dataIndex;
                moveto.moveToType = moveToType;
                moveto.time = time;
                moveto.curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

                moveto.autoDestory = true;

                moveto.Play();
            }
        }

        [Serializable]
        public class StateChangeEvent : UnityEvent<bool>
        {
            public StateChangeEvent()
            {
            }
        }
    }
}
