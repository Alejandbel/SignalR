using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
	public record struct PlayerMove(string Name, 
		IEnumerable<int>? Dices = null, 
		bool IsEnded = false, 
		int Score = 0,
		bool IsZonked = false);

}
