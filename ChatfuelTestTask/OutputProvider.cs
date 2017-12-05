using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	public class OutputProvider
	{
		public void Add( LiftEvent item )
		{
			throw new NotImplementedException();
		}

		public Task<LiftEvent> TakeAsync( CancellationToken cancellationToken )
		{
			throw new NotImplementedException();
		}
	}
}
