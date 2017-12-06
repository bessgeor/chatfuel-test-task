using HellBrick.Collections;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	public class OutputProvider
	{
		private readonly AsyncQueue<LiftEvent> _events = new AsyncQueue<LiftEvent>();

		public OutputProvider()
		{
			Task.Run( async () =>
			{
				while ( true )
				{
					LiftEvent @event = await _events.TakeAsync().ConfigureAwait( false );
					@event.WriteToConsoleLocking(); // is pretty OK since the output is fast
				}
			} );
		}

		public void Write( LiftEvent item )
			=> _events.Add( item );
	}
}
