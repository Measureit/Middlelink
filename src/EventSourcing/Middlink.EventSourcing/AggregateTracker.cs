﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Middlink.EventSourcing
{
    public class AggregateTracker
    {
        private readonly ConcurrentDictionary<Type, Dictionary<Guid, object>> _track = new ConcurrentDictionary<Type, Dictionary<Guid, object>>();

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : AggregateRoot
        {
            Dictionary<Guid, object> aggregates;
            if (!_track.TryGetValue(typeof(TAggregate), out aggregates))
                return null;

            object aggregate;
            if (!aggregates.TryGetValue(id, out aggregate))
                return null;

            return (TAggregate)aggregate;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : AggregateRoot
        {
            Dictionary<Guid, object> aggregates;
            if (!_track.TryGetValue(typeof(TAggregate), out aggregates))
            {
                aggregates = new Dictionary<Guid, object>();
                _track.TryAdd(typeof(TAggregate), aggregates);
            }

            if (aggregates.ContainsKey(aggregateRoot.Id))
                return;

            aggregates.Add(aggregateRoot.Id, aggregateRoot);
        }

        public void Remove(Type aggregateType, Guid aggregateId)
        {
            Dictionary<Guid, object> aggregates;
            if (!_track.TryGetValue(aggregateType, out aggregates))
                return;

            aggregates.Remove(aggregateId);
        }
    }
}
