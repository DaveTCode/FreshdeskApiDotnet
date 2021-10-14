using System;
using System.Collections.Generic;
using System.Linq;

namespace FreshdeskApi.Client.Infrastructure
{
    internal sealed class DisposingCollection : IDisposable
    {
        private readonly ICollection<IDisposable> _disposables = new List<IDisposable>();

        public void Add(IDisposable disposable)
        {
            lock (_disposables)
            {
                _disposables.Add(disposable);
            }
        }

        public void Dispose()
        {
            ICollection<IDisposable> disposables;
            lock (_disposables)
            {
                disposables = _disposables.ToList();
                _disposables.Clear();
            }

            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
