using System;
using System.Runtime.Caching;
using DLMS.Comm;
using System.Diagnostics;

namespace DLMS.LRUCache
{
    public delegate DLMSCachePriority DLMSCalculatePriority(KeyIndexer OBjIndex);

    public class LRUPriorityDLMSCache : IDisposable
    {
        #region DataMembers

        private ObjectCache sharedObjectCache = null;
        private CacheItemPolicy hPriorityCacheItemPolicy = null;
        private CacheItemPolicy lPriorityCacheItemPolicy = null;
        private DLMSCalculatePriority priorityComputerDlg;
        private GetSAPEntryKeyIndex GetDLMSClassDelg;

        #endregion

        #region Constructor

        public LRUPriorityDLMSCache(TimeSpan minAge, TimeSpan maxAge)
        {
            try
            {
                sharedObjectCache = new MemoryCache("MyApplicationObjectCacheTester");
                ((MemoryCache)sharedObjectCache).Trim(100);

                Debug.WriteLine(String.Format("Memory Cache Limits {0} MB_  {1}  _{2} Roll_Interval Percent",
                ((MemoryCache)sharedObjectCache).CacheMemoryLimit, ((MemoryCache)sharedObjectCache).PhysicalMemoryLimit, ((MemoryCache)sharedObjectCache).PollingInterval));

                lPriorityCacheItemPolicy = new CacheItemPolicy();
                hPriorityCacheItemPolicy = new CacheItemPolicy();

                lPriorityCacheItemPolicy.SlidingExpiration = minAge;
                hPriorityCacheItemPolicy.SlidingExpiration = minAge;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LRUPriorityDLMSCache(TimeSpan minAge, TimeSpan maxAge, DLMSCalculatePriority PriorityComputer,
            GetSAPEntryKeyIndex DLMSClassMakerDlg)
            : this(minAge, maxAge)
        {
            try
            {
                GetDLMSClassDelg = new GetSAPEntryKeyIndex(DLMSClassMakerDlg);
                priorityComputerDlg = new DLMSCalculatePriority(this.DefaultPriorityComputer);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        public ObjectCache SharedObjectCache
        {
            get { return sharedObjectCache; }
            set { sharedObjectCache = value; }
        }

        public DLMSCalculatePriority PriorityComputerDelegate
        {
            get { return priorityComputerDlg; }
            set { priorityComputerDlg = value; }
        }

        public GetSAPEntryKeyIndex GetDLMSClassDelegate
        {
            get { return GetDLMSClassDelg; }
            set { GetDLMSClassDelg = value; }
        }

        public CacheItemPolicy HighPriorityCacheItemPolicy
        {
            get { return hPriorityCacheItemPolicy; }
            set { hPriorityCacheItemPolicy = value; }
        }

        public CacheItemPolicy LowPriorityCacheItemPolicy
        {
            get { return lPriorityCacheItemPolicy; }
            set { lPriorityCacheItemPolicy = value; }
        }

        #endregion

        #region Member Methods

        public Base_Class GetBaseObject(Get_Index OBISIndex)
        {
            return GetBaseObject(new KeyIndexer(OBISIndex));
        }

        public Base_Class GetBaseObject(KeyIndexer OBISIndex)
        {
            Base_Class obj = null;
            try
            {
                try
                {
                    ///Synchronization Read Monitor Lock
                    try
                    {
                        obj = SharedObjectCache[OBISIndex.Key] as Base_Class;
                    }
                    finally
                    {

                    }
                }
                catch (Exception)
                {
                    obj = null;
                }
                if (obj == null)
                {
                    obj = GetDLMSClassDelegate.Invoke(OBISIndex);
                    if (obj == null)
                        throw new Exception(String.Format("Unable to create DLMS object of {0}", OBISIndex.ObisCode));
                    ///Synchronization Write Monitor Lock
                    ///if (Monitor.TryEnter(this, Commons.WriteLOCK_TimeOut))
                    //{
                    try
                    {
                        //DLMSCachePriority PR = PriorityComputerDelegate.Invoke(OBISIndex);
                        //if (PR == DLMSCachePriority.HighPriority)
                        //{
                        //    SharedObjectCache.Remove(OBISIndex.Key);
                        //    SharedObjectCache.Add(new CacheItem(OBISIndex.Key, obj), HighPriorityCacheItemPolicy);
                        //}
                        //else
                        //{
                        SharedObjectCache.Remove(OBISIndex.Key);
                        SharedObjectCache.Add(new CacheItem(OBISIndex.Key, obj), LowPriorityCacheItemPolicy);
                        //}
                    }
                    finally
                    {
                        ///Monitor.Exit(this);
                    }
                    //}
                    //else
                    //    throw new SynchronizationLockException("Unable to obtain Write Lock GetBaseObject_LRUPriorityDLMSCache");
                    return obj;
                }
                else
                    return obj;

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to retrive DLMS object of {0}", OBISIndex.ObisCode), ex);
            }
        }

        #endregion

        #region Support_Methods

        public DLMSCachePriority DefaultPriorityComputer(KeyIndexer OBISIndex)
        {
            try
            {
                if (OBISIndex.ObisCode.ClassId == 7 || OBISIndex.ObisCode.ClassId == 17)
                {
                    return DLMSCachePriority.HighPriority;
                }
                else
                    return DLMSCachePriority.LowPriority;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FlushAll()
        {
            try
            {

                if (SharedObjectCache != null)
                    ((MemoryCache)sharedObjectCache).Trim(100);

            }
            catch { }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (SharedObjectCache != null && SharedObjectCache is IDisposable)
                {
                    ((IDisposable)SharedObjectCache).Dispose();
                }
                SharedObjectCache = null;
                hPriorityCacheItemPolicy = null;
                lPriorityCacheItemPolicy = null;
            }
            catch (Exception) { }
        }

        #endregion
    }

    public enum DLMSCachePriority : byte
    {
        HighPriority = 1,
        LowPriority = 2
    }

    public struct KeyIndexer
    {
        #region Data_Members

        private StOBISCode obisCode;
        private ushort? ownerId;
        private ushort? instanceId;

        #endregion

        #region Properties

        public StOBISCode ObisCode
        {
            get { return obisCode; }
            set { obisCode = value; }
        }

        public ushort? OwnerId
        {
            get { return ownerId; }
            set { ownerId = value; }
        }

        public ushort? InstanceId
        {
            get { return instanceId; }
            set { instanceId = value; }
        }

        public string Key
        {
            get
            {
                try
                {
                    return ObisCode.ToString() + String.Format("_{0}_", ((OwnerId != null) ? OwnerId.ToString() : string.Empty)) +
                                String.Format("_{0}", ((InstanceId != null) ? InstanceId.ToString() : string.Empty));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Construcutur

        public KeyIndexer(StOBISCode OBISCode)
        {
            obisCode = OBISCode;
            ownerId = null;
            instanceId = null;
        }

        public KeyIndexer(StOBISCode OBISCode, ushort? OwnerId)
            : this(OBISCode)
        {
            this.ownerId = OwnerId;
        }

        public KeyIndexer(StOBISCode OBISCode, ushort? OwnerId, ushort? instanceId)
            : this(OBISCode, OwnerId)
        {
            this.instanceId = instanceId;
        }

        #endregion

        public static implicit operator KeyIndexer(StOBISCode value)
        {
            return new KeyIndexer { obisCode = value };
        }

        #region Over_Ridden_Methods

        public override bool Equals(object obj)
        {
            if (obj is KeyIndexer)
            {
                KeyIndexer index = (KeyIndexer)obj;
                bool flag = (OwnerId == index.OwnerId);
                bool flag_1 = (InstanceId == index.InstanceId);
                return obisCode.Equals(index.ObisCode) && flag && flag_1;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            try
            {
                return ObisCode.ToString() + String.Format("_{0}_", ((OwnerId != null) ? OwnerId.ToString() : string.Empty)) +
                    String.Format("_{0}", ((InstanceId != null) ? InstanceId.ToString() : string.Empty));
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        #endregion
    }

}
