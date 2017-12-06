using System;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	public class Lift
    {
		private readonly OutputProvider _out = new OutputProvider();
		private readonly TimeSpan _doorsFreeze;
		private readonly TimeSpan _floorFreeze;
		private readonly byte _numberOfStoreys;

		internal Lift( Config config )
		{
			_doorsFreeze = TimeSpan.FromSeconds( config.LiftDoorsOpenCloseTime );
			_floorFreeze = TimeSpan.FromSeconds( ( (double) config.StoreyHeight ) / config.LiftSpeed );
			_numberOfStoreys = config.NumberOfStoreys;
		}

		public Task CallFromOutside( byte floor )
		{
			if ( floor < 1 || floor > _numberOfStoreys )
				return Task.FromException( new ArgumentOutOfRangeException() );
			return Task.FromException( new NotImplementedException() );
		}

		public Task DirectFromInside( byte floor )
		{
			if ( floor < 1 || floor > _numberOfStoreys )
				return Task.FromException( new ArgumentOutOfRangeException() );
			return Task.FromException( new NotImplementedException() );
		}
	}
}
