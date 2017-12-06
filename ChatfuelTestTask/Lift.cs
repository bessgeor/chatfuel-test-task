using System;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	public class Lift
    {
		private readonly OutputProvider _out = new OutputProvider();
		private readonly TimeSpan _doorsFreeze;
		private readonly TimeSpan _floorFreeze;
		private readonly int _numberOfStoreys;

		internal Lift( Config config )
		{
			_doorsFreeze = TimeSpan.FromSeconds( config.LiftDoorsOpenCloseTime );
			_floorFreeze = TimeSpan.FromSeconds( ( (double) config.StoreyHeight ) / config.LiftSpeed );
			_numberOfStoreys = config.NumberOfStoreys;
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
