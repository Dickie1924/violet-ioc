using System;
using System.Collections.Generic;
using System.Linq;

namespace VioletIoc
{
    internal class ResolutionTracer
    {
        private readonly List<Func<string>> _messages = new List<Func<string>>();
        private readonly int _indent = 0;
        private readonly string _prefix;

        public ResolutionTracer(ResolutionTracer parent, string prefix)
        {
            _indent = (parent?._indent ?? 0) + 1;
            _prefix = prefix ?? string.Empty;
        }

        public ResolutionTracer Add(string text)
        {
            _messages.Add(() => text.ThrowIfNull(nameof(text)));
            return this;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _messages.Select(m => $"\n{new string(' ', _indent * 2)}{_prefix} : {m()}"));
        }

        public ResolutionTracer CreateChild(string prefix)
        {
            var child = new ResolutionTracer(this, prefix);
            _messages.Add(child.ToString);
            return child;
        }
    }
}
