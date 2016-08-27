using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IEvent
{
    uint GetKey();
    object GetParam1();
    object GetParam2();
}

public interface IEventListener
{
    /// <summary>
    /// <returns>拦截事件返回true;继续派发返回false</returns>
    /// </summary>
    bool OnFireEvent(uint key, object param1, object param2);

    /// <summary>
    /// priority of this listener to get event with specified eventKey
    /// the larger the higher
    /// set before attach
    /// arrange sequence only happens a single time at Attach
    /// </summary>
    /// <param name="eventKey"></param>
    /// <returns></returns>
    int GetListenerPriority(uint eventKey);

    /// <summary>
    /// Attachs the event.
    /// </summary>
    void AttachEvent();

    /// <summary>
    /// 必须在OnDestroy中销毁消息注册
    /// </summary>
    void DetachEvent();
}


/// <summary>
/// 事件派发器
/// 目前所有的监听对象在派发器中是弱引用，已防止内存泄漏。
/// </summary>
public class EventDispatcher
{
    private Dictionary<uint, List<WeakReference>> m_listenerTable = new Dictionary<uint, List<WeakReference>>();
    private Queue m_listenersToUpdateCache = new Queue();

    protected string m_evtQueueLock = "lock";
    protected Queue m_eventQueueNow = new Queue();
    protected Queue m_eventQueueNext = new Queue();

    protected ArrayList m_DelingEventArray = new ArrayList();
        
    private class ListenerPack
    {
        public uint m_eventKey = 0;
        public WeakReference m_listener = null;
        public bool m_addOrRemove = false;

        public ListenerPack(IEventListener listener, uint eventKey, bool addOrRemove)
        {
            m_eventKey = eventKey;
            m_listener = new WeakReference(listener, false);
            m_addOrRemove = addOrRemove;
        }
    }

    public void Print()
    {
        foreach (var entry in this.m_listenerTable)
        {
            foreach (WeakReference listener in entry.Value)
            {

                if (listener != null)
                {
                    if (listener.Target == null) continue;
                }
                else
                { }
                    //HDebug.Log("key=" + entry.Key.ToString() + ", listener=null");
            }
        }
    }
        
    /// <summary>
    /// 交换消息队列
    /// </summary>
    private void SwapNowAndNextEventQueue()
    {
        Queue temp = m_eventQueueNow;
        m_eventQueueNow = m_eventQueueNext;
        m_eventQueueNext = temp;
    }

    #region 对监听者的处理逻辑
    private void _AttachListner(uint eventKey, List<WeakReference> listenerList, IEventListener listener)
    {
        int pos = 0;
        for (int n = 0; n < listenerList.Count; n++)
        {
            IEventListener tmp = listenerList[n].Target as IEventListener;

            if (tmp == null)
            {
                pos++;
                continue;
            }

            if (listener.GetListenerPriority(eventKey) > tmp.GetListenerPriority(eventKey))
            {
                break;
            }
            pos++;
        }
        listenerList.Insert(pos, new WeakReference(listener, false));
    }

    /// <summary>
    /// 非线程安全,只允许在主线程调用
    /// </summary>
    public void AttachListenerNext(IEventListener listener, uint eventKey)
    {
        //不需要判断空//
        lock (m_listenersToUpdateCache)
        {
            m_listenersToUpdateCache.Enqueue(new ListenerPack(listener, eventKey, true));
        }
    }

    /// <summary>
    /// 非线程安全,只允许在主线程调用
    /// 不允许在IEventListener.HandleEvent 中使用
    /// </summary>
    public void AttachListenerNow(IEventListener listener, uint eventKey)
    {
#if EVENT_DEBUG
    EventDebug("AttachListenerNow");
#endif
        if (null == listener || 0 == eventKey)
        {
            //SimpleLog.Error("EventDispacher, AttachListenerNow:", " failed due to no listener or event key specified.");
            return;
        }

        if (!m_listenerTable.ContainsKey(eventKey))
            m_listenerTable.Add(eventKey, new List<WeakReference>());

        var listenerList = m_listenerTable[eventKey];

        if (!IsListenerExist(listenerList, listener))
        {
            _AttachListner(eventKey, listenerList, listener);
        }
        else
        {
            //SimpleLog.Error("EventDispacher, AttachListenerNow: ", listener.GetType().ToString() + " is already in list for event: " + eventKey.ToString());
        }
    }

    private bool IsListenerExist(List<WeakReference> listenerList, IEventListener listener)
    {
        if (listener == null) return false;

        bool bHaveContained = false;

        for (int i = 0; i < listenerList.Count; ++i)
        {
            if (listenerList[i].Target == listener)
            {
                bHaveContained = true;
                break;
            }
        }

        return bHaveContained;
    }

    /// <summary>
    /// 检查Listernr是否已经注册
    /// </summary>
    /// <returns></returns>
    public bool ListenerAttached(IEventListener listener, uint eventKey)
    {
        return m_listenerTable.ContainsKey(eventKey);
    }

    /// <summary>
    /// 非线程安全,只允许在主线程调用
    /// </summary>
    public void DetachListenerNext(IEventListener listener, uint eventKey)
    {
        //不需要判断空//
        lock (m_listenersToUpdateCache)
        {
            m_listenersToUpdateCache.Enqueue(new ListenerPack(listener, eventKey, false));
        }
    }

