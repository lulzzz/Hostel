﻿using Akka.Actor;
using Hostel.Command.Create;
using Hostel.Command.Internal;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class RoomManagerActor : HostelActor<RoomManagerState>
    {
        private string _connectionString;
        public RoomManagerActor(ICommandHandler<RoomManagerState> handler, RoomManagerState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
            Command<LayoutRoom>(room => {
                CreateRooms(State);
            });
        }
        protected override void OnSnapshotOffer(RoomManagerState state)
        {
            CreateRooms(state);
            base.OnSnapshotOffer(state);
        }
        protected override void PreStart()
        {
            base.PreStart();
        }
        protected override void OnPersist(IEvent persistedEvent)
        {
            base.OnPersist(persistedEvent);
        }
        public static Props Prop(ICommandHandler<RoomManagerState> handler, RoomManagerState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new RoomManagerActor(handler, defaultState, persistenceId, connectionString));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
        private void CreateRooms(RoomManagerState state)
        {
            var rooms = state.Rooms;
            foreach (var rm in rooms)
            {
                var createRoom = new CreateRoom(rm);
                Self.Tell(createRoom);
            }
        }
    }
}
