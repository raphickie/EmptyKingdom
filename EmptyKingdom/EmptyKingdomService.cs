using System;
using System.Threading;
using System.Threading.Tasks;
using Castle.Windsor;

namespace EK.EmptyKingdom
{
	internal class EmptyKingdomService
	{
		private readonly Timer _timer;
		private readonly IWorker _worker;

		public EmptyKingdomService(IWindsorContainer container)
		{
			_timer = new Timer(TimerCallback);
			_worker = container.Resolve<IWorker>();
		}
		public void Start() { _timer.Change(0, (int)TimeSpan.FromHours(1).TotalMilliseconds); }

		public void Stop() { _timer.Change(Timeout.Infinite, Timeout.Infinite); }

		private void TimerCallback(object o)
		{
			Task.Run(async () => await _worker.UpdateWallAsync());
		}
	}
}
