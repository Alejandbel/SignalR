using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
	public record PlayerMove
	{
		public string Name { get; set; }
		public IEnumerable<int>? Dices { get; set; } = null;
		public bool IsEnded { get; set; } = false;
		public int Score { get; set; } = 0;
		public int DiceCount { get; set; } = 6;
		public bool IsZonked { get; set; } = false;
	}
	

}
