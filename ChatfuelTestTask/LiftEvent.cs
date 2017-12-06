using System;

namespace ChatfuelTestTask
{
	public enum LiftEventType
	{
		ArrivedToFloor,
		DoorsOpened,
		DoorsClosed
	}

    public struct LiftEvent : IEquatable<LiftEvent>
	{
		public DateTime OccuredAt { get; }
		public LiftEventType Type { get; }
		public int Floor { get; }

		public LiftEvent( LiftEventType type, int floor )
		{
			OccuredAt = DateTime.UtcNow;
			Floor = floor;
			Type = type;
		}

		/// <summary>
		/// Outputs itself to a console. Should only be used from outside of ThreadPool because of thread locking
		/// </summary>
		public void WriteToConsoleLocking() // ToString() are called to avoid boxing
			=> Console.WriteLine( $"{OccuredAt.Hour.ToString()}:{OccuredAt.Minute.ToString()}:{OccuredAt.Second.ToString()}.{OccuredAt.Millisecond.ToString()} UTC: on floor {Floor.ToString()} {Type.ToString()}" );

		public override int GetHashCode()
		{
			unchecked
			{
				const int prime = -1521134295;
				int hash = 12345701;
				hash = hash * prime + System.Collections.Generic.EqualityComparer<DateTime>.Default.GetHashCode( OccuredAt );
				hash = hash * prime + System.Collections.Generic.EqualityComparer<LiftEventType>.Default.GetHashCode( Type );
				hash = hash * prime + System.Collections.Generic.EqualityComparer<int>.Default.GetHashCode( Floor );
				return hash;
			}
		}

		public bool Equals( LiftEvent other ) => OccuredAt == other.OccuredAt && System.Collections.Generic.EqualityComparer<LiftEventType>.Default.Equals( Type, other.Type ) && System.Collections.Generic.EqualityComparer<int>.Default.Equals( Floor, other.Floor );
		public override bool Equals( object obj ) => obj is LiftEvent && Equals( (LiftEvent) obj );

		public static bool operator ==( LiftEvent x, LiftEvent y ) => x.Equals( y );
		public static bool operator !=( LiftEvent x, LiftEvent y ) => !x.Equals( y );
	}
}