    /// <summary>
    /// 非线程安全,只允许在主线程调用
    /// 不允许在IEventListener.HandleEvent 中使用
    /// </summary>
    public void DetachListenerNow(IEventListener listener, uint eventKey)
    {
#if EVENT_DEBUG
    EventDebug("DetachListenerNow");
#endif
        if (0 == eventKey)
        {
            //HDebug.LogError("EventDispacher, DetachListenerNow: failed due to no listener or event key specified.");
            return;
        }
        if (!m_listenerTable.ContainsKey(eventKey))
            return;

        var listenerList = m_listenerTable[eventKey];

        foreach (WeakReference listenerRef in listenerList)
        {
            if (listenerRef.Target == listener)
            {
                listenerRef.Target = null;  // 
                if (!m_DelingEventArray.Contains(eventKey))
                    m_DelingEventArray.Add(eventKey);
                break;
            }
        }
    }

    /// <summary>
    /// 每个EventDispatcher会在DispatchEvent开始的时候更新自己的ListenerMap
    /// </summary>
    private void UpdateListenerMap()
    {
        if (m_DelingEventArray.Count > 0)
        {
            foreach(uint eventKey in m_DelingEventArray)
            {
                if (eventKey == 0) continue;

                var listenerList = m_listenerTable[eventKey];

                for (int i = 0; i < listenerList.Count; )
                {
                    if (listenerList[i].Target == null)
                    {
                        listenerList.RemoveAt(i);
                        continue;
                    }
                    ++i;
                }
                if (listenerList.Count == 0) m_listenerTable.Remove(eventKey);
            }
            m_DelingEventArray.Clear();
        }

        lock (m_listenersToUpdateCache)
        {
            while (m_listenersToUpdateCache.Count != 0)
            {
                ListenerPack pack = m_listenersToUpdateCache.Dequeue() as ListenerPack;
                if (pack.m_addOrRemove)
                {
                    AttachListenerNow(pack.m_listener.Target as IEventListener, pack.m_eventKey);
                }
                else
                {
                    DetachListenerNow(pack.m_listener.Target as IEventListener, pack.m_eventKey);
                }
            }
        }
    }
    #endregion
        
    protected bool TriggerEvent(uint key, object param1, object param2)
    {
        if (!m_listenerTable.ContainsKey(key))
        {
            return false;
        }

        var listenerList = m_listenerTable[key];

        for (int n = 0; n < listenerList.Count; n++)
        {
            if (listenerList[n].Target == null) continue;
            IEventListener listener = listenerList[n].Target as IEventListener;
            if (listener != null)
            {
#if UNITY_EDITOR
                if (listener.OnFireEvent(key, param1, param2))
			{
				//一条消息要求有多人接收，这里直接返回的话，只有第一个注册的人能收到消息//
				//return true; //Event consumed.
			}
#else
                try
                {
                    if (listener.OnFireEvent(key, param1, param2))
                    {
                        //一条消息要求有多人接收，这里直接返回的话，只有第一个注册的人能收到消息//
                        //return true; //Event consumed.
                    }
                }
                catch (System.Exception ex)
                {
                    //HDebug.LogError("[Event]:TriggerEvent key=" + key + " Exception=" + ex.ToString());
                }
#endif
            }
        }

        return false;
    }

    protected bool TriggerEvent(IEvent evt)
    {
        return (null != evt) && TriggerEvent(evt.GetKey(), evt.GetParam1(), evt.GetParam2());
    }

    protected bool _DispatchEvent(uint key, object param1, object param2)
    {
        return TriggerEvent(key, param1, param2);
    }

    protected bool _DispatchEvent(IEvent evt)
    {
        return (null != evt) && _DispatchEvent(evt.GetKey(), evt.GetParam1(), evt.GetParam2());
    }

    private void DispatchEvent()
    {
        //c#的lock允许同一线程多次进入//
        lock (m_evtQueueLock)
        {
            //发布自己的event
            while (m_eventQueueNow.Count > 0)
            {
                IEvent evt = m_eventQueueNow.Dequeue() as IEvent;
                _DispatchEvent(evt);
            }
        }
            
        //交换event缓冲//
        lock (m_evtQueueLock)
        {
            SwapNowAndNextEventQueue();
        }
    }

    /// <summary>
    /// 更新逻辑核层级挂接
    /// </summary>
    //private float mLastUpdateTime = 0.0f;
    //static private float msUpdateInternal = 10.0f;
    public void Update()
    {
        UpdateListenerMap();
        DispatchEvent();
    }

    /// <summary>
    /// 线程安全,允许多线程调用，下帧抛出
    /// </summary>
    public void FireAsynchorEvent(uint key, object param1, object param2)
    {
        lock (m_evtQueueLock)
        {
            m_eventQueueNext.Enqueue(new GameEvent(key, param1, param2));
        }
    }

    /// <summary>
    /// 立刻抛出事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <returns>事件是否被监听者拦截</returns>
    public bool FireSynchorEvent(uint key, object param1, object param2)
    {
        bool ret = false;
        lock (m_evtQueueLock)
        {
            ret = _DispatchEvent(key, param1, param2);
        }
        return ret;
    }

    public void Clear()
    {
        m_listenerTable.Clear();
        m_listenersToUpdateCache.Clear();
            
        m_eventQueueNow.Clear();
        m_eventQueueNext.Clear();
    }
}