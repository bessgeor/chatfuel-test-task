using HellBrick.Collections;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	public class Lift
    {
		private enum State
		{
			Immobile,
			MoveUp,
			MoveDown,
			OpenDoors,
			CloseDoors
		}

		private readonly OutputProvider _out = new OutputProvider();
		private readonly TimeSpan _doorsFreeze;
		private readonly TimeSpan _floorFreeze;
		private readonly int _numberOfStoreys;
		private readonly AsyncBoundedPriorityQueue<int> _q;

		private volatile int _currentFloor = 1;
		private volatile int _currentTarget = 0;
		private volatile State _state = State.Immobile;
		private TaskCompletionSource<int> _stateChangeTcs = new TaskCompletionSource<int>();

		internal Lift( Config config )
		{
			_doorsFreeze = TimeSpan.FromSeconds( config.LiftDoorsOpenCloseTime );
			_floorFreeze = TimeSpan.FromSeconds( ( (double) config.StoreyHeight ) / config.LiftSpeed );
			_numberOfStoreys = config.NumberOfStoreys;
			_q = new AsyncBoundedPriorityQueue<int>( _numberOfStoreys, floor => ResolvePriority( floor ) );
			Task.Run( () => HandleOutput() );
		}

		private int ResolvePriority( int floor )
		{
			int currentTarget = _currentTarget;
			int currentFloor = _currentFloor;
			if ( currentTarget == -1 || ( floor == currentFloor  )
				return 0;

		}
		
		private async Task HandleOutput()
		{
			TaskCompletionSource<int> stateChangeTcs = _stateChangeTcs;
			stateChangeTcs.SetResult( 0 );
			while ( true )
			{
				await stateChangeTcs.Task.ConfigureAwait( false );
				Interlocked.CompareExchange( ref stateChangeTcs, new TaskCompletionSource<int>(), stateChangeTcs );
				PrioritizedItem<int> prioritized = await _q.TakeAsync().ConfigureAwait( false );
				int closest = prioritized.Item;
				if ( _currentFloor == closest && _state == State.Immobile )
				{
					await OpenDoors( closest ).ConfigureAwait( false );
					await DrainCurrentFloor().ConfigureAwait( false ); // let the people in and out
					await CloseDoors( closest ).ConfigureAwait( false );
					_state = State.Immobile;
				}
				Task DrainCurrentFloor()
				{
					return _q.Count > 0
						? DrainCurrentFloorInternal()
						: Task.CompletedTask;

					async Task DrainCurrentFloorInternal()
					{
						int i = 0, last;
						do
						{
							prioritized = await _q.TakeAsync().ConfigureAwait( false );
							last = prioritized.Item;
							i++;
						}
						while ( last == closest && _q.Count > 0 );
						if ( last != closest )
							_q.AddTopPriority( last );
					}
				}
			}
			async Task OpenDoors( int floor )
			{
				stateChangeTcs.SetResult( 0 );
				_state = State.OpenDoors;
				await Task.Delay( _doorsFreeze ).ConfigureAwait( false );
				_out.Write( new LiftEvent( LiftEventType.DoorsOpened, floor ) );
			}
			async Task CloseDoors( int floor )
			{
				_state = State.CloseDoors;
				await Task.Delay( _doorsFreeze ).ConfigureAwait( false );
				_out.Write( new LiftEvent( LiftEventType.DoorsClosed, floor ) );
			}
			async Task MoveUp()
			{
				_state = State.MoveUp;
				int floor = _currentFloor + 1;
				await Task.Delay( _floorFreeze ).ConfigureAwait( false );
				_out.Write( new LiftEvent( LiftEventType.ArrivedToFloor, floor ) );
				_currentFloor = floor;
			}
			async Task MoveDown()
			{
				_state = State.MoveDown;
				int floor = _currentFloor - 1;
				await Task.Delay( _floorFreeze ).ConfigureAwait( false );
				_out.Write( new LiftEvent( LiftEventType.ArrivedToFloor, floor ) );
				_currentFloor = floor;
			}
		}

		public Task CallFromOutside( int floor )
		{
			if ( floor < 1 || floor > _numberOfStoreys )
				return Task.FromException( new ArgumentOutOfRangeException() );
			return Task.FromException( new NotImplementedException() );
		}

		public Task DirectFromInside( int floor )
		{
			if ( floor < 1 || floor > _numberOfStoreys )
				return Task.FromException( new ArgumentOutOfRangeException() );
			return Task.FromException( new NotImplementedException() );
		}
	}
}
