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
    bool OnFireEvent(uint key, object param1, object param2);

    int GetListenerPriority(uint eventKey);

    void AttachEvent();
    void DetachEvent();
}

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
                {
                    Debug.Log("EventDispatcher Print");
                }
            }
        }
    }

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

    public void AttachListenerNext(IEventListener listener, uint eventKey)
    {
        lock (m_listenersToUpdateCache)
        {
            m_listenersToUpdateCache.Enqueue(new ListenerPack(listener, eventKey, true));
        }
    }

    public void AttachListenerNow(IEventListener listener, uint eventKey)
    {
#if EVENT_DEBUG
    EventDebug("AttachListenerNow");
#endif
        if (null == listener || 0 == eventKey)
        {
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
            Debug.Log("EventDispatcher AttachListenerNow");
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

    public bool ListenerAttached(IEventListener listener, uint eventKey)
    {
        return m_listenerTable.ContainsKey(eventKey);
    }

    public void DetachListenerNext(IEventListener listener, uint eventKey)
    {
        lock (m_listenersToUpdateCache)
        {
            m_listenersToUpdateCache.Enqueue(new ListenerPack(listener, eventKey, false));
        }
    }

    public void DetachListenerNow(IEventListener listener, uint eventKey)
    {
#if EVENT_DEBUG
    EventDebug("DetachListenerNow");
#endif
        if (0 == eventKey)
        {
            Debug.LogError("EventDispatcher DetachListenerNow");
            return;
        }
        if (!m_listenerTable.ContainsKey(eventKey))
            return;

        var listenerList = m_listenerTable[eventKey];

        foreach (WeakReference listenerRef in listenerList)
        {
            if (listenerRef.Target == listener)
            {
                listenerRef.Target = null;
                if (!m_DelingEventArray.Contains(eventKey))
                    m_DelingEventArray.Add(eventKey);
                break;
            }
        }
    }

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
        lock (m_evtQueueLock)
        {
            while (m_eventQueueNow.Count > 0)
            {
                IEvent evt = m_eventQueueNow.Dequeue() as IEvent;
                _DispatchEvent(evt);
            }
        }
        lock (m_evtQueueLock)
        {
            SwapNowAndNextEventQueue();
        }
    }

    public void Update()
    {
        UpdateListenerMap();
        DispatchEvent();
    }

    public void FireAsynchorEvent(uint key, object param1, object param2)
    {
        lock (m_evtQueueLock)
        {
            m_eventQueueNext.Enqueue(new GameEventManager(key, param1, param2));
        }
    }

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