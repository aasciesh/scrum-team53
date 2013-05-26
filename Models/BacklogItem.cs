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
    [KnownType(typeof(Project))]
    [KnownType(typeof(Sprint))]
    [KnownType(typeof(Status))]
    [KnownType(typeof(Task))]
    public partial class BacklogItem: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int backlogId
        {
            get { return _backlogId; }
            set
            {
                if (_backlogId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'backlogId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _backlogId = value;
                    OnPropertyChanged("backlogId");
                }
            }
        }
        private int _backlogId;
    
        [DataMember]
        public int projectId
        {
            get { return _projectId; }
            set
            {
                if (_projectId != value)
                {
                    ChangeTracker.RecordOriginalValue("projectId", _projectId);
                    if (!IsDeserializing)
                    {
                        if (Project != null && Project.projectId != value)
                        {
                            Project = null;
                        }
                    }
                    _projectId = value;
                    OnPropertyChanged("projectId");
                }
            }
        }
        private int _projectId;
    
        [DataMember]
        public Nullable<int> sprintId
        {
            get { return _sprintId; }
            set
            {
                if (_sprintId != value)
                {
                    ChangeTracker.RecordOriginalValue("sprintId", _sprintId);
                    if (!IsDeserializing)
                    {
                        if (Sprint != null && Sprint.sprintId != value)
                        {
                            Sprint = null;
                        }
                    }
                    _sprintId = value;
                    OnPropertyChanged("sprintId");
                }
            }
        }
        private Nullable<int> _sprintId;
    
        [DataMember]
        public string story
        {
            get { return _story; }
            set
            {
                if (_story != value)
                {
                    _story = value;
                    OnPropertyChanged("story");
                }
            }
        }
        private string _story;
    
        [DataMember]
        public int estimatedSize
        {
            get { return _estimatedSize; }
            set
            {
                if (_estimatedSize != value)
                {
                    _estimatedSize = value;
                    OnPropertyChanged("estimatedSize");
                }
            }
        }
        private int _estimatedSize;
    
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
        public int priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged("priority");
                }
            }
        }
        private int _priority;
    
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

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Project Project
        {
            get { return _project; }
            set
            {
                if (!ReferenceEquals(_project, value))
                {
                    var previousValue = _project;
                    _project = value;
                    FixupProject(previousValue);
                    OnNavigationPropertyChanged("Project");
                }
            }
        }
        private Project _project;
    
        [DataMember]
        public Sprint Sprint
        {
            get { return _sprint; }
            set
            {
                if (!ReferenceEquals(_sprint, value))
                {
                    var previousValue = _sprint;
                    _sprint = value;
                    FixupSprint(previousValue);
                    OnNavigationPropertyChanged("Sprint");
                }
            }
        }
        private Sprint _sprint;
    
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
        public TrackableCollection<Task> Tasks
        {
            get
            {
                if (_tasks == null)
                {
                    _tasks = new TrackableCollection<Task>();
                    _tasks.CollectionChanged += FixupTasks;
                }
                return _tasks;
            }
            set
            {
                if (!ReferenceEquals(_tasks, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tasks != null)
                    {
                        _tasks.CollectionChanged -= FixupTasks;
                    }
                    _tasks = value;
                    if (_tasks != null)
                    {
                        _tasks.CollectionChanged += FixupTasks;
                    }
                    OnNavigationPropertyChanged("Tasks");
                }
            }
        }
        private TrackableCollection<Task> _tasks;

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
            Project = null;
            Sprint = null;
            Status1 = null;
            Tasks.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupProject(Project previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.BacklogItems.Contains(this))
            {
                previousValue.BacklogItems.Remove(this);
            }
    
            if (Project != null)
            {
                if (!Project.BacklogItems.Contains(this))
                {
                    Project.BacklogItems.Add(this);
                }
    
                projectId = Project.projectId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Project")
                    && (ChangeTracker.OriginalValues["Project"] == Project))
                {
                    ChangeTracker.OriginalValues.Remove("Project");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Project", previousValue);
                }
                if (Project != null && !Project.ChangeTracker.ChangeTrackingEnabled)
                {
                    Project.StartTracking();
                }
            }
        }
    
        private void FixupSprint(Sprint previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.BacklogItems.Contains(this))
            {
                previousValue.BacklogItems.Remove(this);
            }
    
            if (Sprint != null)
            {
                if (!Sprint.BacklogItems.Contains(this))
                {
                    Sprint.BacklogItems.Add(this);
                }
    
                sprintId = Sprint.sprintId;
            }
            else if (!skipKeys)
            {
                sprintId = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Sprint")
                    && (ChangeTracker.OriginalValues["Sprint"] == Sprint))
                {
                    ChangeTracker.OriginalValues.Remove("Sprint");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Sprint", previousValue);
                }
                if (Sprint != null && !Sprint.ChangeTracker.ChangeTrackingEnabled)
                {
                    Sprint.StartTracking();
                }
            }
        }
    
        private void FixupStatus1(Status previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.BacklogItems.Contains(this))
            {
                previousValue.BacklogItems.Remove(this);
            }
    
            if (Status1 != null)
            {
                if (!Status1.BacklogItems.Contains(this))
                {
                    Status1.BacklogItems.Add(this);
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
    
        private void FixupTasks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Task item in e.NewItems)
                {
                    item.BacklogItem = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Tasks", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Task item in e.OldItems)
                {
                    if (ReferenceEquals(item.BacklogItem, this))
                    {
                        item.BacklogItem = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Tasks", item);
                    }
                }
            }
        }

        #endregion
    }
}