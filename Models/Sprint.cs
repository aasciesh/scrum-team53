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
    [KnownType(typeof(Project))]
    public partial class Sprint: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int sprintId
        {
            get { return _sprintId; }
            set
            {
                if (_sprintId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'sprintId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _sprintId = value;
                    OnPropertyChanged("sprintId");
                }
            }
        }
        private int _sprintId;
    
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
        public string sprintNumber
        {
            get { return _sprintNumber; }
            set
            {
                if (_sprintNumber != value)
                {
                    _sprintNumber = value;
                    OnPropertyChanged("sprintNumber");
                }
            }
        }
        private string _sprintNumber;
    
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
        public System.DateTime endDate
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
        private System.DateTime _endDate;
    
        [DataMember]
        public string sprintGoal
        {
            get { return _sprintGoal; }
            set
            {
                if (_sprintGoal != value)
                {
                    _sprintGoal = value;
                    OnPropertyChanged("sprintGoal");
                }
            }
        }
        private string _sprintGoal;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<BacklogItem> BacklogItems
        {
            get
            {
                if (_backlogItems == null)
                {
                    _backlogItems = new TrackableCollection<BacklogItem>();
                    _backlogItems.CollectionChanged += FixupBacklogItems;
                }
                return _backlogItems;
            }
            set
            {
                if (!ReferenceEquals(_backlogItems, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_backlogItems != null)
                    {
                        _backlogItems.CollectionChanged -= FixupBacklogItems;
                    }
                    _backlogItems = value;
                    if (_backlogItems != null)
                    {
                        _backlogItems.CollectionChanged += FixupBacklogItems;
                    }
                    OnNavigationPropertyChanged("BacklogItems");
                }
            }
        }
        private TrackableCollection<BacklogItem> _backlogItems;
    
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
            BacklogItems.Clear();
            Project = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupProject(Project previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Sprints.Contains(this))
            {
                previousValue.Sprints.Remove(this);
            }
    
            if (Project != null)
            {
                if (!Project.Sprints.Contains(this))
                {
                    Project.Sprints.Add(this);
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
    
        private void FixupBacklogItems(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (BacklogItem item in e.NewItems)
                {
                    item.Sprint = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("BacklogItems", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (BacklogItem item in e.OldItems)
                {
                    if (ReferenceEquals(item.Sprint, this))
                    {
                        item.Sprint = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("BacklogItems", item);
                    }
                }
            }
        }

        #endregion
    }
}