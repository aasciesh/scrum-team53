//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace ScrumMainApp.Models
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(BacklogItem))]
    [KnownType(typeof(Status))]
    [KnownType(typeof(User))]
    public partial class Task: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int taskId
        {
            get { return _taskId; }
            set
            {
                if (_taskId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'taskId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _taskId = value;
                    OnPropertyChanged("taskId");
                }
            }
        }
        private int _taskId;
    
        [DataMember]
        public int backlogId
        {
            get { return _backlogId; }
            set
            {
                if (_backlogId != value)
                {
                    ChangeTracker.RecordOriginalValue("backlogId", _backlogId);
                    if (!IsDeserializing)
                    {
                        if (BacklogItem != null && BacklogItem.backlogId != value)
                        {
                            BacklogItem = null;
                        }
                    }
                    _backlogId = value;
                    OnPropertyChanged("backlogId");
                }
            }
        }
        private int _backlogId;
    
        [DataMember]
        public int userId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    ChangeTracker.RecordOriginalValue("userId", _userId);
                    if (!IsDeserializing)
                    {
                        if (User != null && User.userId != value)
                        {
                            User = null;
                        }
                    }
                    _userId = value;
                    OnPropertyChanged("userId");
                }
            }
        }
        private int _userId;
    
        [DataMember]
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("description");
                }
            }
        }
        private string _description;
    
        [DataMember]
        public Nullable<int> timeEstimate
        {
            get { return _timeEstimate; }
            set
            {
                if (_timeEstimate != value)
                {
                    _timeEstimate = value;
                    OnPropertyChanged("timeEstimate");
                }
            }
        }
        private Nullable<int> _timeEstimate;
    
        [DataMember]
        public Nullable<int> actualTimeUse
        {
            get { return _actualTimeUse; }
            set
            {
                if (_actualTimeUse != value)
                {
                    _actualTimeUse = value;
                    OnPropertyChanged("actualTimeUse");
                }
            }
        }
        private Nullable<int> _actualTimeUse;
    
        [DataMember]
        public string status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    ChangeTracker.RecordOriginalValue("status", _status);
                    if (!IsDeserializing)
                    {
                        if (Status1 != null && Status1.status1 != value)
                        {
                            Status1 = null;
                        }
                    }
                    _status = value;
                    OnPropertyChanged("status");
                }
            }
        }
        private string _status;
    
        [DataMember]
        public System.DateTime startDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("startDate");
                }
            }
        }
        private System.DateTime _startDate;
    
        [DataMember]
        public Nullable<System.DateTime> endDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged("endDate");
                }
            }
        }
        private Nullable<System.DateTime> _endDate;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public BacklogItem BacklogItem
        {
            get { return _backlogItem; }
            set
            {
                if (!ReferenceEquals(_backlogItem, value))
                {
                    var previousValue = _backlogItem;
                    _backlogItem = value;
                    FixupBacklogItem(previousValue);
                    OnNavigationPropertyChanged("BacklogItem");
                }
            }
        }
        private BacklogItem _backlogItem;
    
        [DataMember]
        public Status Status1
        {
            get { return _status1; }
            set
            {
                if (!ReferenceEquals(_status1, value))
                {
                    var previousValue = _status1;
                    _status1 = value;
                    FixupStatus1(previousValue);
                    OnNavigationPropertyChanged("Status1");
                }
            }
        }
        private Status _status1;
    
        [DataMember]
        public User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                    OnNavigationPropertyChanged("User");
                }
            }
        }
        private User _user;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            BacklogItem = null;
            Status1 = null;
            User = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupBacklogItem(BacklogItem previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Tasks.Contains(this))
            {
                previousValue.Tasks.Remove(this);
            }
    
            if (BacklogItem != null)
            {
                if (!BacklogItem.Tasks.Contains(this))
                {
                    BacklogItem.Tasks.Add(this);
                }
    
                backlogId = BacklogItem.backlogId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("BacklogItem")
                    && (ChangeTracker.OriginalValues["BacklogItem"] == BacklogItem))
                {
                    ChangeTracker.OriginalValues.Remove("BacklogItem");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("BacklogItem", previousValue);
                }
                if (BacklogItem != null && !BacklogItem.ChangeTracker.ChangeTrackingEnabled)
                {
                    BacklogItem.StartTracking();
                }
            }
        }
    
        private void FixupStatus1(Status previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Tasks.Contains(this))
            {
                previousValue.Tasks.Remove(this);
            }
    
            if (Status1 != null)
            {
                if (!Status1.Tasks.Contains(this))
                {
                    Status1.Tasks.Add(this);
                }
    
                status = Status1.status1;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Status1")
                    && (ChangeTracker.OriginalValues["Status1"] == Status1))
                {
                    ChangeTracker.OriginalValues.Remove("Status1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Status1", previousValue);
                }
                if (Status1 != null && !Status1.ChangeTracker.ChangeTrackingEnabled)
                {
                    Status1.StartTracking();
                }
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Tasks.Contains(this))
            {
                previousValue.Tasks.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.Tasks.Contains(this))
                {
                    User.Tasks.Add(this);
                }
    
                userId = User.userId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("User")
                    && (ChangeTracker.OriginalValues["User"] == User))
                {
                    ChangeTracker.OriginalValues.Remove("User");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("User", previousValue);
                }
                if (User != null && !User.ChangeTracker.ChangeTrackingEnabled)
                {
                    User.StartTracking();
                }
            }
        }

        #endregion
    }
}
