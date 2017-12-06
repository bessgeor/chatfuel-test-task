using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatfuelTestTask
{
	[Route( "" )]
	public class InputController : Controller
    {
		private readonly Lift _lift;

		public InputController( Lift lift ) => _lift = lift;

		[HttpGet( "call-from/{floor}" )]
		public Task CallFrom( int floor )
			=> _lift.CallFromOutside( floor );

		[HttpGet( "direct-to/{floor}" )]
		public Task DirectTo( int floor )
			=> _lift.DirectFromInside( floor );
    }
}
