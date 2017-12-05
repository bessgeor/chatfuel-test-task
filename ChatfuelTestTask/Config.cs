using System;

namespace ChatfuelTestTask
{
	internal struct Config : IEquatable<Config>
	{
		public byte NumberOfStoreys { get; }
		public byte StoreyHeight { get; }
		public byte LiftSpeed { get; }
		public byte LiftDoorsOpenCloseTime { get; }

		public static Config FromArgs( string[] args )
		{
			if ( args.Length == 4 )
			{
				byte numberOfStoreys = Byte.Parse( args[ 0 ] );
				if ( numberOfStoreys < 5 || numberOfStoreys > 20 )
					throw new ArgumentOutOfRangeException( nameof( numberOfStoreys ) );
				byte storeyHeight = Byte.Parse( args[ 1 ] );
				byte liftSpeed = Byte.Parse( args[ 2 ] );
				byte liftDoorsOpenCloseTime = Byte.Parse( args[ 3 ] );
				return new Config( numberOfStoreys, storeyHeight, liftSpeed, liftDoorsOpenCloseTime );
			}
			return new Config( numberOfStoreys: 10, storeyHeight: 1, liftSpeed: 1, liftDoorsOpenCloseTime: 1 );
		}

		private Config( byte numberOfStoreys, byte storeyHeight, byte liftSpeed, byte liftDoorsOpenCloseTime )
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
}
