using System;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	public class Lift
    {
		internal Lift( Config config )
		{

		}

		public Task CallFromOutside( byte floor )
			=> Task.FromException( new NotImplementedException() );

		public Task DirectFromInside( byte floor )
			=> Task.FromException( new NotImplementedException() );
	}
}
