using System;

namespace ChatfuelTestTask
{
	internal struct Config : IEquatable<Config>
	{
		public byte NumberOfStoreys { get; }
		public byte StoreyHeight { get; }
		public byte LiftSpeed { get; }
		public byte LiftDoorsOpenCloseTime { get; }

		public Config( byte numberOfStoreys, byte storeyHeight, byte liftSpeed, byte liftDoorsOpenCloseTime )
		{
			NumberOfStoreys = numberOfStoreys;
			StoreyHeight = storeyHeight;
			LiftSpeed = liftSpeed;
			LiftDoorsOpenCloseTime = liftDoorsOpenCloseTime;
		}

		#region equalability
		public override int GetHashCode()
		{
			unchecked
			{
				const int prime = -1521134295;
				int hash = 12345701;
				hash = hash * prime + System.Collections.Generic.EqualityComparer<byte>.Default.GetHashCode( NumberOfStoreys );
				hash = hash * prime + System.Collections.Generic.EqualityComparer<byte>.Default.GetHashCode( StoreyHeight );
				hash = hash * prime + System.Collections.Generic.EqualityComparer<byte>.Default.GetHashCode( LiftSpeed );
				hash = hash * prime + System.Collections.Generic.EqualityComparer<byte>.Default.GetHashCode( LiftDoorsOpenCloseTime );
				return hash;
			}
		}

		public bool Equals( Config other ) => NumberOfStoreys.Equals( other.NumberOfStoreys ) && StoreyHeight.Equals( other.StoreyHeight ) && LiftSpeed.Equals( other.LiftSpeed ) && LiftDoorsOpenCloseTime.Equals( other.LiftDoorsOpenCloseTime );
		public override bool Equals( object obj ) => obj is Config config && Equals( config );

		public static bool operator ==( Config x, Config y ) => x.Equals( y );
		public static bool operator !=( Config x, Config y ) => !x.Equals( y );
		#endregion
	}

	internal class Program
	{
		public static void Main( string[] args )
		{
		}
	}
}
