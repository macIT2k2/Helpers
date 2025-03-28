using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Dev_Tools
{
        public enum QueueItemType
        {
                Popup,
                Tutorial,
                Animation,
                Dialog,
                Reward,
        }

        public interface IQueueItem
        {
                string DisplayName { get; }

                QueueItemType ItemType { get; }
                bool IsCompleted { get; }
                void ShowByQueue();
                event Action OnComplete;
        }

        public abstract class BaseQueueManager<T> : Singleton<BaseQueueManager<T>> where T : IQueueItem
        {
                [ShowInInspector, ReadOnly]
                protected Queue<T> itemQueue = new Queue<T>();

                // [ShowInInspector, ReadOnly]
                protected List<string> queueDisplayNames => GetDisplayNames();

                [ShowInInspector, ReadOnly]
                protected int QueueCount => itemQueue.Count;

                protected bool isProcessingQueue = false;
                protected T currentItem;

                protected virtual void EnqueueItem(
                    T item,
                    Action onComplete = null,
                    bool prioritizeItemType = false)
                {
                        if (prioritizeItemType && itemQueue.Count > 0)
                        {
                                var queueList = new List<T>(itemQueue);

                                int insertIndex = queueList.FindIndex(
                                    x => x.ItemType == item.ItemType
                                );

                                if (insertIndex != -1)
                                {
                                        queueList.Insert(insertIndex, item);
                                        itemQueue = new Queue<T>(queueList);
                                }
                                else
                                {
                                        itemQueue.Enqueue(item);
                                }
                        }
                        else
                        {
                                itemQueue.Enqueue(item);
                        }

                        item.OnComplete += () =>
                        {
                                Debug.Log($"<color=#2884FB>Hoàn thành: {item.DisplayName}</color>");
                                isProcessingQueue = false;
                                onComplete?.Invoke();
                                ProcessNextItem();
                        };

                        if (!isProcessingQueue)
                        {
                                ProcessNextItem();
                        }
                }
                List<string> GetDisplayNames()
                {
                        List<string> displayNames = new List<string>();
                        foreach (var item in itemQueue)
                        {
                                displayNames.Add(item.DisplayName);
                        }
                        return displayNames;
                }
                protected virtual void ProcessNextItem()
                {
                        if (itemQueue.Count == 0)
                        {
                                isProcessingQueue = false;
                                currentItem = default;
                                OnQueueEmpty();
                                return;
                        }

                        isProcessingQueue = true;

                        T previousItem = currentItem;
                        currentItem = itemQueue.Dequeue();

                        switch (currentItem.ItemType)
                        {
                                case QueueItemType.Tutorial:
                                        HandleTutorialItem(previousItem);
                                        break;
                                case QueueItemType.Popup:
                                        HandlePopupItem();
                                        break;
                                default:
                                        break;
                        }

                        currentItem.ShowByQueue();
                }

                protected virtual void HandleTutorialItem(T previousItem)
                {
                        //handle logic with previous and current item
                }

                protected virtual void HandlePopupItem()
                {
                        //handle logic with previous and current item
                }
                
                void OnQueueEmpty()
                {
                        // handle logic when queue is empty
                }
                // Các phương thức khác giữ nguyên như trước...

        }

        public abstract class BaseQueueItemHandler : IQueueItem
        {
                public abstract string DisplayName { get; }
                public abstract QueueItemType ItemType { get; }
                public bool IsCompleted { get; protected set; }
                public event Action OnComplete;

                public virtual void ShowByQueue()
                {
                        OnComplete?.Invoke();
                }

                protected void NotifyComplete()
                {
                        IsCompleted = true;
                        OnComplete?.Invoke();
                }
        }
}